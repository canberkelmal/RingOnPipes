using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallower : MonoBehaviour
{
    public GameObject player;
    public float offsetZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position=new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, player.transform.position.z-offsetZ);
    }
}
