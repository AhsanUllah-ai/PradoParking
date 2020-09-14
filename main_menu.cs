using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class main_menu : MonoBehaviour
{
    public static main_menu instance;
    public GameObject menuPanal, Camera,QuitPanal, Store_panel,NotificationIcon,LanguagePanal;
    public GameObject[] RemoveAd_IAP, Vehicale_IAP, colors_IAP, rims_IAP;
    public GameObject[] pack_panels, ControlUIBtn, inapp_sp_btn, in_app_panels;
    public Text walletAll, ModeNameText,AdvaceMode,ModeCoinsText;
    public Button[] inapp_btns;
    public Image[] tick_controls;
           int  count = 0;
    public List<GameObject> Gamepanals = new List<GameObject>();

    public GameObject view_btn,Camera_orbit,reset_pos;
    bool OnceIAP;

    public GameObject Daily_mission_btn;

    private void Awake()
    {
        instance = this;
       
        if (PlayerPrefs.GetInt("currentMode") == 4)
        {
            PlayerPrefs.SetInt("currentMode", 1);
        }
        if (!PlayerPrefs.HasKey("startReward"))
        {
            PlayerPrefs.SetInt("cash", 100);
            PlayerPrefs.SetInt("startReward", 1);
        }
        if (!PlayerPrefs.HasKey("LanguageIndex"))
        {
            PanalOpen(LanguagePanal);
        }
    }
    void Start()
    {
        if (menuPanal == null)
            Gamepanals.Add(menuPanal);
        PlayerPrefs.SetInt("manuScene_load", PlayerPrefs.GetInt("manuScene_load") + 1);
        if (PlayerPrefs.GetInt("manuScene_load") >= 2)
        {
            PlayerPrefs.SetInt("manuScene_load", 0);
            PopPup();
        }

        Invoke("Cam_F", 1f);
        FBAManager.Instance.SelectContent("Screen_Main_Manu");
        Update_interctable();
        Ads_Manager.Instance.ShowSmallAdmobBanner();
    //    ShowCash();
        walletAll.text = PlayerPrefs.GetInt("cash").ToString();
        Ads_Manager.Instance.HideLargeAdmobBanner();

        if(PlayerPrefs.GetInt("Daily_mission_no")>=5)
        {
            Daily_mission_btn.SetActive(false);
        }
         Cash_assign();
       
    }

    void Cash_assign()
    {   
           a = PlayerPrefs.GetInt("cash");
           walletAll.text = a.ToString();
    }
    public void Cam_F()
    {
        Camera.GetComponent<Animator>().enabled = true;
    }
    int a;
void Counter()
{
        a = System.Convert.ToInt32(walletAll.text); 
    if (a < PlayerPrefs.GetInt("cash") - 200)
    {
            a += 300;
            walletAll.fontSize = 28;
            Invoke("fountStyle", 0.05f);
            
    }
    else
    {
             a = PlayerPrefs.GetInt("cash");
             CancelInvoke("Counter");
             Invoke("animatoreValueSet", 3f);
             walletAll.gameObject.GetComponent<Animator>().SetInteger("Coinscounted", 1);
            dontDestroy.instance.sound[8].Stop();
    }
        walletAll.text = a.ToString();
        if (a > 3000) NotificationIcon.SetActive(true); else NotificationIcon.SetActive(false);
    }
      void fountStyle()
      {
        walletAll.fontSize = 20;
        
    }
    public void ShowCash()
    {
        dontDestroy.instance.sound[8].Play();
        InvokeRepeating("Counter", 0.1f, 0.05f);
    }

    void animatoreValueSet()
    {
            walletAll.gameObject.GetComponent<Animator>().SetInteger("Coinscounted", 2);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton();
        }
    }
    public void play()
    {
        GameAnalytics.instance.UnityCustomEvent("PlayButton");
        FBAManager.Instance.SelectContent("Screen_Mode_Selection");
    }
    public void more_by_us()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Spartans+Global+INC.");
        Analytics.CustomEvent("MoreGames");

    }
    public void exit()
    {
        Analytics.CustomEvent("exitgame");
        FBAManager.Instance.SelectContent("Screen_Quit_Panal");
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowLargeAdmobBanner();
    }
    public void exit_panal_yes()
    {
        Ads_Manager.Instance.HideLargeOnRemoveAd();
        Application.Quit();
    }
    public void watchADD()
    {
        pack_pp();
        if (Grage_Manager.instance.Gamepanals.Count > 2)// store sy jab get coins wala panal open hot hy us sy IAP panal par jaty hn tu tab get coin pala panal remove
            Grage_Manager.instance.Gamepanals.RemoveAt(Grage_Manager.instance.Gamepanals.Count - 2);
            Ads_Manager.Instance.HideSmallAdmobBanner();
    }
    int index;
    void PopPup()
    {

        index = Random.RandomRange(0, pack_panels.Length);
        if ( PlayerPrefs.GetInt("manuScene_load") == 0 || count >= 3)
        {
            pack_panels[index].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("purchased_all_parado") != 1 && index == 0)
                PanalOpen(pack_panels[index]);
            if (PlayerPrefs.GetInt("purchased_all_paints") != 1 && index == 2)
                PanalOpen(pack_panels[index]);
            if (PlayerPrefs.GetInt("purchased_all_rims") != 1 && index == 1)
                 PanalOpen(pack_panels[index]);
            //Invoke("CrossPackage",1f);
            if (count >=3)
            {
                count = 0;
            }
        }
    }
    void CrossPackage()
    {
        pack_panels[index].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
    public void pack_pp()
    {
        if (!OnceIAP)
        {
            OnceIAP = true;
               count++;
            if (count == 3)
            {
                Invoke("PopPup", 0.3f);
            }
        }
    }
    public void WatchVideo()
    {
       Ads_Manager.Instance.Show_Iron_source_unity__video("FreeCoins");
    }
    public void Update_interctable()
    {
        if (PlayerPrefs.GetInt("purchased_all_parado") == 1)
        {    for (int i = 0; i < Vehicale_IAP.Length; i++)
            {
                Vehicale_IAP[i].GetComponent<Button>().interactable = false;
            }
        }
        if (PlayerPrefs.GetInt("purchased_all_paints") == 1)
        {
            for (int i = 0; i < colors_IAP.Length; i++)
            {
                colors_IAP[i].GetComponent<Button>().interactable = false;
            }
        }
        if (PlayerPrefs.GetInt("purchased_all_rims") == 1)
        {
            for (int i = 0; i < rims_IAP.Length; i++)
            {
                rims_IAP[i].GetComponent<Button>().interactable = false;
            }
        }
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 1)
        {
            for (int i = 0; i < RemoveAd_IAP.Length; i++)
            {
                RemoveAd_IAP[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void Inapp_Btn_f(int index)
    {
        in_app_panels[index].SetActive(true);
        int current_p = index;

        if (index == 1)
        {
            Update_interctable();
        }

        if (current_p == 0)
        {

            in_app_panels[0].SetActive(true);
            in_app_panels[1].SetActive(false);
            in_app_panels[2].SetActive(false);
            //
            inapp_sp_btn[0].SetActive(false);
            inapp_sp_btn[1].SetActive(true);
            inapp_sp_btn[2].SetActive(true);


        }
        else if (current_p == 1)
        {
            in_app_panels[0].SetActive(false);
            in_app_panels[1].SetActive(true);
            in_app_panels[2].SetActive(false);
            //
            inapp_sp_btn[0].SetActive(true);
            inapp_sp_btn[1].SetActive(false);
            inapp_sp_btn[2].SetActive(true);
        }
        else if (current_p == 2)
        {
            in_app_panels[0].SetActive(false);
            in_app_panels[1].SetActive(false);
            in_app_panels[2].SetActive(true);
            //
            inapp_sp_btn[0].SetActive(true);
            inapp_sp_btn[1].SetActive(true);
            inapp_sp_btn[2].SetActive(false);
        }


        dontDestroy.instance.sound[0].Play();
    }
    public void Pack_Exit(int index)
    {
        pack_panels[index].SetActive(false);
        dontDestroy.instance.sound[1].Play();
        
    }

    
    public void control_Select_Settting(int Index)
    {
        PlayerPrefs.SetInt("ControlIndex", Index);
        ControlActiveUIEffict();
        dontDestroy.instance.sound[1].Play();
    }
    void ControlActiveUIEffict()
    {

        for (int i = 0; i < ControlUIBtn.Length; i++)
        {

            if (i == PlayerPrefs.GetInt("ControlIndex"))
            {
                ControlUIBtn[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                ControlUIBtn[i].GetComponent<Image>().color = new Color(0.8f, 1, 0, 1);

            }
            else
            {
                ControlUIBtn[i].transform.localScale = new Vector3(1, 1, 1);
                ControlUIBtn[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void PanalOpen(GameObject CurrentPanal)
    {

        if (LevelSelection_Manager.Arcadehit == 2)
        { CurrentPanal = LevelSelection_Manager.instance.ArcadeHitPanal; LevelSelection_Manager.Arcadehit = 0; }
        if (CurrentPanal != Gamepanals[Gamepanals.Count - 1])
        {
            Gamepanals[Gamepanals.Count - 1].SetActive(false);
            CurrentPanal.SetActive(true);
            Gamepanals.Add(CurrentPanal);
            if (CurrentPanal.name == "Control Panal")
            {
                ControlActiveUIEffict();
            }
        }
        dontDestroy.instance.sound[0].Play();
    }
    public void backButton()
    {
        if (Camera_orbit.GetComponent<OrbitCamera>().enabled == true)
        {
            veiw_btn_back();
            PanalExit();
            return;
        }
        if (Grage_Manager.instance.Gamepanals.Count > 1)
        {
            Grage_Manager.instance.PanalExit();
            dontDestroy.instance.sound[1].Play();
        }
        else
        {
                PanalExit();
        }
    }
    public void PanalExit()
    {
        if (Gamepanals.Count == 1)
        {
            exit();//FOR AD
            PanalOpen(QuitPanal);
        }
        else
        {
            if(QuitPanal == Gamepanals[Gamepanals.Count - 1])
            {
                Ads_Manager.Instance.ShowSmallAdmobBanner();
                Ads_Manager.Instance.HideLargeAdmobBanner();
            }
            if (Store_panel == Gamepanals[Gamepanals.Count - 1])
            {
                Ads_Manager.Instance.ShowSmallAdmobBanner();
            }
            Gamepanals[Gamepanals.Count - 2].SetActive(true);
            Gamepanals[Gamepanals.Count - 1].SetActive(false);
            Gamepanals.RemoveAt(Gamepanals.Count - 1);
        }
        if(OnceIAP)
         OnceIAP = false;
        if (Gamepanals.Count == 1)
            Grage_Manager.instance.FristVehical();
          dontDestroy.instance.sound[1].Play();
    }
    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
        dontDestroy.instance.sound[0].Play();
    }

    public void veiw_btn()
    {
        FadeIn.instance.Start_manul();
        Camera_orbit.GetComponent<OrbitCamera>().enabled = true;
        PanalOpen(view_btn);
    }

    public void veiw_btn_back()
    {
          Camera_orbit.GetComponent<OrbitCamera>().enabled = false;
          Camera_orbit.transform.position = reset_pos.transform.position;
          Camera_orbit.transform.rotation = reset_pos.transform.rotation;
    }

    public void show_Rewared_Admob()
    {
        Ads_Manager.Instance.ShowRewardBaseAd();
    }
}