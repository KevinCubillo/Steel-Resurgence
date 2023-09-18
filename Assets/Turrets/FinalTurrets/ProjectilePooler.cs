using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    [SerializeField]
    int KeepInStock;

    [SerializeField]
    List<InspectorProjectileType> ProjectileTypes;

    Dictionary<string,GameObject> ProjectileTypesDict;

    Dictionary<string, List<GameObject> > Pools;

    // Start is called before the first frame update
    void Start()
    {
        ProjectileTypesDict = new Dictionary<string, GameObject>();
        Pools = new Dictionary<string, List<GameObject> >();

        //Store Pool data on a dictionary, just in case
        for (int i = 0; i < ProjectileTypes.Count; i++) {
            ProjectileTypesDict.Add(ProjectileTypes[i].name, ProjectileTypes[i].projectile);
            Pools.Add(ProjectileTypes[i].name, new List<GameObject>());
        }


        //Create Pools
        GameObject o;
        for (int i = 0; i < KeepInStock; i++)
        {
            for (int j = 0; j < ProjectileTypes.Count; j++)
            {
                o = Instantiate(ProjectileTypes[j].projectile, transform);
                o.SetActive(false);
                Pools.GetValueOrDefault(ProjectileTypes[j].name).Add(o);
            }
        }
    }

    public GameObject GetProjectile(string name)
    {
        List<GameObject> projectiles = Pools.GetValueOrDefault(name, null);
        if (projectiles == null)
            return null;

        for (int i = 0; i < KeepInStock; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                projectiles[i].SetActive(true);
                return projectiles[i];
            }
        }
        return null;
    }
}

[System.Serializable]
struct InspectorProjectileType {
    public string name;
    public GameObject projectile;
}