using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorless : MonoBehaviour
{
    bool degistiMi;
    public Animator anim;

    public static Transform child ;
    private void Start()
    { 
        child = transform.GetChild(0);
        anim = GetComponent<Animator>();
    }

    public void Degistir(GameObject obj)
    {
        if (!degistiMi)
        {
            RandomSelection.Instance.RenkSecimi(obj);
            Destroy(obj.GetComponent<Colorless>());
        }
    }
}
