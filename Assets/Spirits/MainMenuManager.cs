using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : PanelManager
{
    public string SceneStart = "zombieland";
    public Text CharacterName;
    public GameObject Preloader;
    void Start()
    {
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenPreviousPanel()
    {
        if (currentPanel && currentPanel.PanelBefore)
        {
            CloseAllPanels();
            Animator anim = currentPanel.PanelBefore.GetComponent<Animator>();
            if (anim && anim.isActiveAndEnabled)
            {
                anim.SetBool("Open", true);
            }
            currentPanel.PanelBefore.gameObject.SetActive(true);
            currentPanel = currentPanel.PanelBefore;
        }
    }
}