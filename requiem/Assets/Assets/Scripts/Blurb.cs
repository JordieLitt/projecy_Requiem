using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blurb : MonoBehaviour
{
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    }
}
