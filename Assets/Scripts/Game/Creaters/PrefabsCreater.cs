using System.Collections.Generic;
using UnityEngine;

public class PrefabsCreater : IPrefabsCreater
{
    private IArenaCreater _arenaCreater;
    private ICharactersCreater _charactersCreater;
    private GameSettings _gameSettings;
    private Arena _arena;
    private Player _player;
    private List<GameObject> _enemies = new List<GameObject>();

    public PrefabsCreater(IArenaCreater arenaCreater, ICharactersCreater charactersCreater, GameSettings gameSettings)
    {
        _arenaCreater = arenaCreater;
        _charactersCreater = charactersCreater;
        _gameSettings = gameSettings;
    }

    public Arena Arena
    {
        get { return _arena; }
    }

    public Player Player
    {
        get { return _player; }
    }

    public List<GameObject> Enemies
    {
        get { return _enemies; }
    }

    public void CreatePrefabs()
    {
        switch (_gameSettings.Level)
        {
            case 1:
                CreateLevel_1();
                break;
            case 2:
                CreateLevel_2();
                break;
        }
    }

    private void CreateArena()
    {
        _arena = _arenaCreater.CreateArena(_gameSettings.Level);
        _arena.transform.position = new Vector3(0, 0, 0);
    }

    private void CreatePlayer()
    {
        _player = _charactersCreater.CreatePlayer();
        _player.transform.position = new Vector3(0, 0.5f, -10);
    }

    private void CreateLevel_1()
    {
        CreateArena();
        CreatePlayer();

        for (int i = 0; i < _gameSettings.CountOfTheSliderEnemies; i++)
        {
            var enemySlider = _charactersCreater.CreateEnemySlider();
            enemySlider.transform.position = new Vector3(Random.Range(-6.6f, 6.6f), 0.5f, Random.Range(15, 3.5f));
            _enemies.Add(enemySlider.gameObject);
        }
        for (int i = 0; i < _gameSettings.CountOfTheFlyerEnemies; i++)
        {
            var enemyFlyer = _charactersCreater.CreateEnemyFlyer();
            enemyFlyer.transform.position = new Vector3(Random.Range(-6.6f, 6.6f), 0.5f, Random.Range(15, 3.5f));
            _enemies.Add(enemyFlyer.gameObject);
        }
    }

    private void CreateLevel_2()
    {
        CreateArena();
        CreatePlayer();

        for (int i = 0; i < _gameSettings.CountOfTheBossEnemies; i++)
        {
            var enemyBoss = _charactersCreater.CreateEnemyBoss();
            enemyBoss.transform.position = Vector3.zero;
            _enemies.Add(enemyBoss.gameObject);
        }
    }

}
