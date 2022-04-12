using Controllers.BaseUnit;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.Outpost;

namespace Controllers
{
    public class InputController: MonoBehaviour
    {
        [SerializeField] private BaseUnitSpawner _spawner;
        //[SerializeField] private BuildingGrid _buildingGrid;
        
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
                    if (_spawner.SpawnIsActiveIndex != -1)
                    {
                        _spawner.UnShowMenu();
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