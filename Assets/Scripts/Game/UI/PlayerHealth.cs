using UnityEngine.UI;
using Zenject;

public class PlayerHealth : IPlayerHealth
{
    private Slider _slider;

    [Inject]
    private void Construct(Slider slider, CharactersData charactersData)
    {
        _slider = slider;
        _slider.maxValue = charactersData.PlayerData.CharacterHp;
    }

    public void ChangeHealth(int health)
    {
        _slider.value = health;
    }
}
