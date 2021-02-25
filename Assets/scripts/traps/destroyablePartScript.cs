using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyablePartScript : MonoBehaviour
{
    private Vector3 initPos;
    private bool destroyed, black, white;
    private Material mat;
    private Color color;
    public float userStartSpeed;
    private float startSpeed;
    private int fades;
    // Start is called before the first frame update
    void Start()
    {
        initPos=this.transform.position;
        destroyed=false;
        black=false;
        white=false;
        fades=6;
        startSpeed=userStartSpeed;
    }
    public void rePos()
    {
        this.transform.localPosition = initPos;
        destroyed=false;
        black=false;
        white=false;
        fades=6;
        startSpeed=userStartSpeed;
        mat=this.GetComponent<Renderer>().material;
        mat.color = new Color(1, 1, 1,1);
    }
   private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag=="Player" && !destroyed)
          {     
                
               
                mat=this.GetComponent<Renderer>().material;
                color = mat.color;
                black=true;           
          }
    }
    void Update()
    {
        
        if(fades>0)
        {
            if(black==true)
            if(mat.color.r >0.1f)
                mat.color = new Color(mat.color.r-startSpeed*Time.deltaTime, mat.color.g-startSpeed*Time.deltaTime, mat.color.b-startSpeed*Time.deltaTime, color.a);  
            else
            {
                fades--;
                white=true;
                black=false;
                startSpeed = startSpeed*3/2;
            }    
            if(white==true)
            if(mat.color.r <0.9f )
                mat.color = new Color(mat.color.r+startSpeed*Time.deltaTime, mat.color.g+startSpeed*Time.deltaTime, mat.color.b+startSpeed*Time.deltaTime, color.a);  
            else
            {
                fades--;
                black=true;
                white=false;
                startSpeed = startSpeed*3/2;
            }     
        }
                
            
        if(fades==0 && this.transform.position.y>=-3.0f)
        {
            transform.Translate(0, -5*Time.deltaTime, 0);
        }
    }
        
}
