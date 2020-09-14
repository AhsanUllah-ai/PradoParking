using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
public class Ads_Manager : MonoBehaviour
{
    public static Ads_Manager Instance;
    public string appID, bannerID, interstitialID, unityAdID;
    public AdPosition bannerSmallPosition;
    public AdPosition bannerLargePosition;
    // public bool showBannerOnStart;
    [SerializeField]
    private bool enableTestMode;
    //	[Header("Rewarded Video")]
    //	public string rewardedVideoID;
    private BannerView smallbannerView;
    private BannerView largebannerView;
    private InterstitialAd interstitial;
    //private RewardBasedVideoAd rewardBasedVideo;
    private bool SmallBannerOnceLoaded;
    private bool LargeBannerOnceLoaded;
    private bool isInternet = false;
    private bool isAdInitialized = false;
    public static bool small_bann_bool = false;
    //IronSourceKey***********
    public static string uniqueUserId = "DefaultInterstitial";
    public static string appKey = "a7f24595";
  //  public static string appKey = "a5cc9425";
    GameObject InitText;
    GameObject LoadButton;
    GameObject LoadText;
    GameObject ShowButton;
    GameObject ShowText;
    public static String INTERSTITIAL_INSTANCE_ID = "0";
    GameObject InitText2;
    GameObject ShowButton2;
    GameObject ShowText2;
    GameObject AmountText;
    int userTotalCredits = 0;
    public static String REWARDED_INSTANCE_ID = "0";
    private RewardBasedVideoAd rewardBasedVideoAd;
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
   
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        LoadDemandOnlyInterstitial();
    }
    private bool CheckInitialization()
    {
        if (isAdInitialized)
        {
            isAdInitialized = true;
            return isAdInitialized;
        }
        else
        {
            isAdInitialized = false;
            InitializeAds();
            return false;
        }

    }
    public bool IsInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            isInternet = true;
        }
        else
            isInternet = false;

        return isInternet;
    }
    private void Start()
    {
        PlayerPrefs.SetInt("purcahsed_color", 1);
        if (!PlayerPrefs.HasKey("manuScene_load"))
            PlayerPrefs.SetInt("manuScene_load", 0);
        if (enableTestMode)
        {
            //test ids
            bannerID = "ca-app-pub-3940256099942544/6300978111";
            interstitialID = "ca-app-pub-3940256099942544/1033173712";
        }
          rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        SmallBannerOnceLoaded = false;
        LargeBannerOnceLoaded = false;
        if (IsInternetConnection())
        {
            InitializeAds();
        }
        else
            isAdInitialized = false;
        // Invoke("SetFalse",1);

        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        string id = IronSource.Agent.getAdvertiserId();
        IronSource.Agent.validateIntegration();

        // SDK init
        IronSource.Agent.init(appKey);
        LoadInterstitialButtonClicked();

          //Admob Rewareded

        rewardBasedVideoAd.OnAdClosed              +=  HandleOnAdClosed;
        rewardBasedVideoAd.OnAdFailedToLoad        +=  HandleOnAdFailedToLoad;
        rewardBasedVideoAd.OnAdLeavingApplication  +=  HandleOnAdLeavingApplication;
        rewardBasedVideoAd.OnAdLoaded              +=  HandleOnAdLoaded;
        rewardBasedVideoAd.OnAdOpening   +=    HandleOnAdOpening;
        rewardBasedVideoAd.OnAdRewarded  +=    HandleOnAdRewarded;
        rewardBasedVideoAd.OnAdStarted   +=     HandleOnAdStarted;



        LoadRewardBaseAd();
    }

        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<EventArgs> OnAdOpening;

        public event EventHandler<EventArgs> OnAdStarted;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<Reward> OnAdRewarded;

        public event EventHandler<EventArgs> OnAdLeavingApplication;

public void LoadRewardBaseAd()

{
    #if UNITY_EDITOR
       String adUnitId = "unused";
    #elif UNITY_ANDROID
    string adUnitId = "ca-app-pub-8220336299264563/5335428879";
    #elif UNITY_IOS
    string adUnitId = "";
    #else
    string adUnitId = "unexpected_plateform";
    #endif

    rewardBasedVideoAd.LoadAd(new AdRequest.Builder().Build(),adUnitId);


}


  public void HandleOnAdLoaded(object sender,EventArgs args)
        {
                    
        }

    public void HandleOnAdOpening(object sender,EventArgs args)    
       {
        
       }
    public void HandleOnAdStarted(object sender,EventArgs args)    
       {

       }
    public void HandleOnAdClosed(object sender,EventArgs args)    
       {

       }

    public void HandleOnAdRewarded(object sender,EventArgs args)    
       {
              GiveReward();
       }

     public void HandleOnAdLeavingApplication(object sender,EventArgs args)    
       {

       }
    public void HandleOnAdFailedToLoad(object sender,EventArgs args)    
       {

       }

   
public void ShowRewardBaseAd()
{
   if(rewardBasedVideoAd.IsLoaded())
   {
     rewardBasedVideoAd.Show();
   }else
   {
        #if UNITY_EDITOR
         Debug.Log("Ad not Loaded rEWARDED BASE VIDEO ad");
        #endif
   }
}
    /************* IronSourceInterstitial API *************/
    /************* IronSourceRewardedVideo API *************/
    public void ShowVideoIronSource()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
            GiveReward();
        }
        // DemandOnly
        // ShowDemandOnlyRewardedVideo ();
    }

    void LoadDemandOnlyRewardedVideo()
    {
        IronSource.Agent.loadISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
    }

    void ShowDemandOnlyRewardedVideo()
    {
        if (IronSource.Agent.isISDemandOnlyRewardedVideoAvailable(REWARDED_INSTANCE_ID))
        {
            IronSource.Agent.showISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
        }
    }

    /************* RewardedVideo Delegates *************/
    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        if (canShowAd)
        {
            //       ShowText2.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.blue;
        }
        else
        {
            //       ShowText2.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
        }
    }

    void RewardedVideoAdOpenedEvent()
    {
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
    }

    void RewardedVideoAdClosedEvent()
    {
    }

    void RewardedVideoAdStartedEvent()
    {
    }

    void RewardedVideoAdEndedEvent()
    {
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
    }

    /************* RewardedVideo DemandOnly Delegates *************/

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {
        //      ShowText2.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.blue;
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        //       ShowText2.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
    }

    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    {
    }

    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    {
    }

    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    {
    }

    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
    }

    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    {
    }
    /************* IronSourceInterstitial API *************/
    public void LoadInterstitialButtonClicked()
    {
        IronSource.Agent.loadInterstitial();
    }

    public void ShowInterstitialIronSource()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
    }

    void LoadDemandOnlyInterstitial()
    {
        IronSource.Agent.loadISDemandOnlyInterstitial(INTERSTITIAL_INSTANCE_ID);
    }

    void ShowDemandOnlyInterstitial()
    {
        if (IronSource.Agent.isISDemandOnlyInterstitialReady(INTERSTITIAL_INSTANCE_ID))
        {
            IronSource.Agent.showISDemandOnlyInterstitial(INTERSTITIAL_INSTANCE_ID);
        }
    }

    /************* Interstitial Delegates *************/
    void InterstitialAdReadyEvent()
    {
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
    }

    void InterstitialAdShowSucceededEvent()
    {
    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
    }

    void InterstitialAdClickedEvent()
    {
    }

    void InterstitialAdOpenedEvent()
    {
    }

    void InterstitialAdClosedEvent()
    {
    }

    void InterstitialAdRewardedEvent()
    {
    }

    /************* Interstitial DemandOnly Delegates *************/

    void InterstitialAdReadyDemandOnlyEvent(string instanceId)
    {
    }

    void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
    }

    void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
    }

    void InterstitialAdClickedDemandOnlyEvent(string instanceId)
    {
    }

    void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
    {
    }

    void InterstitialAdClosedDemandOnlyEvent(string instanceId)
    {
    }

    void InterstitialAdRewardedDemandOnlyEvent(string instanceId)
    {
    }
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    /* *******************AdMob************************* */


    void InitializeAds()
    {
        isAdInitialized = true;

        MobileAds.Initialize(appID);
        InitUnityAds();
        RequestBanner();
        RequestLargeBanner();
        RequestInterstitial();
    }
    void SetFalse()
    {
        // showBannerOnStart = false;
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void RequestBanner()
    {
        if (smallbannerView == null)
        {
            this.smallbannerView = new BannerView(bannerID, AdSize.Banner, bannerSmallPosition);
            this.smallbannerView.OnAdLoaded += this.HandleAdLoaded;
            this.smallbannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
            this.smallbannerView.LoadAd(this.CreateAdRequest());
                  
        }
    }
    private void RequestLargeBanner()
    {
        if (largebannerView == null)
        {
            this.largebannerView = new BannerView(bannerID, AdSize.MediumRectangle, bannerLargePosition);
            this.largebannerView.OnAdLoaded += this.HandleLargeBannerAdLoaded;
            this.largebannerView.OnAdLeavingApplication += this.HandleLargeBannerAdLeftApplication;
            this.largebannerView.LoadAd(this.CreateAdRequest());
            this.largebannerView.Hide();
        }

    }
    public void ShowSmallAdmobBanner()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {

            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (SmallBannerOnceLoaded)
                    {
                        smallbannerView.Show();
                        small_bann_bool = true;
                    }
                    else
                    {
                        small_bann_bool = false;
                    }
                }

            }

        }

    }

    public void HideSmallAdmobBanner()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (SmallBannerOnceLoaded && CheckInitialization())
                smallbannerView.Hide();

        }
    }
    public void ShowLargeAdmobBanner()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (LargeBannerOnceLoaded)
                        largebannerView.Show();
                }
            }
        }
    }

    public void HideLargeAdmobBanner()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (LargeBannerOnceLoaded && CheckInitialization())
                largebannerView.Hide();
        }
    }
    public void HideLargeOnRemoveAd()
    {

        if (LargeBannerOnceLoaded && CheckInitialization())
            largebannerView.Hide();
    }
    public void DestroySmallBanner()
    {
        smallbannerView.Destroy();
    }

    public void DestroyLargeBanner()
    {
        largebannerView.Destroy();
    }

    private void RequestInterstitial()
    {
        // Create an interstitial.
        this.interstitial = new InterstitialAd(interstitialID);
        // Register for ad events.
       // this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        //this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        //	this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (interstitial.IsLoaded())
                    {
                        interstitial.Show();
                    }
                }
            }

        }

    }




    #region Small Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        SmallBannerOnceLoaded = true;
    }

    /*     public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */


    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        this.smallbannerView.OnAdLoaded -= this.HandleAdLoaded;
        //this.smallbannerView.OnAdFailedToLoad -= this.HandleAdFailedToLoad;
        //this.smallbannerView.OnAdOpening -= this.HandleAdOpened;
        //this.smallbannerView.OnAdClosed -= this.HandleAdClosed;
        this.smallbannerView.OnAdLeavingApplication -= this.HandleAdLeftApplication;
    }

    #endregion

    #region LargeBanner callback handlers

    public void HandleLargeBannerAdLoaded(object sender, EventArgs args)
    {
        LargeBannerOnceLoaded = true;
    }

    /*     public void HandleLargeBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */
    /*
	public void HandleLargeBannerAdOpened(object sender, EventArgs args)
	{

	}

	public void HandleLargeBannerAdClosed(object sender, EventArgs args)
	{

	} */

    public void HandleLargeBannerAdLeftApplication(object sender, EventArgs args)
    {
        this.largebannerView.OnAdLoaded -= this.HandleLargeBannerAdLoaded;
        //this.largebannerView.OnAdFailedToLoad -= this.HandleLargeBannerAdFailedToLoad;
        //this.largebannerView.OnAdOpening -= this.HandleLargeBannerAdOpened;
        //this.largebannerView.OnAdClosed -= this.HandleLargeBannerAdClosed;
        this.largebannerView.OnAdLeavingApplication -= this.HandleLargeBannerAdLeftApplication;
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {

    }

    /*     public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */

    /* 	public void HandleInterstitialOpened(object sender, EventArgs args)
	{

	} */

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {

        //this.interstitial.OnAdLoaded -= this.HandleInterstitialLoaded;
        //this.interstitial.OnAdFailedToLoad -= this.HandleInterstitialFailedToLoad;
        //this.interstitial.OnAdOpening -= this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed -= this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication -= this.HandleInterstitialLeftApplication;
    }

    #endregion



    //==================================================================================================================//

    //=============================================  Unity Ads  ========================================================//

    public void InitUnityAds()
    {
        string gameId = null;

#if UNITY_ANDROID
        gameId = unityAdID;
#elif UNITY_IOS
		gameId = iOSGameID;
#endif

        if (string.IsNullOrEmpty(gameId))
        {
        }
        else if (!Advertisement.isSupported)
        {
        }
        else if (Advertisement.isInitialized)
        {
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log(string.Format("Initialize Unity Ads using Game ID {0} with Test Mode {1}.",
                gameId, enableTestMode ? "enabled" : "disabled"));
#endif
            Advertisement.Initialize(gameId, enableTestMode);
        }
    }


    public void ShowUnityVideoAd()
    {
                 if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                } 
    }
    public bool isRewardedReady()
    {

        bool isCheck = false;
        if (IsInternetConnection())
        {
            if (CheckInitialization())
            {
                if (Advertisement.IsReady("rewardedVideo"))
                    isCheck = true;
            }

        }
        return isCheck;


    }
    string RewardInfo;
    public void ShowUnityRewardedVideoAd(string Info)
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo", options);
            }
        }
        RewardInfo = Info;
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    GiveReward();

                }
                break;
            case ShowResult.Skipped:
                //Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
               // Debug.LogError("Video failed to show.");
                break;
        }
    }
    //==================================================================================================================//

    //=============================================  Priority Ads  =====================================================//


    public void Show_Unity()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            ShowUnityVideoAd();
        }
    }
    public void Show_Unity_Admob()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (Advertisement.IsReady("video"))
                    {
                        Advertisement.Show("video");
                    }
                    else
                    {
                        ShowInterstitial();
                    }
                }
            }
        }
    }

    public void Show_Unity_Iron_sorce_Admob()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (Advertisement.IsReady("video"))
                    {
                        Advertisement.Show("video");
                    }
                    else if (IronSource.Agent.isInterstitialReady())
                    {
                        ShowInterstitialIronSource();
                    }
                    else if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                    else
                    {
                       
                    }
                }
            }
        }
    }

    public void Show_Admob_IronSource_Unity_Ad()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (IsInternetConnection())
                {
                    if (CheckInitialization())
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                        }
                    }
                }
                else if (IronSource.Agent.isInterstitialReady())
                {
                    ShowInterstitialIronSource();
                }

                else if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }   
                else
                {

                }
            }
        }
    }


      public void Show_Admob_Unity_Ad_Iron_Source()
    {
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
                if (IsInternetConnection())
                {
                    if (CheckInitialization())
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                        }
                    }
                }
               
                else if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }   

                 else if (IronSource.Agent.isInterstitialReady())
                {
                    ShowInterstitialIronSource();
                }
                else
                {

                }
            }
        }
    }
    public void GiveReward()
    {
        if (RewardInfo == ("Complete2X"))
        {
            complete_manager.instance.DoubleReward();
            FBAManager.Instance.SelectContent("RewardedEvent_2x");
        } 
        if (RewardInfo == "rewared_Arcde")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_2x_Mode_4");
            game_manager.instance.DoubleReward();
           
        }
        if (RewardInfo == "Reward_Respwan")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_Respwan_Vehcile");
            game_manager.instance.Respwan_Vehcile();
        }
        if (RewardInfo == "DailyReward")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_DailyReward");
            DailyReward.instance.DailyRewardRecive();
        }
        if (RewardInfo == "FreeCoins")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_FreeCoins");
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 250);
            main_menu.instance.ShowCash();
        }
        if (RewardInfo == "StoreBuy")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_StoreBuy");
            Grage_Manager.instance.BuyBtnFun("WacthVideo");
        }
        if (RewardInfo == "AchimentClaim")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_AchimentClaim");
            Grage_Manager.instance.BuyBtnFun("WacthVideo");
            Achivements.instance.RewardClaimGive();
        }
         if (RewardInfo == "Reset_Energy")
        {
            FBAManager.Instance.SelectContent("RewardedEvent_ResetEnergy");
            CountDown.instance.Reset_Energy();
           
            
        }

      
    }

    public void Show_Iron_source_unity__video(string Info)
    {
        RewardInfo = Info;
        if (PlayerPrefs.GetInt("IsAdsRemoved") == 0)
        {
            if (IsInternetConnection())
            {
               if (Advertisement.IsReady("rewardedVideo"))
               {
                    ShowUnityRewardedVideoAd(RewardInfo);
               }

                else if (IronSource.Agent.isInterstitialReady())
                {
                    ShowVideoIronSource();
                }
               else if (rewardBasedVideoAd.IsLoaded())
                {
                     ShowRewardBaseAd();
                }
                else
                {

                }
            }
        }
    }
    public bool IsRewardedAdAvilable()
    {
        if (IsInternetConnection())
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                return true;
            }

            else if (IronSource.Agent.isInterstitialReady())
            {
                return true;
            }
             else if (isRewardedReady())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
