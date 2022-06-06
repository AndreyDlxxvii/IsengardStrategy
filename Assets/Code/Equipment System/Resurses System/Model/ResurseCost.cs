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
        [SerializeField]
        private bool _itIsResetable;

        public ResurseCost(List<ResurseHolder> resurseHolders)
        {
            _coastInResurse = new List<ResurseHolder>(resurseHolders);
            _pricePaid = false;
        }
        public ResurseCost(ResurseCost cost)
        {
            _coastInResurse = new List<ResurseHolder>(cost.CoastInResurse);
            _pricePaid = cost.PricePaidFlag;
        }

        public void CheckRequiredResurses()
        {
            foreach (ResurseHolder holder in _coastInResurse)
            {
                if (holder.CurrentResurseCount!=holder.MaxResurseCount)
                {
                    _pricePaid = false;
                    Debug.Log($"Need {holder.MaxResurseCount-holder.CurrentResurseCount} of {holder.ResurseInHolder.NameOFResurse} for produce or building");
                }
                
            }
            if (_pricePaid)
            {
                if (!_itIsResetable)
                {
                    _coastInResurse = null;
                }
                else
                {
                    ResetPaid();
                }
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
        public void GetNeededResurse(ResurseStock stock)
        {            
            foreach (ResurseHolder costHolder in _coastInResurse)
            {
                var tempRes= stock.GetResursesInStock(costHolder.ResurseInHolder.ResurseType, costHolder.MaxResurseCount - costHolder.CurrentResurseCount);
                costHolder.AddResurse(tempRes, out tempRes);
            }            
            CheckRequiredResurses();
        }
        public void ResetPaid()
        {
            _pricePaid = false;
            foreach (ResurseHolder holder in _coastInResurse)
            {
                holder.SetCurrentResValue(0);
            }
        }
    }
}
