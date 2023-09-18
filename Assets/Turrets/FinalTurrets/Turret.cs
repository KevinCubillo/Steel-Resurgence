using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Turret : MonoBehaviour
{
    float projectileTimer = 0;
    float maxTimer;

    [SerializeField]
    public float Range;

    [SerializeField]
    public float FireRate;

    [SerializeField]
    public float DPS;

    [SerializeField]
    public string ProjectileName;

    [SerializeField]
    public string DamageType;

    CircleCollider2D area;
    List<GameObject> EnemiesInRange;
    GameObject ProjectilePool;
    ProjectilePooler Pooler;

    float Damage;

    private void Awake()
    {
        area = gameObject.GetComponent<CircleCollider2D>();
        EnemiesInRange = new List<GameObject>();

        area.radius = Range / 2f;
        maxTimer = 1.0f / FireRate;

        ProjectilePool = GameObject.Find("ProjectilePooler");
        Pooler = ProjectilePool.GetComponent<ProjectilePooler>();

        Damage = DPS / FireRate;

        Damage *= GlobalValues.BaseTurretDamageMultiplier;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Look at enemy
        //First enemy that enters range is first to shoot at
        float angle = 0;
        if (EnemiesInRange.Count > 0)
        {
            Vector3 pos2 = EnemiesInRange[0].transform.position - transform.position;
            pos2.z = 0;
            angle = Vector2.SignedAngle(Vector2.right, pos2);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        projectileTimer += projectileTimer < maxTimer? Time.deltaTime : 0;
        if (projectileTimer >= maxTimer && EnemiesInRange.Count > 0)
        {
            projectileTimer = projectileTimer - maxTimer;
            //Shoot projectile
            GameObject bullet = Pooler.GetProjectile(ProjectileName);
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<Rigidbody2D>().velocity = EnemiesInRange[0].transform.position - transform.position;
                bullet.GetComponent<Projectile>().Damage = Damage * GlobalValues.TurretDamageMultiplier;
                bullet.GetComponent<Projectile>().Type = DamageType;
                bullet.GetComponent<Projectile>().target = EnemiesInRange[0];
            }
        }
    }

        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemiesInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemiesInRange.Remove(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        if(area == null)
            area = gameObject.GetComponent<CircleCollider2D>();
        area.radius = Range/2f;
    }
}
