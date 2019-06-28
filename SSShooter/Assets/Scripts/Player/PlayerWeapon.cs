using UnityEngine;
using UnityEngine.Assertions;

public class PlayerWeapon : MonoBehaviour
{
    #region Variables

    // Components
    public GameObject bullet;
    [Range(1f,20f)]
    public float bulletSpeed;
    public Transform weaponPosition;

    #endregion

    private void Awake()
    {
        Assert.IsNotNull(weaponPosition, "WeaponPosition Transform not found");
    }

    public void Shoot(Transform target)
    {
        EnemyDisplay enemy = target.GetComponent<EnemyDisplay>();

        Vector3 position = weaponPosition ? weaponPosition.position : transform.position;
        GameObject newBullet = Instantiate(bullet, position, transform.rotation);
        BulletController bulletController = newBullet.GetComponent<BulletController>();
        if (!bulletController)
        {
            Debug.Log("The Bullet Gameobject must have a BulletController Script attached to it");
            return;
        }

        if (enemy.Word == "")
            bulletController.lastBullet = true;
        bulletController.ShootAtTarget(target, bulletSpeed);
    }
}
