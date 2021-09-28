using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GlobalStaticUIManager : MonoBehaviour
    {
        public static GlobalStaticUIManager Instance => instance;

        private static GlobalStaticUIManager instance;

        #region properties

        private GameObject ExitGamePanel;

        #endregion

        void Start()
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
            SceneSwitchManager.Init();
            Init();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                BackToPrevScene();
            }
        }

        #region Public Method

        public void BackToPrevScene()
        {
            var result = SceneSwitchManager.SceneEscape();
            if (!result)
            {
                // 是否要退出游戏
                if (!ExitGamePanel)
                {
                    ExitGamePanel = PopupView.Instance.ShowUniversalWindow("是否要退出游戏？",
                        onCancel: () => { },
                        onOk: () =>
                        {
                            Application.Quit(0);
                        });
                }
                else
                {
                    ExitGamePanel.SetActive(!ExitGamePanel.activeSelf);
                }
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 初始化各种常驻面板
        /// </summary>
        private void Init()
        {
            // var loadingPanel = Resources.Load<GameObject>("Prefabs/UI/LoadingPanel");
            // Instantiate(loadingPanel, transform);
            LoadingUIManager.Instance.Hide();
        }

        #endregion
    }
}