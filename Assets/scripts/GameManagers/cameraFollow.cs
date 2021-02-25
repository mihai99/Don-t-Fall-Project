using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraPosition
{
    Front,
    Back,
    Left,
    Right,
    Menu
}

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 lookAtPlayer;
    public float cameraOffsetX, cameraOffsetY, cameraOffsetZ;
    private float currentY, posY;
    public bool isGameCamera;
    public Vector3 menuCameraPosition, gameCameraStartPosition;
    public float realOffsetZ;
    public float realOffsetX;
    public CameraPosition cameraPosition = CameraPosition.Front;
    // Start is called before the first frame update
    private void Awake() {
          player = GameObject.FindGameObjectWithTag("Player"); 
          Vector3 playerPos = player.transform.position;
          lookAtPlayer = new Vector3(playerPos.x, playerPos.y, playerPos.z+cameraOffsetZ);
          this.transform.position = menuCameraPosition;
    }
    void Start()
    {
        realOffsetZ = 1;
        isGameCamera = false;
        posY = player.GetComponent<SimpleCharacterControl>().Yground + cameraOffsetY;
        currentY = posY;         
    }
   
    public void resetYCamera()
    {
        currentY = player.GetComponent<SimpleCharacterControl>().Yground + cameraOffsetY;
    }
  
    public void focusPlayer(){
       
        transform.LookAt(lookAtPlayer);   
    }
    // Update is called once per frame
     public void fixOffset()
    {
        if(isGameCamera == true)
        {
            if(realOffsetZ < cameraOffsetZ )
                realOffsetZ += 3 *Time.deltaTime;
        }
        else
        {
            if(realOffsetZ >1 )
                realOffsetZ -= 3*Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (cameraPosition == CameraPosition.Front || cameraPosition == CameraPosition.Back || cameraPosition == CameraPosition.Menu)
        {
            fixOffset();
        }
        
    Vector3 playerPos = player.transform.position;
    lookAtPlayer = new Vector3(playerPos.x + realOffsetX, isGameCamera==true?player.GetComponent<SimpleCharacterControl>().Yground : 0.3f , playerPos.z+realOffsetZ);
    
    Quaternion rotationTarget = Quaternion.LookRotation(lookAtPlayer - transform.position, Vector3.up);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, 5*Time.deltaTime);

        if(isGameCamera){

              
            Vector3 shouldBeHere =  new Vector3(playerPos.x+cameraOffsetX, posY, playerPos.z+cameraOffsetZ);

            transform.position = Vector3.MoveTowards(transform.position, shouldBeHere, Mathf.Max(5, Vector3.Distance(transform.position, shouldBeHere)*2)*Time.deltaTime);

            if(Mathf.Abs(currentY-posY)>=0.1f)
                 posY += (currentY>posY?1:-1)*Time.deltaTime*5; 
            if(Input.GetKeyDown(KeyCode.C))
                cameraOffsetX *= -1;

            switch (cameraPosition)
            {
                case CameraPosition.Right: {
                        cameraOffsetX = -3f;
                        cameraOffsetZ = 5.5f;
                        realOffsetZ = 0;
                        realOffsetX = -3;
                        break;
                    }
                case CameraPosition.Left:
                    {
                        cameraOffsetX = 3f;
                        cameraOffsetZ = -5.5f;
                        realOffsetZ = 0;
                        realOffsetX = 3;
                        break;
                    }
                case CameraPosition.Front:
                    {
                        cameraOffsetX = -5.5f;
                        cameraOffsetZ = 3;
                        realOffsetX = 0;
                        realOffsetZ = 3;
                        break;
                    }
                case CameraPosition.Back:
                    {
                        cameraOffsetX = 5.5f;
                        cameraOffsetZ = 3;
                        realOffsetX = 0;
                        realOffsetZ = 3;
                        break;
                    }
                case CameraPosition.Menu: {
                        cameraOffsetX = 5.5f;
                        cameraOffsetZ = 3;
                        realOffsetZ = 1;
                        realOffsetX = 0;
                        break;
                    }               
            }
        }
        else
        {
            cameraPosition = CameraPosition.Menu;
            cameraOffsetX = 5.5f;
            cameraOffsetZ = 3;
            realOffsetZ = 1;
            realOffsetX = 0;
            if (Vector3.Distance(transform.position, menuCameraPosition)>0.1f)
            this.transform.position = Vector3.MoveTowards(transform.position, menuCameraPosition, Mathf.Max(5, Vector3.Distance(transform.position, menuCameraPosition))*Time.deltaTime);
        }
        
    }
}
