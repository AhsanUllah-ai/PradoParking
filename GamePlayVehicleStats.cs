using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayVehicleStats : MonoBehaviour
{
    public Material[] RimsMaterial, BodyMaterial;
    public GameObject[] playerVehicles;
     public static bool Spwan_vechile_bool;
    int currentVehicle;

     [Space]
    public GameObject[] Daily_mission_Spwan_point;
     [Space]
    // Start is called before the first frame update
    public VehiclePlate[] VehiclePlateNo;
    [System.Serializable]
    public class VehiclePlate
    {
        public Text FrontPlate, BackPlate;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentVehicle = Grage_Manager.currentVehicle;
        CheckVechaleLockStats();
    }

    void Update()
    {
        if(Spwan_vechile_bool == true)
        {
          CheckVechaleLockStats();

          Spwan_vechile_bool = false;
          Debug.Log("Spwaneddd_-----");

        }
    }
    void CheckVechaleLockStats()
    {
        // playerVehicles active
      if(PlayerPrefs.GetInt("currentMode")!= 5)
        {

        for (int i = 0; i <playerVehicles.Length ; i++)
        { if(i == currentVehicle)
              playerVehicles[i].SetActive(true);
          else
                playerVehicles[i].SetActive(false);
        }

        }
        else
        {
          
            int R =Random.Range(0,7);

            playerVehicles[R].SetActive(true);
            playerVehicles[R].transform.position = Daily_mission_Spwan_point[0].transform.position ;
            playerVehicles[R].transform.localRotation = Daily_mission_Spwan_point[0].transform.rotation;
        }
        // playerVehicles Number Plate set
        if (PlayerPrefs.GetString("Vehicle Plate" + currentVehicle) != null)
        {
            VehiclePlateNo[currentVehicle].FrontPlate.text = PlayerPrefs.GetString("Vehicle Plate" + currentVehicle);
            VehiclePlateNo[currentVehicle].BackPlate.text = PlayerPrefs.GetString("Vehicle Plate" + currentVehicle);
        }
        // playerVehicles Color Set
       if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Color" + PlayerPrefs.GetInt("ColorSelected")) == 1)
       {
            BodyMaterial[currentVehicle].color = Grage_Manager.instance.BodyColors[PlayerPrefs.GetInt("ColorSelected")];
       }
        // playerVehicles Rim Set
       if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Rim" + PlayerPrefs.GetInt("RimSelected")) == 1)
       {
          RimsMaterial[currentVehicle].mainTexture = Grage_Manager.instance.RimsTextures[PlayerPrefs.GetInt("RimSelected")];
       }
        if (PlayerPrefs.GetInt("Vehicle" + currentVehicle + "Decal" + PlayerPrefs.GetInt("DecalSelected")) == 1 && PlayerPrefs.GetInt("DefultBodyTexture") != 1)
        {
            BodyMaterial[currentVehicle].color = Color.white;
            BodyMaterial[currentVehicle].mainTexture = Grage_Manager.instance.DacelsTexture[PlayerPrefs.GetInt("DecalSelected")];
        }
        else
        {
            BodyMaterial[currentVehicle].mainTexture = Grage_Manager.instance.defultBodyTexture[currentVehicle];
        }
    }
  
}
