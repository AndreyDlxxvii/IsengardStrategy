using System.Collections.Generic;
using System;
using UnityEngine;

namespace ResurseSystem
{   
    public interface IResurseStock 
    {        
        public List<ResurseHolder> ResursesInStock { get; }        

        public int GetResursesCount(ResurseType type);
        public int GetResursesInStock(ResurseType type, int count);
        public void AddResursesFromHolder(IResurseHolder _getterHolder);
        public void AddResurseHolder(ResurseHolder holder);
        public void AddResursesCount(ResurseType res, int value);






    }
}
