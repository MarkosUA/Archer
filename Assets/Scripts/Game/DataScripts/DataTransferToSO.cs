
public class DataTransferToSO : IDataTransferToSO
{
    private GameData _gameData;
    private BulletData _bulletData;
    private CharacterData _playerData;
    private CharacterData _enemySliderData;
    private CharacterData _enemyFlyerData;
    private CharacterData _enemyBossData;

    public DataTransferToSO(BulletData bulletData, CharactersData charactersData, GameSettings gameSettings)
    {
        gameSettings.Level = 1;
        _gameData = DataLoader.LoadData<GameData>();
        _bulletData = bulletData;
        _playerData = charactersData.PlayerData;
        _enemySliderData = charactersData.EnemySliderData;
        _enemyFlyerData = charactersData.EnemyFlyerData;
        _enemyBossData = charactersData.EnemyBossData;
    }

    public void TransferAllData()
    {
        TransferBulletData();
        TransferPlayerData();
        TransferEnemySliderData();
        TransferEnemyFlyerData();
        TransferEnemyBossData();
    }

    private void TransferBulletData()
    {
        _bulletData.BulletDamage = _gameData.BulletDamage;
        _bulletData.BulletSpeed = _gameData.BulletSpeed;
    }

    private void TransferPlayerData()
    {
        _playerData.CharacterHp = _gameData.PlayerHP;
        _playerData.CharacterSpeed = _gameData.PlayerSpeed;
    }

    private void TransferEnemySliderData()
    {
        _enemySliderData.CharacterHp = _gameData.EnemySliderHp;
        _enemySliderData.CharacterSpeed = _gameData.EnemySliderSpeed;
    }

    private void TransferEnemyFlyerData()
    {
        _enemyFlyerData.CharacterHp = _gameData.EnemyFlyerHp;
        _enemyFlyerData.CharacterSpeed = _gameData.EnemyFlyerSpeed;
    }

    private void TransferEnemyBossData()
    {
        _enemyBossData.CharacterHp = _gameData.EnemyBossHp;
        _enemyBossData.CharacterSpeed = _gameData.EnemyBossSpeed;
    }
}
