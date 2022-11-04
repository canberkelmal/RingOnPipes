using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float ForwardSpeed=10;
    public float DimensionAnim=80f;
    public bool isTouch=false;
    public Text ScoreText;
    Rigidbody rb;
    Vector3 ForceVector;
    
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody>();
        ForceVector=new Vector3(0,0,1);
    }

    void FixedUpdate()
    {
        rb.velocity= ForceVector * ForwardSpeed;

        if(Input.GetMouseButton(0) && !isTouch){
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.zero, DimensionAnim * Time.deltaTime);
        }

        if(!Input.GetMouseButton(0)){
            isTouch= false;
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*91.2f, DimensionAnim * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.tag);
        isTouch= other.gameObject.tag=="Cylinder" && !isTouch ? true : false;

        if(other.gameObject.tag=="Cube"){
            ScoreText.text=(int.Parse(ScoreText.text)+1).ToString();
        }
    }

    
}
