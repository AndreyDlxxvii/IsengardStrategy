using Controllers.OutPost;
using UnityEngine;
using UnityEngine.AI;

public class GameInit
{
    public GameInit(Controller controller, GameConfig gameConfig, RightUI rightUI, NavMeshSurface navMeshSurface,
        Transform canvas, LeftUI leftUI, LayerMask layerMask, OutpostSpawner outpostSpawner)
    {

        var tiles = GetTileList.GetTiles(gameConfig);
            
        var btnConroller = new BtnUIController(rightUI, gameConfig);
        var levelGenerator = new GeneratorLevelController(tiles, gameConfig, rightUI, btnConroller, canvas, navMeshSurface);
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outpostSpawner);
        var resourceGenerator = new ResourceGenerator(levelGenerator.PositionSpawnedTiles, buildController.Buildings, leftUI, gameConfig);
        
        controller.Add(btnConroller);
        controller.Add(levelGenerator);
        controller.Add(buildController);
        controller.Add(resourceGenerator);
    }
}