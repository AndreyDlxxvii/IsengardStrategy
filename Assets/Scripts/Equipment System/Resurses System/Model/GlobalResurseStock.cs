using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Global Resurse Stock", menuName = "Resurse System/Global Resurse Stock", order = 1)]
    public class GlobalResurseStock :ScriptableObject
    {
        private List<ResurseStock> AllResursesStockPlayer;
        [SerializeField]
        private ResurseStock GlobalResursesStock;
        public Action GlobalResChange;

        
        public GlobalResurseStock( ResurseStock resurseStock)
        {
            GlobalResursesStock = Instantiate<ResurseStock> (resurseStock);
            AllResursesStockPlayer = new List<ResurseStock>();
        }
        public GlobalResurseStock(GlobalResurseStock globalResurseStock)
        {
            GlobalResursesStock = Instantiate<ResurseStock>(globalResurseStock.GlobalResursesStock);
            AllResursesStockPlayer = new List<ResurseStock>();
        }

        public void AddResurseStock(ResurseStock stock)
        {
            AllResursesStockPlayer.Add(stock);

            stock.ResValueChange += ChangeGlobalResurse;            
            foreach (ResurseHolder holder in stock.ResursesInStock)
            {
                GlobalResursesStock.AddResursesCount(holder.ResurseInHolder.ResurseType, holder.CurrentResurseCount);
            }
            GlobalResChange?.Invoke();
        }
        public void ChangeGlobalResurse(int value, ResurseType type)
        {
            GlobalResursesStock.AddResursesCount(type, value);
            GlobalResChange?.Invoke();
        }
        public void DeleteStock(ResurseStock stock)
        {
            stock.ResValueChange -= ChangeGlobalResurse;
            foreach (ResurseHolder holder in stock.ResursesInStock)
            {
                foreach (ResurseHolder globalHolder in GlobalResursesStock.ResursesInStock)
                {
                    if (globalHolder.ResurseInHolder.ResurseType == holder.ResurseInHolder.ResurseType)
                    {
                        globalHolder.MineResurses(holder.CurrentResurseCount);
                    }
                }
                GlobalResChange?.Invoke();
            }            
            AllResursesStockPlayer.Remove(stock);
        }
        public ResurseStock GetGlobalResurseStock()
        {
            return GlobalResursesStock;
        }
        public void ResetGlobalRes()
        {
            foreach (ResurseHolder holder in GlobalResursesStock.ResursesInStock)
            {
                holder.SetCurrentResValue(0);
            }
        }
    }
}
