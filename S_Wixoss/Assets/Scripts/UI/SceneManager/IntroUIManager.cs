using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.SceneManager
{
    public class IntroUIManager : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            if (Input.anyKey)
            {
                SceneSwitchManager.LoadScene(GameScene.MainScene, GameScene.None);
            }
        }
    }
}
