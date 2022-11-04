using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float DimensionAnim=80f;
    public bool isTouch=false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && !isTouch){
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.zero, DimensionAnim * Time.deltaTime);
        }

        if(!Input.GetMouseButton(0)){
            isTouch= false;
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*100, DimensionAnim * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other){
        Debug.Log("col");
        isTouch= other.gameObject.tag=="Cylinder" && !isTouch ? true : false;
    }

    
}
