using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.redblobgames.com/grids/hexagons/
/// The walking direction (y) should be along the column: neatly aligned hexes!
/// Use either axial coords or cube coords
/// </summary>
public class HexGrid : MonoBehaviour
{

    [SerializeField] HexNode prefab_Node;

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    [SerializeField] private float hexRadius = 0.5f;

    /// <summary>
    /// The height of an equilateral triangle with side 0.5, six of which are contained in a hexagon
    /// </summary>
    private const float TRIHEIGHT = 0.433012702f;

    private HexNode[,] nodes;
    private bool spawned = false;

    private void Awake()
    {
        this.nodes = new HexNode[this.width, this.height];
    }

    private void Update()
    {
        // Spawn a grid of prefabs!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!this.spawned)
            {
                this.spawned = true;
                SpawnNodes();
            }
        }

        // Select [3,3] and highlight everything in 1 range!
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            HexNode selectedNode = GetNode(3, 3);
            selectedNode.SetSelected();

            List<HexNode> adjacentNodes = GetWithinRange(3, 3, 2);
            foreach (HexNode n in adjacentNodes)
            {
                n.SetHighlighted();
            }
        }
    }

    /// <summary>
    /// Returns a copy of the nodes array
    /// </summary>
    /// <returns></returns>
    public HexNode[,] GetNodes()
    {
        HexNode[,] ns = new HexNode[this.width, this.height];
        System.Array.Copy(this.nodes, ns, this.nodes.Length);
        return ns;
    }

    /// <summary>
    /// Does the specified coordinate fall within the grid system?
    /// </summary>
    /// <param name="xCoord"></param>
    /// <param name="yCoord"></param>
    /// <returns></returns>
    public bool IsValidNode(int xCoord, int yCoord)
    {
        return
            (xCoord < this.width && xCoord >= 0) &&
            (yCoord < this.height && yCoord >= 0);
    }
    /// <summary>
    /// Does the specified coordinate fall within the grid system?
    /// </summary>
    /// <param name="coords"></param>
    /// <returns></returns>
    public bool IsValidNode(Vector2Int coords)
    {
        return IsValidNode(coords.x, coords.y);
    }

    /// <summary>
    /// Get the HexNode at the specified coordinate. Returns null if invalid or empty
    /// </summary>
    /// <param name="xCoord"></param>
    /// <param name="yCoord"></param>
    /// <returns></returns>
    public HexNode GetNode(int xCoord, int yCoord)
    {
        if (IsValidNode(xCoord, yCoord))
            return this.nodes[xCoord, yCoord];
        else
            return null;
    }
    /// <summary>
    /// Get the HexNode at the specified coordinate. Returns null if invalid or empty
    /// </summary>
    /// <param name="coords"></param>
    /// <returns></returns>
    public HexNode GetNode(Vector2Int coords)
    {
        return GetNode(coords.x, coords.y);
    }

    /// <summary>
    /// Returns a List of all HexNodes that are within 'range' of the specified coordinate (except itself)
    /// </summary>
    /// <param name="xCoord"></param>
    /// <param name="yCoord"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<HexNode> GetWithinRange(int xCoord, int yCoord, int range)
    {
        List<HexNode> inRange = new List<HexNode>();
        HexNode baseNode = GetNode(xCoord, yCoord);
        if (baseNode == null)
        {
            return null;
        }

        HexNode currentNode = null;
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                if (Mathf.Abs(x + y) > range) continue; // "cut out the corners"

                if (x == 0 && y == 0) continue; // skip basenode itself
                currentNode = GetNode(xCoord + x, yCoord + y);
                if (currentNode != null)
                    inRange.Add(currentNode);
            }
        }
        return inRange;
    }
    /// <summary>
    /// Returns a list of all HexNodes that are within 'range' of the specified coordinate (except itself)
    /// </summary>
    /// <param name="coords"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<HexNode> GetWithinRange(Vector2Int coords, int range)
    {
        return GetWithinRange(coords.x, coords.y, range);
    }

    /// <summary>
    /// Returns whether the target coordinate is within 'range' of the base coordinate. Returns false if either coordinate is invalid.
    /// </summary>
    /// <param name="xCoord"></param>
    /// <param name="yCoord"></param>
    /// <param name="targetX"></param>
    /// <param name="targetY"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IsInRange(int xCoord, int yCoord, int targetX, int targetY, int range)
    {
        return
            (Mathf.Abs(targetX - xCoord) <= range) &&
            (Mathf.Abs(targetX - yCoord) <= range) &&
            (Mathf.Abs((targetX - xCoord) + (targetY - yCoord)) <= range);
    }
    /// <summary>
    /// Returns whether the target coordinate is within 'range' of the base coordinate. Returns false if either coordinate is invalid.
    /// </summary>
    /// <param name="coords"></param>
    /// <param name="target"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IsInRange(Vector2Int coords, Vector2Int target, int range)
    {
        return IsInRange(coords.x, coords.y, target.x, target.y, range);
    }

    /// <summary>
    /// Returns whether nodeOne and nodeTwo are adjacent to eachother (that is: distance 1)
    /// </summary>
    /// <param name="nodeOne"></param>
    /// <param name="nodeTwo"></param>
    /// <returns></returns>
    public bool IsAdjacent(HexNode nodeOne, HexNode nodeTwo)
    {
        return IsAdjacent(nodeOne.gridPosition, nodeTwo.gridPosition);
    }
    /// <summary>
    /// Returns whether the nodes at coordsOne and coordsTwo are adjacent (that is: distance 1)
    /// </summary>
    /// <param name="coordsOne"></param>
    /// <param name="coordsTwo"></param>
    /// <returns></returns>
    public bool IsAdjacent(Vector2Int coordsOne, Vector2Int coordsTwo)
    {
        return IsInRange(coordsOne, coordsTwo, 1);
    }

    private void SpawnNodes()
    {
        HexNode currentNode = null;
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                currentNode = Instantiate<HexNode>(prefab_Node, GridToWorldPos(x, y), Quaternion.identity);
                currentNode.Init(x, y);
                this.nodes[x, y] = currentNode;
            }
        }
    }

    private Vector3 GridToWorldPos(int xCoord, int yCoord)
    {
        if (IsValidNode(xCoord, yCoord))
        {
            Vector3 offset = new Vector3(
                (2f * xCoord + yCoord) * TRIHEIGHT * this.hexRadius,
                0f,
                yCoord * 0.75f * this.hexRadius);

            return this.transform.position + offset;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private Vector3 GridToWorldPos(Vector2Int coords)
    {
        return GridToWorldPos(coords.x, coords.y);
    }

}
