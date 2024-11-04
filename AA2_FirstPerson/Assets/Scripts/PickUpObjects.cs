using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupRange = 2f;            // Distancia m�xima para recoger el objeto
    public LayerMask pickupLayer;              // Capa de objetos que se pueden recoger
    public float minDistance = 1f;             // Distancia m�nima del objeto recogido
    public float maxDistance = 5f;             // Distancia m�xima del objeto recogido
    public float scrollSpeed = 2f;             // Velocidad de desplazamiento al usar la rueda del rat�n

    private GameObject currentPickup;          // Referencia al objeto actualmente recogido
    private Rigidbody currentPickupRigidbody;  // Referencia al Rigidbody del objeto recogido

    void Update()
    {
        // Raycast desde la c�mara hacia adelante para detectar objetos
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * pickupRange, Color.red);

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            // Comprobar si el objeto puede ser recogido
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup") && currentPickup == null)
            {
                if (Input.GetMouseButtonDown(1)) // Clic derecho
                {
                    Pickup(hit.collider.gameObject);
                }
            }
        }

        // Comprobar si se suelta el clic derecho para soltar el objeto
        if (Input.GetMouseButtonUp(1) && currentPickup != null)
        {
            Release();
        }

        // Si se tiene un objeto recogido, manejar la distancia y la posici�n
        if (currentPickup != null)
        {
            AdjustPickupDistance();
            PositionPickup();
        }
    }

    void Pickup(GameObject obj)
    {
        currentPickup = obj; // Guarda el objeto recogido
        currentPickupRigidbody = currentPickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.isKinematic = true; // Desactiva la gravedad
    }

    void Release()
    {
        // Reactiva el Rigidbody y a�ade una fuerza al objeto lanzado
        currentPickupRigidbody.isKinematic = false;
        currentPickupRigidbody.AddForce(Camera.main.transform.forward * 10f, ForceMode.Impulse); // Lanza el objeto
        currentPickup = null; // Libera el objeto
    }

    void PositionPickup()
    {
        // Cambia la posici�n del objeto recogido para que est� frente a la c�mara
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
        // Ajustar la distancia del objeto recogido con la rueda del rat�n
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float currentDistance = Vector3.Distance(currentPickup.transform.position, Camera.main.transform.position);
            float newDistance = currentDistance - scroll * scrollSpeed;

            // Verifica los l�mites de distancia manualmente
            if (newDistance < minDistance)
            {
                newDistance = minDistance; // Establece en la distancia m�nima si es menor
            }
            else if (newDistance > maxDistance)
            {
                newDistance = maxDistance; // Establece en la distancia m�xima si es mayor
            }

            currentPickup.transform.position = Camera.main.transform.position + Camera.main.transform.forward * newDistance;
        }
    }
}
