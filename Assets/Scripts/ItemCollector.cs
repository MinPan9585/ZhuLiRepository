using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry")) 
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject, 5f);
            cherries++;
            Debug.Log("Cherries" + cherries);
        }
    }
}
