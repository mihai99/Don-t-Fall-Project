using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scrollFixerShop : MonoBehaviour
{
    public float minY;
    public float maxY;
    public Scrollbar ScrollbarComponent;

    private void Start()
    {
        if (ScrollbarComponent != null)
        {
            SetupScrollbar();
        }
    }

    void Update()
    {
        this.GetComponent<RectTransform>().transform.localPosition = new Vector3(
            this.GetComponent<RectTransform>().transform.localPosition.x,
            Mathf.Clamp(this.GetComponent<RectTransform>().transform.localPosition.y, minY ,maxY ),
            0);

        if (ScrollbarComponent != null)
        {
            UpdateScrollbarPosition();
        }
    }
    private void UpdateScrollbarPosition()
    {
        ScrollbarComponent.value = GetComponent<RectTransform>().localPosition.y / maxY;
    }

    public void SetupScrollbar()
    {
        var parentSize = GetParentSize();
        var childsSize = GetChildsSize();      
    
        ScrollbarComponent.size = parentSize / childsSize;
    }
    private float GetChildsSize()
    {
        var childsCount = transform.childCount;
        Debug.Log(childsCount);
        if (childsCount == 0)
            return 0.1f;
        return Mathf.Abs(
                transform.GetChild(childsCount - 1).GetComponent<RectTransform>().localPosition.y -
                transform.GetChild(0).GetComponent<RectTransform>().localPosition.y -
                transform.GetChild(0).GetComponent<RectTransform>().rect.height);
    }
    private float GetParentSize()
    {
        return transform.parent.GetComponent<RectTransform>().rect.height;
    }
}
