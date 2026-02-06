using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public int value = 1; // Points or currency this item gives

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that touched it is the player
        if (other.CompareTag("Player"))
        {
            // Add points to player (you can change this to any effect)
            PlayerInventory player = other.GetComponent<PlayerInventory>();
            if (player != null)
            {
                player.AddPoints(value);
            }

            // Destroy the item after collecting
            Destroy(gameObject);
        }
    }
}
