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
        public void AddResursesCount(ResurseType type,int count);
        public void AddResurseHolder(ResurseHolder holder);       
       




    }
}
