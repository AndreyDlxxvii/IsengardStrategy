using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{
    public enum ChoiceColourType
    {
        black,
        blue,
        brown,
        green,
        purple,
        red,
        tan,
        white
    }
    [CreateAssetMenu(fileName = "RaceAndColourConfiguration", menuName = "RaceMaterialPreset", order = 1)]
    public class RaceAndColourBaseSO : ScriptableObject
    {
        
        [SerializeField]
        private string NameOfRace;
        [SerializeField]
        private Material BaseMaterialOfRace;
        [SerializeField]
        private List<Material> ColourMaterial;

        public Material GetBaseMaterial()
        {
            return BaseMaterialOfRace;
        }

        public Material GetColourMaterial(int index)
        {
            return ColourMaterial[index];
        }

    }
}
