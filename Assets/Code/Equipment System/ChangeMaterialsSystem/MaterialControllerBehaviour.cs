using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EquipSystem
{ 
    public class MaterialControllerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<RaceAndColourBaseSO> CurrentRaceMaterials;

        [SerializeField]
        private GameObject CharacterPrefab;

        [SerializeField]
        private TMP_Dropdown ChoiceSubRaceDropdown;

        [SerializeField]
        private TMP_Dropdown ChoiceColourDropdown;

        [SerializeField]
        private Material ChangeMTest;

        private Renderer[] CharacterMeshRenders;
       

        private void Awake()
        {
            CharacterMeshRenders = CharacterPrefab.GetComponentsInChildren<Renderer>();
            
        }

        public void ChangeMaterialsInCharacter(Material changeMaterial)
        {
            for(int i=0;i< CharacterMeshRenders.Length;i++)
            {
                CharacterMeshRenders[i].material = changeMaterial;
            }
        }
        public void ChangeSubRace()
        {
            ChangeMaterialsInCharacter(CurrentRaceMaterials[ChoiceSubRaceDropdown.value].GetBaseMaterial());
        }
        public void ChangeColourMaterial()
        {
            ChangeMaterialsInCharacter(CurrentRaceMaterials[ChoiceSubRaceDropdown.value].GetColourMaterial(ChoiceColourDropdown.value));
        }
        private void OnValidate()
        {
            if (ChangeMTest!=null)
            {
                ChangeMaterialsInCharacter(ChangeMTest);
                ChangeMTest = null;
            }
        }
    }
}
