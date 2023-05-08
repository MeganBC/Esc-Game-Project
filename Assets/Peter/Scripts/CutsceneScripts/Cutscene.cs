using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            SceneManager.LoadScene("Cutscene1");
    }
}
