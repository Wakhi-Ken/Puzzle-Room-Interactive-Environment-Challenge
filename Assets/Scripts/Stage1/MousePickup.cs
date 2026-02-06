using UnityEngine;

public class MousePickup : MonoBehaviour
{
    public float pickupDistance = 3f;
    public Transform holdPoint;

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void TryPickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                heldRb.useGravity = false;
                heldRb.isKinematic = true;
            }
        }
    }

    void DropObject()
    {
        heldRb.useGravity = true;
        heldRb.isKinematic = false;

        heldObject = null;
        heldRb = null;
    }
}
