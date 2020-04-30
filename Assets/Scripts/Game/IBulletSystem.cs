using UnityEngine;

public interface IBulletSystem
{
    void Shot(Vector3 shooterPosition, Vector3 target, Vector3 vector, string layer);
}
