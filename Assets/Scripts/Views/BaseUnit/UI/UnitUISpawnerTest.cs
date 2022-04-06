using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Views.BaseUnit.UI
{
    public class UnitUISpawnerTest : MonoBehaviour
    {
        [SerializeField] private Button _spawnButton;
        [SerializeField] private GroupData _data;
        public Action<Vector3> spawnUnit = delegate {  };

        private void Start()
        {
            _spawnButton.onClick.AddListener(buttonPressed);
        }

        private void buttonPressed()
        {
            spawnUnit.Invoke(_data.CentralPosition);
        }

        private void OnDestroy()
        {
            _spawnButton.onClick.RemoveAllListeners();
        }
    }
}