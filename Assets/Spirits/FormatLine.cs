using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FormatLine : MonoBehaviour
{
    public FormatLine formatLine;

    public enum FORMATLINE_TYPES
    {
        FISH = 0,
        CRAFTING = 1
    }
    public FORMATLINE_TYPES SELECTED;

    public struct FISHINFORMATION
    {
        public string ����;
        public string ���;
        public string[] Ч��;
    }
    public FISHINFORMATION FishInformation;
    public GameObject line;
    public GameObject newLine;
    public GameObject addLine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Hashtable GetAddFish()
    {
        if (!gameObject.activeSelf) { return new Hashtable { { -1, "QF" } }; }
        TMP_Dropdown fishlevel = transform.Find("Level").GetComponentInChildren<TMP_Dropdown>();
        TMP_InputField fishname = transform.Find("Name").GetComponentInChildren<TMP_InputField>();
        TMP_InputField fishdecsribe = transform.Find("Describe").GetComponentInChildren<TMP_InputField>();
        TMP_Dropdown fisheffect = transform.Find("Effect").GetComponentInChildren<TMP_Dropdown>();
        TMP_InputField fisheffectmore = transform.Find("EffectMore").GetComponentInChildren<TMP_InputField>();
        string[] effects = new string[0];
        switch (fisheffect.value)
        {
            case 0:
                effects = new string[0];
                break;
            case 1:
                if (fisheffectmore.text == "" || fisheffectmore.text == null) { IsOK(false); return new Hashtable { { -1, "QF" } }; }
                effects = new string[2];
                effects[0] = "����";
                effects[1] = fisheffectmore.text;
                break;
            case 2:
                effects = new string[1];
                effects[0] = "���ɲ���";
                break;
        }
        if (fishname.text == "" || fishname.text == null) { IsOK(false); return new Hashtable { { -1, "QF" } }; }
        if (fishdecsribe.text == "" || fishdecsribe.text == null) { fishdecsribe.text = "��"; }
        FishInformation = new FISHINFORMATION
        {
            ���� = fishname.text,
            ��� = fishdecsribe.text,
            Ч�� = effects
        };
        Debug.Log(JsonUtility.ToJson(FishInformation));
        IsOK(true);
        return new Hashtable { { fishlevel.value, JsonUtility.ToJson(FishInformation) } };
    }

    public void Test()
    {
        _ = GetAddFish();
    }

    public void IsOK(bool isok)
    {
        if (isok) { transform.GetComponent<UnityEngine.UI.Image>().color = UnityEngine.Color.green; }
        else { transform.GetComponent<UnityEngine.UI.Image>().color = UnityEngine.Color.red; }
    }

    public void DelLine()
    {
        Destroy(transform.gameObject);
    }

    public void AddLine()
    {
        newLine = Instantiate(line, Vector3.zero, Quaternion.identity);
        newLine.transform.SetParent(transform);
        newLine.SetActive(true);
        newLine.transform.SetAsLastSibling();
        addLine.transform.SetAsLastSibling();
    }
}
