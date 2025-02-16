using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSelection : MonoBehaviour
{
  public static RandomSelection Instance;
    
    public List<Material> colors = new List<Material>();  // Materyal listesi
    public List<GameObject> clothes = new List<GameObject>();  // Kıyafetler
    public List<GameObject> renksiz = new List<GameObject>();  // Renk olmayan kıyafetler
    public List<renkler> _levelRenkleri = new List<renkler>();  // Renkler listesi

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
        // Rengi siyah olan kareleri bul ve renksiz listesine ekle
        for (int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].tag == "Hided")
            {
                renksiz.Add(clothes[i]);
            }
        }

        // Siyah karelere Colorless scriptini ekle
        for (int i = 0; i < renksiz.Count; i++)
        {
            renksiz[i].AddComponent<Colorless>();
        }

        // Renkler enum'ını _levelRenkleri listesine ekle
        _levelRenkleri.AddRange(Enum.GetValues(typeof(renkler)) as renkler[]);
    }

    // Renk seçimi metodu
    public void RenkSecimi(GameObject obj)
    {
        StartCoroutine(RenkSecimiCoroutine(obj));
    }

    // Coroutine ile renk seçimi
    IEnumerator RenkSecimiCoroutine(GameObject obj)
    {
        string ta = obj.transform.GetChild(0).tag;  // Objeye ait tag alınıyor
        renkler t = _levelRenkleri[Random.Range(0, _levelRenkleri.Count)];  // Rastgele renk seçimi

        switch (ta)
        {
            case "White":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "White";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
            case "Green":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "Green";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
            case "Yellow":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "Yellow";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
            case "Purple":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "Purple";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
            case "Blue":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "Blue";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
            case "Pink":
                yield return new WaitForSeconds(0.2f);
                obj.tag = "Pink";
                _levelRenkleri.Remove(t);
                renksiz.Remove(obj);
                break;
        }

        Debug.Log(ta);  // Tag'ı yazdır
    }
}

// Enum tanımı
public enum renkler
{
    mavi,
    yeşil,
    sarı,
    mor,
    beyaz
}