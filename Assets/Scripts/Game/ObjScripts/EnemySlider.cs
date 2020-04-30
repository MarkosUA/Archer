using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Zenject;

public class EnemySlider : MonoBehaviour, IDestroyable
{
    private IPrefabsCreater _prefabsCreater;
    private IEnemyRemover _enemyRemover;

    private NavMeshAgent _navMeshAgent;
    private GameSettings _gameSettings;

    private int _enemySliderHp;

    private bool _canAttack;

    [Inject]
    private void Construct(IPrefabsCreater prefabsCreater, CharactersData charactersData, GameSettings gameSettings,
        IEnemyRemover enemyRemover)
    {
        _prefabsCreater = prefabsCreater;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = charactersData.EnemySliderData.CharacterSpeed;
        _enemySliderHp = charactersData.EnemySliderData.CharacterHp;
        _gameSettings = gameSettings;
        _enemyRemover = enemyRemover;
        _canAttack = true;
    }

    public class Factory : PlaceholderFactory<EnemySlider>
    {

    }

    private void Start()
    {
        _navMeshAgent.enabled = true;
    }

    private void Update()
    {
        EnemySliderAction();
    }

    public void ApplyDamage(int damage)
    {
        if (_enemySliderHp > 0)
        {
            _enemySliderHp -= damage;
        }
        else
        {
            _enemyRemover.DeleteEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private void EnemySliderAction()
    {
        if (_prefabsCreater.Player != null)
        {
            var enemyTarget = _prefabsCreater.Player.transform.position;
            var distance = Vector3.Distance(enemyTarget, this.transform.position);

            if (distance > _gameSettings.DamageDistance)
            {
                _navMeshAgent.SetDestination(enemyTarget);
            }
            else
            {
                if (_canAttack)
                {
                    _canAttack = false;
                    _prefabsCreater.Player.ApplyDamage(_gameSettings.MeleeDamage);
                    StartCoroutine(Wait(_gameSettings.FirstTimerOption_1s));
                }
            }
        }
    }

    private IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
        _canAttack = true;
    }

}
