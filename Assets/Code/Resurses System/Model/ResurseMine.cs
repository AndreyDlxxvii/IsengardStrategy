using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    
    [CreateAssetMenu(fileName = "Resurse Mine", menuName = "Resurse System/Resurse Mine", order = 1)]
    [System.Serializable]
    public class ResurseMine : ScriptableObject, IResurseMine,IIconHolder
    {
        public string NameOfMine => _nameOfMine;
        public float ExtractionTime => _extractionTime;
        public ResurseHolder ResurseHolderMine => _resurseHolderMine;
        public Sprite Icon => _icon;
        public float CurrentMineValue => _currentMineValue;

        [SerializeField]
        private string _nameOfMine;
        [SerializeField]
        private float _extractionTime;
        [SerializeField]
        private ResurseHolder _resurseHolderMine;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private float _currentMineValue;
        public Action<ResurseHolder> resurseMined;
        
        public ResurseMine (ResurseMine mine)
        {
            _nameOfMine = mine.NameOfMine;
            _extractionTime = mine.ExtractionTime;
            _resurseHolderMine = new ResurseHolder(mine.ResurseHolderMine.ObjectInHolder, mine.ResurseHolderMine.MaxValue, mine.ResurseHolderMine.CurrentValue);
            _icon = mine.Icon;            
        }
        //Метод переопределения времени добычи
        public void SetExtractionTime(float time)
        {
            _extractionTime=time;
        } 
        //метод переопределения к-во ресурса в "шахте" для добычи 1 юнитом за раз
        public void SetCurrentValueMine(float value)
        {
            if (value<=_resurseHolderMine.MaxValue)
            {
                _resurseHolderMine.SetCurrentValueHolder(value);
            }
            else
            {
                Debug.Log("нельзя просто так взять, и заполнить шахту выше максимум!");
            }
        }
        //метод добычи из шахты возвращает сразу "холдер" для транспортировки ресурса
        public ResurseHolder MineResurse()
        {
            ResurseHolder tempResHolder = new ResurseHolder(_resurseHolderMine.ObjectInHolder, 0f, _currentMineValue);
            _resurseHolderMine.GetFromHolder(tempResHolder);
            resurseMined?.Invoke(_resurseHolderMine);
            return tempResHolder;
        }
        //метод переопределения иконки шахты
        public void SetIconMine(Sprite icon)
        {
            _icon = icon;
        }           

        public ResurseMine(string name, float time, ResurseHolder resholder, Sprite icon, int value)
        {
            _nameOfMine = name;
            _extractionTime = time;
            _resurseHolderMine = resholder;
            _icon = icon;
            _currentMineValue = value;
        }
    }
}
