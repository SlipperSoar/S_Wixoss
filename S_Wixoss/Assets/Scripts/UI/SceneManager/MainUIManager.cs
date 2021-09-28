using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.SceneManager
{
    public class MainUIManager : MonoBehaviour
    {
        #region properties

        #region Scene Object

        [SerializeField] private Button BattleButton;

        #endregion

        #endregion

        void Start()
        {
            BattleButton.onClick.AddListener(OnBattleButton);
        }

        void Update()
        {

        }

        #region Public Method



        #endregion

        #region Private Method

        private void OnBattleButton()
        {

        }

        #endregion
    }
}