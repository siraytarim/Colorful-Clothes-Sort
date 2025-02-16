using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextScene : MonoBehaviour
{
    public static NextScene Instance;
    [SerializeField] private int colorCount;
    public int completed = 0;
    public bool isFinish;
    [SerializeField] private RectTransform nextPanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        levelText.text = SceneManager.GetActiveScene().name;
    }

    public void Control()
    {
        completed++;
        if (completed == colorCount)
        {
            isFinish = true;
            Invoke("PanelActive",.5f);
        }
    }

    void PanelActive()
    {
        nextPanel.DOAnchorPos(new Vector2(0f,5f),0.3f);


    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
