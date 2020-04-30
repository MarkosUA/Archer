using System.Collections.Generic;
using UnityEngine;

public interface IPrefabsCreater
{
    void CreatePrefabs();
    Arena Arena { get; }
    Player Player { get; }
    List<GameObject> Enemies { get; }
}
