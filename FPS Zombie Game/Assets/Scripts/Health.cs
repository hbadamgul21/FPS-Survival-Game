using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int playerHealth;

    public GameObject healthDisplay;

    void Update()
    {
        healthDisplay.GetComponent<Text>().text = "+" + playerHealth;

        if (playerHealth == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(2);
            
        } 
    }
}
