using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using HSVPicker;
namespace HSVPicker
{
    public class ColorSucker : UIBehaviour
    {
        public GameObject _HSVField;
        public Camera _camera;
        public ColorPicker picker;
        public Button _button;
        public Image _image;
        private Color curColor;
        #region sucker
        private Transform m_transform = null;//本脚本所在物体 
        private Transform m_suckScreen = null;//吸管所在物体父物体位置
        private Texture2D m_texture = null;//吸管截屏图片
        private Image m_screenImage = null;//图片放置位置
        private ImageMesh m_imageMesh = null;//网格位置
        #endregion
        internal enum E_WorkState : int
        {
            Normal = 0,
            Sucker = 1,
        }
        private E_WorkState m_workState = E_WorkState.Normal;
        private E_WorkState WorkState
        {
            get { return m_workState; }
            set
            {
                m_workState = value;
                switch (value)
                {
                    case E_WorkState.Normal://吸管隐藏，颜色面板显示，滑动条显示
                        m_suckScreen.gameObject.SetActive(false);
                        _HSVField.gameObject.SetActive(true);
                        //m_colorPalette.gameObject.SetActive(true);
                        //m_coloredPate.gameObject.SetActive(true);
                        break;
                    case E_WorkState.Sucker://显示吸管
                        m_suckScreen.gameObject.SetActive(true);
                        _HSVField.gameObject.SetActive(false);
                        //m_colorPalette.gameObject.SetActive(false);
                        //m_coloredPate.gameObject.SetActive(false);
                        break;
                }
            }
        }
        protected override void Start()
        {
            m_transform = this.transform;
            m_suckScreen = m_transform.Find("SuckScreen");
            m_screenImage = m_suckScreen.Find("Texture").GetComponent<Image>();
            m_imageMesh = m_suckScreen.Find("Mesh").GetComponent<ImageMesh>();
            _button.onClick.AddListener(() => { WorkState = E_WorkState.Sucker;  });
        }

        // Update is called once per frame
        void Update()
        {
            if (WorkState == E_WorkState.Sucker)
            {
                StartCoroutine(ScreenShot());
                if (Input.GetMouseButtonDown(0))
                {
                    curColor = m_screenImage.sprite.texture.GetPixel(m_imageMesh.XAxisCount / 2 + 1, m_imageMesh.YAxisCount / 2 + 1);
                    //SetNoniusPositionByColor();
                    WorkState = E_WorkState.Normal;
                    //_image.color = curColor;
                    picker.CurrentColor = curColor;
                }
            }
               
        }
        #region suck color 


        private IEnumerator ScreenShot()
        {
            var xCount = m_imageMesh.XAxisCount;
            var yCount = m_imageMesh.YAxisCount;
            m_texture = new Texture2D(xCount, yCount, TextureFormat.RGB24, false);
            yield return new WaitForEndOfFrame();            
            float m_positionx =Input.mousePosition.x;
            float m_positiony =Input.mousePosition.y;
            //Debug.Log(m_positionx+"+"+m_positiony);
            //Debug.Log(Screen.width + "+" + Screen.height);
            if (m_positionx<Screen.width- (int)(xCount / 2) && m_positionx>0&&m_positiony<Screen.height-(int)(yCount / 2) && m_positiony>0)
            {
                m_texture.ReadPixels(new Rect((int)Input.mousePosition.x - (int)(xCount / 2),
               (int)Input.mousePosition.y - (int)(yCount / 2), xCount, yCount), 0, 0);
            }           
            m_texture.Apply();
            m_screenImage.sprite = Sprite.Create(m_texture, new Rect(0, 0, xCount, yCount), Vector2.zero);

            //Rect rect = new Rect(0, 0, 1920, 1080);
            //RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
            //// 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
            //_camera.targetTexture = rt;
            //_camera.Render();
            //// 激活这个rt, 并从中中读取像素。  
            //RenderTexture.active = rt;
            //Texture2D screenShot = new Texture2D(m_imageMesh.XAxisCount, m_imageMesh.YAxisCount, TextureFormat.RGB24, false);
            //// 注：这个时候，它是从RenderTexture.active中读取像素 
            //screenShot.ReadPixels(new Rect((int)Input.mousePosition.x - (int)(m_imageMesh.XAxisCount / 2),
            //(int)Input.mousePosition.y - (int)(m_imageMesh.YAxisCount / 2), m_imageMesh.XAxisCount, m_imageMesh.YAxisCount), 0, 0);
            //screenShot.Apply();
            //// 重置相关参数，以使用camera继续在屏幕上显示  
            //_camera.targetTexture = null;
            //RenderTexture.active = null;
            //GameObject.Destroy(rt);
            //m_screenImage.sprite = Sprite.Create(screenShot, new Rect(0, 0, m_imageMesh.XAxisCount, m_imageMesh.YAxisCount), Vector2.zero);
            //yield return new WaitForEndOfFrame();
        }  
        #endregion
    }
}
