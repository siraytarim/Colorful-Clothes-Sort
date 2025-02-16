using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using UnityEngine.UI;
using DG.Tweening;

public class Fields : MonoBehaviour
{
    [Header("Core")] public List<GameObject> kıyafetList = new List<GameObject>();
    [SerializeField] GameObject basePos;
    [SerializeField] int kapasite = 0; // max=4;
    public bool isFull;
    [SerializeField] bool selected = false;
    private Collider collider;
    [SerializeField] private Transform selectedCloth;
    
    /*[Header("UI")]
    private Canvas myCanvas;
    [SerializeField] GameObject successConfetti;
    [SerializeField] Image conf;
    public Animator confAn;*/

    [Header("Stack")] public Stack<GameObject> kıyafetler = new Stack<GameObject>();
    public GameObject enÜstteki;
    public Vector3 enÜst;
    bool b = false;
    private void Start()
    {
        collider = GetComponent<Collider>();
        
        foreach (GameObject c in kıyafetList)
        {
            kıyafetler.Push(c);
            kapasite++;
        }

        if (kapasite == 4)
            isFull = true;
        else
            isFull = false;
        PeekStack(this.kıyafetler);
    }

    void PeekStack(Stack<GameObject> stack)
    {
        if (stack.Count != 0)
        {
            enÜstteki = stack.Peek().gameObject;
            enÜst = new Vector3(enÜstteki.transform.position.x, enÜstteki.transform.position.y, enÜstteki.transform.position.z); 
            
            if (RandomSelection.Instance.renksiz.Contains(enÜstteki))
            {
                enÜstteki.GetComponent<Colorless>().anim.SetTrigger("selectColor");
                RandomRenkSec(enÜstteki.GetComponent<Colorless>());
            }
        }
        else if (stack.Count == 0)
        {
            enÜstteki = null;
            enÜst = Vector3.zero;
        }
    }
    void RandomRenkSec(Colorless enüstteki)
    {
        enüstteki.Degistir(enüstteki.gameObject);
    }

    private void OnMouseDown()
    {
        ClickControl.Instance.click++;
        if (ClickControl.Instance.click == 1)
        {
            FieldsController.Instance.ilktıklanan = gameObject;
            enÜstteki.transform.DOMove(new Vector3(selectedCloth.position.x,selectedCloth.position.y-1,selectedCloth.position.z),.2f);
        }

        else if (ClickControl.Instance.click == 2)
        {
            FieldsController.Instance.ikincitıklanan = gameObject;
            MoveObject();
            ClickControl.Instance.click = 0;
        }
    }

    void MoveObject()
    {
        FieldsController.Instance.TagControl();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
           if(Physics.Raycast(ray,out hit, Mathf.Infinity))
           {
               FieldsController.Instance.ikincitıklanan = hit.collider.GetComponent<Fields>().gameObject;

               if (FieldsController.Instance.ilktıklanan == FieldsController.Instance.ikincitıklanan)
               {
                   enÜstteki.transform.DOMove(enÜst, .2f);
               }    
               //HEDEF DOLU DEĞİL + BOŞ DEĞİL
               else if (hit.collider.GetComponent<Fields>().kıyafetList.Count > 0)
               {
                   GameObject TargetObj = hit.collider.GetComponent<Fields>().enÜstteki;
                   Vector3 targetPos = hit.collider.GetComponent<Fields>().enÜst;

                   GameObject movingObj = FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki;
                   Debug.Log(movingObj.name);
                   Vector3 oldPosition = movingObj.transform.position;

                   if ((FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki
                           .CompareTag(TargetObj.tag)) &&
                       !(FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().isFull))
                   {
                           movingObj.transform.DOMove(new Vector3(targetPos.x,
                               targetPos.y - 0.670f, targetPos.z - 0.2f), .2f).SetEase(Ease.InQuad).OnComplete(() =>
                           {
                               hit.collider.GetComponent<Fields>().kıyafetList.Add(movingObj);
                               hit.collider.GetComponent<Fields>().kıyafetler.Push(movingObj);
                               hit.collider.GetComponent<Fields>().kapasite++;
                               hit.collider.GetComponent<Fields>().isFieldFull();
                               hit.collider.GetComponent<Fields>()
                                   .PeekStack(hit.collider.GetComponent<Fields>().kıyafetler);

                               if (FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList.Count >= 0)
                               {
                                   FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList
                                       .Remove(movingObj);
                                   FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler.Pop();
                                   FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kapasite--;
                                   FieldsController.Instance.ilktıklanan.GetComponent<Fields>().isFieldFull();
                                   FieldsController.Instance.ilktıklanan.GetComponent<Fields>().PeekStack(
                                       FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler);
                                   ColorControl();
                               }

                           });
                   }
                   else // tıklanan yerdeki renk aynı değil
                   {
                       FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki.transform.DOMove(
                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜst, .5f);
                       hit.collider.GetComponent<Fields>().transform.DOShakePosition(.4f, .1f);
                   }
               }
               // ALAN BOŞ
               else if (hit.collider.GetComponent<Fields>().kıyafetList.Count == 0)
               {
                   GameObject movingObj = FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki;
                   FieldsController.Instance.ikincitıklanan = hit.collider.GetComponent<Fields>().gameObject;

                   Vector3 targetPos = new Vector3(hit.collider.GetComponent<Fields>().basePos.transform.position.x,
                       hit.collider.GetComponent<Fields>().basePos.transform.position.y,
                       hit.collider.GetComponent<Fields>().basePos.transform.position.z);

                   movingObj.transform.DOMove(new Vector3(targetPos.x, targetPos.y, targetPos.z), .2f)
                       .SetEase(Ease.InQuad).OnComplete(() =>
                       {
                           hit.collider.GetComponent<Fields>().kıyafetList.Add(movingObj);
                           hit.collider.GetComponent<Fields>().kıyafetler.Push(movingObj);
                           hit.collider.GetComponent<Fields>().kapasite++;
                           hit.collider.GetComponent<Fields>().isFieldFull();
                           hit.collider.GetComponent<Fields>()
                               .PeekStack(hit.collider.GetComponent<Fields>().kıyafetler);

                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList.Remove(movingObj);
                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler.Pop();
                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kapasite--;
                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().isFieldFull();
                           FieldsController.Instance.ilktıklanan.GetComponent<Fields>().PeekStack(
                               FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler);
                           ColorControl();
                       });
               }
               //ALAN DOLU
                else if (hit.collider.GetComponent<Fields>().isFull)
               {
                      Debug.Log("DOLU");
               }
           }
    }

    void isFieldFull()
    {
        if (gameObject.GetComponent<Fields>().kapasite == 4)
        {
            isFull = true;
            Kontrol();
        }   
        else
            isFull = false;
    }

    public void Kontrol()
    {
        bool Completed = true;
        string tag = gameObject.GetComponent<Fields>().kıyafetList[0].tag;
        for (int i = 1; i < gameObject.GetComponent<Fields>().kıyafetList.Count; i++)
        {
            if (tag != gameObject.GetComponent<Fields>().kıyafetList[i].tag)
            {
                Completed = false;
            }
        }

        if (Completed)
        {
            Debug.Log(basePos.transform.position);
            //confAn.transform.position = basePos.transform.position;
            //confAn.SetBool("completed",true);
            FieldsController.Instance.confAn.SetBool("completed",true);
            NextScene.Instance.Control();
            collider.enabled = false; 
        }
    }

    void PanelKapat()
    {
        FieldsController.Instance.successConfetti.SetActive(false);
        //gameObject.GetComponent<Fields>().successConfetti.SetActive(false);
    }

    void ColorControl()
    {
        var sequence = DOTween.Sequence();
        List<GameObject> secilecekler = FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList;
        int i = secilecekler.Count -1 ;
        while (i >= 0)
        {
            if (secilecekler[i].CompareTag(FieldsController.Instance.gidentag) && !b &&
                (FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().isFull == false))
            {
                b = true;
                var moveTween = secilecekler[i].transform.DOMove(new Vector3(FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().enÜst.x,
                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().enÜst.y -
                    ((secilecekler.Count - 1 - i) * 0.63f) - 0.67f,
                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().enÜst.z - 0.2f), .2f);
                
                moveTween.OnComplete(() =>
                {
                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().kıyafetList.Add(
                        FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki);

                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().kıyafetler.Push(
                        FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki);

                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().kapasite++;
                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().isFieldFull();
                    FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().PeekStack   
                        (FieldsController.Instance.ikincitıklanan.GetComponent<Fields>().kıyafetler);

                    if (FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList.Count > 0)
                    { 
                        FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetList.Remove(
                            FieldsController.Instance.ilktıklanan.GetComponent<Fields>().enÜstteki);
                       FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler.Pop();
                        FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kapasite--;
                        FieldsController.Instance.ilktıklanan.GetComponent<Fields>().PeekStack
                            (FieldsController.Instance.ilktıklanan.GetComponent<Fields>().kıyafetler);
                    }
                });
                i--;
                sequence.Append(moveTween);
                sequence.AppendInterval(0.3f);  
                b = false;
            }
            else
                return;
        }
    }
   
}
