using UnityEngine;
using System.Collections.Generic;
using Zenject;


public class BulletSystem : MonoBehaviour, IBulletSystem
{
    private GameSettings _gameSettings;
    private PrefabBulletsData _prefabBulletsData;
    private BulletData _bulletData;
    private Bullet _bullet;

    private List<Bullet> _activePlayersBullets = new List<Bullet>();
    private List<Bullet> _activeEnemyBullets = new List<Bullet>();
    private List<Bullet> _freePlayersBullets = new List<Bullet>();
    private List<Bullet> _freeEnemyBullets = new List<Bullet>();

    [Inject]
    private void Construct(GameSettings gameSettings, PrefabBulletsData prefabBulletsData, BulletData bulletData)
    {
        _gameSettings = gameSettings;
        _prefabBulletsData = prefabBulletsData;
        _bulletData = bulletData;
    }

    private void Update()
    {
        Shooting();
    }

    public void Shot(Vector3 shooterPosition, Vector3 target, Vector3 vector, string layer)
    {
        if (layer == "Player")
        {
            if (_freePlayersBullets.Count == 0)
            {
                var newBullet = Instantiate(_prefabBulletsData.PlayerBullet, shooterPosition, Quaternion.identity);
                var newBulletClass = new Bullet(_bulletData, target, newBullet);
                _activePlayersBullets.Add(newBulletClass);
                newBulletClass.Direction = vector;
            }
            else
            {
                var newBulletClass = _freePlayersBullets[0];
                _freePlayersBullets.Remove(newBulletClass);
                _activePlayersBullets.Add(newBulletClass);
                newBulletClass.BulletObj.transform.position = shooterPosition;
                newBulletClass.Activate(target);
                newBulletClass.Direction = vector;
            }
        }
        else
        {
            if (_freeEnemyBullets.Count == 0)
            {
                var newBullet = Instantiate(_prefabBulletsData.EnemyBullet, shooterPosition, Quaternion.identity);
                var newBulletClass = new Bullet(_bulletData, target, newBullet);
                _activeEnemyBullets.Add(newBulletClass);
                newBulletClass.Direction = vector;
            }
            else
            {
                var newBulletClass = _freeEnemyBullets[0];
                _freeEnemyBullets.Remove(newBulletClass);
                _activeEnemyBullets.Add(newBulletClass);
                newBulletClass.BulletObj.transform.position = shooterPosition;
                newBulletClass.Activate(target);
                newBulletClass.Direction = vector;
            }
        }
    }

    private void Shooting()
    {
        if (_activePlayersBullets.Count > 0)
        {

            for (int i = 0; i < _activePlayersBullets.Count; i++)
            {
                var bullet = _activePlayersBullets[i];
                var layerMask = ~0 << 7;

                if (Physics.Raycast(bullet.BulletObj.transform.position, bullet.Direction, out var hit, 
                    bullet.Speed * Time.deltaTime, layerMask))
                {
                    if (hit.collider.TryGetComponent(out IDestroyable destroyable))
                    {
                        destroyable.ApplyDamage(bullet.Damage);
                        bullet.DeActivate();
                        destroyable.ApplyDamage(bullet.Damage);
                        _freePlayersBullets.Add(bullet);
                        _activePlayersBullets.Remove(bullet);
                    }
                }
                else
                {
                    if (Vector3.Distance(bullet.BulletObj.transform.position, bullet.Target) < 50)
                    {
                        bullet.BulletObj.transform.position += bullet.Direction * bullet.Speed * Time.deltaTime;
                    }
                    else
                    {
                        bullet.DeActivate();
                        _freePlayersBullets.Add(bullet);
                        _activePlayersBullets.Remove(bullet);
                    }
                }
            }
        }

        if (_activeEnemyBullets.Count > 0)
        {

            for (int i = 0; i < _activeEnemyBullets.Count; i++)
            {
                var bullet = _activeEnemyBullets[i];
                var layerMask = ~0 << 8;

                if (Physics.Raycast(bullet.BulletObj.transform.position, bullet.Direction, out var hit,
                    bullet.Speed * Time.deltaTime, layerMask))
                {
                    if (hit.collider.TryGetComponent(out IDestroyable destroyable))
                    {
                        destroyable.ApplyDamage(bullet.Damage);
                        bullet.DeActivate();
                        destroyable.ApplyDamage(bullet.Damage);
                        _freeEnemyBullets.Add(bullet);
                        _activeEnemyBullets.Remove(bullet);
                    }
                }
                else
                {
                    if (Vector3.Distance(bullet.BulletObj.transform.position, bullet.Target) < 50)
                    {
                        bullet.BulletObj.transform.position += bullet.Direction * bullet.Speed * Time.deltaTime;
                    }
                    else
                    {
                        bullet.DeActivate();
                        _freeEnemyBullets.Add(bullet);
                        _activeEnemyBullets.Remove(bullet);
                    }
                }
            }
        }
    }
}
