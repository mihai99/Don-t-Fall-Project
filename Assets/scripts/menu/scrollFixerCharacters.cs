using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollFixerCharacters : MonoBehaviour
{
    public float minX;
    public float maxX;
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
            Mathf.Clamp(this.GetComponent<RectTransform>().transform.localPosition.x, minX, maxX),
            this.GetComponent<RectTransform>().transform.localPosition.y,
            0);
        if (ScrollbarComponent != null)
        {
            UpdateScrollbarPosition();
        }
    }

    private void UpdateScrollbarPosition()
    {
        ScrollbarComponent.value = Mathf.Abs(GetComponent<RectTransform>().localPosition.x / minX);
    }

    public void SetupScrollbar()
    {
        var parentSize = GetParentSize();
        var childsSize = GetChildsSize();
        Debug.Log("dada" + parentSize.ToString() + "fdsf" + childsSize.ToString());
            ScrollbarComponent.size = Mathf.Min(0.6f, parentSize / childsSize);
    }
    private float GetChildsSize()
    {
        var childsCount = transform.childCount;
        Debug.Log(childsCount);
        if (childsCount == 0)
            return 0.1f;
        return Mathf.Abs(
                transform.GetChild(childsCount - 1).GetComponent<RectTransform>().localPosition.x -
                transform.GetChild(0).GetComponent<RectTransform>().localPosition.x -
                transform.GetChild(0).GetComponent<RectTransform>().rect.width);
    }
    private float GetParentSize()
    {
        return transform.parent.GetComponent<RectTransform>().rect.width;
    }
}
