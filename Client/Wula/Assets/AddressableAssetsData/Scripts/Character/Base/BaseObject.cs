using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int entityID { get; set; }
  
    
    public virtual void OnInit()
    {
        
    }

    public int GetId()
    {
        return entityID;
    }

    public virtual void Update()
    {
        
    }

    public virtual void OnDestroy()
    {
    }


}

