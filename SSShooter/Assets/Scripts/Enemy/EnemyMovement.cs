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
    private ScreenBoundaries _screenBoundaries;

    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyDisplay = GetComponent<EnemyDisplay>();
        _screenBoundaries = GetComponent<ScreenBoundaries>();

        Assert.IsNotNull(_rigidbody2D);
        Assert.IsNotNull(_enemyDisplay);
        Assert.IsNotNull(_screenBoundaries);
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
        float rightScreenLimit = GlobalVariables.ScreenBounds.x;
        float leftScreenLimit = -rightScreenLimit;

        float topScreenLimit = GlobalVariables.ScreenBounds.y;
        float bottomScreenLimit = -topScreenLimit;

        Vector2 newPosition = Vector2.zero;
            
        if (_screenBoundaries)
        {
            int[] signals = {-1,1};
            int randomSignal = signals[Random.Range(0, 2)];

            if (Random.value < 0.5f)
            {
                newPosition = new Vector2(
                    randomSignal * (rightScreenLimit + 1.5f * _screenBoundaries.screenBoundsOffset.x),
                    Random.Range(bottomScreenLimit, topScreenLimit)
                );   
            }
            else
            {
                newPosition = new Vector2(
                    Random.Range(leftScreenLimit, rightScreenLimit),
                    randomSignal * (topScreenLimit + 1.5f * _screenBoundaries.screenBoundsOffset.y)
                ); 
            }
        }
        else
        {
            newPosition = new Vector2(
                Random.Range(leftScreenLimit, rightScreenLimit),
                Random.Range(bottomScreenLimit, topScreenLimit));
        }
        
        transform.position = newPosition;

    }

    private void SetEnemyStartMovement()
    {
        if (_rigidbody2D)
        {
            Vector3 centerRange = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            Vector2 startImpulse = (centerRange - transform.position).normalized;
            
            if (_enemyDisplay)
            {
                if (_enemyDisplay.enemy)
                    startImpulse *= _enemyDisplay.enemy.speed;
                else
                    startImpulse *= 0.5f;
            }
            
            _rigidbody2D.AddForce(startImpulse, ForceMode2D.Impulse);
            
            float maxInitialTorque = 0.01f;
            if (_enemyDisplay.enemy)
                maxInitialTorque = _enemyDisplay.enemy.maxInitialTorque;
            _rigidbody2D.AddTorque(
                Random.Range(-maxInitialTorque, maxInitialTorque),
                ForceMode2D.Impulse);
        }
    }

    private void UpdateCanvasPosition()
    {
        if (!_enemyDisplay || !_enemyDisplay.panel)
            return;
        
        _enemyDisplay.panel.transform.position = transform.position;
    }
    
    private void LateUpdate()
    {
        UpdateCanvasPosition();
    }
}