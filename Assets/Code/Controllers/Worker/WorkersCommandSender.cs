using System.Collections.Generic;
using Controllers.ResouresesPlace;
using Controllers.Worker;
using Enums.BaseUnit;
using UnityEngine;

namespace Controllers.BaseUnit
{
    public sealed class WorkersCommandSender: IOnController
    {
        public WorkersCommandSender()
        {
            
        }

        public void SendMiningCommand(WorkerController workerController, ResourcesPlaceController resourcesPlaceController)
        {
            List<Vector3> listPositions = new List<Vector3>();
            List<float> listOfTimers = new List<float>();
            listPositions.Add(resourcesPlaceController.PlaceView.gameObject.transform.position);
            listPositions.Add(resourcesPlaceController.Warehouse.transform.position);
            listOfTimers.Add(resourcesPlaceController.ResurseMine.ExtractionTime);
            
            List<UnitStates> workerActionsList = new List<UnitStates>();
            workerActionsList.Add(UnitStates.MOVING);
            SetEndPosition(workerController,listPositions[0]);
            workerActionsList.Add(UnitStates.ATTAKING); //work
            SetEndTime(workerController,listOfTimers[0]);
            workerActionsList.Add(UnitStates.GETSTAFF);
            workerActionsList.Add(UnitStates.MOVING);
            SetEndPosition(workerController,listPositions[1]);
            workerActionsList.Add(UnitStates.SETSTAFF);
            workerController.SetUnitSequence(workerActionsList);
        }

        public void SendBuildingCommand(WorkerController workerController, ResourcesPlaceController resourcesPlaceController)
        {
            //заготовка
            /*List<Vector3> listPositions = new List<Vector3>();
            List<float> listOfTimers = new List<float>();
            listPositions.Add(resourcesPlaceController.PlaceView.gameObject.transform.position);
            listPositions.Add(resourcesPlaceController.Warehouse.transform.position);
            listOfTimers.Add(resourcesPlaceController.ResurseMine.ExtractionTime);
            
            List<UnitStates> workerActionsList = new List<UnitStates>();
            workerActionsList.Add(UnitStates.MOVING);
            SetEndPosition(workerController,listPositions[0]);
            workerActionsList.Add(UnitStates.GETSTAFF);
            workerActionsList.Add(UnitStates.MOVING);
            SetEndPosition(workerController,listPositions[0]);
            workerActionsList.Add(UnitStates.ATTAKING); //work
            SetEndTime(workerController,listOfTimers[0]);
            workerActionsList.Add(UnitStates.MOVING);
            SetEndPosition(workerController,listPositions[1]);
            workerController.SetUnitSequence(workerActionsList);*/
        }

        public void StopCommand(WorkerController workerController) //Go home
        {
            
        }
        
        private void SetEndPosition(WorkerController workerController, Vector3 endpos)
        {
            workerController.SetDestination(endpos);
        }

        private void SetEndTime(WorkerController workerController, float time)
        {
            workerController.SetEndTime(time);
        }
    }
}