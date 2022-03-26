using UnityEngine;
using static UnityEngine.Vector3;

public class VoxelTile : MonoBehaviour
{
    private float offset = 0.1f;
    private bool [] table = new bool[4];
    private int sizeTile;

    public int SizeTile => sizeTile;

    public bool[] Table => table;

    void Awake()
    {
        GetVoxileColor();
    }

    private void GetVoxileColor()
    {
        var meshCollider = GetComponentInChildren<MeshCollider>();
        var bounds = meshCollider.bounds;
        sizeTile = (int) bounds.size.x;
        if (!CheckRoad(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.min.z - offset), forward))
        {
            Debug.DrawRay(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.min.z - offset),
                forward * .1f, Color.red, 30);
            table[0] = true;
        }

        if (!CheckRoad(new Vector3(bounds.min.x - offset, bounds.center.y + bounds.center.y/2, bounds.center.z), right))
        {
            Debug.DrawRay(new Vector3(bounds.min.x - offset, bounds.center.y + bounds.center.y/2, bounds.center.z),
                right*.1f, Color.yellow, 30);
            table[1] = true;
        }

        if (!CheckRoad(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.max.z + offset), back))
        {
            Debug.DrawRay(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.max.z + offset),
                back*.1f, Color.blue, 30);
            table[2] = true;
        }

        if (!CheckRoad(new Vector3(bounds.max.x + offset, bounds.center.y + bounds.center.y/2, bounds.center.z), left))
        {
            Debug.DrawRay(new Vector3(bounds.max.x + offset, bounds.center.y + bounds.center.y/2, bounds.center.z),
                left*.1f, Color.white, 30);
            table[3] = true;
        }
    }

    private bool CheckRoad(Vector3 dir, Vector3 direction)
    {
        if (Physics.Raycast(new Ray(dir, direction),0.2f))
        {
            return true;
        }
        return false;
    }
}
