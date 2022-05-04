using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 

    public interface IResurseCost 
    {
        public List<ResurseHolder> CoastInResurse { get; }
        public bool PricePaidFlag { get; }

        public void CheckRequiredResurses();
        public IResurseHolder TryAddResurse(ResurseHolder holder);

    }
}
