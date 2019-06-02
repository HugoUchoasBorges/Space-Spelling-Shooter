using UnityEngine;
using UnityEngine.Assertions;

public class PlayerCollision : MonoBehaviour
{
    #region Variables

    // Components
    private Rigidbody2D _rigidbody2D;
    private Player _player;
    
    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        
        Assert.IsNotNull(_rigidbody2D);
        Assert.IsNotNull(_player);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyDisplay enemy = other.gameObject.GetComponent<EnemyDisplay>();

        if (!enemy)
            return;

        if (_player)
        {
            _player.Death();
        }

        EnemyManager enemyManager = enemy.EnemyManager;
        if (!enemyManager)
            return;

        enemyManager.DestroyEnemy(enemy.gameObject);
    }
}
