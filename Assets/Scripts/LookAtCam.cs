using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{

    /// <todo>
    /// Has to be replaced with Constructor/Factory approach if spawned dynamically.
    /// </todo>
    [Zenject.Inject]
    CameraManager cameraManager;

    public void FixedUpdate()
    {
        Camera cam = this.cameraManager.MainCam;
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
