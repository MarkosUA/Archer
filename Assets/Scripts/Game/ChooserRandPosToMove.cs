using UnityEngine;

public class ChooserRandPosToMove : IChooserRandPosToMove
{
    private GameSettings _gameSettings;

    public ChooserRandPosToMove(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public Vector3 ChooseRandPos(Vector3 currentPos)
    {
        return new Vector3(currentPos.x + Random.Range(-_gameSettings.DistanceForRandMovement, _gameSettings.DistanceForRandMovement),
            currentPos.y, currentPos.z + Random.Range(-_gameSettings.DistanceForRandMovement, _gameSettings.DistanceForRandMovement));
    }
}
