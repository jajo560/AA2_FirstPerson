using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject torreta;
    public GameObject troncoTorreta;
    private float rotation_y = 0f;
    private float rotation_x = 0f;
    private float rotation_x2 = 0f;
    private float rotation_y2 = 0f;
    public float sensibility_X;
    public float sensibility_Y;
    public float maxRotationY = 45.0f;
    public float minRotationY = -45.0f;
    public float maxRotationX = 45.0f;
    public float minRotationX = -45.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation_y = Input.GetAxis("Mouse Y") * sensibility_Y;
        rotation_x = Input.GetAxis("Mouse X") * sensibility_X;

        rotation_x2 += rotation_x;

        rotation_y2 -= rotation_y;

        if (rotation_y2 > maxRotationY)
        {
            rotation_y2 = maxRotationY;
        }
        else if (rotation_y2 < minRotationY)
        {
            rotation_y2 = minRotationY;
        }

        if (rotation_x2 > maxRotationX)
        {
            rotation_x2 = maxRotationX;
        }
        else if (rotation_x2 < minRotationX)
        {
            rotation_x2 = minRotationX;
        }

        torreta.transform.localRotation = Quaternion.Euler(0, rotation_x2, 0);
        troncoTorreta.transform.localRotation = Quaternion.Euler(rotation_y2, 0, 0);
        


    }
}
