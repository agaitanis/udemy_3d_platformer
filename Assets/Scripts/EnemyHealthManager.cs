using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public int deathSound;
    public GameObject deathEffect, itemToDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0f) {
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);
            PlayerController.instance.Bounce();
            Instantiate(deathEffect, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
            Instantiate(itemToDrop, transform.position + new Vector3(0f, .5f, 0f), transform.rotation);
        }
    }
}
