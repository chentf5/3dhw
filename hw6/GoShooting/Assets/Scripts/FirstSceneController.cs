using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
   
    public Camera child_camera;                                      
    public Camera main_camera;                                       
    public ScoreRecorder recorder;                                   
    public ArrowFactory arrow_factory;                               
    public ArrowFlyActionManager action_manager;                     
    private int[] targetscore = { 60 };                 
    private int round = 0;                                           
    public GameObject bow;                                         
    private GameObject arrow;                                                        
    private GameObject target;                                           
    private int arrow_num = 0;                                       
    private List<GameObject> arrow_queue = new List<GameObject>();   
    private bool game_over = false;                                  
    private bool game_start = false;                                 
    private bool iswin = false;
    private string wind = "";                                        
    private float wind_directX;                                               
    private float wind_directY;                                      

    void Start ()
    {
        SSDirector director = SSDirector.GetInstance();
        arrow_factory = Singleton<ArrowFactory>.Instance;
        recorder = Singleton<ScoreRecorder>.Instance;
        director.CurrentScenceController = this;
        action_manager = gameObject.AddComponent<ArrowFlyActionManager>() as ArrowFlyActionManager;
        LoadResources();
        main_camera.GetComponent<CameraFlow>().bow = bow;
        
        wind_directX = Random.Range(-1, 1);
        wind_directY = Random.Range(-1, 1);
        
        CreateWind();
    }
	
	void Update ()
    {
        if(game_start)
        {
            for (int i = 0; i < arrow_queue.Count; i++)
            {
                GameObject temp = arrow_queue[i];
                
                if (temp.transform.position.z > 30 || arrow_queue.Count > 5)
                {
                    arrow_factory.FreeArrow(arrow_queue[i]);
                    arrow_queue.Remove(arrow_queue[i]);
                }
            }
        }
    }
    public void LoadResources()
    {
        bow = Instantiate(Resources.Load("Prefabs/bow", typeof(GameObject))) as GameObject;
        bow.transform.rotation = Quaternion.Euler(90f, 0.0f, 90f);
        target = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject))) as GameObject;
    }
    
    public void MoveBow(float offsetX, float offsetY)
    {
        
        if (game_over || !game_start)
        {
            return;
        }
        
        if (bow.transform.position.x > 5)
        {
            bow.transform.position = new Vector3(5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if(bow.transform.position.x < -5)
        {
            bow.transform.position = new Vector3(-5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y < -3)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, -3, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y > 5)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, 5, bow.transform.position.z);
            return;
        }

        
        offsetY *= Time.deltaTime;
        offsetX *= Time.deltaTime;
        bow.transform.Translate(0, -offsetX, 0);
        bow.transform.Translate(0, 0, -offsetY);
    }

    public void Shoot()
    {
        if((!game_over || game_start) && arrow_num <= 10)
        {
            arrow = arrow_factory.GetArrow();
            arrow_queue.Add(arrow);
            
            Vector3 wind = new Vector3(wind_directX, wind_directY, 0);
            
            action_manager.ArrowFly(arrow, wind);
            
            child_camera.GetComponent<ChildCamera>().StartShow();
            
            recorder.arrow_number--;
          
            arrow_num++;
        }
    }
    
    public int GetScore()
    {
        return recorder.score;
    }
    
    public int GetTargetScore()
    {
        return recorder.target_score;
    }
    
    public int GetResidueNum()
    {
        return recorder.arrow_number;
    }
    
    public bool GetGameover()
    {
        return game_over;
    }
    public bool GetisWin()
    {
        return iswin;
    }
    
    public string GetWind()
    {
        return wind;
    }
    
    public void Restart()
    {
        game_over = false;
        recorder.arrow_number = 10;
        recorder.score = 0;
        recorder.target_score = 60;
        round = 0;
        arrow_num = 0;
        for (int i = 0; i < arrow_queue.Count; i++)
        {
            arrow_factory.FreeArrow(arrow_queue[i]);
        }
        arrow_queue.Clear();
    }
    
    public void BeginGame()
    {
        game_start = true;
    }



public void CheckGamestatus()
    {
        if(recorder.arrow_number > 0 && recorder.score >= recorder.target_score)
        {
            game_over = true;
            iswin = true;
        }

        if (recorder.arrow_number <= 0 && recorder.score < recorder.target_score)
        {
            game_over = true;
            iswin = false;
            return;
        }
        else if (recorder.arrow_number <= 0 && recorder.score >= recorder.target_score)
        {
            
            arrow_num = 0;
        
            
             game_over = true;
            iswin = true;
         
            for (int i = 0; i < arrow_queue.Count; i++)
            {
                arrow_factory.FreeArrow(arrow_queue[i]);
            }
            arrow_queue.Clear();
            recorder.arrow_number = 10;
            recorder.score = 0;
            recorder.target_score = 60;
        }
        
        wind_directX = Random.Range(-(round + 1), (round + 1));
        wind_directY = Random.Range(-(round + 1), (round + 1));
        CreateWind();
    }
    
    public void CreateWind()
    {
        string Horizontal = "", Vertical = "", level = "";
        if (wind_directX > 0)
        {
            Horizontal = "西";
        }
        else if (wind_directX <= 0)
        {
            Horizontal = "东";
        }
        if (wind_directY > 0)
        {
            Vertical = "南";
        }
        else if (wind_directY <= 0)
        {
            Vertical = "北";
        }
        if ((wind_directX + wind_directY) / 2 > -1 && (wind_directX + wind_directY) / 2 < 1)
        {
            level = "1 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -2 && (wind_directX + wind_directY) / 2 < 2)
        {
            level = "2 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -3 && (wind_directX + wind_directY) / 2 < 3)
        {
            level = "3 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -5 && (wind_directX + wind_directY) / 2 < 5)
        {
            level = "4 级";
        }

        wind = Horizontal + Vertical + "风" + " " + level;
    }

}