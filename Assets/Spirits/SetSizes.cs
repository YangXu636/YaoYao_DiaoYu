using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SetSizes : MonoBehaviour
{
    public Canvas canvas;
    public GameObject thisPanel;
    //public GameObject[] Panels_Zonings;
    //public int n_Functionlist = 3;
    //public int n_Content = 1;
    public UnityEngine.UI.Button[] Buttons_setsize;
    public ScrollRect[] sv;
    public float interval = 20f;
    public float SideLength = 60f;
    //public Button[] Button_Functionslist;

    public float canvas_width;
    public float canvas_height;
    public float panel_width;
    public float panel_height;
    public float content_width;
    public float content_height;

    public GameObject content;

    public enum SIZE_SELECTED
    {
        TYPRS = 0,
        FUNCTIONSLIST = 1,
        ADDFISH_LINE = 2,
        ADD_LINES = 3,
        ADDFISHES = 4,
        ADDCRAFT_LINE = 5
    };

    public SIZE_SELECTED SELECTED;
    //public float Types_Panels_width = 0.05f;
    //public float Functionslist_Panels_width = 0.15f;

    public float[] Addfish_line = { 0.1f, 0.1f, 0.5f, 0.2f, 0.1f };

    // Start is called before the first frame update
    void Start()
    {
        if (canvas == null) { canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); }
        thisPanel = transform.gameObject;
        Buttons_setsize = thisPanel.GetComponentsInChildren<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvas_width = canvasRect.rect.width;
        canvas_height = canvasRect.rect.height;
        RectTransform panelRect = thisPanel.GetComponent<RectTransform>();
        panel_width = panelRect.rect.width;
        panel_height = panelRect.rect.height;
        try
        {
            RectTransform contentRect = content.GetComponent<RectTransform>();
            content_width = contentRect.rect.width;
            content_height = contentRect.rect.height;
        }
        catch
        {
        }
        


        float temp;
        int i;
        switch (SELECTED)
        {
            case SIZE_SELECTED.TYPRS:
                temp = panel_height / 2.000f - interval - SideLength / 2.000f;
                for (i = 0; i < Buttons_setsize.Length - 1; i++)
                {
                    Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(SideLength, SideLength);
                    Buttons_setsize[i].transform.localPosition = new Vector3(0f, temp, 0f);
                    temp -= SideLength + interval;
                }
                temp = -panel_height / 2.000f + interval + SideLength / 2.000f;
                Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(SideLength, SideLength);
                Buttons_setsize[i].transform.localPosition = new Vector3(0f, temp, 0f);
                break;
            case SIZE_SELECTED.FUNCTIONSLIST:
                if (thisPanel.activeSelf)
                {
                    float _y = panel_height / Buttons_setsize.Length;
                    temp = panel_height / 2.000f - _y / 2.000f;
                    for (i = 0; i < Buttons_setsize.Length; i++)
                    {
                        Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(panel_width, _y);
                        Buttons_setsize[i].transform.localPosition = new Vector3(0f, temp, 0f);
                        temp -= _y;
                    }
                }
                break;
            case SIZE_SELECTED.ADDFISH_LINE:
                if (thisPanel.gameObject.activeSelf)
                {
                    UnityEngine.UI.Toggle delete = transform.Find("Delete").GetComponentInChildren<UnityEngine.UI.Toggle>();
                    TMP_Dropdown fishlevel = transform.Find("Level").GetComponentInChildren<TMP_Dropdown>();
                    TMP_InputField fishname = transform.Find("Name").GetComponentInChildren<TMP_InputField>();
                    TMP_InputField fishdecsribe = transform.Find("Describe").GetComponentInChildren<TMP_InputField>();
                    TMP_Dropdown fisheffect = transform.Find("Effect").GetComponentInChildren<TMP_Dropdown>();
                    TMP_InputField fisheffectmore = transform.Find("EffectMore").GetComponentInChildren<TMP_InputField>();
                    float newPanelWidth = panel_width - interval * 8 - SideLength;
                    temp = -panel_width / 2.00f + interval + SideLength / 2.000f;
                    delete.GetComponent<RectTransform>().sizeDelta = new Vector2(SideLength, SideLength);
                    delete.transform.localPosition = new Vector3(temp, 0f, 0f);
                    temp += interval * 2.0f + SideLength / 2.00f + newPanelWidth * Addfish_line[0] / 2.00f;
                    fishlevel.GetComponent<RectTransform>().sizeDelta = new Vector2(newPanelWidth * Addfish_line[0], SideLength);
                    fishlevel.transform.localPosition = new Vector3(temp, 0f, 0f);
                    temp += interval + newPanelWidth * Addfish_line[0] / 2.00f + newPanelWidth * Addfish_line[1] / 2.00f;
                    fishname.GetComponent<RectTransform>().sizeDelta = new Vector2(newPanelWidth * Addfish_line[1], SideLength);
                    fishname.transform.localPosition = new Vector3(temp, 0f, 0f);
                    temp += interval + newPanelWidth * Addfish_line[1] / 2.00f + newPanelWidth * Addfish_line[2] / 2.00f;
                    fishdecsribe.GetComponent<RectTransform>().sizeDelta = new Vector2(newPanelWidth * Addfish_line[2], SideLength);
                    fishdecsribe.transform.localPosition = new Vector3(temp, 0f, 0f);
                    temp += interval + newPanelWidth * Addfish_line[2] / 2.00f + newPanelWidth * Addfish_line[3] / 2.00f;
                    fisheffect.GetComponent<RectTransform>().sizeDelta = new Vector2(newPanelWidth * Addfish_line[3], SideLength);
                    fisheffect.transform.localPosition = new Vector3(temp, 0f, 0f);
                    temp += interval + newPanelWidth * Addfish_line[3] / 2.00f + newPanelWidth * Addfish_line[4] / 2.00f;
                    fisheffectmore.GetComponent<RectTransform>().sizeDelta = new Vector2(newPanelWidth * Addfish_line[4], SideLength);
                    fisheffectmore.transform.localPosition = new Vector3(temp, 0f, 0f);
                }
                break;
            case SIZE_SELECTED.ADD_LINES:
                if (thisPanel.gameObject.activeSelf)
                {
                    Buttons_setsize = thisPanel.GetComponentsInChildren<UnityEngine.UI.Button>();
                    thisPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, Buttons_setsize.Length * (interval + SideLength));
                    temp =  - interval - SideLength / 2.000f;
                    for (i=0;i< Buttons_setsize.Length; i++)
                    {
                        if (!Buttons_setsize[i].gameObject.activeSelf) { continue; }
                        Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(content_width, SideLength);
                        Buttons_setsize[i].transform.localPosition = new Vector3(panel_width / 2.000f,temp,0f);
                        temp -= interval + SideLength;
                    }
                }
                break;
            case SIZE_SELECTED.ADDFISHES:
                float onesix = 1.00f / 6.00f;
                //sv = gameObject.GetComponentsInChildren<ScrollRect>();
                Buttons_setsize = gameObject.GetComponentsInChildren<UnityEngine.UI.Button>();
                sv[0].GetComponent<RectTransform>().sizeDelta = new Vector2(panel_width, panel_height * 0.800f);
                sv[0].transform.localPosition = new Vector3(0f, panel_height * 0.100f, 0f);
                float tempy = -0.400f * panel_height;
                for (i = 0; i < Buttons_setsize.Length; i++)
                {
                    if (Buttons_setsize[i].name == "Button_GetFishes")
                    {
                        Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(panel_width * onesix, panel_height * 0.100f);
                        Buttons_setsize[i].transform.localPosition = new Vector3(-(panel_width * 5.000f) * (onesix / 2.000f), tempy + panel_height * 0.050f, 0f);
                    }
                    if (Buttons_setsize[i].name == "Button_Restting")
                    {
                        Buttons_setsize[i].GetComponent<RectTransform>().sizeDelta = new Vector2(panel_width * onesix, panel_height * 0.100f);
                        Buttons_setsize[i].transform.localPosition = new Vector3(-(panel_width * 5.000f) * (onesix / 2.000f), tempy - panel_height * 0.050f, 0f);
                    }
                }
                for (i = 1; i < sv.Length; i++)
                {
                    sv[i].GetComponent<RectTransform>().sizeDelta = new Vector2(panel_width * onesix, panel_height * 0.200f);
                    sv[i].transform.localPosition = new Vector3((-panel_width * 0.500f)+(i+onesix+(1.00f/3.00f))*onesix* panel_width, tempy, 0f);
                }
                break;
            case SIZE_SELECTED.ADDCRAFT_LINE:
                break;
        }
    }
}
