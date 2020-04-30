using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
public class GameSettings : ScriptableObject
{
    public int Level;
    public int CountOfTheSliderEnemies;
    public int CountOfTheFlyerEnemies;
    public int CountOfTheBossEnemies;
    public int MeleeDamage;
    public float DamageDistance;
    public float FirstTimerOption_1s;
    public float SecondTimerOption_2s;
    public float ThirdTimerOption_200ms;
    public float DistanceForRandMovement;
}
