using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable), typeof(HexNode))]
public class TestClickableGridRange : MonoBehaviour
{

    [SerializeField] int selectRange = 1;

    HexGrid grid;
    HexNode node;

    void Start()
    {
        node = GetComponent<HexNode>();
        grid = GameObject.FindObjectsOfType<HexGrid>()[0];

        Clickable c = GetComponent<Clickable>();
        c.Click += (Clickable sender) =>
        {
            HexNode[,] nodes = this.grid.GetNodes();
            for (int x = 0; x < nodes.GetLength(0); x++)
            {
                for (int y = 0; y < nodes.GetLength(1); y++)
                {
                    nodes[x, y].ResetColor();
                }
            }

            this.node.SetSelected();
            List<HexNode> nodesInRange = this.grid.GetWithinRange(this.node.gridPosition.x, this.node.gridPosition.y, this.selectRange);
            foreach (HexNode node in nodesInRange)
            {
                node.SetHighlighted();
            }
        };
    }


}
