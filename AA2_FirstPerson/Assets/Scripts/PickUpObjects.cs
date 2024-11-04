using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupRange = 2f;
    public LayerMask pickupLayer;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float scrollSpeed = 2f;
    private GameObject currentPickup;
    private Rigidbody currentPickupRigidbody;

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * pickupRange, Color.red);

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup") && currentPickup == null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Pickup(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonUp(1) && currentPickup != null)
        {
            Release();
        }
        if (currentPickup != null)
        {
            AdjustPickupDistance();
            PositionPickup();
        }
    }

    void Pickup(GameObject obj)
    {
        currentPickup = obj;
        currentPickupRigidbody = currentPickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.isKinematic = true;
    }

    void Release()
    {
        currentPickupRigidbody.isKinematic = false;
        currentPickupRigidbody.AddForce(Camera.main.transform.forward * 10f, ForceMode.Impulse);
        currentPickup = null;
    }

    void PositionPickup()
    {
        float currentDistance = Vector3.Distance(currentPickup.transform.position, Camera.main.transform.position);
        if (currentDistance < minDistance)
        {
            currentPickup.transform.position = Camera.main.transform.position + Camera.main.transform.forward * minDistance;
        }
        else if (currentDistance > maxDistance)
        {
            currentPickup.transform.position = Camera.main.transform.position + Camera.main.transform.forward * maxDistance;
        }
        else
        {
            currentPickup.transform.position = Camera.main.transform.position + Camera.main.transform.forward * currentDistance;
        }
    }

    void AdjustPickupDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float currentDistance = Vector3.Distance(currentPickup.transform.position, Camera.main.transform.position);
            float newDistance = currentDistance - scroll * scrollSpeed;

            if (newDistance < minDistance)
            {
                newDistance = minDistance;
            }
            else if (newDistance > maxDistance)
            {
                newDistance = maxDistance;
            }

            currentPickup.transform.position = Camera.main.transform.position + Camera.main.transform.forward * newDistance;
        }
    }
}
