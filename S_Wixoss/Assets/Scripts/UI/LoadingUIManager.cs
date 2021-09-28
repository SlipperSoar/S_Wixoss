using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingUIManager : MonoBehaviour
{
    #region properties

    public static LoadingUIManager Instance;

    // Tips的显示对象
    [SerializeField]
    Text TipsView;

    [SerializeField]
    Image loadingImg;

    [Header("logo显隐曲线")]
    [SerializeField]
    AnimationCurve curve;

    // 使用txt储存Tips，返璞归真（不是
    private string[] Tips;

    private Action callback;

    [SerializeField]
    [Range(1, 10)]
    private float timeScale = 1;
    private float timeCount;

    #endregion

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        timeCount = 0;
        ParseTips();
        TipsView.text = GetTips();
        StartCoroutine(DelayInvoke());
    }

    void Start()
    {

    }

    void Update()
    {
        if (loadingImg == null)
        {
            return;
        }

        // 通过曲线获取图像的透明度
        var alpha = curve.Evaluate(GetTimeAxis());
        var newColor = loadingImg.color;
        newColor.a = alpha;
        loadingImg.color = newColor;

        // 0 - 1 - 2，把0-1的变化曲线以1为对称轴做对称处理，以此实现显隐循环（也就是在轴上0-1-0的往复运动）
        timeCount += Time.deltaTime;
        if (timeCount >= 2 * timeScale)
        {
            timeCount -= 2 * timeScale;
        }
    }

    #region Public Method

    public string GetTips()
    {
        return $"Tips: {LocalizedManager.Localizer(Tips[Random.Range(0, Tips.Length)])}";
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log("Show Loading");
            gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
        {
            Debug.Log("Hide Loading");
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void SetCallBack(Action callback)
    {
        this.callback = callback;
    }

    #endregion

    #region Private Method

    float GetTimeAxis()
    {
        float timeAxis;
        if (timeCount >= timeScale)
        {
            timeAxis = 2 * timeScale - timeCount;
        }
        else
        {
            timeAxis = timeCount;
        }

        return timeAxis / timeScale;
    }

    void ParseTips()
    {
        var tips = Resources.Load<TextAsset>("TextData/Tips").text.Replace('\r', '\n').Split('\n');
        List<string> temp_Tips = new List<string>();
        foreach (var tip in tips)
        {
            if (string.IsNullOrEmpty(tip))
            {
                continue;
            }
            temp_Tips.Add(tip);
        }

        Tips = temp_Tips.ToArray();
    }

    #endregion

    #region Coroutine

    IEnumerator DelayInvoke()
    {
        yield return new WaitForSeconds(2f);
    }

    #endregion
}
