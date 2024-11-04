using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    public Animator anim;
    private Transform currentPoint;
    public float speed;

    public int maxHealth;
    int currentHealth;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= 0)
        {
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform) {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform) 
            {
                flip();
                audioManager.PlaySFX(audioManager.crow);
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
            {
                flip();
                //audioManager.PlaySFX(audioManager.crow);
                currentPoint = pointB.transform;
            }    
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            StartCoroutine("WaitTime");
        }
    }

    IEnumerator WaitTime()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Die();
    }

    void Die()
    {
        audioManager.PlaySFX(audioManager.crowDeath);
        Debug.Log("enemy Died!");
        gameObject.SetActive(false);
    }

    

}
