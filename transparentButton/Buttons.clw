; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CButtonsDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "Buttons.h"

ClassCount=4
Class1=CButtonsApp
Class2=CButtonsDlg
Class3=CAboutDlg

ResourceCount=4
Resource1=IDD_BUTTONS_DIALOG
Resource2=IDR_MAINFRAME
Resource3=IDD_ABOUTBOX
Class4=CSlider
Resource4=IDD_DIALOG1

[CLS:CButtonsApp]
Type=0
HeaderFile=Buttons.h
ImplementationFile=Buttons.cpp
Filter=N

[CLS:CButtonsDlg]
Type=0
HeaderFile=ButtonsDlg.h
ImplementationFile=ButtonsDlg.cpp
Filter=D
LastObject=IDC_BTN_ME
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=ButtonsDlg.h
ImplementationFile=ButtonsDlg.cpp
Filter=D
LastObject=IDOK

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=5
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889
Control5=IDC_STATIC,static,1342308352

[DLG:IDD_BUTTONS_DIALOG]
Type=1
Class=CButtonsDlg
ControlCount=6
Control1=IDOK,button,1342242817
Control2=IDC_BUTTON1,button,1342242827
Control3=IDC_BUTTON2,button,1342242827
Control4=IDC_BUTTON3,button,1342242827
Control5=IDC_BTN_ME,button,1342242827
Control6=IDC_STATIC,button,1342177287

[DLG:IDD_DIALOG1]
Type=1
Class=CSlider
ControlCount=2
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816

[CLS:CSlider]
Type=0
HeaderFile=Slider.h
ImplementationFile=Slider.cpp
BaseClass=CDialog
Filter=D

