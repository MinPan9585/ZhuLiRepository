using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    //public Sprite newSprite;

    public GameObject door;
    public float xxx;
    public float yyy;

    //private Vector3 targetPosition = new Vector3(xxx, yyy, 0);



/*    private void OnDestroy()
    {
        StartCoroutine(Move());
        gameObject.transform.position = Vector3.Lerp(transform.position,targetPosition, 0.001f);
        //door.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_force, y_force));
    }

    IEnumerator Move()
    {
        while (true)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.001f);
        }
    }*/













    // Update is called once per frame
    /*void Update()
    {
        
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(x_force, y_force));
            yield return new WaitForSeconds(3);
        }
    }*/
}
