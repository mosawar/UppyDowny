using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth;
    public int playerCurrentHealth;

    public Animator anim;
    public Timer time;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        anim.SetTrigger("playerHurt");
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector2(-5.232f, 3.12f);
            playerCurrentHealth = playerMaxHealth;
            gameObject.SetActive(true);
            time.elapsedTime = 0f;
        }
    }
}
