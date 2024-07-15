using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

/*
 * 
 * 
 */
namespace AI.FSM
{
    /// <summary>
    /// AI�����ļ���ȡ��
    /// </summary>
    public class AIConfigurationReader
    {
        //���ݽṹ
        //���ֵ䣺key ״̬         value ӳ��
        //С�ֵ䣺key �������   value ״̬���
        public Dictionary<string, Dictionary<string, string>> Map { get; private set; }

        public AIConfigurationReader(string fileName) 
        {
            Map = new Dictionary<string, Dictionary<string, string>>();
            //��ȡ�����ļ�
            string configFile = ConfigurationReader.GetConfigFile(fileName);
            //���������ļ�
            ConfigurationReader.Reader(configFile, BuildMap);
        }

        private string currentKey = string.Empty;

        private void BuildMap(string line)
        {
            //1.ȥ���հף����������Ϊ���ַ�����
            line.Trim();
            //if (line == "" || line == null) return;
            if (string.IsNullOrEmpty(line)) return;
            if (line.StartsWith("["))
            {
                currentKey = line.Substring(1, line.Length - 2);
                //2.״̬
                Map.Add(currentKey, new Dictionary<string, string>());
            }
            else
            {
                //3.ӳ��
                string[] keyValue = line.Split('>');
                Map[currentKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }
}
