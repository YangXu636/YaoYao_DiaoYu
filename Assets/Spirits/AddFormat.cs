using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddFormat : MonoBehaviour
{
    public TMP_Text[] OutPut;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddFish()
    {
        Button[] Buttons_setsize = transform.GetComponentsInChildren<UnityEngine.UI.Button>();
        Hashtable fishes = new Hashtable { { -1, "" }, { 0, "" }, { 1, "" }, { 2, "" }, { 3, "" }, { 4, "" } };
        for (int i = 0; i < Buttons_setsize.Length; i++)
        {
            try
            {
                Hashtable temp = Buttons_setsize[i].GetComponent<FormatLine>().GetAddFish();
                foreach (DictionaryEntry item in temp) { fishes[(int)item.Key] =  (string)fishes[item.Key] + ",\n" + (string)item.Value; }
            }
            catch { }
        }
        for (int i = 0; i < OutPut.Length; i++) { OutPut[i].text = "µÈ¼¶"+ (i+1) + "\n\n" + (string)fishes[i]; }
    }

    public void AddCrafting()
    {

    }

    public void AddAdvancement()
    {

    }
}
/*
        lineImage = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity);
        lineImage.transform.SetParent(transform);
 */