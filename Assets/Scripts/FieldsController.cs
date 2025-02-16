using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldsController : MonoBehaviour
{
    public static FieldsController Instance { get; private set; }
    public Fields[] Fields;

    public GameObject ilktıklanan,ikincitıklanan;
    public string gidentag;
    
    [Header("UI")]
    private Canvas myCanvas;
    public GameObject successConfetti;
    public Image conf;
    public Animator confAn;
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

    public void TagControl()
    {
        gidentag = ilktıklanan.GetComponent<Fields>().kıyafetler.Peek().tag;
    }
   
}
