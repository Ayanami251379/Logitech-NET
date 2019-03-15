Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports LgLcd

Namespace LgLcd
    Public Class LgFormsApplet
        Inherits UserControl
        Implements IApplet

        Protected Device As Device
        Private _applet As IApplet

        Protected Sub New()
            If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then Return
            _bm = New Bitmap(320, 240, PixelFormat.Format32bppArgb)
            _gfx = Graphics.FromImage(_bm)

        End Sub

        Protected Sub InitializeApplet()
            _applet = New AppletProxy(Me)
            Connect(AppletName, True, AppletCapabilities.Qvga)
            Device = New Device()
            Device.Open(_applet, DeviceType.Qvga)
        End Sub

        'Public Event UpdateLcdScreen As EventHandler

        Public Sub UpdateLcdScreen(ByVal sender As Object, ByVal e As EventArgs)
            DrawToBitmap2()

            Try
                Device.UpdateBitmap(_bm, Priority.Normal)
            Catch __unusedWin32Exception1__ As Win32Exception
            Catch __unusedInvalidOperationException2__ As InvalidOperationException
            Catch __unusedInvalidAsynchronousStateException3__ As InvalidAsynchronousStateException
            End Try
        End Sub

        Private Sub DrawToBitmap2()
            If Not IsHandleCreated Then CreateHandle()
            Dim hdc As IntPtr = _gfx.GetHdc()
            SendMessage(New HandleRef(Me, Handle), &H317, hdc, CType(30, IntPtr))
            _gfx.ReleaseHdc(hdc)
        End Sub

        Private _bm As Bitmap
        Private _gfx As Graphics
        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Protected Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        Public Overridable Sub OnDeviceArrival(ByVal deviceType As DeviceType)
        End Sub

        Public Overridable Sub OnDeviceRemoval(ByVal deviceType As DeviceType)
        End Sub

        Public Overridable Sub OnAppletEnabled()
        End Sub

        Public Overridable Sub OnAppletDisabled()
        End Sub

        Public Overridable Sub OnCloseConnection()
        End Sub

        Public Overridable Sub OnConfigure()
        End Sub

        Public Overridable ReadOnly Property AppletName As String
            Get
                Return "WinFormsApplet"
            End Get
        End Property

        Friend Class AppletProxy
            Inherits Applet

            Private ReadOnly _proxy As IApplet

            Public Sub New(ByVal proxy As IApplet)
                _proxy = proxy
            End Sub

            Public Overrides Sub OnDeviceArrival(ByVal deviceType As DeviceType)
                _proxy.OnDeviceArrival(deviceType)
            End Sub

            Public Overrides Sub OnDeviceRemoval(ByVal deviceType As DeviceType)
                _proxy.OnDeviceRemoval(deviceType)
            End Sub

            Public Overrides Sub OnAppletEnabled()
                _proxy.OnAppletEnabled()
            End Sub

            Public Overrides Sub OnAppletDisabled()
                _proxy.OnAppletDisabled()
            End Sub

            Public Overrides Sub OnCloseConnection()
                _proxy.OnCloseConnection()
            End Sub

            Public Overrides Sub OnConfigure()
                _proxy.OnConfigure()
            End Sub
        End Class

        Public Sub Connect(ByVal friendlyName As String, ByVal autostartable As Boolean, ByVal appletCaps As AppletCapabilities)
            _applet.Connect(friendlyName, autostartable, appletCaps)
        End Sub

        Public Sub Disconnect()
            _applet.Disconnect()
        End Sub

        Private Sub IApplet_OnDeviceArrival(deviceType As DeviceType) Implements IApplet.OnDeviceArrival

        End Sub

        Private Sub IApplet_OnDeviceRemoval(deviceType As DeviceType) Implements IApplet.OnDeviceRemoval

        End Sub

        Private Sub IApplet_OnAppletEnabled() Implements IApplet.OnAppletEnabled

        End Sub

        Private Sub IApplet_OnAppletDisabled() Implements IApplet.OnAppletDisabled

        End Sub

        Private Sub IApplet_OnCloseConnection() Implements IApplet.OnCloseConnection

        End Sub

        Private Sub IApplet_OnConfigure() Implements IApplet.OnConfigure

        End Sub

        Private Sub IApplet_Connect(friendlyName As String, autostartable As Boolean, appletCaps As AppletCapabilities) Implements IApplet.Connect

        End Sub

        Private Sub IApplet_Disconnect() Implements IApplet.Disconnect

        End Sub
    End Class
End Namespace
