using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ChangeTime : MonoBehaviour
{
    string pattern = @"(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2})";

    public TMP_Text Files;
    public Button FilesTime;
    public TMP_InputField Files_CreationTime;
    public TMP_InputField Files_LastAccessTime;
    public TMP_InputField Files_LastWriteTime;
    public Button Files_CreationTime_Button;
    public Button Files_LastAccessTime_Button;
    public Button Files_LastWriteTime_Button;

    public TMP_Text Dirs;
    public Button DirsTime;
    public TMP_InputField Dirs_CreationTime;
    public TMP_InputField Dirs_LastAccessTime;
    public TMP_InputField Dirs_LastWriteTime;
    public Button Dirs_CreationTime_Button;
    public Button Dirs_LastAccessTime_Button;
    public Button Dirs_LastWriteTime_Button;


    // Start is called before the first frame update
    void Start()
    {
        Files.text = Dirs.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddFiles()
    {
        try
        {
            ChooseFile temp = gameObject.AddComponent<ChooseFile>();
            string[] _filesGet = temp.GetFilesPath();
            foreach (string filePath in _filesGet)
            {
                if (filePath != "QF") { Files.text = Files.text + filePath + "\n"; }
            }
            IsOk(true, FilesTime);
        }
        catch
        {
            IsOk(false, FilesTime);
        }
    }

    public void AddDirs()
    {
        try
        {
            ChooseFile temp = gameObject.AddComponent<ChooseFile>();
            string _dirsGet = temp.GetDirPath();
            if (_dirsGet != "QF") { Dirs.text = Dirs.text + _dirsGet + "\n"; }
            IsOk(true, DirsTime);
        }
        catch
        {
            IsOk(false, DirsTime);
        }
    }




    public void ChangeFileTime(string Style)
    {
        string[] files = Files.text.Split("\n");
        Match match;
        switch (Style)
        {
            case "CREATIONTIME":
                match = Regex.Match(Files_CreationTime.text, pattern);
                if (files.Length < 1 || !match.Success) { IsOk(false,Files_CreationTime_Button); return; }
                for (int i=0;i<files.Length - 1;i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);
                    fileInfo.CreationTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                IsOk(true, Files_CreationTime_Button);
                break;
            case "LASTACCESSTIME":
                match = Regex.Match(Files_LastAccessTime.text, pattern);
                if (files.Length < 1|| !match.Success) { IsOk(false, Files_LastAccessTime_Button); return; }
                for (int i = 0; i < files.Length - 1; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);
                    fileInfo.LastAccessTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                IsOk(true, Files_LastAccessTime_Button);
                break;
            case "LASTWRITETIME":
                match = Regex.Match(Files_LastWriteTime.text, pattern);
                if (files.Length < 1 || !match.Success) { IsOk(false, Files_LastWriteTime_Button); return; }
                for (int i = 0; i < files.Length - 1; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);
                    fileInfo.LastWriteTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); 
                }
                IsOk(true, Files_LastWriteTime_Button);
                break;
        }
        return;
    }

    public void ChangeDirTime(string Style)
    {
        string[] dirs = Dirs.text.Split("\n");
        Match match;
        switch (Style)
        {
            case "CREATIONTIME":
                match = Regex.Match(Dirs_CreationTime.text, pattern);
                if (dirs.Length < 2 || !match.Success) { IsOk(false, Dirs_CreationTime_Button); return; }
                for (int i = 0; i < dirs.Length - 1; i++)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
                    dirInfo.CreationTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                IsOk(true, Dirs_CreationTime_Button);
                break;
            case "LASTACCESSTIME":
                match = Regex.Match(Dirs_LastAccessTime.text, pattern);
                if (dirs.Length < 2 || !match.Success) { IsOk(false, Dirs_LastAccessTime_Button); return; }
                for (int i = 0; i < dirs.Length - 1; i++)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
                    dirInfo.LastAccessTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                IsOk(true, Dirs_LastAccessTime_Button);
                break;
            case "LASTWRITETIME":
                match = Regex.Match(Dirs_LastWriteTime.text, pattern);
                if (dirs.Length < 2 || !match.Success) { IsOk(false, Dirs_LastWriteTime_Button); return; }
                for (int i = 0; i < dirs.Length - 1; i++)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
                    dirInfo.LastWriteTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                IsOk(true, Dirs_LastWriteTime_Button);
                break;
        }
        return;
    }

    public void IsOk(bool success,Button ui)
    {
        if (success) { ui.transform.GetComponent<UnityEngine.UI.Image>().color = UnityEngine.Color.green; }
        else { ui.transform.GetComponent<UnityEngine.UI.Image>().color = UnityEngine.Color.red; }
    }
}
