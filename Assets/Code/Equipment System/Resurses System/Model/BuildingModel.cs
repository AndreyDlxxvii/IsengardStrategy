using ResurseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Building Model", menuName = "Buildings/BuildingModel", order = 1)]
public class BuildingModel : ScriptableObject, IBuildingModel
{
    public ResurseCost ThisBuildingCost => _thisBuildinCost;
    public GameObject BasePrefab => _thisBuildingPrefab;
    public ResurseStock ThisBuildingStock => _thisBuildingStock;
    public string Name => _nameBuilding;
    public float Health => _currentHealth;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    public GameObject GotBuildPrefab => _gotBuildPrefab;

    [SerializeField] private ResurseCost _thisBuildinCost;
    [SerializeField] private GameObject _thisBuildingPrefab;
    [SerializeField] private ResurseStock _thisBuildingStock;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _nameBuilding;
    [SerializeField] private GameObject _gotBuildPrefab;

    public BuildingModel()
    {

    }
    public void SetName(string name)
    {
        _nameBuilding=name;
    }
    
}
