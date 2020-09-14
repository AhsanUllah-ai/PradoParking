using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DailyReward : MonoBehaviour
{
    public GameObject[] DailyrewardBtn,Disable,Claim,RewardedVideo;
    public GameObject DailyRewardPanal;

    public GameObject Daily_Mission_btn;
    public Text Time_daily_m;
    public Text Daily_info_text;
    int hours, mits,Scend,coins;
    public static DailyReward instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void OnEnable()
    {
    DailyrewardBtn[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,15f);
        if (!PlayerPrefs.HasKey("LastDay"))
        {
            PlayerPrefs.SetInt("LastDay", 0);
            PlayerPrefs.SetInt("ActiveReward", -1);
        }
        string day = DateTime.Now.ToString("dd");
       
        int currentday = Convert.ToInt32(day);
        if (PlayerPrefs.GetInt("LastDay") != currentday)
        {
            PlayerPrefs.SetInt("LastDay", currentday);
            PlayerPrefs.SetInt("ActiveReward", PlayerPrefs.GetInt("ActiveReward") + 1);
            PlayerPrefs.SetInt("RewardRecived", 0);
            PlayerPrefs.SetInt("Daily_mission_active", 0);
            if(PlayerPrefs.GetInt("ActiveReward") > 6)
                PlayerPrefs.SetInt("ActiveReward",0);
              CancelInvoke("Timer");
        }

            for (int i = 0; i < DailyrewardBtn.Length; i++)
            {
                if (i < PlayerPrefs.GetInt("ActiveReward"))
                {
                    DailyrewardBtn[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    DailyrewardBtn[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                    DailyrewardBtn[i].transform.GetChild(3).gameObject.SetActive(false);
                    Disable[i].SetActive(true);
                    Claim[i].SetActive(false);

                }else
                if (i == PlayerPrefs.GetInt("ActiveReward") && PlayerPrefs.GetInt("RewardRecived") != 1)
                {
                    DailyrewardBtn[i].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    DailyrewardBtn[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 15f);
                    DailyrewardBtn[i].transform.GetChild(3).gameObject.SetActive(true);
                    Claim[i].SetActive(true);
                    main_menu.instance.PanalOpen(DailyRewardPanal);
                    FBAManager.Instance.SelectContent("Screen_DailyReward");
                }else
                if (PlayerPrefs.GetInt("RewardRecived") == 1)
                {
                DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")].transform.localScale = new Vector3(1f, 1f, 1f);
                DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

                Disable[PlayerPrefs.GetInt("ActiveReward")].SetActive(true);
                Claim[PlayerPrefs.GetInt("ActiveReward")].SetActive(false);
                }
            }

            if(PlayerPrefs.GetInt("Daily_mission_active")!=0)
            {
                Daily_Mission_btn.GetComponent<Button>().interactable = false;
                 Daily_Mission_btn.GetComponent<Animator>().enabled = false;
                 InvokeRepeating("Timer",0.5f,0.5f);
            
            }
            else
            {
                Daily_Mission_btn.GetComponent<Button>().interactable = true;
                Daily_Mission_btn.GetComponent<Animator>().enabled = false;
                Daily_Mission_btn.GetComponent<Animator>().enabled = true;
                CancelInvoke("Timer");
            }

             if (PlayerPrefs.GetInt("Gameplay_load") == 1)
             {
              Daily_Mission_btn.SetActive(true);
             }
    }

    void Timer()
    {
        hours = System.Convert.ToInt32(DateTime.Now.ToString("HH"));
        Scend = 60- DateTime.Now.Second;
        mits = 60 - DateTime.Now.Minute;
        // RewardActive();// yah nichy hours wali condition khatk kar k es ko call karni hy
        if (hours != 0)
            hours = 24 - hours;
        string Timer = hours.ToString()+":" + mits.ToString()+":" + Scend.ToString();
        Time_daily_m.text = Timer;
        Daily_info_text.text = "Next mission".ToString();
    }
    void VideoReward()
    {
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")+1].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward") + 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 15f);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward") + 1].transform.GetChild(3).gameObject.SetActive(true);
        RewardedVideo[PlayerPrefs.GetInt("ActiveReward") + 1].SetActive(true);
    }
    public void GetVideoReward(int coin)
    {
        coins = coin;
        Ads_Manager.Instance.Show_Iron_source_unity__video("DailyReward");
    }
    public void DailyRewardRecive()
    {
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward") + 1].transform.localScale = new Vector3(1f, 1f, 1f);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward") + 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        Disable[PlayerPrefs.GetInt("ActiveReward") + 1].SetActive(true);
        RewardedVideo[PlayerPrefs.GetInt("ActiveReward") + 1].SetActive(false);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward") + 1].transform.GetChild(3).gameObject.SetActive(false);
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + coins);
        PlayerPrefs.SetInt("RewardRecived", 1);
        PlayerPrefs.SetInt("ActiveReward", PlayerPrefs.GetInt("ActiveReward") + 1);
        main_menu.instance.ShowCash();
        Invoke("dailyPanalExit", 3f);
    }
    public void dailyPanalExit()
    {
        if(DailyRewardPanal.activeInHierarchy)
           main_menu.instance.PanalExit();
    }
    public void GetReward(int coins)
    {
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")].transform.localScale = new Vector3(1f, 1f, 1f);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        Disable[PlayerPrefs.GetInt("ActiveReward")].SetActive(true);
        Claim[PlayerPrefs.GetInt("ActiveReward")].SetActive(false);
        DailyrewardBtn[PlayerPrefs.GetInt("ActiveReward")].transform.GetChild(3).gameObject.SetActive(false);
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash")+ coins);
        PlayerPrefs.SetInt("RewardRecived", 1);
        if (PlayerPrefs.GetInt("ActiveReward") < 6 && Ads_Manager.Instance.IsRewardedAdAvilable())
        {
            VideoReward();
        }
        else
        {
            Invoke("dailyPanalExit", 3f);
        }

        main_menu.instance.ShowCash();
    }

// yah serf notification kly banay hn jab notification add karen gy tab es ko call karen gy start main
    int numOfDays, SpentHours ,hour;
    public void RewardActive()
    {
        int currentMonth = DateTime.Now.Month;
        int currentday = DateTime.Now.Day;
        if (PlayerPrefs.GetInt("CRMonth") != currentMonth)
        {

            if (DateTime.Now.Month != 1)
                numOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
            else
                numOfDays = DateTime.DaysInMonth(DateTime.Now.Year - 1, 12);

            if (PlayerPrefs.GetInt("CRDay") == numOfDays)
                PlayerPrefs.SetInt("CRDay", -0);
            else
                PlayerPrefs.SetInt("CRDay", -1);

            PlayerPrefs.SetInt("CRMonth", currentMonth);
        }
        if (PlayerPrefs.GetInt("CRDay") < currentday - 1)
        {
            PlayerPrefs.SetInt("CRDay", currentday);
            // daily reward Active if day change and grater then 24 hours above 
            PlayerPrefs.SetInt("Daily_mission_active", 0);
            CancelInvoke("Timer");
        }  
        else
        {
            if (PlayerPrefs.GetInt("CRHours") < hours)
            {
                SpentHours = PlayerPrefs.GetInt("CRHours") - hours;
            }
            else
            if (PlayerPrefs.GetInt("CRHours") > hours)
            {
                SpentHours = PlayerPrefs.GetInt("CRHours") - 24;
                SpentHours += hours;
            }
            else
                SpentHours = 0;

            if (SpentHours >= 24)
            {
                PlayerPrefs.SetInt("CRHours", hours);
                // daily reward Active if time upto 24 hourse 
                PlayerPrefs.SetInt("Daily_mission_active", 0);
                CancelInvoke("Timer");
            }
            else
            {
                hour = 23 -SpentHours;
            }
        }
    }
}
