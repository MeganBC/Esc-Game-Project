using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxController : MonoBehaviour
{
    Transform cam;
    Vector3 camStarPosition;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backspeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;
    
    void Start()
    {
        cam = Camera.main.transform;
        camStarPosition = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backspeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i =0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculator(backCount);
    }
    
    void BackSpeedCalculator (int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;

            }
        }
        for (int i = 0; i < backCount; i++)
        {
            backspeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;

        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStarPosition.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backspeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }

}
