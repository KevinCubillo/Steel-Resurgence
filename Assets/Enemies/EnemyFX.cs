using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class EnemyFX : MonoBehaviour{/*
    [SerializeField] private Transform textDamageSpawnPosition;
    Enemy enemy;


    void Start()
    {
        enemy = GetComponent<Enemy>();
    
        
    }

    public void EnemyHit(Enemy _enemy, float damage){

        print("Enemy hitting");
        if (enemy == _enemy){

            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);
          
        }
       
    }

    private void OnEnable(){
        Projectile.onEnemyHit += EnemyHit;
    }

    private void OnDisable(){
        Projectile.onEnemyHit -= EnemyHit;
    }*/
}


