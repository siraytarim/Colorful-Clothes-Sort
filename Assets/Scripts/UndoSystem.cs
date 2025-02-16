using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{
   public Stack<Vector3> moves = new Stack<Vector3>();
   private bool isRecording = false;

   private void Update()
   {
      if (isRecording)
      {
         if (ClickControl.Instance.click % 2 == 0)
         {
            GameObject recordingObj = FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki;
            moves.Push(recordingObj.transform.position);
         }
      }
   }

   void startRecord()
   {
      isRecording = true;
      moves.Clear();
   }

   void stopRecoridng()
   {
      isRecording = false;
   }
}
