using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{   
    [System.Serializable]
    public class ResurseStock : IResurseStock
    {           
        public List<ResurseHolder> ResursesInStock => _resurseInStock;
        [SerializeField]
        private List<ResurseHolder> _resurseInStock;
        [SerializeField]
       
        public Action<ResurseHolder> ResursesChange;
        public Action<int, ResurseType> ResValueChange;

        public ResurseStock(List<ResurseHolder> resurses)
        {            
            _resurseInStock = resurses;
            
        }
        public ResurseStock(ResurseStock stock)
        {            
            _resurseInStock = stock._resurseInStock;            
        }

        public void AddResurseHolder(ResurseHolder holder)
        {
            _resurseInStock.Add(holder);
            ResursesChange?.Invoke(holder);
        }

        public int GetResursesCount(ResurseType type)
        {
            foreach (IResurseHolder stock in _resurseInStock)
            {
                if (stock.ResurseInHolder.ResurseType == type)
                    return stock.CurrentResurseCount;
                break;                    
            }
            return 0;
        }

        public void AddResursesCount(ResurseType type,int count)
        {
            foreach (ResurseHolder holder in _resurseInStock)
            {
                if (holder.ResurseInHolder.ResurseType == type)
                {
                    holder.AddResurse(count,out int addedRes);
                    ResursesChange?.Invoke(holder);
                    ResValueChange?.Invoke(addedRes, type);
                }                    
            }            
        } 
        public int GetResursesInStock(ResurseType type, int count)
        {
            var mainedResurse = 0;
            foreach (ResurseHolder holder in _resurseInStock)
            {
                if (holder.ResurseInHolder.ResurseType == type)
                {
                    mainedResurse = holder.MineResurses(count);
                    ResursesChange?.Invoke(holder);
                    ResValueChange?.Invoke(-mainedResurse, type);
                    return mainedResurse;
                }
                break;
            }
            return mainedResurse;
        }
        public IResurseHolder GetResurseInStock(ResurseType type, int count)
        {
            
            foreach(ResurseHolder tempholder in _resurseInStock)
            {
                if (tempholder.ResurseInHolder.ResurseType==type)
                {
                    ResurseHolder MinedResurseHolder = new ResurseHolder(tempholder.ResurseInHolder, tempholder.TryGetResurses(count), tempholder.TryGetResurses(count));
                    return MinedResurseHolder;
                }
            }
            return null;             
        }        
    }


}
