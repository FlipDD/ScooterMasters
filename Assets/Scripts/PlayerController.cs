using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private enum State { Driving, Stunned, Dying }
    private State state;

    private Transform myTransform;
    private Rigidbody rb;

    //0 left, 1 mid, 2 right
    private int lane = 1;
    private float switchTimer;
    private bool switching;

    private float dyingTimer;

    void Start()
    {
        myTransform = transform;
        state = State.Driving;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Driving:
                Drive();
            break;

            case State.Stunned:
                Stun();
            break;

            case State.Dying:
                Die();
            break;

            default:
                //Do default stuff
            break;
        }
    }

    private void Die()
    {
        dyingTimer += Time.deltaTime;
        if (dyingTimer > 3.5f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Drive()
    {
        myTransform.position += new Vector3(0, 0, 60 * Time.deltaTime * (Time.timeSinceLevelLoad / 150)); 

        if (!switching)
        {
            if (Input.GetKeyDown(KeyCode.D) && lane != 2)
            {
                lane++;
                StartCoroutine(SwitchLanes(1.5f));
            }
            else if (Input.GetKeyDown(KeyCode.A) && lane != 0)
            {
                lane--;
                StartCoroutine(SwitchLanes(-1.5f));
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Jump());
            }
        }
    }

    IEnumerator SwitchLanes(float amount)
    {
        switching = true;
        float t = .3f;
        Vector3 pos = myTransform.position;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float x = Mathf.Lerp(myTransform.position.x, pos.x + amount, .25f);
            myTransform.position = new Vector3(x, myTransform.position.y, myTransform.position.z);
            yield return null;
        }
        switching = false;
    }

    IEnumerator Jump()
    {
        switching = true;
        float t = 1f;
        Vector3 pos = myTransform.position;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            if (t > .6f)
            {
                float y = Mathf.Lerp(myTransform.position.y, pos.y + 2, .15f);
                myTransform.position = new Vector3(myTransform.position.x, y, myTransform.position.z);
            }
            else if (t < .3f)
            {
                float y = Mathf.Lerp(myTransform.position.y, pos.y, .2f);
                myTransform.position = new Vector3(myTransform.position.x, y, myTransform.position.z);
            }
            yield return null;
        }

        myTransform.position = new Vector3(myTransform.position.x, 0.02f, myTransform.position.z);
        switching = false;
    }


    void Stun()
    {

    }

    void OnCollisionEnter(Collision hit)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddTorque(myTransform.right * 100);
        state = State.Dying;
    }
}
