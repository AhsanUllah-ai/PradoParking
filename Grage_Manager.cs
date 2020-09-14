using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Grage_Manager : MonoBehaviour
{// for color & rim set defult ///
     Image[] FristRimSprite;
     Texture[] FristRimTexture;
     Color[] FristColors;
    public Image[] ColorUI,RimsUI;
    public Sprite[] DefoultRimsSprite;
    /// end color rim ///
    public Material[] RimsMaterial,BodyMaterial;
    public Color[] BodyColors,DefultColor;
    public Texture[] RimsTextures, DefultTexture,DacelsTexture,defultBodyTexture;
    public GameObject[] playerVehicles,RimLock,RimTick,ColorLock,Colortick,DecalsLock,DecalsTick;
    public GameObject PlayBtn,BuyWacthBtn,BuyBtn,LockBtn,NotEnoughCoins,AllColorPanal,AllRimPanal,AllVehicles, AllDecalsPanal,DecalRemovePanal;
    public static int currentVehicle;
    public Text InputText,BuyPriceText;
    public List<GameObject> Gamepanals = new List<GameObject>();
    int BuyPrice,BuyIndex,IApColor,IAPRim,IAPdecal,IAPVehicle = 0;
    string BuyPrefab;
    // Start is called before the first frame update
    public VehiclePlate[] VehiclePlateNo;
    [System.Serializable]
    public class VehiclePlate
    {
    public Text FrontPlate,BackPlate ;
    }
    public static Grage_Manager instance;
    void Start()
    {
        FristRimSprite = RimsUI;
        FristRimTexture = RimsTextures;
        FristColors = BodyColors;
        instance = this;
        IAPdecal = IApColor = IAPRim = IAPVehicle = currentVehicle = 0;
        if (!PlayerPrefs.HasKey("Vehicle Plate" + 0)){
            for (int i = 0; i < playerVehicles.Length; i++) {
                PlayerPrefs.SetInt("Vehicle" + i + "Color" + 0, 1);
                PlayerPrefs.SetInt("Vehicle" + i + "Rim" + 0, 1);
                PlayerPrefs.SetString("Vehicle Plate" + i, "Spartans");
            }
            PlayerPrefs.SetInt("currentVehicle" + currentVehicle, 1);
            PlayerPrefs.SetInt("RimSelected", 0);
        }
    }
    public void CurrentUnlockVehical()//jab bhi grage panal open ho ga first es ko call ho gi main manu sy ho yah level sy ya arcade mode 
    {
        for (int i = 0; i < playerVehicles.Length; i++)
        {
            if (PlayerPrefs.GetInt("currentVehicle" + i) == 1)
            {
                currentVehicle = i;
                for (int s = 0; s < playerVehicles.Length; s++)
                {
                    if (s == currentVehicle)
                        playerVehicles[currentVehicle].SetActive(true);
                    else
                        playerVehicles[s].SetActive(false);
                }
                
            }
        }
        StartVehicleStats();
        CheckVechaleLockStats();
        CheckIAPAll();
    }
    public void FristVehical() // only for back then call 
    {
        for (int i = 0; i < playerVehicles.Length; i++)
        {
            if (i == 0)
            {
                currentVehicle = 0;
                playerVehicles[currentVehicle].SetActive(true);
            }
            else { playerVehicles[i].SetActive(false); }
        }
        
    }
    public void leftVechale()
    {

        if (currentVehicle < playerVehicles.Length-1)
        {
            currentVehicle++;
            playerVehicles[currentVehicle].SetActive(true);
            playerVehicles[currentVehicle - 1].SetActive(false);
        }
        else
        {
            currentVehicle = 0;
            playerVehicles[currentVehicle].SetActive(true);
            playerVehicles[playerVehicles.Length-1].SetActive(false);
        }
        CheckVechaleLockStats();
        IAPVehicle++;
        PanalExit();
        if (IAPVehicle >= 4 && PlayerPrefs.GetInt("purchased_all_parado") != 1)
        {
            PanalOpen(AllVehicles);
            IAPVehicle = 0;
        }
    }
    public void RightVechale()
    {
        if (currentVehicle > 0)
        {
            currentVehicle--;
            playerVehicles[currentVehicle].SetActive(true);
            playerVehicles[currentVehicle + 1].SetActive(false);

        }
        else
        {
            currentVehicle = playerVehicles.Length-1;
            playerVehicles[currentVehicle].SetActive(true);
            playerVehicles[0].SetActive(false);
        }
        CheckVechaleLockStats();
        PanalExit(); // if paint panal or rims panal open therefore close these panal 
    }
    void FristRimColor()//diffrent vehical set rim ui & value Inspactor,set color ui & value Inspactor
    {
        for (int i = 0; i < BodyColors.Length; i++)
        {
            if (BodyColors[i] == DefultColor[currentVehicle])
            {
                BodyColors[i] = FristColors[0];
                ColorUI[i].color = FristColors[0];
            }
            else
            {
                BodyColors[i] = FristColors[i];
                ColorUI[i].color = FristColors[i];
            }
        }
        for (int i = 0; i < RimsTextures.Length; i++)
        {
            if (RimsTextures[i] == DefultTexture[currentVehicle])
            {
                RimsTextures[i] = FristRimTexture[0];
            }
            else
            {
                RimsTextures[i] = FristRimTexture[i];
            }
        }
        for (int i = 0; i < RimsUI.Length; i++)
        {
            if (RimsUI[i].sprite == DefoultRimsSprite[currentVehicle])
            {
                RimsUI[i].sprite = FristRimSprite[0].sprite;
            }
            else
            {
                RimsUI[i].sprite = FristRimSprite[i].sprite;
            }
        }

        ColorUI[0].color = DefultColor[currentVehicle];
        RimsUI[0].sprite = DefoultRimsSprite[currentVehicle];
        RimsTextures[0] = DefultTexture[currentVehicle];
        BodyColors[0] = DefultColor[currentVehicle];
    }
  public  void StartVehicleStats() {
        FristRimColor();//diffrent vehical set rim ui & value Inspactor,set color ui & value Inspactor
        if (PlayerPrefs.GetString("Vehicle Plate" + currentVehicle) != null)
        {
            VehiclePlateNo[currentVehicle].FrontPlate.text = PlayerPrefs.GetString("Vehicle Plate" + currentVehicle);
            VehiclePlateNo[currentVehicle].BackPlate.text = PlayerPrefs.GetString("Vehicle Plate" + currentVehicle);
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Color" + PlayerPrefs.GetInt("ColorSelected")) == 1)
        {
            BodyMaterial[currentVehicle].color = BodyColors[PlayerPrefs.GetInt("ColorSelected")];
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Rim" + PlayerPrefs.GetInt("RimSelected")) == 1)
        {
            RimsMaterial[currentVehicle].mainTexture = RimsTextures[PlayerPrefs.GetInt("RimSelected")];
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Decal" + PlayerPrefs.GetInt("DecalSelected")) == 1 && PlayerPrefs.GetInt("DefultBodyTexture") != 1)
        {
            BodyMaterial[currentVehicle].color = Color.white;
            BodyMaterial[currentVehicle].mainTexture = DacelsTexture[PlayerPrefs.GetInt("DecalSelected")];
        }
        else
        {
            BodyMaterial[currentVehicle].mainTexture = defultBodyTexture[currentVehicle];
        }
        if (PlayerPrefs.GetInt("currentVehicle" + currentVehicle) == 1)
        {
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            PlayBtn.SetActive(true);
        }
    }
    void CheckVechaleLockStats()
    {
        if (PlayerPrefs.GetInt("currentVehicle" + currentVehicle) == 1)
        {
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            LockBtn.SetActive(false);
            PlayBtn.SetActive(true);

        }
        else
        {
            BuyBtn.SetActive(true);
            LockBtn.SetActive(true);
            PlayBtn.SetActive(false);
            int[] price = { 0, 5000, 10000, 15000, 20000, 30000, 40000, 50000, 60000, 70000, 80000 };
            BuyPrice = price[currentVehicle];
            BuyPrefab = "currentVehicle" + currentVehicle.ToString();
            BuyPriceText.text = BuyPrice.ToString();
        }
        
        checkColorLockStats();
        checkDecalLockStats();
        checkRimsLockStats();
        StartVehicleStats();
    }
    void checkColorLockStats()
    {
        for (int i = 0; i < ColorLock.Length; i++)
        {
           if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Color" + i) == 1)
            {
                ColorLock[i].SetActive(false);
            }
            else
            {
                ColorLock[i].SetActive(true);
            }
            
            Colortick[i].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Color" + PlayerPrefs.GetInt("ColorSelected")) == 1)
            Colortick[PlayerPrefs.GetInt("ColorSelected")].SetActive(true);
    }
    void checkDecalLockStats()
    {
        for (int i = 0; i < DecalsLock.Length; i++)
        {
            if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Decal" + i) == 1)
            {
                DecalsLock[i].SetActive(false);
            }
            else
            {
                DecalsLock[i].SetActive(true);
            }

            DecalsTick[i].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Decal" + PlayerPrefs.GetInt("DecalSelected")) == 1)
            DecalsTick[PlayerPrefs.GetInt("DecalSelected")].SetActive(true);
    }
    void checkRimsLockStats()
    {
        for (int i = 0; i < RimLock.Length; i++)
        {
            if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Rim" + i) == 1)
            {
                RimLock[i].SetActive(false);
            }
            else
            {
                RimLock[i].SetActive(true);
            }
            RimTick[i].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Rim" + PlayerPrefs.GetInt("RimSelected")) == 1)
            RimTick[PlayerPrefs.GetInt("RimSelected")].SetActive(true);
       
    }
    public void SelectColor(int index)
    {
        if (BodyMaterial[currentVehicle].mainTexture != defultBodyTexture[currentVehicle])
        {
            PanalOpen(DecalRemovePanal);
            return;
        }
        BodyMaterial[currentVehicle].color = BodyColors[index];
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Color" + index) == 1)
        {
            PlayerPrefs.SetInt("ColorSelected", index);
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            PlayBtn.SetActive(true);
            for (int i = 0; i < Colortick.Length; i++)
            {
                if (i == index)
                Colortick[i].SetActive(true);
            else
                Colortick[i].SetActive(false);
            }
        }
        else
        {
            BuyBtnActive(index);
            PlayBtn.SetActive(false);
            BuyIndex = index;
            BuyPrice = 500;
            BuyPrefab = "Vehicle" + currentVehicle + "Color" + index.ToString();
            BuyPriceText.text = BuyPrice.ToString();
        }
        IApColor++;
        if (IApColor >= 3 && PlayerPrefs.GetInt("purchased_all_paints") != 1)
        {
            IApColor = 0;
            PanalOpen(AllColorPanal);
        }
    }
    void BuyBtnActive(int index)
    {
        if (index == 2 || index == 5 || index == 9)
        {
            BuyWacthBtn.SetActive(true);
            BuyBtn.SetActive(false);
        }
        else
        {
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(true);
        }
    }
    public void SelectRims(int index)
    {
        RimsMaterial[currentVehicle].SetTexture("_MainTex", RimsTextures[index]);
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Rim" + index) == 1)
        {
            PlayerPrefs.SetInt("RimSelected", index);
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            PlayBtn.SetActive(true);
            for (int i = 0; i < RimTick.Length; i++)
            {
                if (i == index)
                    RimTick[i].SetActive(true);
                else
                    RimTick[i].SetActive(false);
            }
        }
        else
        {
            BuyBtnActive(index);
            PlayBtn.SetActive(false);
            BuyIndex = index;
            BuyPrice = 500;
            BuyPrefab = "Vehicle" + currentVehicle + "Rim" + index.ToString();
            BuyPriceText.text = BuyPrice.ToString();
        }
        IAPRim++;
        if (IAPRim >= 3 && PlayerPrefs.GetInt("purchased_all_rims") != 1)
        {
            IAPRim = 0;
            PanalOpen(AllRimPanal);
        }
    }
    public void SelectDecals(int index)
    {
        if(index == -1)
        {
            PlayerPrefs.SetInt("DefultBodyTexture", 1);
            StartVehicleStats();
            return;
        }

        BodyMaterial[currentVehicle].color = Color.white;
        BodyMaterial[currentVehicle].SetTexture("_MainTex", DacelsTexture[index]);
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Decal" + index) == 1)
        {
            PlayerPrefs.SetInt("DefultBodyTexture", 0);
            PlayerPrefs.SetInt("DecalSelected", index);
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            PlayBtn.SetActive(true);
            for (int i = 0; i < DecalsTick.Length; i++)
            {
                if (i == index)
                    DecalsTick[i].SetActive(true);
                else
                    DecalsTick[i].SetActive(false);
            }
        }
        else
        {
            BuyBtnActive(index);
            PlayBtn.SetActive(false);
            BuyIndex = index;
            BuyPrice = 500;
            BuyPrefab = "Vehicle" + currentVehicle + "Decal" + index.ToString();
            BuyPriceText.text = BuyPrice.ToString();
        }
        IAPdecal++;
        if (IAPdecal >= 3 && PlayerPrefs.GetInt("purchased_all_Decals") != 1)
        {
            IAPdecal = 0;
            PanalOpen(AllDecalsPanal);
        }
    }
    public void SetNoPlate()
    {
        if(InputText.text != "")
        {
        VehiclePlateNo[currentVehicle].FrontPlate.text = InputText.text;
        VehiclePlateNo[currentVehicle].BackPlate.text = InputText.text;
        PlayerPrefs.SetString("Vehicle Plate" + currentVehicle, InputText.text);
        }
    }
    public void BuyWatchVideo()
    {
        Ads_Manager.Instance.Show_Iron_source_unity__video("StoreBuy");
    }
    public void BuyBtnFun(string CallerName)
    {
        if (PlayerPrefs.GetInt("cash") >= BuyPrice || CallerName == "WacthVideo")
        {
            if (CallerName != "WacthVideo")
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - BuyPrice);
                main_menu.instance.ShowCash();
            }
            PlayerPrefs.SetInt(BuyPrefab, 1);
            if (BuyPrefab == "Vehicle" + currentVehicle + "Rim" + BuyIndex)
            {
                checkRimsLockStats();
                SelectRims(BuyIndex);
            }
            else
             if (BuyPrefab == "Vehicle" + currentVehicle + "Color" + BuyIndex)
             {
                checkColorLockStats();
                SelectColor(BuyIndex);
             }
            else
             if (BuyPrefab == "Vehicle" + currentVehicle + "Decal" + BuyIndex)
             {
                checkDecalLockStats();
                SelectDecals(BuyIndex);
             }
            BuyBtn.SetActive(false);
            BuyWacthBtn.SetActive(false);
            PlayBtn.SetActive(true);
            LockBtn.SetActive(false);
        }
        else
        {
            PanalOpen(NotEnoughCoins);
        }
    }
    public void CheckIAPAll()
    {
        
        if (PlayerPrefs.GetInt("purchased_all_paints") == 1)
        {
            for (int v = 0; v < playerVehicles.Length; v++)
            {
                for (int i = 0; i < ColorLock.Length; i++)
                {
                    ColorLock[i].SetActive(false);
                    PlayerPrefs.SetInt("Vehicle" + v + "Color" + i, 1);
                }
            }
            BuyBtn.SetActive(false);
            BuyWacthBtn.SetActive(false);
            PlayBtn.SetActive(true);
            LockBtn.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt("purchased_all_rims") == 1)
        {
            for (int v = 0; v < playerVehicles.Length; v++)
            {
                for (int i = 0; i < RimLock.Length; i++)
                {
                    RimLock[i].SetActive(false);
                    PlayerPrefs.SetInt("Vehicle" + v + "Rim" + i, 1);
                }
            }
            BuyBtn.SetActive(false);
            BuyWacthBtn.SetActive(false);
            PlayBtn.SetActive(true);
            LockBtn.SetActive(false);
        }
        if (PlayerPrefs.GetInt("purchased_all_parado") == 1)
        {
            for (int i = 0; i < playerVehicles.Length; i++)
            {
                PlayerPrefs.SetInt("currentVehicle" + i, 1);
            }
            BuyWacthBtn.SetActive(false);
            BuyBtn.SetActive(false);
            PlayBtn.SetActive(true);
            LockBtn.SetActive(false);
        }
     }
    public void PlayLevel()
    {
        if (main_menu.instance.Gamepanals.Count == 2)
        {
            if (PlayerPrefs.GetInt("level") >= 29)
            {
                if (PlayerPrefs.GetInt("currentMode") == 1)
                {
                    PlayerPrefs.SetInt("level", 0);
                    PlayerPrefs.SetInt("currentMode", 2);
                }
               else if (PlayerPrefs.GetInt("currentMode") == 2)
                {
                    PlayerPrefs.SetInt("level", 0);
                    PlayerPrefs.SetInt("currentMode", 3);
                }
                else if (PlayerPrefs.GetInt("currentMode") == 3)
                {
                    PlayerPrefs.SetInt("currentMode", 4);
                }
            }
                if (!PlayerPrefs.HasKey("level"))
                {
                    PlayerPrefs.SetInt("level", 0);
                    PlayerPrefs.SetInt("currentMode", 1);
                }
        }
        
        SceneManager.LoadScene(2);
        FBAManager.Instance.SelectContent("Screen_Loading");
    }
    public void PanalOpen(GameObject CurrentPanal)
    {
        CurrentPanal.SetActive(true);
        Gamepanals[Gamepanals.Count - 1].SetActive(false);
        Gamepanals.Add(CurrentPanal);
        dontDestroy.instance.sound[0].Play();
    }
        public void PanalExit()
    {
        if (Gamepanals.Count > 1)
        {
            if (main_menu.instance.Store_panel == Gamepanals[Gamepanals.Count - 1])
            {
                Ads_Manager.Instance.ShowSmallAdmobBanner();
            }
            Gamepanals[Gamepanals.Count - 2].SetActive(true);
            Gamepanals[Gamepanals.Count - 1].SetActive(false);
            Gamepanals.RemoveAt(Gamepanals.Count - 1);
            StartVehicleStats();
        }
        dontDestroy.instance.sound[0].Play();
    }
}