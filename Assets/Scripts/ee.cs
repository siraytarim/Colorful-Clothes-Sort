using UnityEngine;
using DG.Tweening;
namespace DefaultNamespace
{
    public class ee:MonoBehaviour
    {
        public static Hamle Instance;
        public GameObject kıyafet;
        public Fields öncekiAskılık;
        public Vector3 öncekiPozisyon;
    
        public ee(GameObject kıyafet, Fields öncekiAskılık, Vector3 öncekiPozisyon)
        {
            this.kıyafet = kıyafet;
            this.öncekiAskılık = öncekiAskılık;
            this.öncekiPozisyon = öncekiPozisyon;
        }
    }
}