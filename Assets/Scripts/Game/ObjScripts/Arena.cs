using UnityEngine;
using Zenject;

public class Arena : MonoBehaviour
{
    public class Factory : PlaceholderFactory<Arena>
    {

    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
