using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCamera : MonoBehaviour
{   
    public bool isShow = false;                   
    public float leftTime;                        

    void Update()
    {
        if (isShow)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                this.gameObject.SetActive(false);
                isShow = false;
            }
        }
    }

    public void StartShow()
    {
        this.gameObject.SetActive(true);
        isShow = true;
        leftTime = 2f;
    }
}