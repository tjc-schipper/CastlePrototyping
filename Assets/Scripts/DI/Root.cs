using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] BuildUI buildUI;
    [SerializeField] UIFactory uiFactory;
    [SerializeField] BuildSystem buildSystem;


    private static Root _instance;
    private static Root instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectsOfType<Root>()[0];
            }
            return _instance;
        }
    }


    public static BuildUI BuildUI
    {
        get
        {
            return instance.buildUI;
        }
    }

    public static UIFactory UIFactory
    {
        get
        {
            return instance.uiFactory;
        }
    }

    public static BuildSystem BuildSystem
    {
        get
        {
            return instance.buildSystem;
        }
    }
}
