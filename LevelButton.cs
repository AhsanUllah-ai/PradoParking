using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour {
	public static LevelButton instance;
	public enum  LevelMode
	{
		Cars,
		Cones,
		Blocks
	}
	public LevelMode modeType;
	private Text levelNumberText; 
	public Image _lock; 
	public CanvasGroup _lockObj;
	private int currentLevel = 0;
    public Animator Anim_btn;
	//private int currentFunnelNumber = 0;
	void Start()
	{
		instance = this;	
	}
	void OnEnable ()
	{
		levelNumberText = GetComponentInChildren<Text>();
		if(!PlayerPrefs.HasKey("currentMode"))
		{
			PlayerPrefs.SetInt("currentMode",1);
		}

		//_lockObj = GetComponentInChildren<CanvasGroup>();
	}

	public void SetLevel (int number) {


        if (PlayerPrefs.GetInt("lockCone") == 1 || PlayerPrefs.GetInt("lockBlock")==1 || PlayerPrefs.GetInt("currentMode")==1)
        {
            currentLevel = number;
			if(levelNumberText)
            levelNumberText.text = (number + 1).ToString();

        }


      

		
	}
	
}
