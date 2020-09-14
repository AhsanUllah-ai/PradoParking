using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class game_manager : MonoBehaviour {
    public static game_manager instance;
    private GameObject spawnpoint;
    public GameObject[] mode1SpawnPoints,mode2SpawnPoints,mode3SpawnPoints;
     [Space]
     public GameObject[] Free_Mode_SpwanPoints;
     [Space]

     public GameObject[] Daily_mission_Spwan_point;
     [Space]


     [Space]
     public GameObject[] levelCar;

     [Space]

     [Space]
     public GameObject[] levelAdvanceMode;

     [Space]

     [Space]
     public GameObject[] levelProMode;

     [Space]
      public GameObject Arcade_Mode;
    [Space]

     public GameObject Daily_Mission_Prefab;

     public Text daily_mission_points_text;

    [Space]
    public GameObject [] playerVehicles;
    private int currentVehicle = 0;
    public GameObject pause_panel,gamePlay_panel,failed_panel,Arcde_fail_panel,Daily_misson_fail_panel; 
    public GameObject levelskipped;
    public GameObject already_skipped;
    public GameObject notenoughcash;
    public GameObject skip_panal;
    public GameObject fail_panal;
    public GameObject SkipButtons;
    public int cash;
    public Image parking_area;
    public Image sign;
    int current_level = 0;
    public GameObject cars_enviorment;//,parkingEnvironment;
    private static GameObject CarsEnvironment;
    private static GameObject conesBlock_Level;
    public Material rings;
    public ButtonInput gas, brake,brakeTilt,left,right;
    private float gasInput,brakeInput, leftInput,rightInput,accelInput,steerInput;
    [HideInInspector]
    public int controlNumber = 5;
    public GameObject steerWheel;
    private bool isForward = true; // forward , backward
    public GameObject mainCamera;
    private GameObject topCamera;
    public int cameraNumber = 0;
	public GameObject ImgHand;
	public Text levelNumberText;
	public GameObject frontmirror;
    //prado Manager data
   
    public GameObject controls_panel;
    
    public GameObject Follow_Camera;
    [Space]
    [Space]
    public Image Parker_Filler;
    public enum GameControl
    {
        Paddle = 0,
        Steering = 1,
        Tilt = 2
    };
    [Header("UI Related")]
    public Button For;
    public Button Rev;
    
    [Space]
   
    public GameObject[] ControlUI;
    public GameObject[] ControlImages;
    public GameControl AssignedControl;
    [HideInInspector]
    public bool Accelerate, Brake, Left, Right;
    [HideInInspector]
    public int Direction;
    public int Control_Index;
    private Transform spawn;
    private int camIndex;
    private int color_index;
    private int LevelNo;
    private bool IsTutorialLevel;
    private bool RevivedOnce;
    public bool Reverse_bool;
    public bool break_bool;
    public AudioSource[] sound;   
    public Text cam_num_text;
    private GameObject bonnet_obj;
    private GameObject Player_obj; 
    public GameObject Render_Ui;
    public GameObject Side_Left;
    public GameObject Side_Right;
    public int couter;
    public int couter_1;
    public GameObject OrbitCamera;
    public GameObject Rest_orb;
    public Text Counter_free;
    public bool Restart_bool;
    public GameObject Timer;
    public GameObject Free_mode_info;
    public GameObject Env_greenbelt;  
    public GameObject Loading;
    public Text total_coins_fail;

    public Text Level_no_fail;
    public Text parking_score_text;

    public int earned_int;

    public Text col_counter_text;


    public GameObject  doubleRewardText, doubleRewardButton;

    public bool Level_comp_bool = false;

    public Text Gift_pick_up_text;


    public GameObject Respwan_panel;

    public Text Total_hits_text,fail_count_text;

    public GameObject[]  ControlUIBtn;


    public Text Level_prevous,Level_plus;

    public Text Current_score,high_score;

    //Daily _Mission data

    public Text daily_Mission_no_text;

    public Image Fuel_png;
    public Sprite Fuel_sprite,driver_sprite;

    public GameObject Coins_ui;



    void Awake ()
    {
     
        instance = this;
        if(PlayerPrefs.GetInt("currentMode") != 5)
        {
          currentVehicle = Grage_Manager.currentVehicle;
        }
        RingColor ();
        Invoke("ChangeRingColors",1);
		int levelnum;
		levelnum = PlayerPrefs.GetInt ("funnelLevelNumber");
		levelNumberText.text = levelnum.ToString ();
        //prado data
        RevivedOnce = false;
        

    }


    void Start()
    {

         if(PlayerPrefs.GetInt("currentMode") != 5)
        {
          playerVehicles[currentVehicle].SetActive(true);
      
        }else
        { 
             
            
              PlayerPrefs.SetInt("currentMode",5);
              Coins_ui.SetActive(false);
                      
        }
        sign.enabled = false;
        PlayerPrefs.SetInt("cam", 1);
        cash = PlayerPrefs.GetInt("cash");
      
        PlayerPrefs.SetInt("front", 0);
        Time.timeScale = 1;
        
        Invoke("Find_TopCamera", 0.2f);
      
        Ads_Manager.Instance.HideSmallAdmobBanner();

        Ads_Manager.Instance.HideLargeAdmobBanner();
        
        Direction = 1;
        camIndex = 0;
        Reverse_bool = false;
        break_bool = false;
        int levelnum;
        levelnum = PlayerPrefs.GetInt("level")+1;
        levelNumberText.text = levelnum.ToString();

        Level_Enb();
        PlayerPrefs.SetInt("Gameplay_load",1);
        if (PlayerPrefs.GetInt("ControlIndex") == 0)
        {
            Control_Index = 1;
        }
        if (PlayerPrefs.GetInt("ControlIndex") == 1)
        {
            Control_Index = 0;
        }
        if (PlayerPrefs.GetInt("ControlIndex") == 2)
        {
            Control_Index = 2;
        }
        if(PlayerPrefs.GetInt("currentMode")<=3)
        {
       if (PlayerPrefs.GetInt("level") == 0)
        {
            gas.GetComponent<Animator>().Play("race");
            gas.GetComponent<Shadow>().enabled= true;
        }
        else
        {
            gas.GetComponent<Animator>().enabled = false;
            gas.GetComponent<Shadow>().enabled = false;
        }
        }
        else 
        {
            gas.GetComponent<Animator>().enabled = false;
            gas.GetComponent<Shadow>().enabled = false;
        }
        SwitchControl();  
        Ads_Manager.Instance.LoadInterstitialButtonClicked();
        FBAManager.Instance.levelStart("M" + PlayerPrefs.GetInt("currentMode") + "Lvl"+ PlayerPrefs.GetInt("level"));
        FollowCam.Game_Start = false;
        bonnet_obj = GameObject.FindGameObjectWithTag("bonnetCam");
        Player_obj = GameObject.FindGameObjectWithTag("Player");

        if(PlayerPrefs.GetInt("currentMode")==4)
        {
            Free_mode_info.SetActive(true);
            Counter_free.enabled = true;
            levelNumberText.gameObject.SetActive(false);
            Player_obj.GetComponent<LookAt>().enabled = true;
            _Load_free_Mode();
            Timer.SetActive (true);
            Fuel_png.sprite = Fuel_sprite;

        }
        else
        {
            levelNumberText.gameObject.SetActive(true);
          
            GameObject.Find("Plane (1)").GetComponent<Example>().Change_Envi();
        }


        if (PlayerPrefs.GetInt("currentMode") == 1)
        {
            Instantiate(Env_greenbelt);
        }
          PlayerPrefs.SetInt("Col_counter",0);
          if(PlayerPrefs.GetInt("manuScene_load")==2)
          {
             PlayerPrefs.SetInt("manuScene_load",0);
          }
           if(PlayerPrefs.GetInt("currentMode")==5)
        {
            Load_Daily_Mission();
            levelNumberText.gameObject.SetActive(false);
            Fuel_png.sprite = driver_sprite;

        
        }
    

    }

    void Load_Daily_Mission()
    {
           
           Instantiate(Daily_Mission_Prefab, Daily_Mission_Prefab.transform.position, Daily_Mission_Prefab.transform.rotation);
           daily_mission_points_text.text = PlayerPrefs.GetInt("parking_point_collected").ToString();
           Invoke("Enable_Daily_Timer",3f);
        
    }

    void Enable_Daily_Timer()
    {
            Timer.SetActive (true);
    }
    void _Load_free_Mode()
    {
           
            Instantiate(Arcade_Mode, Arcade_Mode.transform.position, Arcade_Mode.transform.rotation);
         
    }

  public  void Update_pickup_text()
    {
       Gift_pick_up_text.text = PlayerPrefs.GetInt("cash").ToString();
  

    }


    void imgAct()
	{
		ImgHand.SetActive (true);
        Invoke("imgActfls", 3f);
    }

   
    void imgActfls()
    {
        ImgHand.SetActive(false);
    }
    public void ControlSelection (int cNumber)
    {
        if(cNumber.Equals(0))
        {
            brake.transform.gameObject.SetActive(false);
        
            left.transform.gameObject.SetActive(true);
            right.transform.gameObject.SetActive(true);
            steerWheel.SetActive(false);
        }
        else
        if(cNumber.Equals(1))
        {           
            brake.transform.gameObject.SetActive(true);
            brakeTilt.transform.gameObject.SetActive(false);
            left.transform.gameObject.SetActive(true);
            right.transform.gameObject.SetActive(true);            
            steerWheel.SetActive(false);
        }
        else
        {      
            brake.transform.gameObject.SetActive(true);
            brakeTilt.transform.gameObject.SetActive(false);
            left.transform.gameObject.SetActive(false);
            right.transform.gameObject.SetActive(false);            
            steerWheel.SetActive(true);
        }

        controlNumber = cNumber;
    }

    float GetInput(ButtonInput button){

		if(button == null)
			return 0f;

		return(button.input);

	}
  

    void RingColor ()
    {
      
        PlayerPrefs.SetInt("front",0);  
        PlayerPrefs.SetInt("wrong_front",0);
    }
    public void ChangeRingColors ()
    {
       
       
        int front = PlayerPrefs.GetInt("front");  
        if (PlayerPrefs.GetInt("front")==1)
        {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;    
                rings.color = Color.green;
                parking_area.fillAmount += 0.5f * Time.deltaTime;
                dontDestroy.instance.sound[7].Play();
        }
        else
        {
            parking_area.fillAmount -= 0.5f * Time.deltaTime;
            rings.color = Color.yellow;
            
        }
    }

    public void SwitchControl()
    {
        if (PlayerPrefs.GetInt("ControlIndex") == 1)
        {
        
            ControlUI[0].SetActive(true);
            ControlUI[1].SetActive(false);
            ControlUI[2].SetActive(false);
            ControlImages[3].SetActive(true);
            ControlImages[0].SetActive(true);
            AssignedControl = GameControl.Paddle;
          

        }
        
        if (PlayerPrefs.GetInt("ControlIndex") == 0)
        {
        
            ControlUI[0].SetActive(false);
            ControlUI[1].SetActive(true);
            ControlUI[2].SetActive(false);
            ControlImages[3].SetActive(true);
            AssignedControl = GameControl.Steering;
         
        }
        if (PlayerPrefs.GetInt("ControlIndex") == 2)
        {     
            ControlUI[0].SetActive(false);
            ControlUI[1].SetActive(false);
            ControlUI[2].SetActive(true);
            ControlImages[2].SetActive(true);
            ControlImages[2].SetActive(true);
            ControlImages[3].SetActive(false);
            AssignedControl = GameControl.Tilt;
         
        }
      
    }
   
    
    //Prado_manager_data

    public void ChangeDirection(int v)
    {
        Direction = v;
        if (v == 1)
        {

            For.gameObject.SetActive(false);
            Rev.gameObject.SetActive(true);
            Reverse_bool = false;
          
            if (PlayerPrefs.GetInt("cam") == 1 || PlayerPrefs.GetInt("cam") == 2)
            {
                FollowCam.Instance.offset.z *= -1;
            }
           
        }
        else
        {   
            Reverse_bool = true;
            For.gameObject.SetActive(true);
            Rev.gameObject.SetActive(false);           
            if (PlayerPrefs.GetInt("cam") == 1 || PlayerPrefs.GetInt("cam") == 2)
            {
                FollowCam.Instance.offset.z *= -1;
            }
        }
        dontDestroy.instance.sound[2].Play();
    }

    public void ChangeDirection_cam(int v)
    {


        Direction = v;
        if (v == 1)
        {

            For.gameObject.SetActive(false);
            Rev.gameObject.SetActive(true);
            Reverse_bool = false;
           
            if (PlayerPrefs.GetInt("cam") == 1 || PlayerPrefs.GetInt("cam") == 2)
            {
                FollowCam.Instance.offset.z *= -1;
            }

        }
        else
        {

            Reverse_bool = true;
            For.gameObject.SetActive(true);
            Rev.gameObject.SetActive(false);
      
            if (PlayerPrefs.GetInt("cam") == 1 || PlayerPrefs.GetInt("cam") == 2)
            {
                FollowCam.Instance.offset.z *= -1;
            }

        }
    }


   
    public void ToogleAccelerate(bool flag)
    {
        Accelerate = flag;

    }


    public void ToogleBrake(bool flag)
    {

        Brake = flag;
        if (flag)
        {

        }
        break_bool = flag;

    }

    public void Toogleft(bool flag)
    {
        Left = flag;

    }

    public void ToogleRight(bool flag)
    {
        Right = flag;

    }



    public void GasPress()
	{
		VehicleController.instance.rb.drag = 0.65f;
		VehicleController.instance.CancelInvoke ();
	
	
	}




    void Find_TopCamera ()
    {
        topCamera = GameObject.FindGameObjectWithTag("topcam");
        
    }
    void LoadSceneObjects ()
    {
        current_level = PlayerPrefs.GetInt("level");

      
       
        switch(PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
                if(mode1SpawnPoints[current_level])
                {
                   
                   
                    playerVehicles[currentVehicle].transform.position = mode1SpawnPoints[current_level].transform.position;
                    playerVehicles[currentVehicle].transform.localRotation = mode1SpawnPoints[current_level].transform.localRotation;
                    Show_CarEnvironment ();
                }
            break;
            case 2:
                if(mode2SpawnPoints[current_level])
                {
                    playerVehicles[currentVehicle].transform.position = mode2SpawnPoints[current_level].transform.position;
                    playerVehicles[currentVehicle].transform.localRotation = mode2SpawnPoints[current_level].transform.localRotation;
                    Show_ConesAndBlock ();
                }
            break;
            case 3:
                if(mode3SpawnPoints[current_level])
                {
                    playerVehicles[currentVehicle].transform.position = mode3SpawnPoints[current_level].transform.position;
                    playerVehicles[currentVehicle].transform.localRotation = mode3SpawnPoints[current_level].transform.localRotation;
                    Show_ConesAndBlock ();
                }
            break;

            case 4:
                if(Free_Mode_SpwanPoints[0])
                {
                    int Ran_num = Random.Range(0, 3);
                    playerVehicles[currentVehicle].transform.position = Free_Mode_SpwanPoints[Ran_num].transform.position;
                    playerVehicles[currentVehicle].transform.localRotation = Free_Mode_SpwanPoints[Ran_num].transform.localRotation;
                    Show_CarEnvironment ();
                }
            break;
             case 5:
                if(Daily_mission_Spwan_point[0])
                {
                   
               
                }
            break;
            default :                
            break;                         
        }
       
        SetMusic(1.2f);
    }


    void Update()
    {
//#if !UNITY_EDITOR
        if(controlNumber.Equals(0))
        {
            accelInput = Input.acceleration.x;
            brakeInput = GetInput(brakeTilt);
        
        }
        else
        if(controlNumber.Equals(1))
        {
//button
            leftInput = GetInput(left);
            rightInput = GetInput(right);
            brakeInput = GetInput(brake);
        
        }
        else
       
        gasInput = GetInput(gas);

        if (FollowCam.Game_Start == true && PlayerPrefs.GetInt("level") == 9 && PlayerPrefs.GetInt("currentMode")==1)
        {
            FollowCam.Game_Start = false;
       
            Invoke("imgAct", 0.5f);
            
        }

//#endif
      if(PlayerPrefs.GetInt("front")==1){

            if (parking_area.fillAmount > 0)

            {
                parking_area.fillAmount += 0.5f * Time.deltaTime;
            }
            if (parking_area.fillAmount == 1 && Level_comp_bool == false)
            {
                sign.enabled = true;
                Level_Completd();
                Level_comp_bool = true;

               
            }
        }
       if(PlayerPrefs.GetInt("front") == 0)
        {
            parking_area.fillAmount -= 0.5f * Time.deltaTime;
            rings.color = Color.yellow;
        }
      else
        {

        }

	
    }

    void Level_Completd()
    {
       
        SceneManager.LoadScene(3);
    }

    
    public void level_selection()
    {
        SceneManager.LoadScene("main_menu");
    }
    public void SetMusic (float volume)
    {
      
    }
   
    public void pause()
    {
        Time.timeScale = 0.000001f;
        SetMusic(0.4F);  
        PauseComponent(true);        
        PlayComponent(false);	
		Ads_Manager.Instance.ShowLargeAdmobBanner ();  
        if (PlayerPrefs.GetInt("AdCount_pause") == 0)
        {
         

        }else  if (PlayerPrefs.GetInt("AdCount_pause") > 0)
        {
            Ads_Manager.Instance.Show_Admob_Unity_Ad_Iron_Source();
        }else
        {

        }

         PlayerPrefs.SetInt("AdCount_pause", PlayerPrefs.GetInt("AdCount_pause") + 1);

         dontDestroy.instance.sound[0].Play();
    }



    public void resume()
    {
        Time.timeScale = 1f;
        SetMusic(0.05F);
        PlayComponent(true);
        PauseComponent(false);
		Ads_Manager.Instance.HideLargeAdmobBanner ();
        Ads_Manager.Instance.LoadInterstitialButtonClicked();
        dontDestroy.instance.sound[1].Play();
    
    }
    public void restart()
    {
        
        Loading.SetActive(true);  
        Restart_bool= true;
        GameObject.Find("Plane (1)").GetComponent<Example>().Buildings_bg.SetActive(true);  
        SceneManager.LoadScene("Gameplay");
		Ads_Manager.Instance.HideLargeAdmobBanner ();
        dontDestroy.instance.sound[0].Play();
        Time.timeScale = 1;

    }


     public void restart_daily_mission()
    {
        Loading.SetActive(true);  
        SceneManager.LoadScene("Gameplay");
        Daily_Mission.instance.restart_bool = true;               
        PlayerPrefs.SetInt("parking_point_collected",0);
		Ads_Manager.Instance.HideLargeAdmobBanner ();
        dontDestroy.instance.sound[0].Play();
        Time.timeScale = 1;

    }

    
    public void menu()
    { 
        
        Loading.SetActive(true);
        SetMusic(0.4F);
        SceneManager.LoadScene("main_menu");
        Time.timeScale = 1;
        PlayerPrefs.SetInt("parking_point_collected",0);
		Ads_Manager.Instance.HideLargeAdmobBanner ();
        dontDestroy.instance.sound[0].Play();
        
    }
   
    

    void PauseComponent (bool status)
    {
        if(status)
        {
            pause_panel.SetActive(true);          
            
        }
        else
        {
              pause_panel.SetActive(false);      
        }  
    }
    void PlayComponent (bool status)
    {
        if(status)
        {
           gamePlay_panel.SetActive(true);
        }
        else
        {
           gamePlay_panel.SetActive(false);
        }  
    }
    

    
    
        
    public void open_skip()
    {
    
       
        skip_panal.SetActive(true);
      
      

    }
    public void close_skip()
    {
        SkipButtons.GetComponent<Button>().interactable = true;
        skip_panal.SetActive(false);
        notenoughcash.SetActive(false);
        
        if(PlayerPrefs.GetInt("currentMode")==4 || PlayerPrefs.GetInt("currentMode")==5 )
        {
            
          Respwan_panel.SetActive(true);
          skip_panal.SetActive(false);



        }else
        {
            Fail_info_calculator();
            failed_panel.SetActive(true);
            PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);      
            Ads_Manager.Instance.ShowLargeAdmobBanner();

        }
       
        dontDestroy.instance.sound[1].Play();
    }
    public void SwitchCamera ()
    {
        cameraNumber +=1;
        if(cameraNumber == 1)
        {
            
         
            PlayerPrefs.SetInt("cam", 2);
            cam_num_text.text = 2.ToString(); 
            game_manager.instance.ChangeDirection_cam(1);
            mainCamera.GetComponent<FollowCam>().offset.y = 22.5f;
            mainCamera.GetComponent<FollowCam>().offset.z = -30f;
        }
        else if(cameraNumber==2)
        {
                    
            mainCamera.GetComponent<FollowCam>().offset.y = 61.1f;
            mainCamera.GetComponent<FollowCam>().offset.z = -22f;
            cam_num_text.text = 3.ToString();
         
            PlayerPrefs.SetInt("cam", 0);

        }

         else if(cameraNumber==3)
        {

            Follow_Camera.GetComponent<FollowCam>().enabled = false;
            mainCamera.GetComponent<FollowCam>().offset.y = 14.58f;
            mainCamera.GetComponent<FollowCam>().offset.z = -19.17f;
            OrbitCamera.GetComponent<OrbitCamera>().enabled = true;
            cam_num_text.text = 4.ToString();
        
            PlayerPrefs.SetInt("cam", 3);

        }

         else if(cameraNumber==4)
        {
            GameObject.FindGameObjectWithTag("M_cam1").GetComponent<Camera>().enabled= true;
            
            mainCamera.GetComponent<FollowCam>().enabled = false;
            OrbitCamera.transform.position = new Vector3(0,0,0);
            OrbitCamera.transform.position = Rest_orb.transform.position;
            OrbitCamera.transform.rotation = Rest_orb.transform.rotation;  
            OrbitCamera.GetComponent<OrbitCamera>().enabled = false;
            GameObject.FindGameObjectWithTag("cock_pit").GetComponent<Camera>().enabled= true;
            cam_num_text.text = 5.ToString();
        
            PlayerPrefs.SetInt("cam", 4);
            Render_Ui.SetActive(true);

        }
        else
        {
        
            OrbitCamera.GetComponent<OrbitCamera>().enabled = false;
            Follow_Camera.GetComponent<FollowCam>().enabled = true;
            GameObject.FindGameObjectWithTag("M_cam1").GetComponent<Camera>().enabled= false;
            GameObject.FindGameObjectWithTag("cock_pit").GetComponent<Camera>().enabled= false;
            Player_obj = GameObject.FindGameObjectWithTag("Player");
            mainCamera.GetComponent<FollowCam>().enabled = true;
            mainCamera.GetComponent<FollowCam>().objectToFollow = Player_obj.transform; 
            Render_Ui.SetActive(false);
            cameraNumber = 0;
            cam_num_text.text = 1.ToString();
        
            PlayerPrefs.SetInt("cam", 1);
            game_manager.instance.ChangeDirection_cam(1);
            mainCamera.GetComponent<FollowCam>().offset.y = 14.58f;
            mainCamera.GetComponent<FollowCam>().offset.z = -19.17f;
            mainCamera.GetComponent<FollowCam>().followSpeed = 4f;


        }

      

    }

 
    public void skip_level()
    {
      
        if (cash >= 1000)
        {
            SkipButtons.GetComponent<Button>().interactable = false;
            current_level = PlayerPrefs.GetInt("level");
            failed_panel.SetActive(true);
            PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);
            levelskipped.SetActive(true);
            skip_panal.SetActive(false);
            cash = cash - 1000;
            PlayerPrefs.SetInt("cash", cash);
            FBAManager.Instance.levelComplete("M" + PlayerPrefs.GetInt("currentMode") + "Lvl" + PlayerPrefs.GetInt("level"));
            int level_played = PlayerPrefs.GetInt("level");
            level_played++;
           
            switch (PlayerPrefs.GetInt("currentMode"))
            {
                case 1:
                     PlayerPrefs.SetInt("CarLevelUnlocked", PlayerPrefs.GetInt("CarLevelUnlocked"));
                     PlayerPrefs.SetInt("level", level_played);
                     UnlockNextLevel_next(level_played);
                     PlayerPrefs.Save();
                     Invoke("Load_Scene_After_skip",1f);
                    
                   
                    break;
                case 2:
                    
                    PlayerPrefs.SetInt("ConeLevelUnlocked", PlayerPrefs.GetInt("ConeLevelUnlocked"));     
                    PlayerPrefs.SetInt("level", level_played);
                    UnlockNextLevel_next(level_played);
                     PlayerPrefs.Save();
                    Invoke("Load_Scene_After_skip",1f);
                   

                    break;
                case 3:
  
                       PlayerPrefs.SetInt("BlockLevelUnlocked", PlayerPrefs.GetInt("BlockLevelUnlocked"));  
                       PlayerPrefs.SetInt("level", level_played);
                       UnlockNextLevel_next(level_played);
                       PlayerPrefs.Save();
                       Invoke("Load_Scene_After_skip",1f);
                   
                    break;
                default:
                    
                    break;
            } 
           
        }
        else
        {
            failed_panel.SetActive(true);
            PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);
            notenoughcash.SetActive(true);
            skip_panal.SetActive(false);
         
            SkipButtons.GetComponent<Button>().interactable = false;
        }
        dontDestroy.instance.sound[0].Play();
    }

    void Load_Scene_After_skip()
    {
          Debug.Log("here scene is loaded");
         SceneManager.LoadScene("Gameplay");
    }


     void UnlockNextLevel_next(int level )
    {
      
        switch (PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
                if (level >= PlayerPrefs.GetInt("CarLevelUnlocked"))
                    if (PlayerPrefs.GetInt("CarLevelUnlocked") < 30)
                    {
                        PlayerPrefs.SetInt("CarLevelUnlocked", PlayerPrefs.GetInt("CarLevelUnlocked") + 1);
                    }

               

                break;
            case 2:
                if (level >= PlayerPrefs.GetInt("ConeLevelUnlocked"))
                    PlayerPrefs.SetInt("ConeLevelUnlocked", PlayerPrefs.GetInt("ConeLevelUnlocked") + 1);

                break;
            case 3:
                if (level >= PlayerPrefs.GetInt("BlockLevelUnlocked"))
                    PlayerPrefs.SetInt("BlockLevelUnlocked", PlayerPrefs.GetInt("BlockLevelUnlocked") + 1);

            break;                         
        }  
    }

    public void LevelFailed ()
    {

          Ads_Manager.Instance.ShowLargeAdmobBanner();
          dontDestroy.instance.sound[4].Stop();
        switch (PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
                if(PlayerPrefs.GetInt("level")+ 1>=PlayerPrefs.GetInt("CarLevelUnlocked"))
                {
                    SkipButtons.GetComponent<Button>().interactable = true;
                }

                break;
            case 2:
                if (PlayerPrefs.GetInt("level") + 1 >= PlayerPrefs.GetInt("ConeLevelUnlocked"))
                {
                SkipButtons.GetComponent<Button>().interactable = true;
                }

                break;
            case 3:
                if (PlayerPrefs.GetInt("level") + 1 >= PlayerPrefs.GetInt("BlockLevelUnlocked"))
                {
                  SkipButtons.GetComponent<Button>().interactable = true;
                }
                break;
        }

     
        SetMusic(0.6f);
        failed_panel.SetActive(true);
        PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);
        FBAManager.Instance.levelFailed("M" + PlayerPrefs.GetInt("currentMode") + "Lvl" + PlayerPrefs.GetInt("level"));

        if(PlayerPrefs.GetInt("level") == 29)
        {
         SkipButtons.GetComponent<Button>().interactable = false;
        }

        if(PlayerPrefs.GetInt("currentMode")== 5)
        {
            
            CountDown.instance.Des_Manul();
            Daily_misson_fail_panel.SetActive(true);
            PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);
            
            if(PlayerPrefs.GetInt("Daily_mission_no")!=0)
            {
                int i = PlayerPrefs.GetInt("Daily_mission_no");
                    i+=1;
             daily_Mission_no_text.text = i.ToString();
            }


        }else
        {
            
        }

        if (PlayerPrefs.GetInt("currentMode") == 4)
        {
            CountDown.instance.pause_bool_timer = true;   
            Time.timeScale = 1;
            Fail_info_calculator();
            SkipButtons.GetComponent<Button>().interactable = false;
            Arcde_fail_panel.SetActive(true);
            PlayerPrefs.SetFloat("FailAchivement", PlayerPrefs.GetFloat("FailAchivement")+1);
            
            total_coins_fail.text = earned_int.ToString();
         
        }

        if (PlayerPrefs.GetInt("currentMode") == 4)
        {
            Fail_info_calculator();
            
        }
        else
        {
          
            int prev,plus;
            prev = PlayerPrefs.GetInt("level");
            prev+=1;
            if(prev==0)
            {
               //  prev+=1;
                 
            }
            else
            {

            }
                   
           plus=prev+1;
           Level_prevous.text = prev.ToString();
           Level_plus.text =    plus.ToString();

        
        }

               int Level = PlayerPrefs.GetInt("level");
               if(PlayerPrefs.GetInt("level") ==0)
               {
                  Level=1;
                  Level_no_fail.text = 1.ToString();
             
               }else
               {
                   Level+=1;
               }
    
               Level_no_fail.text = Level.ToString();
        

        if(PlayerPrefs.GetInt("AdCount_fail")==0)
        {
          
        }else if(PlayerPrefs.GetInt("AdCount_fail")<=3 && PlayerPrefs.GetInt("AdCount_fail")!=0)
        {
           Ads_Manager.Instance.Show_Admob_Unity_Ad_Iron_Source();         
        }else
        {
            Ads_Manager.Instance.Show_Unity_Iron_sorce_Admob();    
        }
        PlayerPrefs.SetInt("AdCount_fail",PlayerPrefs.GetInt("AdCount_fail")+1);

     

    }

    void Fail_info_calculator()
    {
          if(PlayerPrefs.GetInt("Col_counter")==0)
        {
            
             earned_int = PlayerPrefs.GetInt("high-score");
             earned_int*=50;   
             PlayerPrefs.SetInt("high-score", PlayerPrefs.GetInt("high-score"));
             col_counter_text.text = earned_int.ToString();
            
        }else
        {
             
        }
        high_score.text    =    PlayerPrefs.GetInt("p-counter").ToString();
        Current_score.text =    PlayerPrefs.GetInt("high-score").ToString();
    }
   
    void Show_CarEnvironment ()
    {
        if(CarsEnvironment == null)
        {
            CarsEnvironment = Instantiate(cars_enviorment , cars_enviorment.transform.position, cars_enviorment.transform.rotation);
          
            if(conesBlock_Level)
            DestroyImmediate(conesBlock_Level);
        }
    }
    
    void Show_ConesAndBlock ()
    {
        if(conesBlock_Level == null)
        {
    
            if(CarsEnvironment)
            DestroyImmediate(CarsEnvironment);
        }

    }
   
    public void AlreadySkipped_ToFail ()
    {

        already_skipped.SetActive(false);
        fail_panal.SetActive(true);        
    }
    public void NotEnough_ToFail ()
    {
        notenoughcash.SetActive(false);
        fail_panal.SetActive(true);        
    }
    public void LevelSkipped_ToMenu ()
    {
      
        SetMusic(0.4F);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene("Gameplay");
		Ads_Manager.Instance.HideLargeAdmobBanner ();
    }



public void Enable_Mirror(int index)
{
      
      if(index== 0 && couter==0)
      {   
          GameObject.FindGameObjectWithTag("M_cam2").GetComponent<Camera>().enabled= true;   
          Side_Right.SetActive(true);
          couter++;
      }else
      {
         GameObject.FindGameObjectWithTag("M_cam2").GetComponent<Camera>().enabled= false;   
         Side_Right.SetActive(false);
         couter=0;
      }
     
}


public void Enable_Mirror_1(int index)
{
      
      if(index== 0 && couter_1==0)
      {   
        GameObject.FindGameObjectWithTag("M_cam3").GetComponent<Camera>().enabled= true;    
        Side_Left.SetActive(true);
        couter_1++;
      }else
      {
        GameObject.FindGameObjectWithTag("M_cam3").GetComponent<Camera>().enabled= false;    
        Side_Left.SetActive(false);
        couter_1=0;
      }
     
}


public void DoubleReward ()
    {
        doubleRewardButton.GetComponent<ClickBtnInteractable>().CancalBtnTrue();
         
      
  
        earned_int*=2;

        total_coins_fail.text = earned_int.ToString();

  

    }

     public void ShowRewardedVideo ()
    {
        
        Ads_Manager.Instance.Show_Iron_source_unity__video("Reward_Respwan");
      
       
    }


    void Level_Enb()
    {

         if(PlayerPrefs.GetInt("currentMode")!=4)
        {
        switch(PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
           
             Instantiate(levelCar[PlayerPrefs.GetInt("level")], levelCar[PlayerPrefs.GetInt("level")].transform.position, 
             levelCar[PlayerPrefs.GetInt("level")].transform.rotation);
            break;
            case 2:
             Instantiate(levelAdvanceMode[PlayerPrefs.GetInt("level")], levelAdvanceMode[PlayerPrefs.GetInt("level")] .transform.position, 
             levelAdvanceMode[PlayerPrefs.GetInt("level")].transform.rotation);
          
            break;
            case 3:
             Instantiate(levelProMode[PlayerPrefs.GetInt("level")], levelProMode[PlayerPrefs.GetInt("level")] .transform.position, 
             levelProMode[PlayerPrefs.GetInt("level")].transform.rotation);
     
            break;  
                                     
        }
        }

         LoadSceneObjects();


    }


    public void unity_video_rewared_Arcde()
    {
     
         
        Ads_Manager.Instance.Show_Iron_source_unity__video("Reward_Respwan");     
     

    }

    public void unity_video_rewared_Coins_Arcde()
    {


        Ads_Manager.Instance.Show_Iron_source_unity__video("rewared_Arcde");


    }




    public void Respwan_Vehcile()
    {
       Time.timeScale = 1;
     if(PlayerPrefs.GetInt("currentMode") != 5)
     {

        Respwan_panel.SetActive(false);
        LoadSceneObjects();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
        
        fail_col.Total_hits = 0;
        Total_hits_text.text =1.ToString();
        fail_count_text.text = 0.ToString();
     }
     else
     {  
            
              Respwan_panel.SetActive(false);
              GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
              GameObject.FindGameObjectWithTag("Player").transform.position      = Daily_mission_Spwan_point[0].transform.position;
              GameObject.FindGameObjectWithTag("Player").transform.localRotation = Daily_mission_Spwan_point[0].transform.localRotation;

            LoadSceneObjects();
            fail_col.Total_hits = 0;
            Total_hits_text.text =1.ToString();
            fail_count_text.text = 0.ToString();
     }
        
        
    }

    public void Use_Coins_Arcade()
    {
         if(PlayerPrefs.GetInt("cash")>= 5000)
         {
           PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash")-5000);
           Respwan_panel.SetActive(false);
           Respwan_Vehcile();
           Update_pickup_text();
          
         }else
         {
              notenoughcash.SetActive(true);
          
              
         }
         

    }

    public void Show_Respwan_panel_func()
    {
        Respwan_panel.SetActive(true);
        Time.timeScale = 0.0001f;
    }


    public void control_Select_Settting(int Index)
    {
        PlayerPrefs.SetInt("ControlIndex", Index);       
        dontDestroy.instance.sound[1].Play();
    }

}
    


    


