using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// 通用弹窗
    /// </summary>
    public class UniversalPopupView : MonoBehaviour
    {
        #region properties

        [SerializeField] private Button cancel;

        [SerializeField] private Button ok;

        [SerializeField] private Text message;

        [SerializeField] private Image icon;

        #endregion

        public void Initialize(string message, PopupType popupType = PopupType.Normal, UnityAction onCancel = null, UnityAction onOk = null)
        {
            this.message.text = LocalizedManager.Localizer(message);
            icon.sprite = Resources.Load<Sprite>($"Image/Feature/Popup/{popupType}");

            // 默认为关闭弹窗方法
            cancel.onClick.AddListener(CloseSelf);
            ok.onClick.AddListener(CloseSelf);

            if (onCancel == null)
            {
                cancel.gameObject.SetActive(onOk == null);
            }
            else
            {
                cancel.onClick.AddListener(onCancel);
            }

            if (onOk != null)
            {
                ok.onClick.AddListener(onOk);
            }
        }

        /// <summary>
        /// 关闭本弹窗
        /// </summary>
        private void CloseSelf()
        {
            Destroy(gameObject);
        }
    }
}