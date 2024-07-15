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
    /// 配置文件读取器
    /// </summary>
    public class ConfigurationReader
    {
        public static string GetConfigFile(string fileName)
        {
            string url;
            #region 分平台判断StreamingAssets资源路径
#if UNITY_EDITOR || UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
#elif UNITY_IPHONE
            url = "file://" + Application.dataPath + "/Raw/"+ fileName;
#elif UNITY_ANDROID
            url = "file://" + Application.dataPath + "!/Assets/"+ fileName;
#endif
            #endregion
            //WWW www = new WWW(url);被弃用的api
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
