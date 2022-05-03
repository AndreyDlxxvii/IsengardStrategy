using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    [System.Serializable]
    public class ResurseHolder : IResurseHolder
    {
        public int MaxResurseCount => _maxResurseCount;  
        public ResurseCraft ResurseInHolder => _resurseInHolder;

        public int CurrentResurseCount => _currentResurseCount;

        [SerializeField]
        private int _maxResurseCount;
        [SerializeField]
        private int _currentResurseCount;
        [SerializeField]
        private ResurseCraft _resurseInHolder;              

        public void AddResurse(int ResurseCount, out int addedRes)
        {
            addedRes = ResurseCount;
            _currentResurseCount+= ResurseCount;
            if (_currentResurseCount >= MaxResurseCount)
            {
                addedRes = ResurseCount - _currentResurseCount + MaxResurseCount;
                _currentResurseCount=MaxResurseCount;
                Debug.Log($"The holder of {_resurseInHolder.NameOFResurse} is full ");
            }
        }       
        public ResurseHolder(ResurseCraft resurse, int thisMaxResurseCount,int currentResurseCount)
        {
            _resurseInHolder = resurse;
            _maxResurseCount = thisMaxResurseCount;
            _currentResurseCount = currentResurseCount;

        }
        public int MineResurses(int countMineRes)
        {
            int minedRes = countMineRes;
            _currentResurseCount-=countMineRes;
            if (_currentResurseCount<=0)
            {
                minedRes += _currentResurseCount;
                Debug.Log($"This holder of {_resurseInHolder.NameOFResurse} is empty");
            }
            return minedRes;
        }       
        public void SetMaxResurseCount(int maxCount)
        {
            _maxResurseCount=maxCount;
        }

        public int TryGetResurses(int count)
        {
            int minedRes=0;
            
            if (_currentResurseCount < count)
            {                
                Debug.Log($"Not enough {_resurseInHolder.NameOFResurse} in holder");
            }
            else
            {
                _currentResurseCount -= count;
                minedRes = count;
            }
            return minedRes;
        }
        public void SetCurrentResValue(int value)
        {
            _currentResurseCount = value;
        }       

        public int TryAddResurse(IResurseHolder holder)
        {
            int notAddedResValue = 0;
            if (holder.ResurseInHolder.ResurseType == _resurseInHolder.ResurseType)
            {
                _currentResurseCount = holder.CurrentResurseCount;
                if (_currentResurseCount>MaxResurseCount)
                {
                    notAddedResValue = _currentResurseCount - MaxResurseCount;
                    _currentResurseCount = MaxResurseCount;
                    Debug.Log($"The holder of {_resurseInHolder.NameOFResurse} is full ");
                }
            }
                return notAddedResValue;
        }
    }
}
