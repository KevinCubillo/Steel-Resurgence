using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target; //punto a donde se dirige el enemigo
    private int pathIndex = 0; //indice del camino que sigue el enemigo

    private void Start()
    {
        //target = LevelManager.main.path[pathIndex];
    }
    private void Update()
    {
        /*if (Vector2.Distance(target.position, tranform.position) <= 0.1f)
        {
            pathIndex++;
            target = LevelManager.main.path[pathIndex];

            if (pathIndex == LevelManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }   
        }*/
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}

     
