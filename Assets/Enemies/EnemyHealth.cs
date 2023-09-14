using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class EnemyHealth: MonoBehaviour
{
public static Action<Enemy> OnEnemykilled; 
public static Action<Enemy> OnEnemyHit;

[SerializeField] private GameObject healthBarPrefab;
 [SerializeField] private Transform barPosition; 
 [SerializeField] private float initialHealth = 10f;
[SerializeField] private float maxHealth = 10f;

public float CurrentHealth { get; set; }

private Image healthBar; 
private Enemy enemy;

private void Start(){

    CreateHealthBar();
    CurrentHealth = initialHealth;
    enemy = GetComponent<Enemy>();
}

private void Update(){
    //DealDamage(0.01f);
    if (Input.GetKeyDown(KeyCode.I)){
    Debug.Log("Enemy took damage");
    DealDamage(5f);
   
    }
    healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);

}

private void CreateHealthBar(){
    GameObject newbar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
    newbar.transform.SetParent(transform);
    EnemyHealthContainer container = newbar.GetComponent<EnemyHealthContainer>();
    healthBar = container.FillAmountImage;
}

public void DealDamage(float damage){
    Debug.Log("Enemy took damage");
    CurrentHealth -= damage;
    if (CurrentHealth <= 0){
        CurrentHealth = 0;
        Die();
        
    }
    else{
        OnEnemyHit?.Invoke(enemy);
    }
}

public void ResetHealth(){
    CurrentHealth = maxHealth;
}

public void Die(){
    OnEnemykilled?.Invoke(enemy);
    Destroy(gameObject);

}
}

