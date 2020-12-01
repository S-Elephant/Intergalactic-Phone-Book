using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ControlCameraRotation : MonoBehaviour
{
    [SerializeField] private CursorLockMode MouseLockMode = CursorLockMode.Locked;
    [SerializeField] private float Sensitivity = 10f;
    [SerializeField] private float MaxAngle_Y = 85f;

    private Vector2 currentRotation;
    private Camera Cam;

#if UNITY_EDITOR
    private void Start()
    {
        Cam = GetComponent<Camera>();
        Cursor.lockState = MouseLockMode;
    }

    void Update()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * Sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * Sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -MaxAngle_Y, MaxAngle_Y);
        Cam.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
#endif
}
