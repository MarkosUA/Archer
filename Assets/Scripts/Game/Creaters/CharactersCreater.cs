
public class CharactersCreater : ICharactersCreater
{
    private EnemySlider.Factory _sliderFactory;
    private EnemyFlyer.Factory _flyerFactory;
    private EnemyBoss.Factory _bossFactory;
    private Player.Factory _playerFactory;

    public CharactersCreater(EnemySlider.Factory sliderFactory, EnemyFlyer.Factory flyerFactory, 
        EnemyBoss.Factory bossFactory, Player.Factory playerFactory)
    {
        _sliderFactory = sliderFactory;
        _flyerFactory = flyerFactory;
        _bossFactory = bossFactory;
        _playerFactory = playerFactory;
    }

    public EnemySlider CreateEnemySlider()
    {
        var enemy = _sliderFactory.Create();
        return enemy;
    }

    public EnemyFlyer CreateEnemyFlyer()
    {
        var enemy = _flyerFactory.Create();
        return enemy;
    }

    public EnemyBoss CreateEnemyBoss()
    {
        var enemy = _bossFactory.Create();
        return enemy;
    }

    public Player CreatePlayer()
    {
        var player = _playerFactory.Create();
        return player;
    }
}
