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
    [SerializeField] public float ScrapValue;
    [SerializeField] public string Weakness;

    public float CurrentHealth { get; set; }
    private float DamageMult = 1;

    private Image healthBar; 
    private Enemy enemy;
    GameObject ResourceController;

    private void Awake()
    {
        ResourceController = GameObject.Find("ResourceController");
    }

    private void Start(){

    CreateHealthBar();
    CurrentHealth = initialHealth;
    enemy = GetComponent<Enemy>();
}

    private void Update(){
        //DealDamage(0.01f);
        if (Input.GetKeyDown(KeyCode.I)){
        //Debug.Log("Enemy took damage");
        DealDamage(new DamageMessage {type = "Kinetic", damage = 5 });
   
        }
            healthBar.fillAmount = CurrentHealth / maxHealth;// Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);

    }

    private void CreateHealthBar(){
        GameObject newbar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newbar.transform.SetParent(transform);
        EnemyHealthContainer container = newbar.GetComponent<EnemyHealthContainer>();
        healthBar = container.FillAmountImage;
    }

    public void DealDamage(DamageMessage damageMessage){
        //Debug.Log("Enemy took damage");
        CurrentHealth -= damageMessage.damage * GlobalValues.EnemyReceivedDamageMultiplier * (damageMessage.type == Weakness? 2f : 1);
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

        ResourceMessage message = new ResourceMessage();
        message.name = "Scrap";
        message.value = (int)(ScrapValue * GlobalValues.ScrapMultiplier);
        ResourceController.SendMessage("giveResource", message);

        Destroy(gameObject);

    }

    private void multScrapValue(float mult) {
        ScrapValue *= mult;
    }

    private void multDamageReceived(float mult)
    {
        DamageMult *= mult;
    }
}

public struct DamageMessage {
    public float damage;
    public string type;
}