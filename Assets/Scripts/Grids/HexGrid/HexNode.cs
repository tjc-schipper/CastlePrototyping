using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{

    public Vector2Int gridPosition { get; private set; }

    public void Init(Vector2Int gridPos)
    {
        this.gridPosition = gridPos;
    }
    public void Init(int x, int y)
    {
        Init(new Vector2Int(x, y));
    }


    private void Start()
    {
        ResetColor();
    }

    public void SetSelected()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f);
    }

    public void SetHighlighted()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void ResetColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }
}
