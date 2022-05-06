using System.Collections;
using System.Collections.Generic;
using ResurseSystem;
using UnityEngine;

public class WorkerView : MonoBehaviour
{
    [SerializeField]
    private ResurseHolder holder;

    public ResurseHolder GetResurseOutOfHolder()
    {
        ResurseHolder _holder;
        _holder = holder;
        holder = null;
        return _holder;
    }

    public void AddResource(ResurseHolder addingHolder)
    {
        if (holder!=null&&holder.ResurseInHolder == addingHolder.ResurseInHolder)
        {
            holder.TryAddResurse(addingHolder);
        }
        else
        {
            holder = addingHolder;
        }
    }
}
