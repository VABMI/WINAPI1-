using System;
using System.Runtime.InteropServices;

    /// <summary>
    /// Description résumée de System.Drawing.Icon.
    /// </summary>
    public class CIcon
    {
        private const UInt32 SHGFI_ICON = 0x100;
        private const UInt32 SHGFI_LARGEICON = 0x0; // 'Large icon
        private const UInt32 SHGFI_SMALLICON = 0x1; // 'Small icon

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, UInt32 dwFileAttributes, ref SHFILEINFO psfi, UInt32 cbSizeFileInfo, UInt32 uFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO 
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public UInt32 dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public static System.Drawing.Icon GetIcon(string filename,bool b_large_icon)
        {
            UInt32 icon_size;
            if (b_large_icon)
                icon_size=SHGFI_LARGEICON;
            else
                icon_size=SHGFI_SMALLICON;

            IntPtr hImgSmall; 
            SHFILEINFO shinfo = new SHFILEINFO();

            hImgSmall = SHGetFileInfo(filename, 0, ref shinfo,(UInt32)Marshal.SizeOf(shinfo),SHGFI_ICON |icon_size);
            System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                        
            return myIcon;
        }

    }
