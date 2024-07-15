using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * 1.���������룺�̳���Editor�ֻ࣬��Ҫ��Unity��������ִ�еĴ���
 * 2.�˵��� ����[Menu Item("....")]������������Ҫ��Unity�������ֲ����˵���ť�ķ���
 * 3.AssetDatabase��ֻ�����ڱ�������ִ�в�����Դ����ع���
 * 4.StreamingAssets��Unity����Ŀ¼֮һ����Ŀ¼�е��ļ����ᱻѹ�����ʺ����ƶ��˶�ȡ��Դ����PC�˻�����д�룩
 *    �־û�·�� Application.persistentDataPath ����������ʱ���ж�д������Unity�ⲿĿ¼����װ����ʱ�Ų�����
 */

/// <summary>
/// ������Դ�����ļ�
/// �ṹ��Ԥ�Ƽ�����=Ԥ�Ƽ�����·��
/// </summary>
public class GenerateResConfig:Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    public static void Generate()
    {
        //������Դ�����ļ�
        //1.����ResourcesĿ¼������Ԥ�Ƽ�����·��
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
        for(int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //Assets/Resources/Skill/...
            //2.���ɶ�Ӧ��ϵ  ����=·��
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string pathName = resFiles[i].Replace( "Assets/Resources/",string.Empty).Replace(".prefab",string.Empty);
            resFiles[i] = fileName + "=" + pathName;
        }

        //3.д���ļ�
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);
        //ˢ��
        AssetDatabase.Refresh();
    }
}

