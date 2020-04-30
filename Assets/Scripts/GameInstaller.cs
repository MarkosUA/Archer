using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private EnemySlider _enemySlider;
    [SerializeField]
    private EnemyFlyer _enemyFlyer;
    [SerializeField]
    private EnemyBoss _enemyBoss;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Arena _arena1;
    [SerializeField]
    private Arena _arena2;
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private BulletSystem _bulletSystem;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private RectTransform _popup;


    public override void InstallBindings()
    {
        BindAllData();
        BindAllInterfaces();
        BindAllPrefabs();

        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<Slider>().FromInstance(_slider).AsSingle();
        Container.Bind<TMP_Text>().FromInstance(_text).AsSingle();
        Container.Bind<RectTransform>().FromInstance(_popup).AsSingle();
    }

    private void BindAllPrefabs()
    {
        Container.BindFactory<EnemySlider, EnemySlider.Factory>().FromComponentInNewPrefab(_enemySlider).AsSingle();
        Container.BindFactory<EnemyFlyer, EnemyFlyer.Factory>().FromComponentInNewPrefab(_enemyFlyer).AsSingle();
        Container.BindFactory<EnemyBoss, EnemyBoss.Factory>().FromComponentInNewPrefab(_enemyBoss).AsSingle();
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(_player).AsSingle();
    }

    private void BindAllInterfaces()
    {
        Container.Bind<IGameController>().To<GameController>().AsSingle();
        Container.Bind<ICharactersCreater>().To<CharactersCreater>().AsSingle();
        Container.Bind<IPrefabsCreater>().To<PrefabsCreater>().AsSingle();
        Container.Bind<IArenaCreater>().To<ArenaCreater>().AsSingle();
        Container.Bind<IArenasFactory>().To<ArenasFactory>().AsSingle();
        Container.Bind<INavMeshCreater>().To<NavMeshCreater>().AsSingle();
        Container.Bind<IChooserRandPosToMove>().To<ChooserRandPosToMove>().AsSingle();
        Container.Bind<IBulletSystem>().To<BulletSystem>().FromInstance(_bulletSystem).AsSingle();
        Container.Bind<IEnemyRemover>().To<EnemyRemover>().AsSingle();
        Container.Bind<IEndGame>().To<EndGame>().AsSingle();
        Container.Bind<IPlayerHealth>().To<PlayerHealth>().AsSingle();
        Container.Bind<IFinalPanel>().To<FinalPanel>().AsSingle();
    }

    private void BindAllData()
    {
        Container.Bind<DataLoader>().AsSingle();

        Container.Bind<IDataTransferToSO>().To<DataTransferToSO>().AsSingle();
    }
}
