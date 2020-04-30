using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;

public class EnemyBoss : MonoBehaviour, IDestroyable
{
    private NavMeshAgent _navMeshAgent;
    private GameSettings _gameSettings;
    private CharactersData _charactersData;
    private BulletData _bulletData;

    private IBulletSystem _bulletSystem;
    private IChooserRandPosToMove _chooserRandPosToMove;
    private IPrefabsCreater _prefabsCreater;
    private IEnemyRemover _enemyRemover;

    private int _enemyBossHP;
    private int _countOfTheBulletSeries;

    private bool _canAttack;
    private bool _needToCheckPosition;

    [Inject]
    private void Construct(IPrefabsCreater prefabsCreater, CharactersData charactersData, GameSettings gameSettings,
        IChooserRandPosToMove chooserRandPosToMove, BulletData bulletData, IBulletSystem bulletSystem,
        IEnemyRemover enemyRemover)
    {
        _prefabsCreater = prefabsCreater;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = charactersData.EnemyBossData.CharacterSpeed;
        _enemyBossHP = charactersData.EnemyBossData.CharacterHp;
        _charactersData = charactersData;
        _gameSettings = gameSettings;
        _chooserRandPosToMove = chooserRandPosToMove;
        _canAttack = true;
        _bulletData = bulletData;
        _countOfTheBulletSeries = bulletData.CountOfTheBulletSeriesForBoss;
        _bulletSystem = bulletSystem;
        _enemyRemover = enemyRemover;
    }

    public class Factory : PlaceholderFactory<EnemyBoss>
    {

    }

    private void Start()
    {
        _navMeshAgent.enabled = true;

        ChooseStateForBoss();
    }

    private void Update()
    {
        CheckPosition();
        MeleeHit();
    }

    public void ApplyDamage(int damage)
    {
        if (_enemyBossHP > 0)
        {
            _enemyBossHP -= damage;
        }
        else
        {
            _enemyRemover.DeleteEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private void ChooseStateForBoss()
    {
        if (_prefabsCreater.Player != null)
        {
            var randState = Random.Range(1, 4);

            switch (randState)
            {
                case 1:
                    State1();
                    break;
                case 2:
                    State2();
                    break;
                case 3:
                    State3();
                    break;
            }
        }
    }

    private void State1()
    {
        if (_prefabsCreater.Player != null)
        {
            var enemyTarget = _prefabsCreater.Player.transform.position;
            var distance = Vector3.Distance(enemyTarget, this.transform.position);
            _navMeshAgent.speed = _charactersData.EnemyBossData.CharacterSpeed * 2;

            if (distance > _gameSettings.DamageDistance)
            {
                _navMeshAgent.SetDestination(enemyTarget);
                ChooseStateForBoss();
            }
        }
    }

    private void State2()
    {
        if (_prefabsCreater.Player != null)
        {
            if (_countOfTheBulletSeries > 0)
            {
                var vector = (_prefabsCreater.Player.transform.position - transform.position).normalized;
                _bulletSystem.Shot(transform.position, _prefabsCreater.Player.transform.position, vector, LayerMask.LayerToName(8));

                StartCoroutine(WaitForTheNextShot());
            }
            else
            {
                var newTargetToMove = _chooserRandPosToMove.ChooseRandPos(transform.position);

                if (gameObject != null)
                {
                    _navMeshAgent.SetDestination(newTargetToMove);
                    _countOfTheBulletSeries = _bulletData.CountOfTheBulletSeriesForBoss;
                    _needToCheckPosition = true;
                }
            }
        }
    }

    private void State3()
    {
        if (_prefabsCreater.Player != null)
        {
            var shotTarget = _prefabsCreater.Player.transform.position;
            var centralVector = (_prefabsCreater.Player.transform.position - transform.position).normalized;
            var leftVector = Quaternion.Euler(0, -30, 0) * centralVector;
            var rightVector = Quaternion.Euler(0, 30, 0) * centralVector;

            _bulletSystem.Shot(transform.position, shotTarget, centralVector, LayerMask.LayerToName(8));
            _bulletSystem.Shot(transform.position, shotTarget, rightVector, LayerMask.LayerToName(8));
            _bulletSystem.Shot(transform.position, shotTarget, leftVector, LayerMask.LayerToName(8));

            StartCoroutine(WaitAndChooseNewState());
        }
    }

    private void CheckPosition()
    {
        if (_needToCheckPosition)
        {
            if (Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1f)
            {
                _needToCheckPosition = false;
                StartCoroutine(WaitAndChooseNewState());
            }
        }
    }

    private void MeleeHit()
    {
        if (_prefabsCreater.Player != null)
        {
            var enemyTarget = _prefabsCreater.Player.transform.position;
            var distance = Vector3.Distance(enemyTarget, this.transform.position);

            if (distance <= _gameSettings.DamageDistance)
            {
                if (_canAttack)
                {
                    _prefabsCreater.Player.ApplyDamage(_gameSettings.MeleeDamage);
                    StartCoroutine(TimerToEndMeleeAtack());
                    State1();
                }
                else
                {
                    StartCoroutine(WaitAndChooseNewState());
                    _navMeshAgent.speed = _charactersData.EnemyBossData.CharacterSpeed;
                    _canAttack = true;
                }
            }
        }
    }

    private IEnumerator TimerToEndMeleeAtack()
    {
        yield return new WaitForSeconds(_gameSettings.SecondTimerOption_2s);
        _canAttack = false;
    }

    private IEnumerator WaitForTheNextShot()
    {
        yield return new WaitForSeconds(_gameSettings.ThirdTimerOption_200ms);
        _countOfTheBulletSeries--;
        State2();
    }

    private IEnumerator WaitAndChooseNewState()
    {
        yield return new WaitForSeconds(_gameSettings.FirstTimerOption_1s);
        ChooseStateForBoss();
    }
}
