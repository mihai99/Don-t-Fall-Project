using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class greyScreenHandler : MonoBehaviour
{
    private bool isShown;
    public bool CanBeClosed = true;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool isForShop;
    void Start()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        this.Hide();
    }
    public void Show()
    {
        this.isShown = true;
    }
    public void Hide()
    {
        if(CanBeClosed)
        {
            if(!isForShop)
            {
                this.isShown = false;
            }
            else if(GameObject.FindGameObjectWithTag("DimondShop").GetComponent<menuDisplayHandler>().shown == false)
            {
                isShown = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isShown && canvasGroup.alpha <= 1)
        {
           canvasGroup.alpha += 5 * Time.deltaTime;
            if(canvasGroup.alpha >= 0.7)
            {
                canvasGroup.blocksRaycasts = true;               
            }
        }
        if (!isShown && canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= 5 * Time.deltaTime;
            if (canvasGroup.alpha <= 0.05)
            {
                canvasGroup.blocksRaycasts = false;
                if (transform.childCount != 0)
                    transform.GetChild(0).gameObject.SetActive(true);
                var text = GetComponentInChildren<Text>();
            }           
        }

    }
}
