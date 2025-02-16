using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialLevel : MonoBehaviour
{
    public RectTransform tickOne;
    private void Start()
    { 
         tickOne.DOAnchorPos3DZ(-190,.4f).SetEase(Ease.OutQuad).SetLoops(-1,LoopType.Yoyo);
    }
    
    private void OnMouseDown()
    {
            tickOne.gameObject.SetActive(false);
    }

}
