using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOpenCloseable {

    void Open(bool instant = false);
    void Close(bool instant = false, System.Action callback = null);
    bool IsOpen();

}
