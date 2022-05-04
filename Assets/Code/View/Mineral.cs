using UnityEngine;
using System;
using ResurseSystem;

public class Mineral : BaseBuildAndResources
{
    [SerializeField]
    private ResurseMine BaseResurseMine;
    [SerializeField]
    private Sprite mineralIcon;
    private ResurseMine thisResurseMine;
    private void Awake()
    {
        thisResurseMine = new ResurseMine(BaseResurseMine);
        thisResurseMine.SetIconMine(mineralIcon);
    }
    public ResurseMine GetMineRes()
    {
        return thisResurseMine;
    }
}