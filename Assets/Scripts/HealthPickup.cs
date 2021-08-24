using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;
    public GameObject heartPickupEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Instantiate(heartPickupEffect, transform.position, transform.rotation);

            if (isFullHeal) {
                HealthManager.instance.ResetHealth();
            } else {
                HealthManager.instance.AddHealth(healAmount);
            }

            Destroy(gameObject);
        }
    }
}
