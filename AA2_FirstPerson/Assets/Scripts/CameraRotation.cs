using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject troncoTorreta;
    private float rotation_y = 0;
    private float rotation_x = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation_y = Input.GetAxis("Mouse Y");
        rotation_x = Input.GetAxis("Mouse X");

        troncoTorreta.transform.localEulerAngles = transform.localEulerAngles + new Vector3(-rotation_y, 0, 0);
        troncoTorreta.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, rotation_x, 0);



    }
}
