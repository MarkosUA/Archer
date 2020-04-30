using System;
using UniRx;

public class EndGame : IEndGame
{
    private IPrefabsCreater _prefabsCreater;
    private IDisposable disposable;
    private IFinalPanel _finalPanel;

    private bool _gameEnded;

    private GameSettings _gameSettings;

    public EndGame(IPrefabsCreater prefabsCreater, GameSettings gameSettings, IFinalPanel finalPanel)
    {
        _prefabsCreater = prefabsCreater;
        _gameSettings = gameSettings;
        _finalPanel = finalPanel;
    }

    public void NextLevel()
    {
        disposable = Observable.Timer(System.TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            _prefabsCreater.Arena.DeActivate();
            _prefabsCreater.Player.DeActivate();
            _gameSettings.Level++;
            _prefabsCreater.CreatePrefabs();
        });
    }

    public void GameOver(string text)
    {
        if (!_gameEnded)
        {
            if (text == "Die")
                _prefabsCreater.Player.DeActivate();

            _gameEnded = true;

            _finalPanel.ActivateFinalPanel(text);
        }
    }

    private void OnDestroy()
    {
        if (disposable != null)
            disposable.Dispose();
    }
}
