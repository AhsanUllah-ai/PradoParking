using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public class GameAnalytics : MonoBehaviour
{
	public static GameAnalytics instance;
	void Awake()
	{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
	}
	void Start()
	{
		Analytics.CustomEvent ("VC_61");
	}
	public void UnityCustomEvent(string _name)
	{
		Analytics.CustomEvent(_name);
	}
    public void UnityCustomEvent(string _name,int index)
    {
        Analytics.CustomEvent("_name", new Dictionary<string, object>
        {
            { "potions", _name },
            { "coins", index }
        });
    }
}
