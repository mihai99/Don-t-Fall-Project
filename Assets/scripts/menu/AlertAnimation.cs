using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlertAnimation : MonoBehaviour
{
    public float Speed;
    public float AlphaSpeed;
    private CanvasGroup canvasGroup;
    public float ChangeAlphaDirectionTime;
    public void Init(string text, Color color)
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, 200, 0);
        GetComponentInChildren<Text>().text = text;
        GetComponentInChildren<Text>().color = color;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        Invoke("ChangeAlpha", ChangeAlphaDirectionTime);
        Invoke("Kill", ChangeAlphaDirectionTime * 2 + 1);
    }
    private void ChangeAlpha()
    {
        AlphaSpeed = -1 * AlphaSpeed;
    }
    private void Kill()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        transform.localPosition += new Vector3(0, Speed * Time.deltaTime, 0);
        canvasGroup.alpha += (AlphaSpeed * Time.deltaTime);
    }
}
