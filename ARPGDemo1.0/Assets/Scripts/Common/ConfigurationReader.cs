using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// �����ļ���ȡ��
    /// </summary>
    public class ConfigurationReader
    {
        public static string GetConfigFile(string fileName)
        {
            string url;
            #region ��ƽ̨�ж�StreamingAssets��Դ·��
#if UNITY_EDITOR || UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
#elif UNITY_IPHONE
            url = "file://" + Application.dataPath + "/Raw/"+ fileName;
#elif UNITY_ANDROID
            url = "file://" + Application.dataPath + "!/Assets/"+ fileName;
#endif
            #endregion
            //WWW www = new WWW(url);�����õ�api
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (true)
            {
                if (www.downloadHandler.isDone)
                    return www.downloadHandler.text;
            }
        }


        public static void Reader(string fileContent,Action<string> handler)
        {
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    handler(line);
                }
            }
        }
    }
}
