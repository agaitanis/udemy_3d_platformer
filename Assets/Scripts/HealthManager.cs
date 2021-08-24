using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int currentHealth, maxHealth;
    public float invincibleLength;
    public float invincibleCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0) {
            invincibleCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++) {
                if (Mathf.Floor(invincibleCounter * 5f) % 2 == 0 && invincibleCounter > 0) {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                } else {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (invincibleCounter <= 0) {
            currentHealth--;

            if (currentHealth <= 0) {
                currentHealth = 0;
                GameManager.instance.Respawn();
            } else {
                PlayerController.instance.KnockBack();
                invincibleCounter = invincibleLength;
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
