using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public GameObject bloodyScreen;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Player took {amount} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
        }
    }

    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }

        yield return new WaitForSeconds(4f);

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Add game over logic here
    }
}
