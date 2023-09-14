using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private SpriteRenderer spriteRenderer;
  
    private Vector3 lastPointPosition;
    private Vector3 CurrentPointPosition;


    [SerializeField]
    float moveSpeed;
    private int currentWaypointIndex;
  

    public event Action<Enemy> OnEndReached;

    [SerializeField] private Path Path;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>(); 
        CurrentPointPosition = Path.Positions[0];
        Debug.Log("CurrentPointPosition" + CurrentPointPosition);
        currentWaypointIndex = 0;
      

    }


    private void Update()
    {
        Move();
        //Rotate();
        if (CurrentPointPositionReached()){
            Debug.Log("CurrentPointPositionReached");
         
            UpdateCurrentPointIndex(); 
            CurrentPointPosition = Path.Positions[currentWaypointIndex];
        }
    }

     private void Move()
     {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
        

     }  

    private void Rotate()
    {
        if (CurrentPointPosition.x > lastPointPosition.x)
        {
            spriteRenderer.flipX = false; 
        }
        else {
            spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f){
            lastPointPosition = transform.position;
            return true;
        }
        return false;
    }
    
    private void UpdateCurrentPointIndex()
    {
       int lastWaypointIndex = Path.Positions.Count - 1;   
     
         if (currentWaypointIndex < lastWaypointIndex)
         {
              currentWaypointIndex++;
         }
         else
         {
             EndPointReached();
         }
    }

    private void EndPointReached()
    {
       OnEndReached?.Invoke(this);
       enemyHealth.ResetHealth();
       EnemyPooler.ReturnToPool(gameObject);
    }
    

}