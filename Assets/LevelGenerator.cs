using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform block;
    private float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            timer = 0;
            int r = Random.Range(0, 3);
            if (r == 0)
                Instantiate(block, new Vector3(0, .2f, transform.position.z + 20), Quaternion.identity);
            else if (r == 1)
                Instantiate(block, new Vector3(1.5f, .2f, transform.position.z + 20), Quaternion.identity);
            else if (r == 2)
                Instantiate(block, new Vector3(-1.5f, .2f, transform.position.z + 20), Quaternion.identity);
        }
    }
}
