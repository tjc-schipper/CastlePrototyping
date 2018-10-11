using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelpers
{
    public static bool IsMouseOverUI()
    {
        UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
        return (es != null && es.IsPointerOverGameObject());
    }
}
