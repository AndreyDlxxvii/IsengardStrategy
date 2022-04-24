using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    
    [CreateAssetMenu(fileName = "Resurse Stock", menuName = "Resurse System/Resurse Stock", order = 1)]
    [System.Serializable]
    public class ResurseStock : ScriptableObject, IResurseStock
    {
        public string NameOfStock =>_nameOfStock;        
        public List<ResurseHolder> ResursesInStock => _resurseInStock;

        public Sprite Icon => _stockIcon;

        [SerializeField]
        private string _nameOfStock;
        [SerializeField]
        private List<ResurseHolder> _resurseInStock;
        [SerializeField]
        private Sprite _stockIcon;
        public Action<ResurseHolder> ResursesChange;
        public Action<int, ResurseType> ResValueChange;

        public ResurseStock(string nameOfStock,List<ResurseHolder> resurses,Sprite icon)
        {
            _nameOfStock = nameOfStock;
            _resurseInStock = resurses;
            _stockIcon = icon;
        }
        public ResurseStock(ResurseStock stock)
        {
            _nameOfStock = stock.NameOfStock;
            _resurseInStock = stock._resurseInStock;
            _stockIcon = stock._stockIcon;
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
        public void SetIconStock(Sprite icon)
        {
            _stockIcon = icon;
        }
        public void SetStockName (string name)
        {
            _nameOfStock = name;
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
        
    }


}
