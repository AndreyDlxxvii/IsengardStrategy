using Controllers.BaseUnit;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.Outpost;
using ResurseSystem;

namespace Controllers
{
    public class InputController: MonoBehaviour
    {
        [SerializeField] private BaseUnitSpawner _spawner;
        [SerializeField] private BuildingResursesUIController _rescontoller;
        [SerializeField] private BuildingsUI _buildingsUI;
        [SerializeField] private ResurseStockView _currStock;
        [SerializeField] private Mineral _currMine;
        //[SerializeField] private BuildingGrid _buildingGrid;

        private void Awake()
        {
            _rescontoller = new BuildingResursesUIController(_buildingsUI);
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
                if(Physics.Raycast(ray, out hit, 100))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    var outpost = hit.collider.gameObject.GetComponent<OutpostUnitView>();
                    var currStock = hit.collider.gameObject.GetComponentInParent<ResurseStockView>();
                    var currMine = hit.collider.gameObject.GetComponentInParent<Mineral>();
                    if (_spawner.SpawnIsActiveIndex != -1)
                    {
                        _spawner.UnShowMenu();
                    }
                    if (currStock & currStock!=_currStock)
                    {
                        _rescontoller.SetActiveUI(currStock);
                        _currStock = currStock;

                    }
                    else
                    {
                        if (currMine & currMine!=_currMine)
                        {
                            _rescontoller.SetActiveUI(currMine);
                            _currMine = currMine;
                        }
                        else
                        {
                            _rescontoller.DisableMenu();
                        }
                    }
                    

                    if (outpost)
                    {
                        _spawner.ShowMenu(outpost);
                    }
                }
        
            }
        }
    }
}