/* don't forget to put the following in the same directory of the exe,in a file called exe_name.exe.manifest were exe_name is the name of the exe

<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0">
<assemblyIdentity version="1.0.0.0" processorArchitecture="X86" name="CompanyName.ProductName.YourApplication" type="win32" />
<description>Your application description here.</description>
<dependency>
<dependentAssembly>
<assemblyIdentity type="win32" name="Microsoft.Windows.Common-Controls" version="6.0.0.0" processorArchitecture="X86" publicKeyToken="6595b64144ccf1df" language="*"/>
</dependentAssembly>
</dependency>
</assembly>

*/
/*
Q: How do I embed the manifest file in my .exe?

A: A Win32 resource must be added to your .exe. The type of the resource must be "RT_MANIFEST" and the resource id must be "1". An easy way to do this is with Visual Studio.NET: 

1. Open your exe in VS (file -> open file) 
2. Right click on it and select add resource 
3. Click "Import..." from the dialog 
4. Select your manifest file 
5. In the "Resource Type" field, enter "RT_MANIFEST" 
6. In the property grid, change the resource ID from "101" to "1". 
7. Save the exe. 
*/

    public class XPStyle
    {
        #region make XPStyle
        public static void MakeXPStyle(System.Windows.Forms.Form the_form)
        {
            // Makes sure Windows XP is running and 
            //  a .manifest file exists for the EXE.
            if(System.Environment.OSVersion.Version.Major > 4 
                & System.Environment.OSVersion.Version.Minor > 0 
                & System.IO.File.Exists(System.Windows.Forms.Application.ExecutablePath + ".manifest")
                )                
            { 
                // Iterate through the controls.
                for(int x = 0; x < the_form.Controls.Count; x++)
                {
                    // If the control derives from ButtonBase,
                    // set its FlatStyle property to FlatStyle.System.
                    if(the_form.Controls[x].GetType().BaseType == typeof(System.Windows.Forms.ButtonBase))
                    {
                        ((System.Windows.Forms.ButtonBase)the_form.Controls[x]).FlatStyle = System.Windows.Forms.FlatStyle.System; 
                    }
                    RecursivelyFormatForWinXP(the_form.Controls[x]);
                }
            }
        }

        private static void RecursivelyFormatForWinXP(System.Windows.Forms.Control control)
        {
            for(int x = 0; x < control.Controls.Count; x++)
            {
                // If the control derives from ButtonBase, 
                //  set its FlatStyle property to FlatStyle.System.
                if(control.Controls[x].GetType().BaseType == typeof(System.Windows.Forms.ButtonBase))
                {
                    ((System.Windows.Forms.ButtonBase)control.Controls[x]).FlatStyle = System.Windows.Forms.FlatStyle.System; 
                }
              
                // If the control holds other controls, iterate through them also.
                if(control.Controls.Count > 0)
                {
                    RecursivelyFormatForWinXP(control.Controls[x]);
                }
            }
        }
        #endregion
    }
