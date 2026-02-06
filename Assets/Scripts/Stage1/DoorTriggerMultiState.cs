using UnityEngine;

public class DoorTriggerMultiState : MonoBehaviour
{
    public Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isPlayerNearby", true); // start opening
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isPlayerNearby", false); // start closing
        }
    }
}
