using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 orbit;
    Quaternion look;
    public Transform player;
    public float rotationSpeed = 100;

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(orbit);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        orbit += rotationSpeed * Time.unscaledDeltaTime * input;
        orbit.x = Mathf.Clamp(orbit.x, -90, 90);
        look = Quaternion.Euler(orbit);

        transform.SetPositionAndRotation(player.transform.position, look);
    }
}
