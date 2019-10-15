using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true;                      
    public bool destroy = false;                   
    public GameObject gameobject;                  
    public Transform transform;                    
    public ISSActionCallback callback;              

    protected SSAction() { }
    //子类可以使用下面这两个函数
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
