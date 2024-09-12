using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    public void PickupFirstAidKit() {
        // Add code to heal player
        print("First aid kit picked up, healing player for 25 health");
        // Destroy the kit
        Destroy(gameObject);
    }
}
