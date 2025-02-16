using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Hamle: MonoBehaviour
    {
        public static Hamle Instance;
        public GameObject kıyafet;
        public Fields öncekiAskılık;
        public Vector3 öncekiPozisyon;
    
        public Hamle(GameObject kıyafet, Fields öncekiAskılık, Vector3 öncekiPozisyon)
        {
            this.kıyafet = kıyafet;
            this.öncekiAskılık = öncekiAskılık;
            this.öncekiPozisyon = öncekiPozisyon;
        }
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
    }

   /* public void Clicked()
    {
        prevPos.Push(transform.position);
    }
    public void UndoMove()
    {
        if (prevPos.Count > 0)
        {
            Vector3 lastPosition = prevPos.Pop();
            transform.position = lastPosition;
        }
    }*/

