using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class BulletController : MonoBehaviour
{
    #region Variables
    
    // Components
    private Rigidbody2D _rigidbody2D;
    public bool lastBullet;
    private Transform _target;
    
    [Header("VFX Prefabs")] 
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    
    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Assert.IsNotNull(_rigidbody2D, "The Bullet must have a RigidBody2D Component");
    }

    private void Start()
    {
        if (muzzlePrefab)
        {
            GameObject muzzleObj = Instantiate(muzzlePrefab, transform.position, transform.rotation);
            ParticleSystem muzzleParticle = muzzleObj.GetComponent<ParticleSystem>();
            if (muzzleParticle != null)
            {
                Destroy(muzzleObj, muzzleParticle.main.duration);
            }
            else
            {
                ParticleSystem muzzleParticleChild = muzzleObj.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleObj, muzzleParticleChild.main.duration);

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
            
        if (hitPrefab)
        {
            ContactPoint2D contactPoint2D = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint2D.normal);
            Vector2 pos = contactPoint2D.point;
            
            GameObject hitVfx = Instantiate(hitPrefab, pos, rot);
            hitVfx.transform.forward = transform.forward;
            ParticleSystem hitParticle = hitVfx.GetComponent<ParticleSystem>();
            if (hitParticle != null)
            {
                Destroy(hitVfx, hitParticle.main.duration);
            }
            else
            {
                ParticleSystem hitParticleChild = hitVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVfx, hitParticleChild.main.duration);

            }
        }

        EnemyDisplay enemy = _target.gameObject.GetComponent<EnemyDisplay>();

        if (lastBullet)
        {
            enemy.enemyManager.DestroyEnemy(other.gameObject);
        }
        Destroy(gameObject);
    }

    public void ShootAtTarget(Transform target, float bulletSpeed)
    {
        StartCoroutine(ShootAtTargetCoroutine(target, bulletSpeed));
    }

    private IEnumerator ShootAtTargetCoroutine(Transform target, float bulletSpeed)
    {
        if (!_rigidbody2D)
            yield break;

        _target = target;

        float enemyRadius = target.GetComponent<CircleCollider2D>().radius;

        Vector2 bulletPosition = transform.position;
        Vector2 targetVector = (Vector2)target.position - bulletPosition;
        
        while (targetVector.magnitude > enemyRadius)
        {
            try
            {
                bulletPosition = transform.position;
                targetVector = (Vector2) target.position - bulletPosition;
                _rigidbody2D.MovePosition(
                    bulletPosition + bulletSpeed * Time.deltaTime * targetVector.normalized);
            }
            catch
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
