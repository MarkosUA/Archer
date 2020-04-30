using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private BulletData _bulletData;
    [SerializeField]
    private CharactersData _charactersData;
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private PrefabArenaData _prefabArenaData;
    [SerializeField]
    private PrefabBulletsData _prefabBulletsData;

    public override void InstallBindings()
    {
        BindAllSO();
    }

    private void BindAllSO()
    {
        Container.Bind<BulletData>().FromInstance(_bulletData).AsSingle();
        Container.Bind<CharactersData>().FromInstance(_charactersData).AsSingle();
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        Container.Bind<PrefabArenaData>().FromInstance(_prefabArenaData).AsSingle();
        Container.Bind<PrefabBulletsData>().FromInstance(_prefabBulletsData).AsSingle();
    }
}
