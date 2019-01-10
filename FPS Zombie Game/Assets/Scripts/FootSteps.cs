using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    CharacterController charCont;
    AudioSource footsteps;
    // Start is called before the first frame update
    void Start()
    {
        charCont = GetComponent<CharacterController>();
        footsteps = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(charCont.isGrounded == true && charCont.velocity.magnitude > 2f && footsteps.isPlaying == false)
        {
            footsteps.volume = Random.Range(0.8f, 1);

            footsteps.Play();
        }
        
    }
}
