using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Transform myTransform;
    private Vector3 startingPosition;
    void Start()
    {
        myTransform = transform;
        startingPosition = myTransform.position;
    }

    void Update()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, new Vector3(player.position.x, player.position.y + 4, player.position.z - 6), .1f); 
    }
}
