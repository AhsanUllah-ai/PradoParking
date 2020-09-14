using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class complete_manager : MonoBehaviour 
{
    public static complete_manager instance;
    public GameObject complete, doubleRewardText, doubleRewardButton,DisableReward;
      Dictionary<string, object> levelDict = new Dictionary<string, object>();
    public Animator ArrowAnimator,BoderAnimator;


    public GameObject panel_small_ads;

    public GameObject hand;

    public GameObject Congrats_panel;

    public GameObject Loading_panel;

    public Text level_no_text;
    public Text col_counter_text;

    public Text total_coins;

    float earned_int,SubCash,SpeedCounter;

    public Image Star_slider;
    public Text  Star_text;

    public GameObject[]  congrats_text;

    public GameObject Daily_mission_panel;

    public Text daily_mission_text,daily_mission_reward;

    void Start()
    {
        instance = this;

        Time.timeScale = 1;
        earned_int = 0;
        SubCash = 0;
        
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
           if(PlayerPrefs.GetInt("currentMode")!=5)
           {
            if (PlayerPrefs.GetInt("level") == 4 || PlayerPrefs.GetInt("level") == 9 ||
                PlayerPrefs.GetInt("level") == 14 || PlayerPrefs.GetInt("level") == 19 || PlayerPrefs.GetInt("level") == 24 || PlayerPrefs.GetInt("level") == 29)
            {
                Congrats_panel.SetActive(false);
                Ads_Manager.Instance.HideLargeAdmobBanner();
                panel_small_ads.SetActive(true);
            }
            else
            {
                
                    Congrats_panel.SetActive(true);
                    congrats_text[Random.Range(0,3)].SetActive(true);
                    Invoke("next_stat", 2f);

                }

        }else
        {
             Invoke("next_stat",2f);
        }

        }
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 500);

        GameAnalytics.instance.UnityCustomEvent("Level_Complete" , (PlayerPrefs.GetInt("level") + 1));

        if (PlayerPrefs.GetInt("currentMode") == 1 && PlayerPrefs.GetInt("level") == 29)
        {
            PlayerPrefs.SetInt("Unlocked_All_Cars_Levels", 1);
        }


        if (PlayerPrefs.GetInt("currentMode") == 2 && PlayerPrefs.GetInt("level") == 29)
        {

            PlayerPrefs.SetInt("Unlocked_All_Cones_Levels", 1);
        }


        if (PlayerPrefs.GetInt("currentMode") == 3 && PlayerPrefs.GetInt("level") == 29)
        {

            PlayerPrefs.SetInt("Unlocked_All_Blocks_Levels", 1);
        }


        if(PlayerPrefs.GetInt("level")>2)
        {
            hand.SetActive(false);
        }

        FBAManager.Instance.levelComplete("M" + PlayerPrefs.GetInt("currentMode") + "Lvl" + PlayerPrefs.GetInt("level"));


        //Level complete data

          int Level = PlayerPrefs.GetInt("level");
               if(Level==0)
               {
                  Level=1;
               }else
               {
                   Level+=1;
               }
        

               level_no_text.text = Level.ToString();
        if(PlayerPrefs.GetInt("Col_counter")==0)
        {
             PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 100);
             col_counter_text.text = "100".ToString();
             earned_int = 100;
           
             Star_slider.fillAmount = 1f;
            Star_text.text = 3.ToString();
             

        }
        else  if(PlayerPrefs.GetInt("Col_counter")==1)
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 50);
            col_counter_text.text = "50".ToString();
            earned_int = 50;
            Star_slider.fillAmount = 0.607f;
            Star_text.text = 2.ToString();

        } else  if(PlayerPrefs.GetInt("Col_counter")==2)
        {
             PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 25);
             col_counter_text.text = "0".ToString();
             earned_int = 0;
             Star_slider.fillAmount = 0.255f;
             Star_text.text = 1.ToString();
        }else
        {

        }
            earned_int+=500;
      
            SpeedCounter = 0.15f;
        doubleRewardText.GetComponent<Text>().text = (earned_int*2).ToString();
    }
    public void Update_text_coins()
    {
        earned_int = earned_int*2;
        doubleRewardText.GetComponent<Text>().text = (earned_int).ToString();
        doubleRewardText.GetComponent<Animator>().enabled = true;
        InvokeRepeating("Counter",0.2f, SpeedCounter);
        dontDestroy.instance.sound[8].Play();
    }

    
    
    void Counter()
    {
        if (SubCash < earned_int-50) {
            SubCash += 50;
           total_coins.fontSize = 35;
            Invoke("fountStyle", 0.05f);
        }
        else
        {
            SubCash = earned_int;
            CancelInvoke("Counter");
            total_coins.gameObject.GetComponent<Animator>().SetInteger("Coinscounted", 1);
            dontDestroy.instance.sound[8].Stop();
        }
        total_coins.text = SubCash.ToString();

     
    } 
    void fountStyle()
    {
        total_coins.fontSize = 28;
    }
    public void next_stat()
    {
            Congrats_panel.SetActive(false);
          if(PlayerPrefs.GetInt("currentMode")!=5)
          {
           if (PlayerPrefs.GetInt("level") != 4)
           {
         
            Ads_Manager.Instance.HideSmallAdmobBanner();
           
             if(PlayerPrefs.GetInt("AdCount_comp")==0)
             {
               

             }else if(PlayerPrefs.GetInt("AdCount_comp")<=9 && PlayerPrefs.GetInt("AdCount_comp")!=0) {

               Ads_Manager.Instance.Show_Admob_Unity_Ad_Iron_Source();
             
             }else 
             {

               Ads_Manager.Instance.Show_Unity_Iron_sorce_Admob();
               
             }

            PlayerPrefs.SetInt("AdCount_comp",PlayerPrefs.GetInt("AdCount_comp")+1);
            
            
            complete.SetActive(true);

            Ads_Manager.Instance.ShowLargeAdmobBanner();
            dontDestroy.instance.sound[8].Play();

            }

          }
          else
          {
                         PlayerPrefs.SetInt("Daily_mission_no",PlayerPrefs.GetInt("Daily_mission_no")+1);
                        int daily_r;
                        if(PlayerPrefs.GetInt("Daily_mission_no")==1)
                        {
                             PlayerPrefs.SetInt("Daily_M_reward",3000); 
                             int R = PlayerPrefs.GetInt("Daily_M_reward");
                             PlayerPrefs.SetInt("cash",PlayerPrefs.GetInt("cash")+R);
                           

            }    
                        else  if(PlayerPrefs.GetInt("Daily_mission_no")==2)
                        {
                               PlayerPrefs.SetInt("Daily_M_reward",5000); 
                               int R = PlayerPrefs.GetInt("Daily_M_reward");
                               PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + R);



            } else  if(PlayerPrefs.GetInt("Daily_mission_no")==3)
                        {
                             PlayerPrefs.SetInt("Daily_M_reward",10000); 
                               int R = PlayerPrefs.GetInt("Daily_M_reward");
                           PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + R);


            }
            else  if(PlayerPrefs.GetInt("Daily_mission_no")==3)
                        {
                             PlayerPrefs.SetInt("Daily_M_reward",25000); 
                               int R = PlayerPrefs.GetInt("Daily_M_reward");
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + R);

            }
                       
                        Daily_mission_panel.SetActive(true);
                        daily_r = PlayerPrefs.GetInt("Daily_M_reward");
                        daily_mission_reward.text =daily_r.ToString();
                        PlayerPrefs.SetInt("Daily_mission_active",1);       
                        if(PlayerPrefs.GetInt("Daily_mission_no")!=0)
                        daily_mission_text.text = PlayerPrefs.GetInt("Daily_mission_no").ToString();
                     
                          Ads_Manager.Instance.Show_Unity_Iron_sorce_Admob();
                         Ads_Manager.Instance.ShowLargeAdmobBanner();

          }

            InvokeRepeating("Counter", 0.2f, SpeedCounter);
            



    }


    public void Dismiss()
    {
        InvokeRepeating("Counter", 0.2f, SpeedCounter);
        panel_small_ads.SetActive(false);
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowLargeAdmobBanner();
        Ads_Manager.Instance.Show_Admob_Unity_Ad_Iron_Source();
        complete.SetActive(true);
        dontDestroy.instance.sound[8].Play();
    }
    public void next()
    {
       
	    Loading_panel.SetActive(true);
        dontDestroy.instance.sound[8].Stop();
        dontDestroy.instance.sound[0].Play();
        int level_played = PlayerPrefs.GetInt("level");
        level_played++;
        PlayerPrefs.SetInt("funnelLevelNumber", PlayerPrefs.GetInt("funnelLevelNumber")+1);
        PlayerPrefs.SetInt("level", level_played);
        
        UnlockNextLevel_next(level_played+1);
        PlayerPrefs.Save();
        switch(PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
                if(level_played >= PlayerPrefs.GetInt("availableCarLevels"))
                {
                     SceneManager.LoadScene("main_menu");
                     PlayerPrefs.SetInt("ModePanalOpen", 1);

                }
                else
                {
                    SceneManager.LoadScene("Gameplay");
                }
            break;
            case 2:
                if(level_played >= PlayerPrefs.GetInt("availableConeLevels"))
                {
                    SceneManager.LoadScene("main_menu");
                    PlayerPrefs.SetInt("ModePanalOpen", 1);
                    

                }
                else
                {
                    SceneManager.LoadScene("Gameplay");
                }
            break;
            case 3:
                if(level_played >= PlayerPrefs.GetInt("availableBlockLevels"))
                {
                    SceneManager.LoadScene("main_menu");
                    PlayerPrefs.SetInt("ModePanalOpen", 1);
                    
                }
                else
                {
                    SceneManager.LoadScene("Gameplay");
                }
            break;                         
        }  
		Ads_Manager.Instance.HideLargeAdmobBanner ();
		Ads_Manager.Instance.HideSmallAdmobBanner ();
       

        if (PlayerPrefs.GetInt("level") == 29)
        {
            PlayerPrefs.SetInt("mode_bool", 1);
        }
       
        
        
    }
    public void restart()
    {
        Loading_panel.SetActive(true);
        dontDestroy.instance.sound[0].Play();
        SceneManager.LoadScene("loading");
		Ads_Manager.Instance.HideLargeAdmobBanner ();
       
       

    }

  
    public void main_menu_func()
    {
       
        int level_played = PlayerPrefs.GetInt("level");
        level_played++;
        if(PlayerPrefs.GetInt("level")<29)
        PlayerPrefs.SetInt("level", level_played);
        UnlockNextLevel_next(level_played);
        SceneManager.LoadScene("main_menu");
		Ads_Manager.Instance.HideSmallAdmobBanner ();
		Ads_Manager.Instance.HideLargeAdmobBanner ();
        dontDestroy.instance.sound[0].Play();
        dontDestroy.instance.sound[8].Stop();


    }

    public void daily_mission_next ()
    {
      PlayerPrefs.SetInt("ModePanalOpen", 1);
      dontDestroy.instance.sound[8].Stop();
      SceneManager.LoadScene("main_menu");

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
    public void ShowRewardedVideo ()
    {
        total_coins.gameObject.GetComponent<Animator>().SetInteger("Coinscounted", 2);
        Ads_Manager.Instance.Show_Iron_source_unity__video("Complete2X");
    }
    public void DoubleReward ()
    {
        doubleRewardButton.GetComponent<ClickBtnInteractable>().CancalBtnTrue();
        ArrowAnimator.Play("SkipLevel", -1, 0);
        ArrowAnimator.enabled = false;
        BoderAnimator.enabled = false;
        BoderAnimator.gameObject.SetActive(false);
        int a = System.Convert.ToInt32(earned_int);
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash")+ a);
        PlayerPrefs.Save();
        doubleRewardText.SetActive(true);
        doubleRewardButton.GetComponent<Button>().interactable = false;
        doubleRewardButton.GetComponent<Animator>().enabled = false;
        Update_text_coins();
        Invoke("Disable",2f);
    }

     void Disable()
    {
        DisableReward.SetActive(true);
    }
    public void Back_panel_ads()
    {
        panel_small_ads.SetActive(false);
    }
    
    
}

