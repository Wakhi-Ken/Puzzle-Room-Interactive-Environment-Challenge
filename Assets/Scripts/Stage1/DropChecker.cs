using UnityEngine;

public class DropChecker : MonoBehaviour
{
    private bool isHeld = true;        // True while player is holding the item
    private bool hasTriggered = false; // Prevent double triggers

    void Update()
    {
        // Detect when the item is dropped
        Rigidbody rb = GetComponent<Rigidbody>();
        if (isHeld && rb != null && !rb.isKinematic)
        {
            isHeld = false;

            // Safe message for TMP
            if (GameManager.instance != null)
                GameManager.instance.Log("[Item] Item dropped");
            else
                Debug.Log("Item dropped");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isHeld || hasTriggered) return;

        hasTriggered = true;

        // Check all supported items and targets
        if (CheckMatch<NarutoItem, NarutoTarget>(other))
            DestroyBoth<NarutoTarget>(other, "Naruto");
        else if (CheckMatch<LuffyItem, LuffyTarget>(other))
            DestroyBoth<LuffyTarget>(other, "Luffy");
        else if (CheckMatch<YujiItem, YujiTarget>(other))
            DestroyBoth<YujiTarget>(other, "Yuji");
        else if (CheckMatch<IchigoItem, IchigoTarget>(other))
            DestroyBoth<IchigoTarget>(other, "Ichigo");
        else if (CheckMatch<GokuItem, GokuTarget>(other))
            DestroyBoth<GokuTarget>(other, "Goku");
        else
            WrongDrop(); // Wrong drop
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHeld || hasTriggered) return;

        if (collision.gameObject.CompareTag("WrongDropZone"))
        {
            hasTriggered = true;
            WrongDrop();
        }
    }

    // Called whenever the drop is wrong
    private void WrongDrop()
    {
        if (GameManager.instance != null)
            GameManager.instance.Log("❌ Wrong drop! Life lost.");
        else
            Debug.Log("Wrong drop! Life lost.");

        // Reduce player life and update HUD
        PlayerLives playerLives = FindFirstObjectByType<PlayerLives>();
        if (playerLives != null)
            playerLives.LoseLife();
        else
        {
            if (GameManager.instance != null)
                GameManager.instance.Log("⚠️ PlayerLives script not found!");
            else
                Debug.LogError("PlayerLives script not found!");
        }

        Destroy(gameObject); // Destroy dropped item
    }

    // Generic helper to check if dropped item matches the target
    private bool CheckMatch<TItem, TTarget>(Collider other)
        where TItem : Component
        where TTarget : Component
    {
        TItem item = GetComponent<TItem>();
        TTarget target = other.GetComponent<TTarget>();
        return item != null && target != null;
    }

    // Destroy item and target, notify GameManager
    private void DestroyBoth<TTarget>(Collider targetCollider, string heroName)
        where TTarget : Component
    {
        if (GameManager.instance != null)
            GameManager.instance.Log($"✅ Correct drop! {heroName} target destroyed.");
        else
            Debug.Log($"Correct drop! {heroName} target destroyed.");

        Destroy(targetCollider.gameObject);
        Destroy(gameObject);

        if (GameManager.instance != null)
            GameManager.instance.TargetDestroyed<TTarget>();
        else
            Debug.LogError("GameManager not found!");
    }
}
