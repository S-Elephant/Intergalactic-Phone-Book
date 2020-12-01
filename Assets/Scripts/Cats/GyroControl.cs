#if !UNITY_EDITOR
using System;
#endif
using UnityEngine;

/// <summary>
/// Todo: There are so many #if's that an interface or subclass should be used instead and then the game spawns the appropriate one. The other one should use the mouse instead.
/// </summary>
[RequireComponent(typeof(Camera))]
public class GyroControl : MonoBehaviour
{
#if !UNITY_EDITOR
    private bool _IsGyroSupported;
    private bool IsGyroSupported { get { return _IsGyroSupported; } }
    private Gyroscope Gyro = null;

    private Quaternion Rotation;
#endif

    private GameObject CameraContainer;

    private void Start()
    {
        CreateCameraContainer();
        transform.SetParent(CameraContainer.transform);

#if !UNITY_EDITOR
        _IsGyroSupported = SystemInfo.supportsGyroscope;
        if (IsGyroSupported)
        {
            CreateGyro();
        }
        else
        {
            throw new Exception("Gyro is not supported. This code should never have been executed!");
        }
#endif
    }

    private void CreateCameraContainer()
    {
        CameraContainer = new GameObject("CameraContainer");
        CameraContainer.transform.position = transform.position;
    }

#if !UNITY_EDITOR
    private void CreateGyro()
    {
        Gyro = Input.gyro;
        Gyro.enabled = true;

        // https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        // So basically this rotated 90 degrees around the x and y axis.
        CameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        
        Rotation = new Quaternion(0f, 0f, 1f, 0f); // Look forward.
    }

    private void Update()
    {
        if (!IsGyroSupported) { return; }

        transform.localRotation = Gyro.attitude * Rotation;
    }
#endif
}
