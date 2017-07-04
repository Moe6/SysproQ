Imports SysproQ.Entity
Public Class WebService
    Implements IDisposable

    'Private _wsUtils As New SysproUtilitiesService.utilitiesclass
    'Private _wsTrans As New SysproTransService.transactionclass
    'Private _wsQuery As New SysproQueryService.queryclass
    Private _wsUtils As New SysproUtilitiesService.utilitiesclassSoapClient
    Private _wsTrans As New SysproTransService.transactionclassSoapClient
    Private _wsQuery As New SysproQueryService.queryclassSoapClient
    Public msgHelper As New Entity.MessagingHelper
    Private _sysproLoginObject As SysproUtilityObject

    Public ReadOnly Property SysproUserID As String
        Get
            Return _sysproUserID
        End Get
    End Property
    Private _sysproUserID As String

    Public ReadOnly Property ExceptionError As String
        Get
            Return _exceptionError
        End Get
    End Property
    Private Property _exceptionError As String

    Public ReadOnly Property XmlOut As String
        Get
            Return _xmlOut
        End Get
    End Property
    Private Property _xmlOut As String

    Public Sub New(loginObject As SysproUtilityObject)
        _sysproLoginObject = loginObject
    End Sub

    Private Sub SetExceptionError(exceptionHeader As String, exceptionMsg As String)
        _exceptionError = String.Format("{0} Exception:{1}{2}", exceptionHeader, Environment.NewLine, exceptionMsg)
    End Sub

    Public Function Logon() As Boolean
        Try
            With _sysproLoginObject
                _sysproUserID = _wsUtils.Logon(.Username, .UserPW, .Company, .CompanyPW, 0, 0, 0, "")
            End With
        Catch ex As Exception
            SetExceptionError("SYSPRO Logon", msgHelper.Getfullmessage(ex))
            _sysproUserID = Nothing
        End Try
        Return _sysproUserID IsNot Nothing
    End Function

    Public Sub Logoff(userID As String)
        Try
            Dim result As String = _wsUtils.Logoff(userID)
        Catch ex As Exception
            SetExceptionError("SYSPRO Log Off", msgHelper.Getfullmessage(ex))
        End Try
    End Sub

    Public Function Post(ByVal businessObj As String, ByVal XmlIn As String, ByVal XmlParam As String) As String
        Dim postResult As String = Nothing
        If Logon() Then
            Try
                postResult = _wsTrans.Post(_sysproUserID, businessObj, XmlParam, XmlIn)
            Catch ex As Exception
                SetExceptionError("SYSPRO Posting", msgHelper.Getfullmessage(ex))
            Finally
                Logoff(_sysproUserID)
            End Try
        End If
        Return postResult
    End Function

    Public Function Query(ByVal businessObj As String, ByVal XmlIn As String) As String
        Dim queryResult As String = Nothing
        If Logon() Then
            Try
                queryResult = _wsQuery.Query(_sysproUserID, businessObj, XmlIn)
            Catch ex As Exception
                SetExceptionError("SYSPRO Query", msgHelper.Getfullmessage(ex))
            Finally
                Logoff(_sysproUserID)
            End Try
        End If
        Return queryResult
    End Function

    Public Function Browse(ByVal XmlIn As String) As String
        Dim queryResult As String = Nothing
        If Logon() Then
            Try
                queryResult = _wsQuery.Browse(_sysproUserID, XmlIn)
            Catch ex As Exception
                SetExceptionError("SYSPRO Browse", msgHelper.Getfullmessage(ex))
            Finally
                Logoff(_sysproUserID)
            End Try
        End If
        Return queryResult
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                'release utility WebService 
                _wsUtils.Close()
                _wsUtils = Nothing
                'release query WebService
                _wsQuery.Close()
                _wsQuery = Nothing
                'release transaction WebService
                _wsTrans.Close()
                _wsTrans = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
