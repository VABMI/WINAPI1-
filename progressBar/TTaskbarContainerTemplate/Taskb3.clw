; CLW-Datei enthält Informationen für den MFC-Klassen-Assistenten

[General Info]
Version=1
LastClass=CNProgressBar
LastTemplate=CProgressCtrl
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "Taskb3.h"

ClassCount=4
Class1=CTaskb3App
Class2=CTaskb3Dlg
Class3=CAboutDlg

ResourceCount=5
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Class4=CNProgressBar
Resource3=IDD_TASKB3_DIALOG (Deutsch (Deutschland))
Resource4=IDD_ABOUTBOX (Deutsch (Deutschland))
Resource5=IDD_TASKB3_DIALOG

[CLS:CTaskb3App]
Type=0
HeaderFile=Taskb3.h
ImplementationFile=Taskb3.cpp
Filter=N

[CLS:CTaskb3Dlg]
Type=0
HeaderFile=Taskb3Dlg.h
ImplementationFile=Taskb3Dlg.cpp
Filter=D
BaseClass=CDialog
VirtualFilter=dWC
LastObject=IDC_CHECK3

[CLS:CAboutDlg]
Type=0
HeaderFile=Taskb3Dlg.h
ImplementationFile=Taskb3Dlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_TASKB3_DIALOG]
Type=1
Class=CTaskb3Dlg
ControlCount=8
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_SLIDER1,msctls_trackbar32,1073807384
Control4=IDC_STATIC,static,1073872896
Control5=IDC_CHECK1,button,1073807363
Control6=IDC_CHECK2,button,1342242819
Control7=IDC_CHECK3,button,1073807363
Control8=IDC_BUTTON1,button,1342242816

[CLS:CNProgressBar]
Type=0
HeaderFile=NProgressBar.h
ImplementationFile=NProgressBar.cpp
BaseClass=CTextProgressCtrl
Filter=W
VirtualFilter=NWC
LastObject=CNProgressBar

[DLG:IDD_TASKB3_DIALOG (Deutsch (Deutschland))]
Type=1
Class=?
ControlCount=7
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_SLIDER1,msctls_trackbar32,1342242840
Control4=IDC_STATIC,static,1342308352
Control5=IDC_CHECK1,button,1342242819
Control6=IDC_CHECK2,button,1342242819
Control7=IDC_CHECK3,button,1342242819

[DLG:IDD_ABOUTBOX (Deutsch (Deutschland))]
Type=1
Class=?
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

