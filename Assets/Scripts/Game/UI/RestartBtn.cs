using UnityEngine;

public class RestartBtn : MonoBehaviour
{
    public void Restart()
    {
        SceneController.LoadGameScene();
    }
}
