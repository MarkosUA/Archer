using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public int BulletDamage;
    public int BulletSpeed;
    public int CountOfTheBulletSeriesForBoss;
    public int CountOfTheSameTimeBullets;
}
