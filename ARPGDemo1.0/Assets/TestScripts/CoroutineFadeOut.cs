using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace ns 
{
    /// <summary>
    /// 
    /// </summary>
    public class CoroutineFadeOut : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Fade"))
            {
                StartCoroutine(Fade());
            }
        }

        private Material mt;

        private void Start()
        {
            mt = GetComponent<MeshRenderer>().material;
        }

        public float fadeSpeed = 10;

        //����
        private IEnumerator Fade()
        {
            Color currentColor = mt.color;
            while (currentColor.a > 0)
            {
                currentColor.a -= fadeSpeed * Time.deltaTime;
                mt.color = currentColor;
                yield return null;
            }
            currentColor.a = 0;
            mt.color = currentColor;
        }

        //��ɫ����
        public Color endColor;

        public AnimationCurve curve;

        public int time;

        private float x;
        public IEnumerator FadeOut()
        {
            Color orginalColor = mt.color;
            float y;
            for (float x = 0; x <= 1; x += Time.deltaTime / time)
            {
                y = curve.Evaluate(x);
                mt.color = Color.Lerp(orginalColor, endColor, y);
                yield return null;
            }
            //�������� AnimationCurve �����ã��ṩ��ֵ���ӻ��Ĳ�����塣
            //��ɫ��ֵ Color.Lerp �����ã�����ֵ�ı仯 ת��Ϊ ��ɫ�ı仯
            //             Vector3.Lerp
            //             Quaternion.Lerp
        }

    }
}
