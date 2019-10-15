using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlyAction : SSAction
{
    public Vector3 force;                      
    public Vector3 wind;                      
    private ArrowFlyAction() { }
    public static ArrowFlyAction GetSSAction(Vector3 wind)
    {
        ArrowFlyAction action = CreateInstance<ArrowFlyAction>();
        
        action.force = new Vector3(0, 0, 20);
        action.wind = wind;
        return action;
    }

    public override void Update(){}

    public override void FixedUpdate()
    {
        
        this.gameobject.GetComponent<Rigidbody>().AddForce(wind, ForceMode.Force);

        
        if (this.transform.position.z > 30 || this.gameobject.tag == "hit")
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,this.gameobject);
        }
    }
    public override void Start()
    {
        gameobject.transform.parent = null;
        gameobject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameobject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
