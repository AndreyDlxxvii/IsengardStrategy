using Controllers.BaseUnit;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.Outpost;

namespace Controllers
{
    public class InputController: IOnController , IOnUpdate
    {
        private BaseUnitSpawner _spawner;

        public InputController(BaseUnitSpawner baseUnitSpawner)
        {
            _spawner = baseUnitSpawner;
        }

        public void OnUpdate(float deltaTime)
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