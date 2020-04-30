using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;

public class Player : MonoBehaviour, IDestroyable
{
    private Joystick _joystick;
    private NavMeshAgent _navMeshAgent;
    private GameSettings _gameSettings;

    private IBulletSystem _bulletSystem;
    private IPrefabsCreater _prefabsCreater;
    private IEndGame _endGame;
    private IPlayerHealth _playerHealth;

    private bool _canShoot;
    private bool _shooting;

    private int _playerHP;
    private float _playerSpeed;

    [Inject]
    private void Construct(Joystick joystick, CharactersData charactersData, IBulletSystem bulletSystem,
        IPrefabsCreater prefabsCreater, GameSettings gameSettings, IEndGame endGame, IPlayerHealth playerHealth)
    {
        _joystick = joystick;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerHP = charactersData.PlayerData.CharacterHp;
        _playerSpeed = charactersData.PlayerData.CharacterSpeed / 6.16f;
        _bulletSystem = bulletSystem;
        _prefabsCreater = prefabsCreater;
        _gameSettings = gameSettings;
        _endGame = endGame;
        _playerHealth = playerHealth;
        _playerHealth.ChangeHealth(_playerHP);
        _canShoot = true;
    }

    public class Factory : PlaceholderFactory<Player>
    {

    }

    private void Start()
    {
        _navMeshAgent.enabled = true;
    }

    private void Update()
    {
        PlayerMove();

        if (_canShoot && !_shooting)
        {
            PlayerShoot();
        }
    }

    public void ApplyDamage(int damage)
    {
        if (_playerHP > 0)
        {
            _playerHP -= damage;
            _playerHealth.ChangeHealth(_playerHP);
        }
        else
        {
            _endGame.GameOver("Die");
        }
    }

    public void DeActivate()
    {
        Destroy(gameObject);
    }

    private void PlayerMove()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _canShoot = false;

            transform.position += new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * _playerSpeed * Time.deltaTime;
        }
        else
        {
            _canShoot = true;
        }
    }

    private void PlayerShoot()
    {
        if (_prefabsCreater.Enemies.Count > 0)
        {
            var target = FindTheNearestEnemy();
            var vector = (target - transform.position).normalized;
            _bulletSystem.Shot(transform.position, target, vector, LayerMask.LayerToName(9));
            _canShoot = false;
            _shooting = true;
            StartCoroutine(WaitForSecond());
        }
    }

    private Vector3 FindTheNearestEnemy()
    {
        var DistanceForTheNearestEnemyPos = float.MaxValue;
        Vector3 nearestEnemyPos = Vector3.zero;

        if (_prefabsCreater.Enemies.Count > 1)
        {
            for (int i = 0; i < _prefabsCreater.Enemies.Count; i++)
            {
                if (Vector3.Distance(transform.position, _prefabsCreater.Enemies[i].transform.position) <= DistanceForTheNearestEnemyPos)
                {
                    DistanceForTheNearestEnemyPos = Vector3.Distance(transform.position, _prefabsCreater.Enemies[i].transform.position);
                    nearestEnemyPos = _prefabsCreater.Enemies[i].transform.position;
                }
            }
        }
        else
        {
            return _prefabsCreater.Enemies[0].transform.position;
        }

        return nearestEnemyPos;
    }

    private IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(_gameSettings.FirstTimerOption_1s);
        _shooting = false;
    }

}
