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
        openDir.lpszTitle = "��Դ�ļ���ѡ��";
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
        public int structSize;       //�ṹ���ڴ��С
        public IntPtr dlgOwner;       //���öԻ���ľ��
        public IntPtr instance;       //����flags��־�����ã�ȷ��instance��˭�ľ���������������
        public string filter;         //��ȡ�ļ��Ĺ��˷�ʽ
        public string customFilter;  //һ����̬������ ���������û�ѡ���ɸѡ��ģʽ
        public int maxCustFilter;     //�������Ĵ�С
        public int filterIndex;                 //ָ��Ļ���������������������ַ�����
        public string file;                  //�洢��ȡ�ļ�·��
        public int maxFile;                     //�洢��ȡ�ļ�·������󳤶� ����256
        public string fileTitle;             //��ȡ���ļ�������չ��
        public int maxFileTitle;                //��ȡ�ļ�����󳤶�
        public string initialDir;            //���Ŀ¼
        public string title;                 //�򿪴��ڵ�����
        public int flags;                       //��ʼ���Ի����һ��λ��־  �������ͺ����ò��Ĺٷ�API
        public short fileOffset;                //�ļ���ǰ�ĳ���
        public short fileExtension;             //��չ��ǰ�ĳ���
        public string defExt;                //Ĭ�ϵ���չ��
        public IntPtr custData;       //���ݸ�lpfnHook��Ա��ʶ�Ĺ����ӳ̵�Ӧ�ó����������
        public IntPtr hook;           //ָ���ӵ�ָ�롣����Flags��Ա����OFN_ENABLEHOOK��־������ó�Ա�������ԡ�
        public string templateName;          //ģ������hInstance��Ա��ʶ�ĶԻ���ģ����Դ������
        public IntPtr reservedPtr;
        public int reservedInt;
        public int flagsEx;                     //�����ڳ�ʼ���Ի����һ��λ��־
    }

    public class WindowDll
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    }

    public string[] GetFilesPath()
    {
        //��ʼ��
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "All Files\0*.*\0\0";
        ofn.file = new string(new char[1024]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        string path = Application.streamingAssetsPath;
        path = path.Replace('/', '\\');
        ofn.initialDir = path;  //Ĭ��·��
        ofn.title = "Open Project";
        ofn.defExt = "JPG";//��ʾ�ļ�������  
        //ע�� һ����Ŀ��һ��Ҫȫѡ ����0x00000008�Ҫȱ��  
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

        //�ж��Ƿ���ļ�
        if (WindowDll.GetOpenFileName(ofn))
        {
            //��ѡ�ļ�
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
