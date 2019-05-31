using System;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    #region Variables

    // Components
    private Rigidbody2D _rigidbody2D;
    private EnemyDisplay _enemyDisplay;

    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyDisplay = GetComponent<EnemyDisplay>();

        Assert.IsNotNull(_rigidbody2D);
        Assert.IsNotNull(_enemyDisplay);
    }

    private void Start()
    {
        SetEnemyStartPosition();
        if (_rigidbody2D)
        {
            SetEnemyStartMovement();
        }
    }

    private void SetEnemyStartPosition()
    {
        float rightScreenLimit = ScreenBoundaries.ScreenBounds.x - ScreenBoundaries.ScreenBoundsOffset.x;
        float leftScreenLimit = -rightScreenLimit;

        float topScreenLimit = ScreenBoundaries.ScreenBounds.y - ScreenBoundaries.ScreenBoundsOffset.y;
        float bottomScreenLimit = -topScreenLimit;

        Vector2 newPosition = new Vector2(
            Random.Range(leftScreenLimit, rightScreenLimit),
            Random.Range(bottomScreenLimit, topScreenLimit));
        transform.position = newPosition;

    }

    private void SetEnemyStartMovement()
    {
        if (_rigidbody2D)
        {
            Vector2 startImpulse = new Vector2(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)).normalized;
            if (_enemyDisplay)
            {
                startImpulse *= _enemyDisplay.enemy.speed;
            }
            _rigidbody2D.AddForce(startImpulse, ForceMode2D.Impulse);
            _rigidbody2D.AddTorque(Random.Range(0.1f, 1f), ForceMode2D.Impulse);
        }
    }

    private void UpdateCanvasPosition()
    {
        _enemyDisplay.panel.transform.position = transform.position;
    }
    
    private void LateUpdate()
    {
        if (_enemyDisplay)
        {
            UpdateCanvasPosition();
        }
    }
}