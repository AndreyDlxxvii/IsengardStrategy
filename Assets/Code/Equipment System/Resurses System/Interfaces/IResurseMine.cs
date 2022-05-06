using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 
    public interface IResurseMine : IIconHolder    
    {
        public string NameOfMine { get; }
        public float ExtractionTime { get; }
        public ResurseHolder ResurseHolderMine { get; }
        public void SetExtractionTime(float time);
        public int MinedResurse(int value);
        public IResurseHolder MinedResurseHolder(int value);



    }
}
