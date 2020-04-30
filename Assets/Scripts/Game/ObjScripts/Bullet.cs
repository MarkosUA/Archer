using UnityEngine;

public class Bullet
{
    private GameObject _bulletObj;
    private Vector3 _target;
    private Vector3 _direction;

    private int _speed;
    private int _damage;

    public GameObject BulletObj
    {
        get { return _bulletObj; }
    }

    public Vector3 Target
    {
        get { return _target; }
    }

    public int Damage
    {
        get { return _damage; }
    }

    public int Speed
    {
        get { return _speed; }
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public Bullet(BulletData _bulletData, Vector3 target, GameObject bulletObj)
    {
        _speed = _bulletData.BulletSpeed;
        _damage = _bulletData.BulletDamage;
        _target = target;
        _bulletObj = bulletObj;
    }

    public void DeActivate()
    {
        _bulletObj.SetActive(false);
    }

    public void Activate(Vector3 target)
    {
        _bulletObj.SetActive(true);
        _target = target;
    }
}