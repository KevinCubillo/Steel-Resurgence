using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    int lives = 1;

    [SerializeField]
    List<int> PoderesDesbloqueados;

    [SerializeField]
    int VidasIniciales;

    private void Awake()
    {
        lives = VidasIniciales;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
