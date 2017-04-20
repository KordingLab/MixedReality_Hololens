//---------------------------------------------------
// this is the basic class of all visual events including all basic actions. The attribution of each visual event has been attached with the individual script
//---------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>

/// </summary>

public abstract class Lab_1_StimulusBase : MonoBehaviour
{
    /// <summary>
    /// visual events controller
    /// </summary>
    [HideInInspector]
    public Lab_1_Controller Ctrl;               

    /// <summary>
    /// time counter
    /// </summary>
    protected float TimeCount = 0f;             

    //Dynamic params
    public int ShowCountMax = 30;               
    public int ShowCount = 0;            
    //life time     
    public float Duration = 1;                
    public float DelayTime = 1f;               

    public Vector3 PositionStart = new Vector3(0, 0, 0); 
    public Vector3 PositionEnd = new Vector3(0, 0, 0);   

    public ObjTypes ObjType = ObjTypes.Obj_Static;  

    /// <summary>
    /// reset visual event state 
    /// </summary>
    public void Active()
    {
        transform.localPosition = PositionStart;
        gameObject.SetActive(true);
        TimeCount = 0;
    }

   /// <summary>
   /// action
   /// </summary>
    public void Action()
    {
        float disx = (PositionEnd.x - PositionStart.x) * (Time.deltaTime / Duration);
        float disy = (PositionEnd.y - PositionStart.y) * (Time.deltaTime / Duration);
        float disz = (PositionEnd.z - PositionStart.z) * (Time.deltaTime / Duration);
        transform.localPosition = transform.localPosition + new Vector3(disx, disy, disz);
    }

    /// <summary>
    /// do action
    /// </summary>
    protected void DoAction()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount <= Duration) {
            Action();
        }
        
        else if (Duration < TimeCount) {
            Ctrl.Active();
            gameObject.SetActive(false);  
        }
    }

    /// <summary>
    /// check visual event
    /// </summary>
    /// <returns>false, can not do action any more. true, can do action</returns>
    public bool IsCanUse() { return ShowCount < ShowCountMax; }

    // Update is called once per frame
    void Update()
    {
        DoAction();
    }
}
