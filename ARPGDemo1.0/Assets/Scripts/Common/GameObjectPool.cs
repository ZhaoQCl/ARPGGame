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
    1.所有频繁创建/销毁的物体，都通过对象池创建/回收
       GameObjectPool.Instance.CreateObject("类别",预制件,位置,旋转);
       GameObjectPool.Instance.CollectObject(游戏对象)
    2.需要通过对象池创建的物体，如需每次创建时执行，则让脚本实现IResetable接口
    */

    /// <summary>
    /// 可重置的
    /// </summary>
    public interface IResetable
    {
        abstract void OnReset();
    }


    /// <summary>
    /// 对象池
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
        /// 通过对象池 创建对象
        /// </summary>
        /// <param name="key">类别</param>
        /// <param name="prefab">需要创建实例的预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="rotate">角度</param>
        /// <returns></returns>
        public GameObject CreateObject(string key,GameObject prefab,Vector3 pos, Quaternion rotate)
        {
            //创建对象
            GameObject go = FindUsableObject(key);

            //没有被禁用的物体就要创建新的物体并填入池中
            if (go == null)
                go = AddObject(key, prefab); //创建对象  -->  Awake

            //使用
            UseObject(pos, rotate, go);
            return go;
        }

        //使用对象
        private void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);

            //抽象：接口
            //遍历执行物体中所有需要重置的逻辑
            if(go.GetComponents<IResetable>() != null)
                foreach(var item in go.GetComponents<IResetable>())
                {
                    item.OnReset();
                }
        }

        //添加对象池
        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            //如果池中没有key 则添加记录
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
            return go;
        }

        //查找可用对象
        private GameObject FindUsableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go">需要被回收的游戏对象</param>
        /// <param name="delay">延迟时间 默认为0</param>
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

            //遍历 字典 集合，不能修改内容
            //foreach(string key in cache.Keys) //异常：无效的操作
            //{
            //    Clear(key);
            //}

            //遍历LIst集合，存储了字典的key，修改字典不影响List集合
            foreach (string key in new List<string>(cache.Keys)) //异常：无效的操作
            {
                Clear(key);
            }
        }
    }
}
