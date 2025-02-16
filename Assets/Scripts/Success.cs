using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    public static Success Instance;
    bool completed = false;
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
    public void Kontrol()
    { 
        //BAŞARI KONTROLÜ SORUNUNU ÇÖZ
        string tag = gameObject.GetComponent<Fields>().kıyafetList[0].tag;
        completed = true;
        for (int i = 1; i < gameObject.GetComponent<Fields>().kıyafetList.Count; i++)
        {
            if (tag != gameObject.GetComponent<Fields>().kıyafetList[i].tag)
                completed = false;
        }

        if (completed)
        {
            Debug.Log("tamamlandı");
           // gameObject.GetComponent<Fields>().Panel.SetActive(true);
            Invoke("PanelKapat", .8f);
        }
    }
     void PanelKapat()
    {
      //  gameObject.GetComponent<Fields>().Panel.SetActive(false);
        NextScene.Instance.Control();

    }
}
