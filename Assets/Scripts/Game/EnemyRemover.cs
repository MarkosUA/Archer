using UnityEngine;

public class EnemyRemover : IEnemyRemover
{
    private IPrefabsCreater _prefabsCreater;
    private IEndGame _endGame;

    private GameSettings _gameSettings;

    public EnemyRemover(IPrefabsCreater prefabsCreater, IEndGame endGame, GameSettings gameSettings)
    {
        _prefabsCreater = prefabsCreater;
        _gameSettings = gameSettings;
        _endGame = endGame;
    }

    public void DeleteEnemy(GameObject enemy)
    {
        _prefabsCreater.Enemies.Remove(enemy);

        if (_prefabsCreater.Enemies.Count == 0)
        {
            if (_gameSettings.Level == 1)
            {
                _endGame.NextLevel();
            }
            else
                _endGame.GameOver("Win");
        }
    }

}
