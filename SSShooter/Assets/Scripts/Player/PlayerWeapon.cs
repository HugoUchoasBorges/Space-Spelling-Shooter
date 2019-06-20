using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region Variables

    // Components
    public GameObject bullet;
    [Range(1f,20f)]
    public float bulletSpeed;

    #endregion
    
    public void Shoot(Transform target)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        BulletController bulletController = newBullet.GetComponent<BulletController>();
        if (!bulletController)
        {
            Debug.Log("The Bullet Gameobject must have a BulletController Script attached to it");
            return;
        }
        StartCoroutine(bulletController.ShootAtTarget(target, bulletSpeed));
    }
}
