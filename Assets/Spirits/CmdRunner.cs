using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CmdRunner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string[] runCmd(string cmd, string args, string dir = null)
    {
        string[] res = new string[2];
        var p = createCmdProcess(cmd, args, dir);
        res[0] = p.StandardOutput.ReadToEnd();
        res[1] = p.StandardError.ReadToEnd();
        p.Close();
        return res;
    }

    private static Process createCmdProcess(string cmd, string args, string dir = null)
    {
        var p = new ProcessStartInfo(cmd);

        p.Arguments = args;
        p.CreateNoWindow = true;
        p.UseShellExecute = false;
        p.RedirectStandardError = true;
        p.RedirectStandardInput = true;
        p.RedirectStandardOutput = true;
        p.StandardErrorEncoding = System.Text.Encoding.GetEncoding("gb2312");
        p.StandardOutputEncoding = System.Text.Encoding.GetEncoding("gb2312");

        if (!string.IsNullOrEmpty(dir))
        {
            p.WorkingDirectory = dir;
        }

        return Process.Start(p);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CmdRunner : MonoBehaviour
{
    public static string[] runCmd(string cmd, string args, string dir = null)
    {
        string[] res = new string[2];
        var p = createCmdProcess(cmd, args, dir);
        res[0] = p.StandardOutput.ReadToEnd();
        res[1] = p.StandardError.ReadToEnd();
        p.Close();
        return res;
    }

    private static Process createCmdProcess(string cmd, string args, string dir = null)
    {
        var p = new ProcessStartInfo(cmd);

        p.Arguments = args;
        p.CreateNoWindow = true;
        p.UseShellExecute = false;
        p.RedirectStandardError = true;
        p.RedirectStandardInput = true;
        p.RedirectStandardOutput = true;
        p.StandardErrorEncoding = System.Text.Encoding.GetEncoding("gb2312");
        p.StandardOutputEncoding = System.Text.Encoding.GetEncoding("gb2312");

        if (!string.IsNullOrEmpty(dir))
        {
            p.WorkingDirectory = dir;
        }

        return Process.Start(p);
    }
}

From: https://blog.csdn.net/delete_you/article/details/127040757

 */