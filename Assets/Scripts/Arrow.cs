using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject arrowDestory;
    //private Vector3 targetPosition = new Vector3(5, 0, 0);




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.001f);
        //gameObject.transform.Translate(Vector3.right * 0.5f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(ArrowSpawner.ars.x_force, ArrowSpawner.ars.y_force));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(arrowDestory, transform.position, Quaternion.identity);
    }

}
