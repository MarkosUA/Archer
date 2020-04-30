using Zenject;

public class ArenasFactory : IArenasFactory
{
    private PrefabArenaData _prefabArenasData;
    private DiContainer _diContainer;

    public ArenasFactory(PrefabArenaData prefabArenaData, DiContainer diContainer)
    {
        _prefabArenasData = prefabArenaData;
        _diContainer = diContainer;
    }

    public Arena Create(int index)
    {
        return _diContainer.InstantiatePrefab(_prefabArenasData.Arenas[index]).GetComponent<Arena>();
    }
}
