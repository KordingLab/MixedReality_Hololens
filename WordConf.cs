using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordConf {

    /// <summary>
    /// 单词列表
    /// </summary>
    private static string[] m_WordList = new string[] {
        "time",
        "person",
        "year",
        "thing",
        "world",
        "life",
        "hand",
        "part",
        "child",
        "woman",
        "place",
        "work",
        "week",
        "case",
        "point",
        "government",
        "company",
        "number",
        "group",
        "problem",
        "fact",
        "have",
        "make",
        "know",
        "take",
        "come",
        "think",
        "look",
        "want",
        "give",
        "find",
        "tell",
        "work",
        "seem",
        "feel",
        "leave",
        "call",
        "good",
        "first",
        "last",
        "long",
        "great",
        "little",
        "other",
        "right",
        "high",
        "different",
        "small",
        "large",
        "next",
        "early",
        "young",
        "important",
        "public",
        "same",
        "able",
        "with",
        "from",
        "about",
        "into",
        "over",
        "after",
         "that",
        "this",
        "they",
       "will",
       "would",
        "there",
        "their"
};

    /// <summary>
    /// 使用过的单词缓存列表，满了之后全部清空
    /// </summary>
    private static string[] m_LastUseStrList = new string[] {"","",""};

    /// <summary>
    /// 缓存单词列表的长度
    /// </summary>
    private static int m_lastUseListLength = 3;

    /// <summary>
    /// 缓存列表使用的索引位置，0表示尚未使用
    /// </summary>
    private static int m_LastUseStrIndex = 0;

    /// <summary>
    /// 随机生成一个单词
    /// </summary>
    /// <returns></returns>
    public static string GenWord() {
        int len = m_WordList.Length;
        if (len < 3) {
            return "";
        }

        string word;

        while (true) {
            int i = Random.Range(0, len);
            word = m_WordList[i];

            bool exists = ((IList)m_LastUseStrList).Contains(word);
            if (!exists) {
                break;
            }
        }

        PushToCache(word);
        return word;
    }

    /// <summary>
    /// 将使用过的单词放入缓存中，用于去重
    /// </summary>
    /// <param name="word"></param>
    private static void PushToCache(string word) {
        //清空缓存数据
        if (m_LastUseStrIndex >= m_lastUseListLength) {
            m_LastUseStrIndex = 0;
            m_LastUseStrList = new string[m_lastUseListLength];
        }

        Debug.Log(m_LastUseStrIndex);

        Debug.Log(m_LastUseStrList);
        m_LastUseStrList[m_LastUseStrIndex] = word;
        m_LastUseStrIndex++;
    }
}
