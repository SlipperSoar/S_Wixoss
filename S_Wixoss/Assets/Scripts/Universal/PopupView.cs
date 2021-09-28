using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// 弹窗界面
    /// </summary>
    public class PopupView : MonoBehaviour
    {
        public static PopupView Instance => instance;

        private static PopupView instance;

        #region properties

        [SerializeField] private Transform PopupParent;

        #endregion

        #region Resources

        private static GameObject UniversalPopupViewPrefab;
        private const string PopupUIPath = "Prefabs/UI/Popup";

        #endregion

        static PopupView()
        {

        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
            UniversalPopupViewPrefab = Resources.Load<GameObject>($"{PopupUIPath}/UniversalPopup");
            if (!PopupParent)
            {
                PopupParent = transform;
            }
        }

        /// <summary>
        /// 显示错误弹窗
        /// </summary>
        public GameObject ShowErrorWindow(string message = "未知错误", UnityAction onCancel = null, UnityAction onOk = null)
        {
            return ShowUniversalWindow(message, PopupType.Error, onCancel, onOk);
        }

        /// <summary>
        /// 显示通用弹窗
        /// </summary>
        /// <param name="onCancel"></param>
        /// <param name="onOk"></param>
        public GameObject ShowUniversalWindow(string message, PopupType popupType = PopupType.Normal, UnityAction onCancel = null, UnityAction onOk = null)
        {
            var universalPopup = Instantiate(UniversalPopupViewPrefab, PopupParent).GetComponent<UniversalPopupView>();
            universalPopup.Initialize(message, popupType, onCancel, onOk);
            return universalPopup.gameObject;
        }

        /// <summary>
        /// 显示特殊弹窗
        /// </summary>
        /// <typeparam name="T">需要返回的类型实例</typeparam>
        /// <param name="name">弹窗名</param>
        /// <returns>弹窗携带的类型实例</returns>
        public T ShowSpecialWindow<T>(string name) where T : MonoBehaviour
        {
            var panel = Resources.Load<GameObject>($"{PopupUIPath}/{name}");
            panel = Instantiate(panel, PopupParent);
            return panel.GetComponent<T>();
        }
    }
}
