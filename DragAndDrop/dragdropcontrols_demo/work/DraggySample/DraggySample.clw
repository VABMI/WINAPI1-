; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CDragDropListBox
LastTemplate=CComboBox
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "DraggySample.h"

ClassCount=6
Class1=CDraggySampleApp
Class2=CDraggySampleDlg
Class3=CAboutDlg

ResourceCount=5
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Resource3=IDD_DRAGGYSAMPLE_DIALOG
Resource4=IDD_DRAGGYSAMPLE_DIALOG (English (U.S.))
Class4=CDragDropListBox
Class5=CDragDropButton
Class6=CDragDropComboBox
Resource5=IDD_ABOUTBOX (English (U.S.))

[CLS:CDraggySampleApp]
Type=0
HeaderFile=DraggySample.h
ImplementationFile=DraggySample.cpp
Filter=N

[CLS:CDraggySampleDlg]
Type=0
HeaderFile=DraggySampleDlg.h
ImplementationFile=DraggySampleDlg.cpp
Filter=D
LastObject=IDC_BUTTON_DRAG
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=DraggySampleDlg.h
ImplementationFile=DraggySampleDlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308352
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889
Class=CAboutDlg


[DLG:IDD_DRAGGYSAMPLE_DIALOG]
Type=1
ControlCount=3
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,static,1342308352
Class=CDraggySampleDlg

[DLG:IDD_ABOUTBOX (English (U.S.))]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_DRAGGYSAMPLE_DIALOG (English (U.S.))]
Type=1
Class=CDraggySampleDlg
ControlCount=8
Control1=IDOK,button,1342242817
Control2=IDC_STATIC,button,1342177287
Control3=IDC_LIST_DRAG,listbox,1352728835
Control4=IDC_STATIC,static,1342308353
Control5=IDC_BUTTON_DRAG,button,1342242816
Control6=IDC_COMBO_DRAG,combobox,1344339971
Control7=IDC_CHECK_DRAG,button,1342242819
Control8=IDC_STATIC_FEEDBACK,static,1342308353

[CLS:CDragDropListBox]
Type=0
HeaderFile=DragDropListBox.h
ImplementationFile=DragDropListBox.cpp
BaseClass=CListBox
Filter=W
LastObject=CDragDropListBox
VirtualFilter=bWC

[CLS:CDragDropButton]
Type=0
HeaderFile=DragDropButton.h
ImplementationFile=DragDropButton.cpp
BaseClass=CButton
Filter=W
LastObject=CDragDropButton
VirtualFilter=BWC

[CLS:CDragDropComboBox]
Type=0
HeaderFile=DragDropComboBox.h
ImplementationFile=DragDropComboBox.cpp
BaseClass=CComboBox
Filter=W
LastObject=CDragDropComboBox
VirtualFilter=cWC

