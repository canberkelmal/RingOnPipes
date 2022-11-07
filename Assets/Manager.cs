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
    GameObject tempCube;
    Rigidbody rb;
    Vector3 ForceVector;
    GameObject tempStg;
    GameObject currentStg;
    GameObject ttempStg;
    
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
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*100f, DimensionAnim * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision other){
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag=="Cube"){
            for(int i=0; i < other.gameObject.transform.parent.childCount; i++){
                tempCube= other.gameObject.transform.parent.GetChild(i).gameObject;
                tempCube.tag="ColledCube";
                tempCube.GetComponent<Rigidbody>().isKinematic=false;
                if(tempCube.transform.position.y>0){
                    tempCube.GetComponent<Rigidbody>().velocity=new Vector3(tempCube.transform.position.x-other.gameObject.transform.parent.parent.GetChild(0).transform.position.x,
                                                                            tempCube.transform.position.y-other.gameObject.transform.parent.parent.GetChild(0).transform.position.y,
                                                                            tempCube.GetComponent<Rigidbody>().velocity.z) *tempCube.transform.position.y*4;
            }
            }

            ScoreText.text=(int.Parse(ScoreText.text)+24).ToString();
        }

        if(other.gameObject.tag=="Cylinder"){
            ttempStg=tempStg;
            tempStg=currentStg;
            currentStg=other.transform.parent.parent.gameObject;
            Destroy(ttempStg);
        }

        
    }

    
}
