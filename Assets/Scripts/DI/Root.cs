using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] BuildUI buildUI;
    [SerializeField] UIFactory uiFactory;
    [SerializeField] BuildSystem buildSystem;
    [SerializeField] PlayerResources playerResources;


    private static Root instance;
    private static Root Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectsOfType<Root>()[0];
            }
            return instance;
        }
    }


    public static BuildUI BuildUI
    {
        get
        {
            return Instance.buildUI;
        }
    }

    public static UIFactory UIFactory
    {
        get
        {
            return Instance.uiFactory;
        }
    }

    public static BuildSystem BuildSystem
    {
        get
        {
            return Instance.buildSystem;
        }
    }

    public static PlayerResources PlayerResources
    {
        get
        {
            return Instance.playerResources;
        }
    }
}
