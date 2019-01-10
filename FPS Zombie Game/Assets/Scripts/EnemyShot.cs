using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public float health;
    public GameObject zombieEnemy;

    public void GotShot(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
        
    }

    IEnumerator Die()
    {
        this.GetComponent<Zombie>().enabled = false;
        zombieEnemy.GetComponent<Animation>().Play("Dying");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }



}
