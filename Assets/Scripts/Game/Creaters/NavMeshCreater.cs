using UnityEngine.AI;

public class NavMeshCreater : INavMeshCreater
{
    public void CreateNavMesh(Arena arena)
    {
        arena.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
