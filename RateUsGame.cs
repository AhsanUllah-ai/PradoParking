using UnityEngine;
using UnityEngine.UI;

public class RateUsGame : MonoBehaviour
{
    public GameObject  ratus_yes_btn, rate_us2, Anim_star;
    public Text  ratus_text;
    public Button[] stars;
    public Sprite Rate_us_img_star_2, Rate_us_img_star_;
    public void ratethisgame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ghive.jeep.parking.car.free.game.master.apps");
        GameAnalytics.instance.UnityCustomEvent("rateUs");
        
    }
    public void rate_us_fuc()
    {
        Anim_star.SetActive(true);
        ratus_text.text = "";
        rate_us2.SetActive(false);
        ratus_yes_btn.SetActive(false);
        dontDestroy.instance.sound[0].Play();
        for (int i = 0; i < 5; i++)
        {
          stars[i].GetComponent<Image>().sprite = Rate_us_img_star_2;
        }
        ratus_text.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void Star_Func(int index)
    {

        for (int i = 0; i < 5; i++)
        {
            if (i <= index)
            {
                stars[i].GetComponent<Image>().sprite = Rate_us_img_star_;

            }
            else
            {
                stars[i].GetComponent<Image>().sprite = Rate_us_img_star_2;
            }

            if (index > 2)
            {
                ratus_text.transform.GetChild(0).gameObject.SetActive(true);
                rate_us2.SetActive(true);
                ratus_text.text = "good";
                ratus_text.color = Color.green;
                Anim_star.SetActive(false);

            }
            else
            {
                ratus_text.transform.GetChild(0).gameObject.SetActive(false);
                rate_us2.SetActive(false);
                ratus_text.text = "bad";
                ratus_text.color = Color.red;
                ratus_yes_btn.SetActive(true);
                Anim_star.SetActive(false);
            }
        }
    }

}
