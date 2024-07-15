using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /*
    1.����Ƶ������/���ٵ����壬��ͨ������ش���/����
       GameObjectPool.Instance.CreateObject("���",Ԥ�Ƽ�,λ��,��ת);
       GameObjectPool.Instance.CollectObject(��Ϸ����)
    2.��Ҫͨ������ش��������壬����ÿ�δ���ʱִ�У����ýű�ʵ��IResetable�ӿ�
    */

    /// <summary>
    /// �����õ�
    /// </summary>
    public interface IResetable
    {
        abstract void OnReset();
    }


    /// <summary>
    /// �����
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;

        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// ͨ������� ��������
        /// </summary>
        /// <param name="key">���</param>
        /// <param name="prefab">��Ҫ����ʵ����Ԥ�Ƽ�</param>
        /// <param name="pos">λ��</param>
        /// <param name="rotate">�Ƕ�</param>
        /// <returns></returns>
        public GameObject CreateObject(string key,GameObject prefab,Vector3 pos, Quaternion rotate)
        {
            //��������
            GameObject go = FindUsableObject(key);

            //û�б����õ������Ҫ�����µ����岢�������
            if (go == null)
                go = AddObject(key, prefab); //��������  -->  Awake

            //ʹ��
            UseObject(pos, rotate, go);
            return go;
        }

        //ʹ�ö���
        private void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);

            //���󣺽ӿ�
            //����ִ��������������Ҫ���õ��߼�
            if(go.GetComponents<IResetable>() != null)
                foreach(var item in go.GetComponents<IResetable>())
                {
                    item.OnReset();
                }
        }

        //��Ӷ����
        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            //�������û��key ����Ӽ�¼
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        //���ҿ��ö���
        private GameObject FindUsableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        /// <summary>
        /// ���ն���
        /// </summary>
        /// <param name="go">��Ҫ�����յ���Ϸ����</param>
        /// <param name="delay">�ӳ�ʱ�� Ĭ��Ϊ0</param>
        public void CollectObject(GameObject go,float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go,delay));
        }
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }


        public void Clear(string key)
        {
            //foreach(GameObject go in cache[key])
            //{
            //    Destroy(go);
            //}

            for(int i = cache[key].Count -1; i >= 0; i--)
            {
                Destroy(cache[key][i]);
            }

            cache.Remove(key);
        }

        public void ClearAll()
        {

            //���� �ֵ� ���ϣ������޸�����
            //foreach(string key in cache.Keys) //�쳣����Ч�Ĳ���
            //{
            //    Clear(key);
            //}

            //����LIst���ϣ��洢���ֵ��key���޸��ֵ䲻Ӱ��List����
            foreach (string key in new List<string>(cache.Keys)) //�쳣����Ч�Ĳ���
            {
                Clear(key);
            }
        }
    }
}
