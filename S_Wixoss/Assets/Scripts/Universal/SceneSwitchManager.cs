using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换管理
/// </summary>
public static class SceneSwitchManager
{
    #region properties

    private static Stack<GameScene> sceneStack;
    private static GameScene CurrentScene;

    #endregion

    static SceneSwitchManager()
    {
        sceneStack = new Stack<GameScene>();
        CurrentScene = GameScene.None;
    }

    public static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoadEnd;
    }

    /// <summary>
    /// 加载场景<c>（异步）</c>，<paramref name="lastScene"/>是None时不会加入到返回栈内
    /// </summary>
    /// <param name="scene">目标场景</param>
    /// <param name="lastScene">当前场景（上一场景）</param>
    public static void LoadScene(GameScene scene, GameScene lastScene)
    {
        if (lastScene != GameScene.None && scene != CurrentScene)
        {
            sceneStack.Push(lastScene);
            CurrentScene = scene;
        }
        LoadSceneAction(scene);
    }

    /// <summary>
    /// 加载场景（异步）并且从返回栈中弹出当前场景
    /// </summary>
    /// <param name="scene"></param>
    public static void LoadSceneAndPopCurrentScene(GameScene scene)
    {
        if (sceneStack.Count > 0)
        {
            sceneStack.Pop();
        }
        LoadSceneAction(scene);
    }

    /// <summary>
    /// 场景退回（当无场景可退时返回 false）
    /// </summary>
    /// <returns>场景退回是否成功</returns>
    public static bool SceneEscape()
    {
        if (sceneStack.Count > 0)
        {
            LoadSceneAction(sceneStack.Pop());
            return true;
        }

        return false;
    }

    public static void ClearEscapeStack()
    {
        sceneStack.Clear();
    }

    #region Private Method

    private static void LoadSceneAction(GameScene scene)
    {
        LoadingUIManager.Instance.Show();
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    private static void OnSceneLoadEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log($"Scene Load Complete: {scene.name}");
        LoadingUIManager.Instance.Hide();
    }

    #endregion
}