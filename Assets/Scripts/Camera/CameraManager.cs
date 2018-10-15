using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Camera mainCam;

    public Camera MainCam
    {
        get
        {
            return this.mainCam;
        }
        set
        {
            Debug.Log("Main camera has been swapped from '" + this.mainCam + "' to '" + value);
            this.mainCam = value;
        }
    }

    private void Awake()
    {
        this.mainCam = Camera.main;
    }
}
