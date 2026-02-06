using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    public string openTriggerName = "Open";
    private bool doorOpened = false;

    void OnTriggerEnter(Collider other)
    {
        if (doorOpened) return;

        // ONLY react when the key object touches the door
        KeyTarget key = other.GetComponent<KeyTarget>();

        if (key != null)
        {
            doorAnimator.SetTrigger(openTriggerName);
            doorOpened = true;

            // ⭐ send message to UI console
            GameManager.instance.Log("🔑 Key inserted. Door unlocked!");
        }
    }
}
