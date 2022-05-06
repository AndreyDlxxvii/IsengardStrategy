using System;
using Controllers.OutPost;
using Data;
using Interfaces;
using Models.BaseUnit;
using UnityEngine;
using UnityEngine.UI;

namespace Views.BaseUnit.UI
{
    public class UnitUISpawnerTest : MonoBehaviour
    {
        [SerializeField] private Button _spawnButton;
        public IUnitSpawner currentController;
        public BaseUnitModel Model;
        public Action<IUnitSpawner> spawnUnit;

        private void Start()
        {
            _spawnButton.onClick.AddListener(buttonPressed);
        }

        private void buttonPressed()
        {
            Model = new BaseUnitModel();
            spawnUnit.Invoke(currentController);
        }

        private void OnDestroy()
        {
            _spawnButton.onClick.RemoveAllListeners();
        }
    }
}