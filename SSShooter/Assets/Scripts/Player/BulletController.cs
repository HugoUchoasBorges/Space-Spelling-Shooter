using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class BulletController : MonoBehaviour
{
    #region Variables

    // Control the bullet Destroy
    private bool _toBeDestroyed;
    
    // Components
    private Rigidbody2D _rigidbody2D;
    public bool lastBullet;
    
    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Assert.IsNotNull(_rigidbody2D, "The Bullet must have a RigidBody2D Component");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        _toBeDestroyed = true;
        EnemyDisplay enemy = other.gameObject.GetComponent<EnemyDisplay>();

        if (lastBullet && enemy.Word == "")
            enemy.enemyManager.DestroyEnemy(other.gameObject);
    }

    public IEnumerator ShootAtTarget(Transform target, float bulletSpeed)
    {
        if (!_rigidbody2D)
            yield break;

        float enemyRadius = target.GetComponent<CircleCollider2D>().radius;

        Vector2 bulletPosition = transform.position;
        Vector2 targetVector = (Vector2)target.position - bulletPosition;
        
        while (!_toBeDestroyed && targetVector.magnitude > enemyRadius)
        {
            bulletPosition = transform.position;
            targetVector = (Vector2)target.position - bulletPosition;
            _rigidbody2D.MovePosition(
                bulletPosition + bulletSpeed * Time.deltaTime * targetVector.normalized);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
