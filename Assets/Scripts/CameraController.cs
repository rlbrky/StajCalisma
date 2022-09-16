using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float camSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] ReadInput readInput;
    float xAxis,  zAxis;
    private void Update()
    {
        if (!readInput.IsCanvasActive)
        {
            xAxis = Input.GetAxis("Horizontal") * camSpeed;
            zAxis = Input.GetAxis("Vertical") * camSpeed;
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += camSpeed * Vector3.up* Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position += camSpeed * Vector3.down * Time.deltaTime;
            }
        }

        if (Input.GetMouseButton(2))
        {
            transform.eulerAngles += rotationSpeed * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
    }
    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(xAxis, 0, zAxis);
    }
}
