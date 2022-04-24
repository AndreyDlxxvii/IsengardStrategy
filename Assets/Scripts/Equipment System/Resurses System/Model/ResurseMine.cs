using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    
    [CreateAssetMenu(fileName = "Resurse Mine", menuName = "Resurse System/Resurse Mine", order = 1)]
    [System.Serializable]
    public class ResurseMine : ScriptableObject, IResurseMine
    {
        public string NameOfMine => _nameOfMine;
        public float ExtractionTime => _extractionTime;
        public ResurseHolder ResurseHolderMine => _resurseHolderMine;
        public Sprite Icon => _icon;

        [SerializeField]
        private string _nameOfMine;
        [SerializeField]
        private float _extractionTime;
        [SerializeField]
        private ResurseHolder _resurseHolderMine;
        [SerializeField]
        private Sprite _icon;
        public Action<ResurseHolder> resurseMined;

        public void SetExtractionTime(float time)
        {
            _extractionTime=time;
        }       

        public int MinedResurse(int value)
        {
            var minedres= _resurseHolderMine.MineResurses(value);
            resurseMined?.Invoke(_resurseHolderMine);
            return minedres;
        }
        public void SetIconMine(Sprite icon)
        {
            _icon = icon;
        }
        public ResurseMine(string name, float time, ResurseHolder resholder, Sprite icon )
        {
            _nameOfMine = name;
            _extractionTime = time;
            _resurseHolderMine = resholder;
            _icon = icon;
        }
    }
}
