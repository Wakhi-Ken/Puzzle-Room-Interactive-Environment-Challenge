using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int score = 0;

    public void AddPoints(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}
