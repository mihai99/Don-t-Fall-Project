using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class playerManager : MonoBehaviour
{
    public GameObject Fire, GreenFire, BlueFire;
    public bool premiumInvulnerability = false;
    public bool premiumMoney;
    public GameObject dieSound, winSound, reviveSound, gotMoneySound;
    
    public GameObject gameSaverObj;    
    public float totalScore;
    public GameObject[] stars;
    public int score;
    public GameObject[] livesImg;
    public GameObject reviveObj;
    public CameraPosition oldCameraPosition;
    private GameObject gameManager;
    public GameObject respawn;
    public int lives;
    public bool inv;
    public bool doubleMoney;
    private bool invulnerability;

    private float invTimer;
    public GameObject invTimerObj;
    public GameObject invIsHere;
    public GameObject menuHandler;

    // Start is called before the  frame update

    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("canvas");
        invulnerability = false;    
     
    }
    void revived()
    {
        this.GetComponent<SimpleCharacterControl>().active = true;
        invulnerability = false;
        
    }
    private float getMoneyWithBoost(float money)
    {
        money = money*(doubleMoney==true?1.5f:1);
        money = money*(premiumMoney==true?2:1);
        return money;
    }
    void movePlayerToRes()
    {
        this.transform.parent = null;
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<SimpleCharacterControl>().active = true;
        reviveSound.GetComponent<AudioSource>().Play();
        dieSound.GetComponent<AudioSource>().Stop();
        GameObject[] fallingBlocks = GameObject.FindGameObjectsWithTag("destroyable");
        foreach(GameObject fal in fallingBlocks)
            fal.GetComponent<destroyablePartScript>().rePos();
        this.gameObject.transform.position = respawn.transform.position;
        Camera.main.GetComponent<cameraFollow>().cameraPosition = this.oldCameraPosition;
        GameObject.FindGameObjectWithTag("DeathScreen").GetComponent<greyScreenHandler>().Hide();
        Invoke("revived", 0.75f);
    }
    void die() {
        if (GameObject.FindGameObjectWithTag("joystick"))
        {
            GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>().ResetPosition();
        }
        GetComponent<SimpleCharacterControl>().m_animator.SetFloat("MoveSpeed",0);
        dieSound.GetComponent<AudioSource>().Play();
        this.GetComponent<SimpleCharacterControl>().active = false;
        if(lives==1)
        {
            gameManager.GetComponent<finalDieHandler>().Init();         
        }
        else
        {
            if(respawn)
            {
                Instantiate(BlueFire, respawn.transform.position, Quaternion.identity);
            }
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            livesImg[lives-1].SetActive(false);
            lives--;
            invulnerability = true;
            Instantiate(Fire, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("DeathScreen").GetComponent<greyScreenHandler>().Show();
            Invoke("movePlayerToRes", 0.75f);
        }      
    }
    public void lastRevive(){
        this.lives = 1;
        livesImg[0].SetActive(true);
        invulnerability = true;
        movePlayerToRes();
       
    }
    void backToMenu()
    {
        menuHandler.GetComponent<GameManager>().backToMenu();
    }
    void win()
    {
        //play sound
        winSound.GetComponent<AudioSource>().Play();

        //calculate score and save it
        totalScore = Mathf.Max(50, 300-gameManager.GetComponent<timerScript>().timePassed);
        totalScore += score * 50;
        totalScore += lives * 100;
        var currentLvl = gameManager.GetComponent<GameManager>().currentLvl;
        {
            gameSaverObj.GetComponent<HighScoreHandler>().scorePerLevels[currentLvl] = Mathf.Max(
                (int)totalScore,
                gameSaverObj.GetComponent<HighScoreHandler>().scorePerLevels[currentLvl]
                );
            gameSaverObj.GetComponent<HighScoreHandler>().SaveHighScore();
        }
        
        //disable player movement
        this.GetComponent<SimpleCharacterControl>().enabled = false;       
       
        //handle inventory
        if(gameManager.GetComponent<GameManager>().currentLvl != -1)
        {
           gameSaverObj.GetComponent<levelsCompletedHandler>().stars[gameManager.GetComponent<GameManager>().currentLvl] =
           Mathf.Max(
                gameSaverObj.GetComponent<levelsCompletedHandler>().stars[gameManager.GetComponent<GameManager>().currentLvl],
                this.score
           );

            gameSaverObj.GetComponent<inventoryHandler>().money += (int)getMoneyWithBoost(totalScore);
            gameSaverObj.GetComponent<inventoryHandler>().dimonds += 1;
            gameSaverObj.GetComponent<inventoryHandler>().SaveInventory();

            //handle level saver
            gameSaverObj.GetComponent<levelsCompletedHandler>().lastLevel =
            Mathf.Max(
               gameSaverObj.GetComponent<levelsCompletedHandler>().lastLevel,
               gameManager.GetComponent<GameManager>().currentLvl + 1);
        }
        else
        {
            //handle level saver
            gameSaverObj.GetComponent<levelsCompletedHandler>().tutorialDone = true;
        }

        //handle level saver
        gameSaverObj.GetComponent<levelsCompletedHandler>().SaveLevels();

        //disable powerups for this level
        inv = false;
        doubleMoney = false;
        premiumMoney = false;

        GameObject.FindGameObjectWithTag("LevelCompleted").GetComponent<WinMenuHandler>()
            .Show(totalScore, gameManager.GetComponent<GameManager>().currentLvl);
    }
    
    void stopInv()
    {
        Debug.Log("stop inv");
        GameObject.FindGameObjectsWithTag("enamy").ToList()
            .FindAll(x => x.GetComponent<Collider>() != null)
            .ForEach(x => x.GetComponent<Collider>().enabled = true);
        inv=false;
        premiumInvulnerability = false;
        invTimerObj.SetActive(false);
    }
 
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.name == "Terrain")
        {
            die();
        }
        else if(collision.gameObject.transform.tag=="enamy" && !invulnerability)
            { 
                if(!inv)
                    die();
                else
                {
                    invTimer = premiumInvulnerability==true?10:5;
                    invIsHere.SetActive(false);
                    GameObject.FindGameObjectWithTag("invTitle").GetComponent<InvulnerabilityTitleHandler>().Show();
                    GameObject.FindGameObjectWithTag("invTitle").GetComponent<InvulnerabilityTitleHandler>().HideInSeconds(premiumInvulnerability == true ? 10 : 5);
               
                    invTimerObj.SetActive(true);
                    GameObject.FindGameObjectsWithTag("enamy")
                        .ToList()
                        .FindAll(x => x.transform.name != "Terrain")
                        .FindAll(x => x.GetComponent<Collider>() != null)
                        .ForEach(x => x.GetComponent<Collider>().enabled = false);
                        
                    Invoke("stopInv", premiumInvulnerability==true?10:5);
                }
            }
        
        if(collision.gameObject.transform.tag=="star")
        {
            menuHandler.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotStar);

            stars[score].SetActive(true);
            score++;
            Destroy(collision.gameObject);
         }
        if(collision.gameObject.transform.tag == "win")
            win();

        if(collision.gameObject.transform.tag == "money")
        {
            menuHandler.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotMoneyFromGameAlert);
            gotMoneySound.GetComponent<AudioSource>().Play();
            Instantiate(GreenFire, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            gameSaverObj.GetComponent<inventoryHandler>().money += (int)getMoneyWithBoost(500.0f);
            gameSaverObj.GetComponent<inventoryHandler>().SaveInventory();
                
        }
        if(collision.gameObject.transform.tag == "dimond")
            {

            menuHandler.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotDimondFromGameAlert);
                Destroy(collision.gameObject);
                gameSaverObj.GetComponent<inventoryHandler>().dimonds += 5;
                gameSaverObj.GetComponent<inventoryHandler>().SaveInventory();
                
            }
        }
  

    // Update is called once per frame
    void Update()
    {
        if(invTimer>0);
           { 
                invTimerObj.GetComponent<Slider>().value = (1-invTimer/(premiumInvulnerability==true?10:5));              
                invTimer -= Time.deltaTime;
           }
    }
}
