using EquipSystem;
using UnityEngine;

namespace ResurseSystem
{     
    public enum ResurseType
    {
        Wood,
        Iron,
        Deer,
        Horse,
        MagikStones,
        Textile,
        Steele,
    }   
    public interface IResurseHolder
    {        
        public int MaxResurseCount { get; }
        public int CurrentResurseCount { get; }
        public ResurseCraft ResurseInHolder { get; }       
        public void AddResurse(int ResurseCount,out int addedRes);        
        public int MineResurses(int countMineRes);
        public int TryGetResurses(int count);        
        public int TryAddResurse(IResurseHolder holder);        
        public void SetMaxResurseCount(int maxCount);
    
    }
}
