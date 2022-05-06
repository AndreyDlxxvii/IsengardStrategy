using ResurseSystem;

namespace Interfaces
{
    public interface ISpawnerLogicWorker
    {
        void SpawnLogic(ISpawnerLogicView view,BuildingView buildingView);
    }
}