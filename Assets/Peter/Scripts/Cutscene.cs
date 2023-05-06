using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public Scene nextScene;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            SceneManager.LoadScene(nextScene.name);
    }
}
