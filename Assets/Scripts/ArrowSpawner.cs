using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;

    public static ArrowSpawner ars;
    private void Awake()
    {
        ars = this;
    }

    public float x_force;
    public float y_force;
    void Start()
    {
        StartCoroutine(SpawnArrow());
        
    }

    IEnumerator SpawnArrow()
    {
        while (true)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
