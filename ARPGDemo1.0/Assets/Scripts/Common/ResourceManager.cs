using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configDic;

        //在static之前要做，且只做一遍
        //静态构造函数
        //作用：初始化类的静态数据成员
        //时机：类被加载时调用一次
        static ResourceManager() 
        {
            configDic = new Dictionary<string, string>();
            //加载文件
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            //Debug.Log(fileContent);
            //解析文件(string --> Dictionary<string,string>)
            //BuildMap(fileContent);
            ConfigurationReader.Reader(fileContent, BulidMap);
        }

        private static void BulidMap(string line)
        {
            string[] keyValue = line.Split('=');
            configDic.Add(keyValue[0], keyValue[1]);
        }

        //建立资源字典，解析文件
        //public static void BuildMap(string fileContent)
        //{
        //    configDic = new Dictionary<string, string>();
        //    //StringReader字符串读取器，提供了逐行读取字符串功能
        //    using (StringReader reader = new StringReader(fileContent))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            //解析行数据
        //            string[] keyValue = line.Split('=');
        //            configDic.Add(keyValue[0], keyValue[1]);
        //        }
        //    }//当程序退出using代码块，将自动调用reader.Dispose()方法
        //}

        //Resources读取
        public static T Load<T>(string prefabName) where T : Object
        {
            //prefabName ---> prefabPath
            string prefabPath = configDic[prefabName];
            return Resources.Load<T>(prefabPath);
        }
    }
}
