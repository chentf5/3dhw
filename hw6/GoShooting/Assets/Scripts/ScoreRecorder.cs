using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
    public int arrow_number;  
    public int score;                  
    public int target_score;           
            
    void Start()
    {
        score = 0;
        target_score = 60;
        arrow_number = 10;
    }
    
    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<RingData>().score;
        score = temp + score;
       
    }
}
