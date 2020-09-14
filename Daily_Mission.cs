using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Daily_Mission : MonoBehaviour
{

    public static Daily_Mission instance;
    public GameObject Parking_Point;

    public GameObject[]   vehciles_Mission_1;

    public GameObject[]   vehciles_Mission_2;

    public GameObject[]   vehciles_Mission_3;

    public GameObject[]   vehciles_Mission_4;

    public GameObject[]   vehciles_Mission_5;

     public static bool Mission_bool;

     public int Counter;

     public GameObject Cube;

     public int Per_Mission_points;

     public float per_Mission_energy;

     public bool restart_bool;
     public Material rings;
    void Start()
    {
       
       instance = this; 
       Daily_Missions_Levels();
       FBAManager.Instance.SelectContent("Start_Mission"+PlayerPrefs.GetInt("Daily_mission_no"));
    
    }


     

  void Update()
     {
        if (Mission_bool == true) {
               game_manager.instance.Parker_Filler.fillAmount += 0.3f * Time.deltaTime;
                rings.color = Color.green;
               if (game_manager.instance.Parker_Filler.fillAmount == 1)
               {
                     GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
                   
                     Next_point ();
                
                    game_manager.instance.Parker_Filler.fillAmount = 0;

                    Mission_bool = false;

                   
               }
         
          }
     }


      public void Next_point () {
         
         
           Check_for_parking();
          
           Debug.Log("next+point");
           FollowCam.Instance.Show_Finish_Aera();    

           Invoke("Change_cam",3f);

           GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
      
       
     }


     void Change_cam()
     {
            FollowCam.Instance.Show_player();    
     }

  void Check_for_parking()
  {
        
       if(PlayerPrefs.GetInt("parking_point_collected")==Per_Mission_points)
       {
         
          PlayerPrefs.SetInt("parking_point_collected",0);
          PlayerPrefs.SetFloat("Last_time",per_Mission_energy);
          Debug.Log("Completed");
          FBAManager.Instance.SelectContent("Complete_Mission"+PlayerPrefs.GetInt("Daily_mission_no"));
          if(PlayerPrefs.GetInt("Daily_mission_no")>= 4)
          {
            //  SceneManager.LoadScene(1);
              PlayerPrefs.SetInt("Daily_mission_no",0);
          }
          SceneManager.LoadScene(3);
       
       }else
       {
         
           PlayerPrefs.SetFloat("Last_time",CountDown.instance.timeLeft);  
           SceneManager.LoadScene(2);
          
       }
  }



     
     



    void Daily_Missions_Levels()
    {
         
          

        if(PlayerPrefs.GetInt("Daily_mission_no")==0)
        {
             Per_Mission_points = 3;
           
             if(PlayerPrefs.GetInt("parking_point_collected")==0)
              {

                       vehciles_Mission_1[0].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_1[0].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_1[0].transform.rotation;
                       #if UNITY_EDITOR
                       Debug.Log("parking_point1"); 
                       #endif
                    
                       per_Mission_energy = 80f;
                       Invoke("Per_mission_Energy",4);

              }else if(PlayerPrefs.GetInt("parking_point_collected")==1)
              {
                   
                       vehciles_Mission_1[1].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_1[1].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_1[1].transform.rotation;
                      #if UNITY_EDITOR
                        Debug.Log("parking_point2"); 
                      #endif
                      if(restart_bool== false)
                      {
                           per_Mission_energy = 80f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 80f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==2)
              {

                        vehciles_Mission_1[2].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_1[2].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_1[2].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point3");
                         #endif
                           if(restart_bool== false)
                      {
                            per_Mission_energy = 80f;
                            Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 80f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else
              {
                      Invoke("Per_mission_Energy",2);
              }

                #if UNITY_EDITOR
                   Debug.Log("Daily_mission_no  _____"+PlayerPrefs.GetInt("Daily_mission_no"));
         
                #endif
               

        }
        else if(PlayerPrefs.GetInt("Daily_mission_no")==1)
        {
              
    
              Per_Mission_points = 5;
            
             if(PlayerPrefs.GetInt("parking_point_collected")==0)
              {

                       vehciles_Mission_2[0].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_2[0].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_2[0].transform.rotation;
                       #if UNITY_EDITOR
                       Debug.Log("parking_point1"); 
                       #endif
                       per_Mission_energy = 130f;
                       Invoke("Per_mission_Energy",4);

              }else if(PlayerPrefs.GetInt("parking_point_collected")==1)
              {
                   
                       vehciles_Mission_2[1].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_2[1].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_2[1].transform.rotation;
                      #if UNITY_EDITOR
                        Debug.Log("parking_point2"); 
                      #endif
                        if(restart_bool== false)
                      {
                           per_Mission_energy = 130f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 130f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==2)
              {

                        vehciles_Mission_2[2].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_2[2].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_2[2].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point3");
                       
                         #endif
                            if(restart_bool== false)
                      {
                          per_Mission_energy = 130f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 130f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }  else if(PlayerPrefs.GetInt("parking_point_collected")==3)
              {

                        vehciles_Mission_2[3].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_2[3].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_2[3].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point4");
                         #endif
                          if(restart_bool== false)
                      {
                           per_Mission_energy = 130f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 130f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }

              else if(PlayerPrefs.GetInt("parking_point_collected")==4)
              {

                        vehciles_Mission_2[4].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_2[4].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_2[4].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point4");
                         #endif
                           if(restart_bool== false)
                      {
                           per_Mission_energy = 130f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 130f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }
              
              
              
              
              else 
              {

              }

                #if UNITY_EDITOR
                   Debug.Log("Daily_mission_no  _____"+PlayerPrefs.GetInt("Daily_mission_no"));
         
                #endif

        } else if(PlayerPrefs.GetInt("Daily_mission_no")==2)
        {
                     Per_Mission_points = 8;
                   
                   
            
             if(PlayerPrefs.GetInt("parking_point_collected")==0)
              {

                       vehciles_Mission_3[0].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_3[0].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_3[0].transform.rotation;
                       #if UNITY_EDITOR
                       Debug.Log("parking_point1"); 
                       #endif
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);

              }else if(PlayerPrefs.GetInt("parking_point_collected")==1)
              {
                   
                       vehciles_Mission_3[1].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_3[1].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_3[1].transform.rotation;
                      #if UNITY_EDITOR
                        Debug.Log("parking_point2"); 
                      #endif
                        if(restart_bool== false)
                      {
                           per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==2)
              {

                        vehciles_Mission_3[2].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[2].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[2].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point3");
                       
                         #endif
                            if(restart_bool== false)
                      {
                           per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }  else if(PlayerPrefs.GetInt("parking_point_collected")==3)
              {

                        vehciles_Mission_3[3].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[3].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[3].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point4");
                         #endif
                           if(restart_bool== false)
                      {
                          per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==4)
              {

                        vehciles_Mission_3[4].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[4].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[4].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point5");
                         #endif
                          if(restart_bool== false)
                      {
                          per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==5)
              {

                        vehciles_Mission_3[5].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[5].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[5].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point6");
                         #endif
                            if(restart_bool== false)
                      {
                          per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==6)
              {

                        vehciles_Mission_3[6].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[6].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[6].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point7");
                         #endif
                        if(restart_bool== false)
                      {
                          per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==7)
              {

                        vehciles_Mission_3[7].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_3[7].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_3[7].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point8");
                         #endif
                        if(restart_bool== false)
                      {
                         per_Mission_energy = 200f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 200f;
                         Invoke("Per_mission_Energy",4);
                      }
                    
                       

              }
              
            




        }
        
        else if(PlayerPrefs.GetInt("Daily_mission_no")==3)
        {
                   
               Per_Mission_points = 10;
             
            
             if(PlayerPrefs.GetInt("parking_point_collected")==0)
              {

                       vehciles_Mission_4[0].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_4[0].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_4[0].transform.rotation;
                       #if UNITY_EDITOR
                       Debug.Log("parking_point1"); 
                       #endif
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);

              }else if(PlayerPrefs.GetInt("parking_point_collected")==1)
              {
                   
                       vehciles_Mission_4[1].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_4[1].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_4[1].transform.rotation;
                      #if UNITY_EDITOR
                        Debug.Log("parking_point2"); 
                      #endif
                             if(restart_bool== false)
                      {
                          per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==2)
              {

                        vehciles_Mission_4[2].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[2].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[2].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point3");
                       
                         #endif
                               if(restart_bool== false)
                      {
                          per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }  else if(PlayerPrefs.GetInt("parking_point_collected")==3)
              {

                        vehciles_Mission_4[3].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[3].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[3].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point4");
                         #endif
                                if(restart_bool== false)
                      {
                           per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==4)
              {

                        vehciles_Mission_4[4].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[4].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[4].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point5");
                         #endif
                             if(restart_bool== false)
                      {
                          per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==5)
              {

                        vehciles_Mission_4[5].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[5].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[5].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point6");
                         #endif

                              if(restart_bool== false)
                      {
                          per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==6)
              {

                        vehciles_Mission_4[6].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[6].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[6].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point7");
                         #endif
                              if(restart_bool== false)
                      {
                          per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==7)
              {

                        vehciles_Mission_4[7].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[7].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[7].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point8");
                         #endif
                               if(restart_bool== false)
                      {
                           per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==8)
              {

                        vehciles_Mission_4[8].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[8].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[8].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point9");
                         #endif
                              if(restart_bool== false)
                      {
                           per_Mission_energy = 230f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==9)
              {

                        vehciles_Mission_4[9].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[9].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[9].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point10");
                         #endif
                            if(restart_bool== false)
                      {
                            per_Mission_energy = 230f;
                            Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 230f;
                         Invoke("Per_mission_Energy",4);
                      }

              }

        }
        else if(PlayerPrefs.GetInt("Daily_mission_no")==4)
        {
                   
                  Per_Mission_points = 12;
               
            
             if(PlayerPrefs.GetInt("parking_point_collected")==0)
              {

                       vehciles_Mission_4[0].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_4[0].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_4[0].transform.rotation;
                       #if UNITY_EDITOR
                       Debug.Log("parking_point1"); 
                       #endif
                          per_Mission_energy = 270f;
                       Invoke("Per_mission_Energy",4);

              }else if(PlayerPrefs.GetInt("parking_point_collected")==1)
              {
                   
                       vehciles_Mission_4[1].SetActive(false);
                       Parking_Point.transform.position   =  vehciles_Mission_4[1].transform.position;
                       Parking_Point.transform.rotation   =  vehciles_Mission_4[1].transform.rotation;
                      #if UNITY_EDITOR
                        Debug.Log("parking_point2"); 
                      #endif
                          if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==2)
              {

                        vehciles_Mission_4[2].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[2].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[2].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point3");
                       
                         #endif
                           if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }  else if(PlayerPrefs.GetInt("parking_point_collected")==3)
              {

                        vehciles_Mission_4[3].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[3].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[3].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point4");
                         #endif
                              if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==4)
              {

                        vehciles_Mission_4[4].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[4].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[4].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point5");
                         #endif
                         if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==5)
              {

                        vehciles_Mission_4[5].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[5].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[5].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point6");
                         #endif
                             if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==6)
              {

                        vehciles_Mission_4[6].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[6].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[6].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point7");
                         #endif
                           if(restart_bool== false)
                      {
                             per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==7)
              {

                        vehciles_Mission_4[7].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[7].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[7].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point8");
                         #endif
                           if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==8)
              {

                        vehciles_Mission_4[8].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[8].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[8].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point9");
                         #endif
                             if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                        
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==9)
              {

                        vehciles_Mission_4[9].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[9].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[9].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point10");
                         #endif
                            if(restart_bool== false)
                      {
                          per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==10)
              {

                        vehciles_Mission_4[10].SetActive(false);
                        Parking_Point.transform.position   =  vehciles_Mission_4[10].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[10].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point11");
                         #endif
                            if(restart_bool== false)
                      { 
                            per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }
                       

              }else if(PlayerPrefs.GetInt("parking_point_collected")==11)
              {
                     
                        vehciles_Mission_4[11].SetActive(false);    
                        Parking_Point.transform.position   =  vehciles_Mission_4[11].transform.position;
                        Parking_Point.transform.rotation   =  vehciles_Mission_4[11].transform.rotation;
                         #if UNITY_EDITOR
                         Debug.Log("parking_point12");
                         #endif
                            if(restart_bool== false)
                      {
                           per_Mission_energy = 270f;
                           Invoke("Get_time",4f);
                      }
                      else
                      {
                         per_Mission_energy = 270f;
                         Invoke("Per_mission_Energy",4);
                      }

                       PlayerPrefs.SetInt("Daily_mission_no",0);
                       

              }
              
              


        }
        else
        {
            
        }
    }

    void Get_time()
    {
        CountDown.instance.timeLeft = per_Mission_energy;
        CountDown.instance.timeLeft = PlayerPrefs.GetFloat("Last_time");
     
        CountDown.instance.pause_bool_timer = true;
       
        
        Invoke("Max_setting_Invo",4f);
        
      
    }

    void Max_setting_Invo()
    {
          
         CountDown.instance.Max_setting();
       

    }

   
  public  void Per_mission_Energy()
    {
      CountDown.instance.timeLeft = per_Mission_energy;
      
    }



    
}
