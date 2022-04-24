using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{   
    public interface IResurseStock : IIconHolder
    {
        public string NameOfStock { get; }
        public List<ResurseHolder> ResursesInStock { get; }

        public int GetResursesCount(ResurseType type);
        public int GetResursesInStock(ResurseType type, int count);
        public void AddResursesCount(ResurseType type,int count);
        

        
    }
}
