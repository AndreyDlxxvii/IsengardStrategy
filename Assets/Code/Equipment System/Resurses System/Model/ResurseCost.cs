using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    [System.Serializable]
    public class ResurseCost : IResurseCost
    {
        public List<ResurseHolder> CoastInResurse => _coastInResurse;

        public bool PricePaidFlag => _pricePaid;

        [SerializeField]
        private List<ResurseHolder> _coastInResurse;
        [SerializeField]
        private bool _pricePaid;        
        public Action BuildingPaidFor;

        public ResurseCost(List<ResurseHolder> resurseHolders)
        {
            _coastInResurse = resurseHolders;
            _pricePaid = false;
        }

        public void CheckRequiredResurses()
        {
            foreach (ResurseHolder holder in _coastInResurse)
            {
                if (holder.CurrentResurseCount==holder.MaxResurseCount)
                {
                    _coastInResurse.Remove(holder);
                }
            }
            if (_coastInResurse==null)
            {
                _pricePaid = true;
                BuildingPaidFor?.Invoke();
            }
        }
        
        public IResurseHolder TryAddResurse(ResurseHolder holder)
        {
            
            foreach (IResurseHolder costholder in _coastInResurse)
            {
                if (holder.ResurseInHolder==costholder.ResurseInHolder)
                {
                    var notAddedres = costholder.TryAddResurse(holder);
                    CheckRequiredResurses();
                    if (notAddedres!=0)
                    {
                        ResurseHolder notAddedResholder = new ResurseHolder(holder.ResurseInHolder, notAddedres, notAddedres);
                        return notAddedResholder;
                    }
                }
            }
            return null;
        }
    }
}
