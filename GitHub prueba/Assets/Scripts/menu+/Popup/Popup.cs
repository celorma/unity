using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Popup : MonoBehaviour
{
    [SerializeField] Button no;
    [SerializeField] Button yes;
    [SerializeField] Text textoNo;
    [SerializeField] Text textoYes;
    [SerializeField] Text textoPopup;


    public void Init(Transform canvas, string popupMsg, string btnNotxt, string btnYestxt, Action action)
    {
        textoPopup.text = popupMsg;
        textoNo.text = btnNotxt;
        textoYes.text = btnYestxt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        //no.OnPointerEnter(Instantiate(sonidoUp));

        no.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject);
        });
        yes.onClick.AddListener(() =>
        {
            action();
        });

    }

}
