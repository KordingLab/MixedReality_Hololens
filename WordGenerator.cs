using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGenerator : MonoBehaviour {
    
    //public Text WordText;

    public TextMeshPro WordText;

    private string m_word;
    /// <summary>
    /// 物体激活时调用，修改label上的文字内容
    /// </summary>
    private void OnEnable()
    {
        m_word = WordConf.GenWord();
       // WordText.text
      // WordText.text
        WordText.text = m_word;
    }

    /// <summary>
    /// 物体消失时调用
    /// </summary>
    private void OnDisable()
    {
        
    }
}
