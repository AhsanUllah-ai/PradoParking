using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSound : MonoBehaviour
{
    public Image tick_muic_on, tick_muic_off, sfx_on, sfx_off;
    float SliderValue;
    public Slider MusicSl, SoundSl;
    public static GameSound instance;
    void Start()
    {

        instance = this;

        if (!PlayerPrefs.HasKey("soundsfx"))
        {
            PlayerPrefs.SetFloat("soundsfx", 1);
        }
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 1);
        }
        Update_seting_interc();
        Update_setting_sound();
    }

    // Update is called once per frame
    
    public void Update_seting_interc()
    {
        if (PlayerPrefs.GetFloat("music") == 2.0f)
        {
            if (tick_muic_off != null) {
                tick_muic_off.enabled = true;
                tick_muic_on.enabled = false;
            }
            dontDestroy.instance.music_Audio_s.volume = 0;
        }
        else
        {
            if (tick_muic_off != null)
            {
                tick_muic_on.enabled = true;
                tick_muic_off.enabled = false;
            }
            if (SceneManager.GetActiveScene().name != "main_menu")
            {
                dontDestroy.instance.music_Audio_s.volume = 0.5f;
            }
            else
            {
                dontDestroy.instance.music_Audio_s.volume = PlayerPrefs.GetFloat("music");
            }
        }



    }
    public void Update_setting_sound()
    {
        if (PlayerPrefs.GetFloat("soundsfx") == 2.0f)
        {
            if (sfx_on != null)
            {
                sfx_on.enabled = false;
                sfx_off.enabled = true;
            }
            for (int i = 0;i < dontDestroy.instance.sound.Length;i++)
            {
                dontDestroy.instance.sound[i].volume = 0;
            }
        }
        else
        {
            if (sfx_on != null)
            {
                sfx_on.enabled = true;
                sfx_off.enabled = false;
            }
            for (int i = 0; i < dontDestroy.instance.sound.Length; i++) { 
                //if (SceneManager.GetActiveScene().name != "main_menu")
                //{
                //    dontDestroy.instance.sound[i].volume = 0.5f;
                //}
                //else
                //{
                    dontDestroy.instance.sound[i].volume =  PlayerPrefs.GetFloat("soundsfx");
               // }
            }
        }
    }
    public void music_on()// yah off kly hy
    {
        PlayerPrefs.SetFloat("music", 2.0f);
        Update_seting_interc();
        dontDestroy.instance.sound[0].Play();
    }
    public void music_off()//yah on kly hy
    {
        if (SoundSl != null)
            SliderValue = MusicSl.value;
        else
            SliderValue = 1;
        PlayerPrefs.SetFloat("music", SliderValue);
        Update_seting_interc();
        dontDestroy.instance.sound[0].Play();
    }
    public void sfx_on_btn()// yah off kly hy
    {
        PlayerPrefs.SetFloat("soundsfx", 2.0f);
        Update_setting_sound();
        dontDestroy.instance.sound[0].Play();
    }
    public void sfx_off_btn()//yah on kly hy
    {
        if (SoundSl != null)
            SliderValue = SoundSl.value;
        else
            SliderValue = 1;
        PlayerPrefs.SetFloat("soundsfx", SliderValue);
        Update_setting_sound();
        dontDestroy.instance.sound[0].Play();
    }
    public void MusicSlider()
    {
        dontDestroy.instance.music_Audio_s.volume = MusicSl.value;
    }
    public void SoundSlider()
    {
        dontDestroy.instance.sound[0].volume = SoundSl.value;
        dontDestroy.instance.sound[1].volume = SoundSl.value;
    }
}
