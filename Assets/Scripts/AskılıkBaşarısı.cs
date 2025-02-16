using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskılıkBaşarısı : MonoBehaviour
{
   public Animation glow;
   public Animation glowl;
   public Animation glowr;

   public void Succesfully()
   {
      glow.Play();
      glowl.Play();
      glowr.Play();
   }
}
