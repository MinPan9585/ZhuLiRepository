using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;



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
