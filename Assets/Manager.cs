using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        rb.velocity= ForceVector * ForwardSpeed;

        if(Input.GetMouseButton(0) && !isTouch){
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*58.5f, DimensionAnim * Time.deltaTime);
        }

        if(!Input.GetMouseButton(0)){
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*91.2f, DimensionAnim * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other){
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag=="Cube"){
            for(int i=0; i < other.gameObject.transform.parent.childCount; i++){
                other.gameObject.transform.parent.GetChild(i).tag="ColledCube";
                other.gameObject.transform.parent.GetChild(i).GetComponent<Rigidbody>().isKinematic=false;
            }

            ScoreText.text=(int.Parse(ScoreText.text)+24).ToString();
        }

        
    }

    
}
