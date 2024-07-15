using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// ��Դ������
    /// </summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configDic;

        //��static֮ǰҪ������ֻ��һ��
        //��̬���캯��
        //���ã���ʼ����ľ�̬���ݳ�Ա
        //ʱ�����౻����ʱ����һ��
        static ResourceManager() 
        {
            configDic = new Dictionary<string, string>();
            //�����ļ�
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            //Debug.Log(fileContent);
            //�����ļ�(string --> Dictionary<string,string>)
            //BuildMap(fileContent);
            ConfigurationReader.Reader(fileContent, BulidMap);
        }

        private static void BulidMap(string line)
        {
            string[] keyValue = line.Split('=');
            configDic.Add(keyValue[0], keyValue[1]);
        }

        //������Դ�ֵ䣬�����ļ�
        //public static void BuildMap(string fileContent)
        //{
        //    configDic = new Dictionary<string, string>();
        //    //StringReader�ַ�����ȡ�����ṩ�����ж�ȡ�ַ�������
        //    using (StringReader reader = new StringReader(fileContent))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            //����������
        //            string[] keyValue = line.Split('=');
        //            configDic.Add(keyValue[0], keyValue[1]);
        //        }
        //    }//�������˳�using����飬���Զ�����reader.Dispose()����
        //}

        //Resources��ȡ
        public static T Load<T>(string prefabName) where T : Object
        {
            //prefabName ---> prefabPath
            string prefabPath = configDic[prefabName];
            return Resources.Load<T>(prefabPath);
        }
    }
}
