using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Text tex;
    private int defSize = 10;
    private int addSize = 20;
    private int timer;
    private IEnumerator coroutine;

    void Start()
    {
        tex = transform.Find("Text").GetComponent<Text>();
        defSize = tex.GetComponent<Text>().fontSize;
        addSize = defSize;
        coroutine = AnimateText();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        timer = 0;
        StartCoroutine(coroutine);

        //tex.GetComponent<Text>().fontSize = 50;
        //tex.GetComponent<Text>().fontSize = defSize + timer;
        //tex.GetComponent<Text>().fontSize = defSize +  (int)((Mathf.Sin(timer/100f) + 1f) * 0.5f)*10;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(coroutine);
        tex.GetComponent<Text>().fontSize = defSize;
    }

    private IEnumerator AnimateText()
    {
        while (true)
        {
            tex.GetComponent<Text>().fontSize = defSize + (int)((Mathf.Sin(timer / 50f) + 1f) * 0.5f * addSize);
            timer++;
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
    }
}
