using Controllers;
using Controllers.BaseUnit;
using Controllers.OutPost;
using UnityEngine;
using UnityEngine.AI;
using Views.BaseUnit.UI;

public class GameInit
{
    public GameInit(Controller controller, GameConfig gameConfig, RightUI rightUI, NavMeshSurface navMeshSurface,
        Transform canvas, LeftUI leftUI, LayerMask layerMask,UnitUISpawnerTest unitUISpawnerTest)
    {

        var tiles = GetTileList.GetTiles(gameConfig);
            
        var btnConroller = new BtnUIController(rightUI, gameConfig);
        var levelGenerator = new GeneratorLevelController(tiles, gameConfig, rightUI, btnConroller, canvas, navMeshSurface);
        var unitController = new UnitController();
        var outPostSpawner = new OutpostSpawner(unitUISpawnerTest);
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outPostSpawner);
        var resourceGenerator = new ResourceGenerator(levelGenerator.PositionSpawnedTiles, buildController.Buildings, leftUI, gameConfig);
        var unitSpawner = new BaseUnitSpawner(gameConfig,unitController,outPostSpawner,gameConfig.BaseUnit);
        var inputController = new InputController(unitSpawner);

        controller.Add(btnConroller);
        controller.Add(levelGenerator);
        controller.Add(buildController);
        controller.Add(resourceGenerator);
        controller.Add(unitController);
        controller.Add(outPostSpawner);
        controller.Add(unitSpawner);
        controller.Add(inputController);

    }
}