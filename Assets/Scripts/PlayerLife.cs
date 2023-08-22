using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMoverment playerMove;
    
    //public GameObject scene;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoverment>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.GetComponent<BoxCollider2D>().enabled = false;
        playerMove.isDead = true;
        anim.SetTrigger("death");
        //StartCoroutine(Die2());
    }

    private void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        anim.SetTrigger("Start");
    }

    /*IEnumerator Die2()
    {
        yield return new WaitForSeconds(1);
        scene.anim.Settrigger("Start");
        yield return new WaitForSeconds(0.1f);
    }*/
}
