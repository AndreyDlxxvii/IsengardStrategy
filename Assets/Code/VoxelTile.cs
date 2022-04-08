using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Vector3;

public class VoxelTile : MonoBehaviour
{
    [SerializeField] private Sprite _iconTile;
    
    private float offset = 0.1f;
    private byte [] _tablePassAccess = new byte[4];
    private int sizeTile;

    public int SizeTile => sizeTile;

    public Sprite IconTile => _iconTile;

    public byte[] TablePassAccess => _tablePassAccess;
    
    void Awake()
    {
        PassAccess();
    }

    private void PassAccess()
    {
        var meshCollider = GetComponentInChildren<MeshCollider>();
        var bounds = meshCollider.bounds;
        sizeTile = (int) bounds.size.x;
        if (!CheckRoad(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.min.z - offset), forward))
        {
            _tablePassAccess[0] = 1;
        }

        if (!CheckRoad(new Vector3(bounds.min.x - offset, bounds.center.y + bounds.center.y/2, bounds.center.z), right))
        {
            _tablePassAccess[1] = 1;
        }

        if (!CheckRoad(new Vector3(bounds.center.x, bounds.center.y + bounds.center.y/2, bounds.max.z + offset), back))
        {
            _tablePassAccess[2] = 1;
        }

        if (!CheckRoad(new Vector3(bounds.max.x + offset, bounds.center.y + bounds.center.y/2, bounds.center.z), left))
        {
            _tablePassAccess[3] = 1;
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
