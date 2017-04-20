using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWWClient : MonoBehaviour
{


    IEnumerator Start()
    {
        WWW www = new WWW("http://165.124.149.147/index.php?filename=StartTest.txt&text=lallala"); yield return www;
        if (www.error != null)
        {
            Debug.Log("error:" + www.error);

        }
        else
        {
            Debug.Log("request ok login: " + www.text);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
