Imports eTrack.Entity

Partial Public Class Log

    Public Sub New()
        InitObject()
    End Sub

    Public Sub New(user As String, xIn As String, xParam As String, businessObj As String, onlyValidate As Boolean)
        InitObject()
        TrnUser = user
        XmlIn = xIn
        XmlParam = xParam
        BusinessObject = businessObj
        ValidateOnly = onlyValidate
    End Sub

    Public Sub New(lType As LogType, user As String, xIn As String, xParam As String, businessObj As String, onlyValidate As Boolean)
        Me.New(user, xIn, xParam, businessObj, onlyValidate)
        LogType = lType
    End Sub

    Public Property PostResult As SysproPostResult

    Private Sub InitObject()
        TrnTime = Date.Now
        Successful = False
        IsException = False
        ValidateOnly = False
        GetMachineInfo()
    End Sub

    Private Sub GetMachineInfo()
        Dim obj = GetMachineUserInfo()
        HostName = obj.HostName
        IPAddress = obj.IPAddress
        WindowsUser = obj.WindowsUser
    End Sub

    Private Function GetMachineUserInfo() As MachineUserDetail
        Dim ipAddress = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                ipAddress = ipheal.ToString()
            End If
        Next
        Return New MachineUserDetail(strHostName, ipAddress, Environment.UserName)
    End Function

End Class
