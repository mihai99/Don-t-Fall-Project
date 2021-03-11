using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TreeDestroyer TreeDestroyer;
    public GameObject tutorialLevel;
    public GameObject[] levels;
    public bool canKillLevel = false;
    public GameObject curentLvlObj;
    public GameObject player;
    public Vector3 playerStartPos;  
    public Quaternion playerStartRot;
    public int currentLvl;
    public GameObject menuUI, gameUI;
    public GameObject gameSound, menuSound;
    public GameObject textLevelAtStart;
    private GameObject adManager;
    public GameObject InGamePowerups;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        adManager = GameObject.FindGameObjectWithTag("adManager");
        player = GameObject.FindGameObjectWithTag("Player");
        canKillLevel = false;
        Camera.main.transform.GetComponent<cameraFollow>().isGameCamera = false;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        
    }
    public void initLevel(int lvl, bool restart)
    {
        lvl = Mathf.Min(lvl, levels.Length - 1);

        if (restart==true || curentLvlObj == null){
            player.GetComponent<playerManager>().invIsHere.SetActive(false);
            GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Hide();

            adManager.GetComponent<AdManagerInGame>().ShowBanner();
            if (Random.Range(1, 10) == 1 || Debug.isDebugBuild)
            {
                adManager.GetComponent<AdManagerInGame>().ShowInterstitial();
            }

            InGamePowerups.GetComponent<InGamePowerups>().InitMenu();
            this.GetComponent<finalDieHandler>().InitOnLevel();
            GetComponent<AlertManager>().SetLevelAlert(lvl);
            Camera.main.GetComponent<cameraFollow>().isGameCamera = true;
            Camera.main.GetComponent<cameraFollow>().cameraPosition = CameraPosition.Back;
            currentLvl = lvl;
            menuUI.SetActive(false);
            gameUI.SetActive(true);
            GameObject.FindGameObjectWithTag("invTitle").GetComponent<InvulnerabilityTitleHandler>().HideInSeconds(0.1f);
            player.GetComponent<playerManager>().invTimerObj.SetActive(false);
            if (lvl == -1)
            {
                curentLvlObj = Instantiate(tutorialLevel, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                curentLvlObj = Instantiate(levels[lvl], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
                gameUI.GetComponent<HiddeRestartOnTutorialLevel>().ForceShow();
            }
            if(TreeDestroyer)
            {
                TreeDestroyer.SetTrees();
            }

            player.GetComponent<playerManager>().livesImg[3].SetActive(false);           
            player.GetComponent<playerManager>().lives = 3;
            player.GetComponent<playerManager>().score = 0;
            player.GetComponent<playerManager>().inv = false;
            player.GetComponent<playerManager>().premiumInvulnerability = false;
            player.GetComponent<playerManager>().respawn = null;

            for(int i=0;i<3;i++)
            {
                player.GetComponent<playerManager>().livesImg[i].SetActive(true);            
                player.GetComponent<playerManager>().stars[i].SetActive(false);
            }

            this.GetComponent<MapSkinManager>().ApplySkin();
            startLevel();
        }
    }

    public void startLevel()
    {
        menuSound.GetComponent<AudioSource>().Stop();
        gameSound.GetComponent<AudioSource>().Play();
        
        this.GetComponent<timerScript>().timer.SetActive(true);
        this.GetComponent<timerScript>().timeStart = Time.time;
        player.GetComponent<SimpleCharacterControl>().active = true;
        player.GetComponent<SimpleCharacterControl>().enabled = true;
        gameUI.SetActive(true);

    }
    public void startBut()
    {
        currentLvl = GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().lastLevel;    
        initLevel(currentLvl, false);
    }
     public void restart()
    {        
        this.GetComponent<timerScript>().timer.SetActive(false);
        player.transform.position = playerStartPos;
        player.GetComponent<SimpleCharacterControl>().active = false;
        Destroy(curentLvlObj);
        initLevel(currentLvl, true);
      
    }
    public void backToMenu()
    {            
        Camera.main.GetComponent<cameraFollow>().isGameCamera = false;
     
        gameSound.GetComponent<AudioSource>().Stop();
        menuSound.GetComponent<AudioSource>().Play();
    
        gameUI.SetActive(false);
        menuUI.SetActive(true);
        
        canKillLevel = true;
        
        player.GetComponent<SimpleCharacterControl>().enabled = false;

        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;

        adManager.GetComponent<AdManagerInGame>().KillBaner();
    }
    public void nextLevel()
   {             
        
        player.GetComponent<SimpleCharacterControl>().enabled = false;
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;
        Destroy(curentLvlObj); 
        initLevel(Mathf.Max(currentLvl+1, GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().lastLevel), true);
      
   }
  
    void Update()
    {
      if(canKillLevel == true && Vector3.Distance(Camera.main.transform.position, Camera.main.GetComponent<cameraFollow>().menuCameraPosition)<0.2f)
        {
            Destroy(curentLvlObj);
            canKillLevel = false;
        }
    }
}
