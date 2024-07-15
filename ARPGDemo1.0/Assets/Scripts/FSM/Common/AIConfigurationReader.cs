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
    /// AI配置文件读取器
    /// </summary>
    public class AIConfigurationReader
    {
        //数据结构
        //大字典：key 状态         value 映射
        //小字典：key 条件编号   value 状态编号
        public Dictionary<string, Dictionary<string, string>> Map { get; private set; }

        public AIConfigurationReader(string fileName) 
        {
            Map = new Dictionary<string, Dictionary<string, string>>();
            //读取配置文件
            string configFile = ConfigurationReader.GetConfigFile(fileName);
            //解析配置文件
            ConfigurationReader.Reader(configFile, BuildMap);
        }

        private string currentKey = string.Empty;

        private void BuildMap(string line)
        {
            //1.去除空白（如果空行则为空字符串）
            line.Trim();
            //if (line == "" || line == null) return;
            if (string.IsNullOrEmpty(line)) return;
            if (line.StartsWith("["))
            {
                currentKey = line.Substring(1, line.Length - 2);
                //2.状态
                Map.Add(currentKey, new Dictionary<string, string>());
            }
            else
            {
                //3.映射
                string[] keyValue = line.Split('>');
                Map[currentKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }
}
