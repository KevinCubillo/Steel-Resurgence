using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{/*
    public GameObject deathParticles;
    private Animator animator;
    private Enemy enemy;
    private EnemyHealth enemyHealth;
 
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyHealth = GetComponent<EnemyHealth>();  
    }


    private float GetCurrentAnimationLength(){

       float animationLength = animatior.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
       

    }

    private IEnumerator PlayDeath(){

        enemy.StopMovement();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        enemyHealth.ResumeMovement();
        enemyHealth.ResetHealth();
        ObjectPooler.returnToPool(gameObject);
       

    }

    private void EnemyDeath(Enemy _enemy){

      if (enemy == _enemy){

        StartCoroutine(PlayDeath());
      }

    }

    private void onEnable(){

        EnemyHealth.onDeath += PlayDeathAnimation;
    }

    private void onDisable(){

        EnemyHealth.onDeath -= PlayDeathAnimation;
    }


    */
    
}
