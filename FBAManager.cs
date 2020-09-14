using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class FBAManager : MonoBehaviour
{
    public static FBAManager Instance { get; private set; }
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;

    private void Awake()
    {
            Instance = this;
    }

    private void Start()
    {
        FBAInitialize();
    }

    private void FBAInitialize()
    {
      
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        firebaseInitialized = true;
    }
    public void levelStart(string levelName)
    {
     
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart + "_" + levelName, new Parameter[]
            {
               new Parameter(FirebaseAnalytics.ParameterLevelName,levelName)
            });
    }
    public void levelComplete(string levelName)
    {
      
        FirebaseAnalytics.LogEvent("Level_Complete" + "_" + levelName, new Parameter[]
           {
               new Parameter(FirebaseAnalytics.ParameterLevelName,levelName)
           });
    }
    public void levelFailed(string levelName)
    {
        
        FirebaseAnalytics.LogEvent("LevelFailed_" + levelName, new Parameter[]{
            new Parameter(FirebaseAnalytics.ParameterLevelName, levelName),
            });
    }

    
    public void GameStartEvent(string version)
    {
       
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen + "_" + version);
    }
    //++++++++++++++++==SelectContent==++++++++++++

    public void SelectContent(string Name)
    {
       
        FirebaseAnalytics.LogEvent(Name, new Parameter[]
            {
               new Parameter(FirebaseAnalytics.ParameterContent,Name)
            });
    }

    
}


