using UnityEngine;

[CreateAssetMenu(fileName = "CharactersData", menuName = "Data/CharactersData")]
public class CharactersData : ScriptableObject
{
    public CharacterData PlayerData;
    public CharacterData EnemySliderData;
    public CharacterData EnemyFlyerData;
    public CharacterData EnemyBossData;
}
