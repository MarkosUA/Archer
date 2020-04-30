using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private IGameController _gameController;

    [Inject]
    private void Construct(IGameController gameController)
    {
        _gameController = gameController;
    }

    private void Start()
    {
        _gameController.StartGame();
    }
}
