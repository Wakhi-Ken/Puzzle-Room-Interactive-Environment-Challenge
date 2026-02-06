using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public float openDistance = 1f;

    bool hasOpened = false;   // remembers if door already opened

    void Update()
    {
        if (hasOpened) return;   // stop checking forever once opened

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= openDistance)
        {
            anim.SetBool("slide", true);
            hasOpened = true;    // lock door in opened state
        }
    }
}
