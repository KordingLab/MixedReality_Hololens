//---------------------------------------------------
// controll the action of each visual event, the time clock, and net message
//---------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This script should be attached to everything in the game scene.
/// </summary>

public enum ObjTypes {
    Empty = 0,
    Obj_Static = 1,
    Obj_Dynamic = 2,
    Obj_DynamicH = 3,
    Obj_DynamicV = 4,
    Text_Static = 5,
    Text_Dynamic = 6,

    // Max = 5,
}

public class Lab_1_Controller : MonoBehaviour
{
    #region object list
    /// <summary>
    /// Object list including static text, dynamic text, static object, dynamic object
    /// </summary>
    public List<Lab_1_StimulusBase> ObjList = new List<Lab_1_StimulusBase>();

    /// <summary>
    /// using to recording current object type
    /// </summary>
    ObjTypes CurObjType = ObjTypes.Empty;
    #endregion

    #region object data paramers
    /// <summary>
    /// filename using save object data in server
    /// </summary>
    string ObjDataFilename = "ObjData.txt";

    /// <summary>
    /// object data buffer using recording object data
    /// </summary>
    string ObjCacheStr = "";

    /// <summary>
    /// last object indexer
    /// </summary>
    int lastObjIndex = -1;
    #endregion

    #region camera data paramers
    /// <summary>
    /// filename using save camera data in server
    /// </summary>
    string CameraDataFilename = "CameraData.txt";
    
    /// <summary>
    /// the max data number in client buffer 
    /// </summary>
    int CameraSendDataLimitCount = 1500;

    /// <summary>
    /// the data number sending to server per experiment
    /// </summary>
    int CameraSendDataCount = 800;

    /// <summary>
    /// camera data buffer using recording camera data
    /// </summary>
    string CameraCacheStr = "";           

    /// <summary>
    /// camera data buffer counter
    /// </summary>
    int CameraCacheCount = 0;           
    #endregion

    #region sample paramers
    /// <summary>
    /// sample time
    /// </summary>
    float SampleTime = 0f;

    /// <summary>
    /// sample delta time
    /// </summary>
    float SampleDeltaTime = 0.025f;
    #endregion

    #region www progress paramers
    /// <summary>
    /// www progress max count
    /// </summary>
    int WWWCountMax = 1;   

    /// <summary>
    /// www progress current count
    /// </summary>
    int wwwCounter = 1;
    #endregion

    /// <summary>
    /// experiment finish flag
    /// </summary>
    bool HasFinished = false;                

    /// <summary>
    /// finish flag is used to tell participants that they have finished the experiment
    /// </summary>
    public GameObject LastFlag;
    Text LastFlgText;

    /// <summary>
    /// </summary>
    int FirstShowIndex = 0;            

    /// <summary>
    /// </summary>
    bool IsClearing = false;

    /// <summary>
    /// timer counter in the experiment
    /// </summary>
    float timeCounter = 0;

    /// <summary>
    /// time counter using to control objects' action ,including show, hide and pause
    /// </summary>
    float ObjRunTimeCounter = 0f;

    /// <summary>
    /// </summary>
    float PauseStartTime = 2000f;

    /// <summary>
    /// </summary>
    float PauseTime = 60f;

    /// <summary>
    /// the flag that the first time of entering pause 
    /// </summary>
    bool firstPause = true;

    /// <summary>
    /// </summary>
    float PauseTimeCounter = 0f;

    /// <summary>
    /// clear old server data in the function awake
    /// </summary>
    void Awake()
    {
        StartCoroutine(ClearData());
    }

    /// <summary>
    /// clear old data in server
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearData()
    {
        IsClearing = true;
        
        WWW www1 = new WWW("http://117.78.41.94/gaiqing/clear.php?filename=" + ObjDataFilename);
       // WWW www1 = new WWW("http://165.124.149.147/clear.php?filename=" + ObjDataFilename);
        yield return www1;
        www1.Dispose();
        
        WWW www2 = new WWW("http://117.78.41.94/gaiqing/clear.php?filename=" + CameraDataFilename);
        //WWW www1 = new WWW("http://165.124.149.147/clear.php?filename=" + ObjDataFilename);
        yield return www2;
        www2.Dispose();

        IsClearing = false;
    }

    /// <summary>
    /// hide all visual events in the list, check error, and start object action
    /// </summary>
    void Start()
    {
        LastFlgText = LastFlag.GetComponent<Text>();
        for (int i = 0; i < ObjList.Count; i++)
        {
            if (ObjList[i] == null)
            {
                Debug.LogError("the obj is not exist in objList: index=" + i);
            }
            else {
                ObjList[i].Ctrl = this;
                ObjList[i].gameObject.SetActive(false);
            }
        }

        LastFlag.SetActive(false);
        Active();
    }

    /// <summary>
    /// </summary>
    public void Active()
    {
        bool findObj = false;
        int index = 0;

        //recorde present time of visual events
        if (lastObjIndex != -1)
        {
            ObjCacheStr = ObjCacheStr + timeCounter + ";"; 
        }

        //the first show time
        if (FirstShowIndex < ObjList.Count)
        {
            ObjList[FirstShowIndex].ShowCountMax++; 
            index = FirstShowIndex;
            findObj = true;
            FirstShowIndex++;
        }
        else
        {
            //generate a random number
            index = UnityEngine.Random.Range(0, ObjList.Count); 
            
            for (int i = 0; i < ObjList.Count; i++)  
            {
                if (ObjList[index].IsCanUse())
                {
                    findObj = true;
                    break;
                }
                else
                {
                    index++;
                    index %= ObjList.Count;
                }
            }
        }

        if (findObj)
        {
            //recorde visual events type
            ObjCacheStr = ObjCacheStr + (int)ObjList[index].ObjType + ",";  
            ObjRunTimeCounter = timeCounter + ObjList[index].DelayTime;
            CurObjType = ObjTypes.Empty;
            lastObjIndex = index;
        }
        else
        {
            ObjRunTimeCounter = -1f;
            HasFinished = true;         
            LastFlag.SetActive(true);
        }
    }

    //call back object active function, and record data
    public void ObjActive(int index)
    {
        ObjRunTimeCounter = timeCounter + ObjList[index].Duration + 0.1f;

        ObjList[index].ShowCount++;
        //recorde action start time
        ObjCacheStr = ObjCacheStr + timeCounter + ","; 
        CurObjType = ObjList[index].ObjType;
        ObjList[index].Active();
    }
    
    /// <summary>
    /// handle pause
    /// collect visual events and camera data
    /// send net message
    /// </summary>
    void FixedUpdate()
    {
        //pause start
        if (timeCounter >= PauseStartTime && PauseTime >0)
        {
            //the first time in the duration of pause
            if (firstPause)
            {
                
                LastFlag.SetActive(true);
                firstPause = false;
            }

            LastFlgText.text = "relax time " + (int)PauseTime;
            PauseTime -= Time.deltaTime;

            //pause finish
            if (PauseTime <= 0)
            {
                LastFlag.GetComponent<Text>().text = "Thank you. You have completed this experiment.";
                LastFlag.SetActive(false);
            }
            return;
        }

        //call back active function
        if (ObjRunTimeCounter > 0 && timeCounter > ObjRunTimeCounter)
        {
            ObjActive(lastObjIndex);
        }

       
        //collect camera data
        if ((!HasFinished) && (CameraCacheCount <= CameraSendDataLimitCount))
        {
            Vector3 position = Camera.main.transform.position;
            Vector3 angle = Camera.main.transform.eulerAngles;
            string str = (int)CurObjType + "," + timeCounter + "," + position.x + "," + position.y + "," + position.z + "," + angle.x + "," + angle.y + "," + angle.z + ";";
            CameraCacheStr += str;
            CameraCacheCount++;
        }

        if ((!IsClearing) && (CameraCacheCount >= CameraSendDataCount) && (wwwCounter <= WWWCountMax)) {
            //StartCoroutine(SenData("http://165.124.149.147/index.php", CameraDataFilename, CameraCacheStr));
            StartCoroutine(SenData("http://117.78.41.94/gaiqing/index.php", CameraDataFilename, CameraCacheStr));
            //
            CameraCacheStr = "";
            CameraCacheCount = 0;
        }

        //send net data that collected in the duration of the last time of the experiment
        if ((!IsClearing) && (HasFinished) && (CameraCacheCount > 0) && (wwwCounter <= WWWCountMax))
        {
            //StartCoroutine(SenData("http://165.124.149.147/index.php", CameraDataFilename, CameraCacheStr));
            StartCoroutine(SenData("http://117.78.41.94/gaiqing/index.php", CameraDataFilename, CameraCacheStr));
            CameraCacheStr = "";
            CameraCacheCount = 0;
        }

        //send net data
        if ((!IsClearing) && (HasFinished) && (!string.IsNullOrEmpty(ObjCacheStr)) && (wwwCounter <= WWWCountMax))
        {
            StartCoroutine(SenData("http://117.78.41.94/gaiqing/index.php", ObjDataFilename, ObjCacheStr));
            //StartCoroutine(SenData("http://165.124.149.147/index.php", ObjDataFilename, ObjCacheStr));
            ObjCacheStr = "";
        }

        //timer
        timeCounter += Time.deltaTime;
    }

    /// <summary>
    /// send internet data
    /// </summary>
    /// <param name="url"></param>
    /// <param name="filename"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator SenData(string url, string filename, string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("filename", filename);
        form.AddField("text", text);

        //auto dispose by using
        using (WWW www = new WWW(url, form))
        {
            wwwCounter++;
            yield return www;
            wwwCounter--;
        }
    }
}
