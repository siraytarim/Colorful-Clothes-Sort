using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialLevel1 : MonoBehaviour
{
    [SerializeField] private RectTransform tickOne;
    [SerializeField] private RectTransform tickTwo;
    private Fields field;
    private void Awake()
    {
        field = GetComponent<Fields>();
        tickTwo.gameObject.SetActive(false);
    }

    private void Start()
    {
        
        tickTwo.DOAnchorPos3DZ(-200, .4f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (!(tickOne.gameObject.activeSelf))
        {
            tickTwo.gameObject.SetActive(true);
        }

        if(field.isFull)
            tickTwo.gameObject.SetActive(false);
    }

    private void Calis()
    {
        Debug.Log("lfg");
    }
}
