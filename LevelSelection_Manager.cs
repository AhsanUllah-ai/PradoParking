using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite LevelUnlock, LevelLock,CurrentUnlockLevel;
    public GameObject Level_IAP,ArcadeHitPanal,NotEnoughtPanal,ModeSelectionPanal,NoicePanl, LevelBouncePanal;
    public Scrollbar LevelSelection;
    public GameObject [] ModeLockBtn,ModeBtn, ModeEffict, Levelbonuses;
    public Text[] Mode_level;
    public Image []Mode_greenFill;
    public List<GameObject> LevelBtn, LevelLockBtn =  new List<GameObject>();
    int UnlockLevelNum, ModeUnlockprice;
    string ModeName;
    public static int Arcadehit = 0;
    public static LevelSelection_Manager instance;
    public Text Arcade_high_Score, LevelBounce;
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("availableCarLevels")){
            PlayerPrefs.SetInt("availableCarLevels", 30);
            PlayerPrefs.SetInt("availableConeLevels", 30);
            PlayerPrefs.SetInt("availableBlockLevels", 30);
            PlayerPrefs.SetInt("GetLevelBouns", 0);
        }

        
    }
    void Start()
    {
        ModeLevelFill();
        CheckModeUnlock();
        Arcade_high_Score.text = PlayerPrefs.GetInt("p-counter").ToString();
    }
    void ModeLevelFill()
    {
       
        Mode_level[0].text = PlayerPrefs.GetInt("CarLevelUnlocked").ToString();
        Mode_greenFill[0].fillAmount = PlayerPrefs.GetInt("CarLevelUnlocked") / 29.0f;
        Mode_level[1].text = PlayerPrefs.GetInt("ConeLevelUnlocked").ToString();
        Mode_greenFill[1].fillAmount = PlayerPrefs.GetInt("ConeLevelUnlocked") / 29.0f;
        Mode_level[2].text = PlayerPrefs.GetInt("BlockLevelUnlocked").ToString();
        Mode_greenFill[2].fillAmount = PlayerPrefs.GetInt("BlockLevelUnlocked") / 29.0f;
    }

     public void Daily_missions()
     {
          PlayerPrefs.SetInt("currentMode", 5);
          SceneManager.LoadScene(2);
     }

    private void GetLevelBtnSprite()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("LevelBtn"))
        {
            LevelBtn.Add(fooObj);
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("LevelLockBtn"))
        {
            LevelLockBtn.Add(fooObj);
        }
    }
    public void CheckModeUnlock()
    {
        int UnlockEffict = 0;
        if (PlayerPrefs.GetInt("ModePanalOpen") == 1)
        {
            main_menu.instance.PanalOpen(ModeSelectionPanal);
            PlayerPrefs.SetInt("ModePanalOpen", 0);
        }
        if (PlayerPrefs.GetInt("Unlocked_All_Modes") == 1)
        {
            PlayerPrefs.SetInt("lockCone", 1);
            PlayerPrefs.SetInt("lockBlock", 1);
            PlayerPrefs.SetInt("lockArcade", 1);
        }
        if (PlayerPrefs.GetInt("CarLevelUnlocked") == 30)
        {
            PlayerPrefs.SetInt("lockCone", 1);
           
        }
        if (PlayerPrefs.GetInt("ConeLevelUnlocked") == 30)
        {
            PlayerPrefs.SetInt("lockBlock", 1);
        }
        if (PlayerPrefs.GetInt("CarLevelUnlocked") >= 10)
        {
            PlayerPrefs.SetInt("lockArcade", 1);
        }
        if (PlayerPrefs.GetInt("lockCone") == 1)
         {
            ModeLockBtn[0].SetActive(false);
            ModeBtn[0].GetComponent<Button>().interactable = true;
            UnlockEffict = 1;
         }
        if (PlayerPrefs.GetInt("lockBlock") == 1)
        {
            ModeLockBtn[1].SetActive(false);
            ModeBtn[1].GetComponent<Button>().interactable = true;
            UnlockEffict = 2;
        }
        if (PlayerPrefs.GetInt("lockArcade") == 1)
        {
            ModeLockBtn[2].SetActive(false);
            ModeBtn[2].GetComponent<Button>().interactable = true;
            UnlockEffict = 3;
        }
        for (int i = 0; i < ModeEffict.Length; i++)
        {
            if (i == UnlockEffict)
                ModeEffict[i].SetActive(true);
            else
                ModeEffict[i].SetActive(false);
        }
       
    }
    public void ModeSelection(int ModeIndex)
    {
        PlayerPrefs.SetInt("currentMode", ModeIndex);

        if (ModeIndex != 4) {
            CheckLevelUnLock();
            Invoke("Scroll_Update",0.02f);
        }
        else
        {
           if (PlayerPrefs.GetInt("Arcade_Mode_Hits") != 1)
            {
                Arcadehit++;
            }
            FBAManager.Instance.SelectContent("Screen_Vehcile_Selection");
        }
        FBAManager.Instance.SelectContent("Screen_Level_Selection");
    }
    public void LockedModeBtn(int Index)
    {
        if (Index == 1)
        {
            ModeUnlockprice = 10000;
            ModeName = "Starter Mode";
            main_menu.instance.AdvaceMode.gameObject.SetActive(true);
            main_menu.instance.ModeNameText.gameObject.SetActive(false);
        }
        else if (Index == 2)
        {
            ModeUnlockprice = 15000;
            ModeName = "Advance Parking";
            main_menu.instance.ModeNameText.gameObject.SetActive(true);
            main_menu.instance.AdvaceMode.gameObject.SetActive(false);
        }
        else if (Index == 3)
        {
            Grage_Manager.instance.CurrentUnlockVehical();
           // main_menu.instance.free_lock_mode_panal.SetActive(true);
            //ModeUnlockprice = 0;
            //ModeName = "'Arcade mode'";
            return;
        }
        if (PlayerPrefs.GetInt("cash") >= ModeUnlockprice)
        {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - ModeUnlockprice);
            if (Index == 1)
                PlayerPrefs.SetInt("lockCone", 1);
            else if (Index == 2)
                PlayerPrefs.SetInt("lockBlock", 1);
            else if (Index == 3)
                PlayerPrefs.SetInt("lockArcade", 1);

            main_menu.instance.ShowCash();
        }
        else
        {
                
                main_menu.instance.ModeCoinsText.text = ModeUnlockprice.ToString();
                Grage_Manager.instance.PanalOpen(NotEnoughtPanal);
        }
        CheckModeUnlock();
    }
    public void CheckLevelUnLock()
    {
        switch (PlayerPrefs.GetInt("currentMode"))
        {
            case 1:
               
                UnlockLevelNum = PlayerPrefs.GetInt("CarLevelUnlocked");
                
                if (PlayerPrefs.GetInt("Unlocked_All_Cars_Levels") == 1)
                {
                    UnlockLevelNum = 29;
                    PlayerPrefs.SetInt("CarLevelUnlocked", 29);
                    Level_IAP.SetActive(false);
                }else
                    Level_IAP.SetActive(true);
                break;
            case 2:
                UnlockLevelNum = PlayerPrefs.GetInt("ConeLevelUnlocked");
                if (PlayerPrefs.GetInt("Unlocked_All_Cones_Levels") == 1) { 
                    UnlockLevelNum = 29;
                    PlayerPrefs.SetInt("ConeLevelUnlocked", 29);
                    Level_IAP.SetActive(false);
                }else
                    Level_IAP.SetActive(true);
                break;
            case 3:
                UnlockLevelNum = PlayerPrefs.GetInt("BlockLevelUnlocked");
                if (PlayerPrefs.GetInt("Unlocked_All_Blocks_Levels") == 1) { 
                    UnlockLevelNum = 29;
                    PlayerPrefs.SetInt("BlockLevelUnlocked", 29);
                    Level_IAP.SetActive(false);
                }
                else
                    Level_IAP.SetActive(true);
                break;
        }

        foreach (GameObject Lock in LevelLockBtn)
        {
            if (UnlockLevelNum >= LevelLockBtn.IndexOf(Lock))
            {
                LevelBtn[LevelLockBtn.IndexOf(Lock)].gameObject.GetComponent<Button>().enabled = false;
                Lock.gameObject.GetComponent<Button>().interactable = true;
                LevelBtn[LevelLockBtn.IndexOf(Lock)].GetComponent<Image>().sprite = LevelUnlock;
                LevelBtn[LevelLockBtn.IndexOf(Lock)].GetComponent<Image>().color = Color.white;
                LevelLockBtn[LevelLockBtn.IndexOf(Lock)].transform.GetChild(2).GetComponent<Text>().color = Color.white;
            }
            else
            {
                LevelBtn[LevelLockBtn.IndexOf(Lock)].gameObject.GetComponent<Button>().enabled = true;
                Lock.gameObject.GetComponent<Button>().interactable = false;
                LevelBtn[LevelLockBtn.IndexOf(Lock)].GetComponent<Image>().sprite = LevelLock;
                LevelBtn[LevelLockBtn.IndexOf(Lock)].GetComponent<Image>().color = Color.white;
                LevelLockBtn[LevelLockBtn.IndexOf(Lock)].transform.GetChild(2).GetComponent<Text>().color = Color.black;
            }
            LevelLockBtn[LevelLockBtn.IndexOf(Lock)].transform.GetChild(0).gameObject.SetActive(false);
        }
        LevelBtn[UnlockLevelNum].GetComponent<Image>().sprite = CurrentUnlockLevel;
        LevelBtn[UnlockLevelNum].GetComponent<Image>().color = Color.green;
        LevelLockBtn[UnlockLevelNum].transform.GetChild(0).gameObject.SetActive(true);
        LevelLockBtn[UnlockLevelNum].transform.GetChild(2).GetComponent<Text>().color = Color.black;
        ModeLevelFill();
        CheckModeUnlock();//if purchase all level if user back 
      
            levelReward();
        
    }

    void Scroll_Update()
    {
         if(UnlockLevelNum>4)
        LevelSelection.value = UnlockLevelNum / 30.0f;
        else
            LevelSelection.value = 0.0f;
    }
    public void LevelReward(int coins)
    {
        LevelBounce.text = coins.ToString();
        LevelBouncePanal.SetActive(true);
      
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + coins);
      
    }
    public void claimFun()
    {
        LevelBouncePanal.SetActive(false);
        main_menu.instance.ShowCash();
        levelReward();
    }
 public void LevelRewarded(int index)
    {
        
            PlayerPrefs.SetInt(index.ToString() + PlayerPrefs.GetInt("currentMode"), 1);
            Levelbonuses[index].GetComponent<Animator>().SetInteger("LevelBounce", 2);
            Levelbonuses[index].GetComponent<Button>().interactable = false;
            Levelbonuses[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.35f);

    }
    void levelReward()
    {
        if (UnlockLevelNum >= 3)
        {
           if(PlayerPrefs.GetInt("0" + PlayerPrefs.GetInt("currentMode")) != 1)
            {
                Levelbonuses[0].GetComponent<Animator>().SetInteger("LevelBounce", 1);
                Levelbonuses[0].GetComponent<Button>().interactable = true;
                Levelbonuses[0].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
        if (UnlockLevelNum >= 7)
        {
            if (PlayerPrefs.GetInt("1" + PlayerPrefs.GetInt("currentMode")) != 1)
            {
                Levelbonuses[1].GetComponent<Animator>().SetInteger("LevelBounce", 1);
                Levelbonuses[1].GetComponent<Button>().interactable = true;
                Levelbonuses[1].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
        if (UnlockLevelNum >= 13)
        {
         
            if (PlayerPrefs.GetInt("2" + PlayerPrefs.GetInt("currentMode")) != 1)
            {
                Levelbonuses[2].GetComponent<Animator>().SetInteger("LevelBounce", 1);
                Levelbonuses[2].GetComponent<Button>().interactable = true;
                Levelbonuses[2].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
        if (UnlockLevelNum >= 19)
        {
  
            if (PlayerPrefs.GetInt("3" + PlayerPrefs.GetInt("currentMode")) != 1)
            {
                Levelbonuses[3].GetComponent<Animator>().SetInteger("LevelBounce", 1);
                Levelbonuses[3].GetComponent<Button>().interactable = true;
                Levelbonuses[3].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
        if (UnlockLevelNum >= 29)
        {
           if (PlayerPrefs.GetInt("4" + PlayerPrefs.GetInt("currentMode")) != 1)
            {
                Levelbonuses[4].GetComponent<Animator>().SetInteger("LevelBounce", 1);
                Levelbonuses[4].GetComponent<Button>().interactable = true;
                Levelbonuses[4].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void LockedLevelBtn(string Notice)
    {
            NoicePanl.SetActive(true);
        NoicePanl.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Notice;
            Invoke("LockedLevelNotice", 2.5f);
    }
    void LockedLevelNotice()
    {
        NoicePanl.SetActive(false);
    }
    public void LevelSelect(int Index)
    {
        PlayerPrefs.SetInt("level", Index);
        FBAManager.Instance.SelectContent("Screen_Vehcile_Selection");
        Grage_Manager.instance.CurrentUnlockVehical();
    }
}