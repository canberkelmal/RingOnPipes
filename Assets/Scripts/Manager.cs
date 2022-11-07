using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public float ForwardSpeed=10;
    public float DimensionAnim=80f;
    public float DimAnimOffset=1f;
    public bool isTouch=false;
    public float Cmin=0.7f;
    public float Cmax = 1.14f;
    public float Rmin = 41;
    public float Rmax = 67;
    public float a,cons;
    public Text ScoreText;
    GameObject tempCube;
    Rigidbody rb;
    Vector3 ForceVector;
    Vector3 RingTargetVector;
    float StgCounter=8;
    public Material OtherCylColor;
    public Material ColledCube;
    public GameObject Floor;
    public GameObject DefStg;
    public GameObject tempStg;
    public GameObject currentStg;
    GameObject ttempStg;
    float RandomRadius;
    bool gameOver=false;
    
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody>();
        ForceVector=new Vector3(0,0,1);
        a=currentStg.transform.localScale.x;
        cons= ( ( (a - Cmin) / (Cmax - Cmin) ) * (Rmax - Rmin) ) + Rmin;
        RingTargetVector=new Vector3(cons, cons, gameObject.transform.localScale.z);
        ChangeCons();
        gameOver=false;
        Cmin=DefStg.transform.localScale.x;
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Replay();
        }

        //rb.velocity= !gameOver ? ForceVector * ForwardSpeed * gameObject.transform.localScale.x * 0.01f : Vector3.zero;
        rb.velocity= !gameOver ? ForceVector * ForwardSpeed : Vector3.zero;

        if(Input.GetMouseButton(0) && !isTouch && !gameOver){
            rb.velocity+= ForceVector / (gameObject.transform.localScale.x*0.005f);
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, RingTargetVector , DimensionAnim * Time.deltaTime);
        }

        if(!Input.GetMouseButton(0)){
            gameObject.transform.localScale=Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one*100f, DimensionAnim * Time.deltaTime);
        }
    }

    /* void OnCollisionEnter(Collision other){

        if(other.gameObject.tag=="Cube"){
            for(int i=0; i < other.gameObject.transform.parent.childCount; i++){
                tempCube= other.gameObject.transform.parent.GetChild(i).gameObject;
                tempCube.tag="ColledCube";
                tempCube.GetComponent<Rigidbody>().isKinematic=false;
                if(tempCube.transform.position.y>0){
                    tempCube.GetComponent<Rigidbody>().velocity=new Vector3(tempCube.transform.position.x-other.gameObject.transform.parent.parent.GetChild(0).transform.position.x,
                                                                            tempCube.transform.position.y-other.gameObject.transform.parent.parent.GetChild(0).transform.position.y,
                                                                            tempCube.GetComponent<Rigidbody>().velocity.z) *tempCube.transform.position.y*40;
                }
            }

            ScoreText.text=(int.Parse(ScoreText.text)+24).ToString();
        }

        if(other.gameObject.tag=="Cylinder"){
            //Debug.Log("CylinderCrushGameOver");
            //GameOver();
        }
    } */

    void OnTriggerEnter(Collider other){

        if(other.gameObject.tag=="Cube"){

            other.gameObject.transform.parent.GetChild(0).gameObject.tag="ColledCube";
            other.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Renderer>().material=ColledCube;
            other.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic=false;
            other.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity=new Vector3(1.8f * other.gameObject.transform.parent.GetChild(0).gameObject.transform.position.x, other.gameObject.transform.parent.GetChild(0).gameObject.transform.position.y,
                                                                        1) * 6;

            other.gameObject.transform.parent.GetChild(1).gameObject.tag="ColledCube";
            other.gameObject.transform.parent.GetChild(1).gameObject.GetComponent<Renderer>().material=ColledCube;
            other.gameObject.transform.parent.GetChild(1).gameObject.GetComponent<Rigidbody>().isKinematic=false;
            other.gameObject.transform.parent.GetChild(1).gameObject.GetComponent<Rigidbody>().velocity=new Vector3(1.8f * other.gameObject.transform.parent.GetChild(1).gameObject.transform.position.x, other.gameObject.transform.parent.GetChild(1).gameObject.transform.position.y,
                                                                        1) * 6;
            
            for(int i=2; i < other.gameObject.transform.parent.childCount; i++){
                tempCube= other.gameObject.transform.parent.GetChild(i).gameObject;
                tempCube.tag="ColledCube";
                other.gameObject.transform.parent.GetChild(i).gameObject.GetComponent<Renderer>().material=ColledCube;
                tempCube.GetComponent<Rigidbody>().isKinematic=false;
                tempCube.GetComponent<Rigidbody>().velocity=new Vector3(1.1f * tempCube.transform.position.x, tempCube.transform.position.y,
                                                                        1) * 6;

            }

            ScoreText.text=(int.Parse(ScoreText.text)+24).ToString();
        }

        if(other.gameObject.tag=="Cylinder"){
            Debug.Log("triggered");
            ttempStg=tempStg;
            tempStg=currentStg;
            currentStg=other.transform.parent.gameObject;
            Destroy(ttempStg);
            CreateStage();
            ChangeCons();
        }
    }


    void ChangeCons(){
        a=currentStg.transform.localScale.x;
        cons= ( ( ( (a - Cmin) / (Cmax - Cmin) ) * (Rmax - Rmin) ) + Rmin ) + DimAnimOffset;
        RingTargetVector=new Vector3(100f, cons, cons);
        Debug.Log(Cmin + "|" + Cmax + "|" + Rmin + "|" + Rmax + "|" + a + "|" + cons);
    }

    void CreateStage(){
        Instantiate(DefStg, new Vector3(0, 0, DefStg.transform.position.z+(10*StgCounter)), DefStg.transform.rotation, Floor.transform);

        if(StgCounter%2==0){
            RandomRadius=Random.Range(Floor.transform.GetChild(Floor.transform.childCount-2).localScale.x, 1.14f);
            Floor.transform.GetChild(Floor.transform.childCount-1).GetChild(0).gameObject.GetComponent<Renderer>().material=OtherCylColor;
        }

        else{
            RandomRadius=Random.Range(0.885f, Floor.transform.GetChild(Floor.transform.childCount-2).localScale.x);
        }
        
        Floor.transform.GetChild(Floor.transform.childCount-1).localScale=new Vector3(RandomRadius , RandomRadius , 1);
        StgCounter++;
        
    }
    
    void GameOver(){
        gameOver=true;
        ForceVector.z=0f;
    }
    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
