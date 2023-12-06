using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowsTools : MonoBehaviour
{
    //���õ�ǰ���ڵ���ʾ״̬
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(System.IntPtr hwnd, int nCmdShow);

    //��ȡ��ǰ�����
    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern System.IntPtr GetForegroundWindow();

    //���ô��ڱ߿�
    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);

    //���ô���λ�ã���С
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    //�����϶�
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    //�߿����
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    const int SW_SHOWMINIMIZED = 2;//(��С������)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��С������
    public void SetMinWindows()
    {
        ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
        //���崰�ڲ�������     https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
    }

    //�����ޱ߿򣬲����ÿ����С��λ��
    public void SetNoFrameWindow(Rect rect)
    {
        //SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, SWP_SHOWWINDOW);
    }

    //�϶�����
    public void DragWindow(IntPtr window)
    {
        ReleaseCapture();
        SendMessage(window, 0xA1, 0x02, 0);
        SendMessage(window, 0x0202, 0, 0);
    }


}
