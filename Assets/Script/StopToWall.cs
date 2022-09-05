using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopToWall : MonoBehaviour
{
    PlayerMovement playermovement = new PlayerMovement();

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Debug.Log("벽 o");
            playermovement.walkSpeed = 0;
            playermovement.runSpeed = 0;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Debug.Log("벽 x");   
            playermovement.walkSpeed = 15f;
            playermovement.runSpeed = 30f;
        }
    }
}
