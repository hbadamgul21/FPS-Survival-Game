using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject player;
    public float targetDistance;
    public float rangeAllowed;
    public GameObject EnemyZ;
    private float enemySpeed;
    public int attackTrigger;
    public RaycastHit Shot;

    public int isAttacking, painSound;
    public GameObject HurtFlash;
    public AudioSource Hurt1, Hurt2, Hurt3;
    public Health Test;
    Transform enem;
    public Canvas hurtCanvas;
    AudioSource zombieSound;

    void Start()
    {
        zombieSound = GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        hurtCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        HurtFlash = hurtCanvas.transform.Find("HurtUI").gameObject;

        Hurt1 = GameObject.Find("Hurt 1").GetComponent<AudioSource>();
        Hurt2 = GameObject.Find("Hurt 2").GetComponent<AudioSource>();
        Hurt3 = GameObject.Find("Hurt 3").GetComponent<AudioSource>();


        Test = GameObject.Find("Health").GetComponent<Health>();
        enem = GetComponent<Transform>();
    }

    void Update()
    {
        
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.forward, out Shot))
        {
            targetDistance = Shot.distance;
            if(targetDistance < rangeAllowed)
            {
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                enemySpeed = 0.04f;
                if(attackTrigger == 0)
                {
                    EnemyZ.GetComponent<Animation>().Play("Walking");
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);
                }
            } else
            {
                enemySpeed = 0;

                EnemyZ.GetComponent<Animation>().Play("Idle");
            }
        }

        if(attackTrigger == 1)
        {
            if (isAttacking == 0)
                StartCoroutine(EnemyDamage());
            enemySpeed = 0;
            EnemyZ.GetComponent<Animation>().Play("Attacking");
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "EnemyTrigger")
        {
            zombieSound.Play();
            attackTrigger = 1;
        }
       
    }

    void OnTriggerExit()
    {
        attackTrigger = 0;
        
    }

    IEnumerator EnemyDamage()
    {
        isAttacking = 1;
        painSound = Random.Range(1, 4);
        yield return new WaitForSeconds(0.9f);
        HurtFlash.SetActive(true);
        Test.playerHealth -= 10;
        if(painSound == 1)
        {
            Hurt1.Play();
        }
        if (painSound == 2)
        {
            Hurt2.Play();
        }
        if (painSound == 3)
        {
            Hurt3.Play();
        }
        yield return new WaitForSeconds(0.05f);
        HurtFlash.SetActive(false);
        yield return new WaitForSeconds(1);
        isAttacking = 0;
    }

}
