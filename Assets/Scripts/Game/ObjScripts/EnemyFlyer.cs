using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;

public class EnemyFlyer : MonoBehaviour, IDestroyable
{
    private NavMeshAgent _navMeshAgent;
    private GameSettings _gameSettings;

    private IChooserRandPosToMove _chooserRandPosToMove;
    private IBulletSystem _bulletSystem;
    private IPrefabsCreater _prefabsCreater;
    private IEnemyRemover _enemyRemover;

    private int _enemyFlyerHP;

    private bool _needToCheckPosition;

    [Inject]
    private void Construct(IPrefabsCreater prefabsCreater, CharactersData charactersData, GameSettings gameSettings,
        IChooserRandPosToMove chooserRandPosToMove, IBulletSystem bulletSystem, IEnemyRemover enemyRemover)
    {
        _prefabsCreater = prefabsCreater;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = charactersData.EnemyFlyerData.CharacterSpeed;
        _enemyFlyerHP = charactersData.EnemyFlyerData.CharacterHp;
        _gameSettings = gameSettings;
        _chooserRandPosToMove = chooserRandPosToMove;
        _bulletSystem = bulletSystem;
        _enemyRemover = enemyRemover;
    }

    public class Factory : PlaceholderFactory<EnemyFlyer>
    {

    }

    private void Start()
    {
        _navMeshAgent.enabled = true;
        EnemyFlyerMovement();
    }

    private void Update()
    {
        CheckPosition();
    }

    public void ApplyDamage(int damage)
    {
        if (_enemyFlyerHP > 0)
        {
            _enemyFlyerHP -= damage;
        }
        else
        {
            _enemyRemover.DeleteEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private void EnemyFlyerMovement()
    {
        var newTargetToMove = _chooserRandPosToMove.ChooseRandPos(transform.position);

        _navMeshAgent.SetDestination(newTargetToMove);
        _needToCheckPosition = true;
    }

    private void CheckPosition()
    {
        if (_needToCheckPosition)
        {
            if (Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1f)
            {
                _needToCheckPosition = false;
                StartCoroutine(WaitAndShoot());
            }
        }
    }

    private IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(_gameSettings.FirstTimerOption_1s);
        if (_prefabsCreater.Player != null)
        {
            var vector = (_prefabsCreater.Player.transform.position - transform.position).normalized;
            _bulletSystem.Shot(transform.position, _prefabsCreater.Player.transform.position, vector, LayerMask.LayerToName(8));
            yield return new WaitForSeconds(_gameSettings.FirstTimerOption_1s);
            EnemyFlyerMovement();
        }
    }

}
