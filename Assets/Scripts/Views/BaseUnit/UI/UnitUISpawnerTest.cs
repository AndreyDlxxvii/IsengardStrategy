using System;
using Controllers.OutPost;
using Data;
using Models.BaseUnit;
using UnityEngine;
using UnityEngine.UI;

namespace Views.BaseUnit.UI
{
    public class UnitUISpawnerTest : MonoBehaviour
    {
        [SerializeField] private Button _spawnButton;
        public OutPostUnitController currentController;
        public BaseUnitModel Model;
        public Action spawnUnit = delegate {  };

        private void Start()
        {
            _spawnButton.onClick.AddListener(buttonPressed);
        }

        private void buttonPressed()
        {
            Model = new BaseUnitModel();
            spawnUnit.Invoke();
        }

        private void OnDestroy()
        {
            _spawnButton.onClick.RemoveAllListeners();
        }
    }
}