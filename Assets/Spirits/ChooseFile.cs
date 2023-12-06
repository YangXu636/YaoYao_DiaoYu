using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChooseFile : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenDialogFile
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenDialogDir
    {
        public IntPtr hwndOwner = IntPtr.Zero;
        public IntPtr pidlRoot = IntPtr.Zero;
        public String pszDisplayName = "123";
        public String lpszTitle = null;
        public UInt32 ulFlags = 0;
        public IntPtr lpfn = IntPtr.Zero;
        public IntPtr lParam = IntPtr.Zero;
        public int iImage = 0;
    }
    public class DllOpenFileDialog
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenDialogFile ofn);

        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenDialogFile ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

        [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDirPath() {
        OpenDialogDir openDir = new OpenDialogDir();
        openDir.pszDisplayName = new string(new char[2000]);
        openDir.lpszTitle = "资源文件夹选择";
        openDir.ulFlags = 0x00000010 | 0x00000040 | 0x00000080;//1;// BIF_NEWDIALOGSTYLE | BIF_EDITBOX;
        IntPtr pidl = DllOpenFileDialog.SHBrowseForFolder(openDir);

        char[] path = new char[2000];
        for (int i = 0; i < 2000; i++)
            path[i] = '\0';
        if (DllOpenFileDialog.SHGetPathFromIDList(pidl, path))
        {
            string str = new string(path);
            string DirPath = str.Substring(0, str.IndexOf('\0'));
            return DirPath.Replace("\\","\\\\") + "\\\\";
        }
        return "QF";
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OpenFileName
    {
        public int structSize;       //结构的内存大小
        public IntPtr dlgOwner;       //设置对话框的句柄
        public IntPtr instance;       //根据flags标志的设置，确定instance是谁的句柄，不设置则忽略
        public string filter;         //调取文件的过滤方式
        public string customFilter;  //一个静态缓冲区 用来保存用户选择的筛选器模式
        public int maxCustFilter;     //缓冲区的大小
        public int filterIndex;                 //指向的缓冲区包含定义过滤器的字符串对
        public string file;                  //存储调取文件路径
        public int maxFile;                     //存储调取文件路径的最大长度 至少256
        public string fileTitle;             //调取的文件名带拓展名
        public int maxFileTitle;                //调取文件名最大长度
        public string initialDir;            //最初目录
        public string title;                 //打开窗口的名字
        public int flags;                       //初始化对话框的一组位标志  参数类型和作用查阅官方API
        public short fileOffset;                //文件名前的长度
        public short fileExtension;             //拓展名前的长度
        public string defExt;                //默认的拓展名
        public IntPtr custData;       //传递给lpfnHook成员标识的钩子子程的应用程序定义的数据
        public IntPtr hook;           //指向钩子的指针。除非Flags成员包含OFN_ENABLEHOOK标志，否则该成员将被忽略。
        public string templateName;          //模块中由hInstance成员标识的对话框模板资源的名称
        public IntPtr reservedPtr;
        public int reservedInt;
        public int flagsEx;                     //可用于初始化对话框的一组位标志
    }

    public class WindowDll
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    }

    public string[] GetFilesPath()
    {
        //初始化
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "All Files\0*.*\0\0";
        ofn.file = new string(new char[1024]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        string path = Application.streamingAssetsPath;
        path = path.Replace('/', '\\');
        ofn.initialDir = path;  //默认路径
        ofn.title = "Open Project";
        ofn.defExt = "JPG";//显示文件的类型  
        //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

        //判断是否打开文件
        if (WindowDll.GetOpenFileName(ofn))
        {
            //多选文件
            string[] Splitstr = { "\0" };
            string[] strs = ofn.file.Split(Splitstr, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length > 1)
            {
                string[] _ans = new string[strs.Length - 1];
                for (int i = 1; i < strs.Length; i++) { _ans[i - 1] = strs[0] + "\\" + strs[i]; }
                return _ans;
            }
            else
            {
                string[] _ans = new string[1];
                _ans[0] = strs[0];
                return _ans;
            }
        }
        else
        {
            string[] _ans = new string[1];
            _ans[0] = "QF";
            return _ans;
        }

    }
}
