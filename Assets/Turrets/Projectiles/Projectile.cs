using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float Speed;

    [SerializeField]
    int maxRange;

    [SerializeField]
    bool homing;

    [SerializeField]
    bool expireOnImpact;

    public GameObject target;
    public float travelledDistance = 0;
    public float Damage;
    public string Type;

    //[SerializeField]
    public string impactProjectile;
    ProjectilePooler Pooler;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up;

        GameObject ProjectilePool = GameObject.Find("ProjectilePooler");
        Pooler = ProjectilePool.GetComponent<ProjectilePooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(homing && target != null) {
            rb.velocity = target.transform.position - transform.position;

            Vector3 pos2 = target.transform.position - transform.position;
            pos2.z = 0;
            rb.velocity = pos2;
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, pos2));
        }

        rb.velocity = rb.velocity.normalized * Speed;
        travelledDistance += Speed * Time.deltaTime;
        if (travelledDistance >= maxRange) { 
            gameObject.SetActive(false);
            travelledDistance = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        GameObject bullet = Pooler.GetProjectile(impactProjectile);
        if (bullet == null)
        {
            collision.gameObject.SendMessage("DealDamage", new DamageMessage { type = Type, damage = Damage }, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Projectile>().Damage = Damage;
            bullet.GetComponent<Projectile>().Type = Type;
            bullet.GetComponent<Projectile>().target = target;
        }
        travelledDistance = 0;
        gameObject.SetActive(!expireOnImpact);
    }
}
