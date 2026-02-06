using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int narutoCount;
    private int luffyCount;
    private int yujiCount;
    private int ichigoCount;
    private int gokuCount;

    [Header("UI DEBUG MESSAGE")]
    public TextMeshProUGUI debugText;
    public float messageDuration = 2f; // Duration for temporary messages
    private Coroutine currentMessageCoroutine;

    [Header("UI LIVES DISPLAY")]
    public TextMeshProUGUI livesText; // New: persistent lives UI

    [Header("Heroes To Reveal On Game Complete")]
    public GameObject[] heroesToReveal;

    private bool gameCompleted = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        CountTargetsAtStart();
        HideHeroesAtStart();
    }

    void CountTargetsAtStart()
    {
        narutoCount = FindObjectsByType<NarutoTarget>(FindObjectsSortMode.None).Length;
        luffyCount = FindObjectsByType<LuffyTarget>(FindObjectsSortMode.None).Length;
        yujiCount = FindObjectsByType<YujiTarget>(FindObjectsSortMode.None).Length;
        ichigoCount = FindObjectsByType<IchigoTarget>(FindObjectsSortMode.None).Length;
        gokuCount = FindObjectsByType<GokuTarget>(FindObjectsSortMode.None).Length;
    }

    void HideHeroesAtStart()
    {
        foreach (GameObject hero in heroesToReveal)
            hero.SetActive(false);
    }

    // 🌍 GLOBAL LOG FUNCTION — shows temporary event messages
    public void Log(string message)
    {
        Debug.Log(message);

        if (currentMessageCoroutine != null)
            StopCoroutine(currentMessageCoroutine);

        currentMessageCoroutine = StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        if (debugText != null)
            debugText.text = FormatMessage(message);

        yield return new WaitForSeconds(messageDuration);

        if (debugText != null)
            debugText.text = ""; // Clear after duration

        currentMessageCoroutine = null;
    }

    // Adds color coding for events
    private string FormatMessage(string message)
    {
        if (message.Contains("✅")) return $"<color=#00FF00>{message}</color>";
        if (message.Contains("❌") || message.Contains("💀")) return $"<color=#FF4444>{message}</color>";
        if (message.Contains("🔑")) return $"<color=#FFFF00>{message}</color>";
        if (message.Contains("📦")) return $"<color=#00BFFF>{message}</color>";
        if (message.Contains("❤️")) return $"<color=#FF69B4>{message}</color>";
        if (message.Contains("🏆")) return $"<color=#FFD700>{message}</color>";
        return $"<color=#FFFFFF>{message}</color>";
    }

    public void TargetDestroyed<T>() where T : Component
    {
        if (gameCompleted) return;

        if (typeof(T) == typeof(NarutoTarget)) narutoCount--;
        if (typeof(T) == typeof(LuffyTarget)) luffyCount--;
        if (typeof(T) == typeof(YujiTarget)) yujiCount--;
        if (typeof(T) == typeof(IchigoTarget)) ichigoCount--;
        if (typeof(T) == typeof(GokuTarget)) gokuCount--;

        Log($"✅ Target Remaining Villan N:{narutoCount} L:{luffyCount} Y:{yujiCount} I:{ichigoCount} G:{gokuCount}");

        CheckGameCompletion();
    }

    void CheckGameCompletion()
    {
        if (narutoCount <= 0 &&
            luffyCount <= 0 &&
            yujiCount <= 0 &&
            ichigoCount <= 0 &&
            gokuCount <= 0)
        {
            GameCompleted();
        }
    }

    void GameCompleted()
    {
        gameCompleted = true;
        Log("🏆 ALL TARGETS CLEARED — HEROES APPEAR!");

        foreach (GameObject hero in heroesToReveal)
            hero.SetActive(true);
    }

    // 🌟 NEW FUNCTION — update lives UI
    public void UpdateLivesDisplay(int lives)
    {
        if (livesText != null)
            livesText.text = $"❤️ Lives: {lives}";
    }
}
