
public class GameController : IGameController
{
    private IPrefabsCreater _prefabsCreater;
    private IDataTransferToSO _dataTransferToSO;

    public GameController(IPrefabsCreater prefabsCreater, IDataTransferToSO dataTransferToSO)
    {
        _prefabsCreater = prefabsCreater;
        _dataTransferToSO = dataTransferToSO;
    }

    public void StartGame()
    {
        _dataTransferToSO.TransferAllData();

        _prefabsCreater.CreatePrefabs();
    }
}
