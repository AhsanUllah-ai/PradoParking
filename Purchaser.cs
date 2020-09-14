using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

//Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager,
//one of the existing Survival Shooter scripts.

//Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{
    public static Purchaser Instance;
    private static IStoreController m_StoreController;          // 
    private static IExtensionProvider m_StoreExtensionProvider; // 


    public static string id_Remove_ads = "remove_ads";
    public static string id_Unlock_All_Levels_Cars = "unlock_mode1_levels";
    public static string id_Unlock_All_Levels_Cones = "unlock_mode2_levels";
    public static string id_Unlock_All_Levels_Block = "unlock_mode3_levels";
    public static string id_Extra_Arcade_Hits = "extra_arcade_hits";
    public static string id_Unlock_All_Modes = "unlock_all_modes";
    public static string id_Unlock_All_Parado = "unlock_all_parado";
    public static string id_Unlock_All_paints = "unlock_all_paints";
    public static string id_Unlock_All_Decals = "unlock_all_decals";
    public static string id_Unlock_All_rims = "unlock_all_rims";

    public static string id_pack1 = "pack_cars_coins_ads";
    public static string id_pack2 = "pack_rims_coins_ads";
    public static string id_pack3 = "pack_paints_coins_ads";
    public static string id_Coins_1k = "coins_1k";
    public static string id_Coins_5k = "coins_5k";
    public static string id_Coins_20k = "coins_20k";
    public static string id_mega_coins_50k = "coins_50k";
    public GameObject No_internet_panel;
    public Text[] LocalizePrice;
    public String[] ProductName;

    public Text[] Pruduct_Off_Text;
    public String[] Off_ProductName;
    public float []Price_Off_Per;
    
    void Start()
    {

        Instance = this;
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
        InitializePurchasing();
        }


       
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
           // ... we are done here.
           return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.id_Extra_Arcade_Hits
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(id_Remove_ads, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_Levels_Cars, ProductType.NonConsumable);
        builder.AddProduct(id_Extra_Arcade_Hits, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_Levels_Cones, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_Levels_Block, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_Modes, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_Parado, ProductType.NonConsumable);

        builder.AddProduct(id_Unlock_All_paints, ProductType.NonConsumable);
        builder.AddProduct(id_Unlock_All_Decals, ProductType.NonConsumable);
        builder.AddProduct(id_Unlock_All_rims, ProductType.NonConsumable);

        builder.AddProduct(id_pack1, ProductType.NonConsumable);

        builder.AddProduct(id_pack2, ProductType.NonConsumable);

        builder.AddProduct(id_pack3, ProductType.NonConsumable);

        builder.AddProduct(id_Coins_1k, ProductType.Consumable);
        builder.AddProduct(id_Coins_5k, ProductType.Consumable);
        builder.AddProduct(id_Coins_20k, ProductType.Consumable);
        builder.AddProduct(id_mega_coins_50k, ProductType.Consumable);



        //id_legendary_offer
        UnityPurchasing.Initialize(this, builder);
        Invoke("PriceLocalize", 4f);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    public void PriceLocalize()
    {
        if (m_StoreController != null && IsInitialized())
        {
            for (int i = 0; i < LocalizePrice.Length; i++)
            {
                LocalizePrice[i].text = m_StoreController.products.WithID(ProductName[i]).metadata.localizedPriceString;
            }
            LocalizeOffPrice();
        }
        else
        {
            Invoke("PriceLocalize", 10f);
        }
    }
    void LocalizeOffPrice()
    {
        for (int i = 0; i < Pruduct_Off_Text.Length; i++)
        {
            float Price = Decimal.ToSingle(m_StoreController.products.WithID(Off_ProductName[i]).metadata.localizedPrice);
            Price = Price/Price_Off_Per[i];
            Pruduct_Off_Text[i].text = "/"+System.Math.Round(Price, 2).ToString();
        }
    }
    public void Buy_RemoveAds()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            BuyProductID(id_Remove_ads);
            FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }
        else
        {
            No_internet_panel.SetActive(true);
        }
    }
    public void UnlockLevelBtn()
    {
        if (PlayerPrefs.GetInt("currentMode") == 1)
        {
            Unlock_All_Levels_Cars();
        }else
            if (PlayerPrefs.GetInt("currentMode") == 2)
        {
            Unlock_All_Levels_Cones();
        }
        else
            if (PlayerPrefs.GetInt("currentMode") == 3)
        {
            Unlock_All_Levels_Block();
        }
    }

    public void Unlock_All_Levels_Cars()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either id_Extra_Arcade_Hits
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (Application.internetReachability != NetworkReachability.NotReachable)
         {  
             BuyProductID(id_Unlock_All_Levels_Cars);
             FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
       
    }
    public void ArcadeMode_Hits()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            BuyProductID(id_Extra_Arcade_Hits);
            FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }
        else
        {
            No_internet_panel.SetActive(true);
        }

    }

    public void Unlock_All_Levels_Cones()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if(Application.internetReachability != NetworkReachability.NotReachable)
         {  
          BuyProductID(id_Unlock_All_Levels_Cones);
          FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
    }

    public void Unlock_All_Levels_Block()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
              BuyProductID(id_Unlock_All_Levels_Block);
              FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
       
    }
    public void Unlock_All_modes()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
           if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
               BuyProductID(id_Unlock_All_Modes);
               FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
               PlayerPrefs.SetInt("All_modes_Unlock",1);
         }else
         {
             No_internet_panel.SetActive(true);
         }

      
    }
    public void Buy_Unlock_All_Parado()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
          if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
              BuyProductID(id_Unlock_All_Parado);
              FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
         
       
    }

   
    public void Buy_pack1()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
          if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
           BuyProductID(id_pack1);
           FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }else
         {
             No_internet_panel.SetActive(true);
         }
    }

    public void Buy_pack2()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
        BuyProductID(id_pack2);
        FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }else
         {
             No_internet_panel.SetActive(true);
         }
    }


    public void Buy_pack3()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
        BuyProductID(id_pack3);
        FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }else
         {
             No_internet_panel.SetActive(true);
         }
    }

    public void Buy_Unlock_All_Paints()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
        BuyProductID(id_Unlock_All_paints);
        FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }else
         {
             No_internet_panel.SetActive(true);
         }
     
    }
    public void Buy_Unlock_All_Decals()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            BuyProductID(id_Unlock_All_Decals);
            FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }
        else
        {
            No_internet_panel.SetActive(true);
        }

    }

    public void Buy_Unlock_All_Rims()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
        BuyProductID(id_Unlock_All_rims);
        FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
    }


    public void Buy_coins_1k()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
        BuyProductID(id_Coins_1k);
        FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }else
         {
             No_internet_panel.SetActive(true);
         }
    }
    public void Buy_coins_5k()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
           BuyProductID(id_Coins_5k);
           FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
    }
    public void Buy_coins_20k()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
         if(Application.internetReachability != NetworkReachability.NotReachable)
         {   
          BuyProductID(id_Coins_20k);
          FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
         }else
         {
             No_internet_panel.SetActive(true);
         }
    }
    //id_legendary_offer
    public void Buy_coins_50k()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            BuyProductID(id_mega_coins_50k);
            FBAManager.Instance.SelectContent("IAP_Btn_Clicked");
        }
        else
        {
            No_internet_panel.SetActive(true);
        }
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        //if (!IsInitialized())
        //{
        //    // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
        //    Debug.Log("RestorePurchases FAIL. Not initialized.");
        //    return;
        //}

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, id_Remove_ads, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("IsAdsRemoved", 1);
            PlayerPrefs.Save();
            main_menu.instance.Update_interctable();
        }

        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Levels_Cars, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("Unlocked_All_Cars_Levels", 1);
            PlayerPrefs.Save();
            LevelSelection_Manager.instance.CheckLevelUnLock();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_Extra_Arcade_Hits, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("Arcade_Mode_Hits", 1);
            main_menu.instance.PanalExit();
            main_menu.instance.PanalOpen(main_menu.instance.Store_panel);
        }

        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Levels_Cones, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("Unlocked_All_Cones_Levels", 1);
            PlayerPrefs.Save();
            LevelSelection_Manager.instance.CheckLevelUnLock();
        }

        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Levels_Block, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("Unlocked_All_Blocks_Levels", 1);
            PlayerPrefs.Save();
            LevelSelection_Manager.instance.CheckLevelUnLock();
        }


        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Modes, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("Unlocked_All_Modes", 1);
            PlayerPrefs.Save();
            LevelSelection_Manager.instance.CheckModeUnlock();
            Grage_Manager.instance.PanalExit();
        }

        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Parado, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_all_parado", 1);
            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();
            Grage_Manager.instance.PanalExit();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_paints, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_all_paints", 1);
            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();
            Grage_Manager.instance.PanalExit();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_Decals, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_all_Decals", 1);
            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();
            Grage_Manager.instance.PanalExit();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_Unlock_All_rims, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_all_rims", 1);
            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();
            Grage_Manager.instance.PanalExit();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_pack1, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_pack1", 1);

            //++++++++++ All prado unlocked

            PlayerPrefs.SetInt("purchased_all_parado", 1);


            //+++++++++ All Ads removed

            PlayerPrefs.SetInt("IsAdsRemoved", 1);
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 15000);
            main_menu.instance.ShowCash();
            //++++++++Update inter

            main_menu.instance.Update_interctable();

            Grage_Manager.instance.CheckIAPAll();


        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_pack2, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_pack2", 1);

            PlayerPrefs.SetInt("purchased_all_rims", 1);

            //+++++++++ All Ads removed

            PlayerPrefs.SetInt("IsAdsRemoved", 1);
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 10000);
            main_menu.instance.ShowCash();
            //++++++++Update inter

            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();

        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_pack3, StringComparison.Ordinal))
        {

            PlayerPrefs.SetInt("purchased_pack3", 1);

            PlayerPrefs.SetInt("purchased_all_paints", 1);

            //+++++++++ All Ads removed

            PlayerPrefs.SetInt("IsAdsRemoved", 1);
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 20000);
            main_menu.instance.ShowCash();
            //++++++++Update inter

            main_menu.instance.Update_interctable();
            Grage_Manager.instance.CheckIAPAll();
        }


        else if (String.Equals(args.purchasedProduct.definition.id, id_Coins_1k, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1000);
            main_menu.instance.ShowCash();
        }

        else if (String.Equals(args.purchasedProduct.definition.id, id_Coins_5k, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 5000);
            if(SceneManager.GetActiveScene().name == "main_menu")
            {
                main_menu.instance.ShowCash();
            }else
            {
               
            }
           
        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_Coins_20k, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 20000);
            main_menu.instance.ShowCash();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, id_mega_coins_50k, StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 50000);
            main_menu.instance.ShowCash();
        }
       


        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
    }

    public void Close_Canvas()
    {
        No_internet_panel.SetActive(false);
    }


}
