using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 movement;

    public Transform playerInputSpace = default;

    public float speed = 100;


    // Update is called once per frame
    void Update()
    {
        
        movement.z = Input.GetAxis("Vertical") * Time.deltaTime *speed;
        movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        movement.y = Input.GetAxis("Up") * Time.deltaTime * speed;

        Vector3 forward = playerInputSpace.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = playerInputSpace.right;
        right.y = 0f;
        right.Normalize();
        Vector3 up = playerInputSpace.up;
        up.x = 0f;
        up.z = 0f;
        up.Normalize();
        movement = (forward * movement.z + right * movement.x + up * movement.y);

        transform.Translate(movement);

    }
}
