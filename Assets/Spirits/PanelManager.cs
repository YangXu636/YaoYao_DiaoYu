using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public PanelInstance[] Pages;
    public PanelInstance currentPanel;
    void Start()
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].gameObject.AddComponent<PanelInstance>();
        }
    }

    void Awake()
    {
        if (Pages.Length <= 0)
        {
            return;
        }
        OpenPanel(Pages[0]);
    }

    //�����
    public void OpenPanel(PanelInstance page)
    {
        if (page == null)
        {
            return;
        }
        page.PanelBefore = currentPanel;
        currentPanel = page;
        CloseAllPanels();
        Animator anim = page.GetComponent<Animator>();
        if (anim && anim.isActiveAndEnabled)
        {
            anim.SetBool("Open", true);
        }
        page.gameObject.SetActive(true);
        Debug.Log("���" + page.gameObject.name + "�Ѿ���");
    }
    //ͨ�����ִ����
    public void OpenPanelByName(string name)
    {
        PanelInstance page = null;
        for (int i = 0; i < Pages.Length; i++)
        {
            if (Pages[i].name == name)
            {
                page = Pages[i];
                break;
            }
        }
        if (page == null)
        {
            return;
        }
        page.PanelBefore = currentPanel;
        currentPanel = page;
        CloseAllPanels();
        Animator anim = page.GetComponent<Animator>();
        if (anim && anim.isActiveAndEnabled)
        {
            anim.SetBool("Open", true);
        }
        page.gameObject.SetActive(true);
    }
    //�ر��������
    public void CloseAllPanels()
    {
        if (Pages.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < Pages.Length; i++)
        {
            Animator anim = Pages[i].gameObject.GetComponent<Animator>();
            if (anim && anim.isActiveAndEnabled)
            {
                anim.SetBool("Open", false);
            }
            if (Pages[i].isActiveAndEnabled)
            {
                StartCoroutine(DisablePanelDeleyed(Pages[i]));
            }
        }
    }

    //�������

    public IEnumerator DisablePanelDeleyed(PanelInstance page)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        Animator anim = page.gameObject.GetComponent<Animator>();
        if (anim && anim.enabled)
        {
            while (!closedStateReached && wantToClose)
            {
                if (anim.isActiveAndEnabled && !anim.IsInTransition(0))
                {

                    closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName("Closing");
                }
                yield return new WaitForEndOfFrame();
            }
            if (wantToClose)
            {
                anim.gameObject.SetActive(false);
            }
        }
        else
        {
            page.gameObject.SetActive(false);
        }

    }

}
