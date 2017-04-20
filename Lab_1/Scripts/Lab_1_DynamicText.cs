//---------------------------------------------------
//set stimulus parameters for dynamic text
//---------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to each UGUI
/// </summary>

public class Lab_1_DynamicText : Lab_1_StimulusBase
{
    void Awake()
    {
        ObjType = ObjTypes.Text_Dynamic;
    }
}
