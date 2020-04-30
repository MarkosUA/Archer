
public class ArenaCreater : IArenaCreater
{
    private IArenasFactory _arenasFactory;
    private INavMeshCreater _navMeshCreater;

    public ArenaCreater(IArenasFactory arenasFactory, INavMeshCreater navMeshCreater)
    {
        _arenasFactory = arenasFactory;
        _navMeshCreater = navMeshCreater;
    }

    public Arena CreateArena(int level)
    {
        if (level > 0)
        {
            var arena = _arenasFactory.Create(level - 1);
            _navMeshCreater.CreateNavMesh(arena);
            return arena;
        }
        return default;
    }

}
