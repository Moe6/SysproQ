Imports SysproQ.Entity
Imports SysproQ.BLL
Imports System.IO
Public Class Face
    Private username As String
    Private uPass As String
    Private company As String
    Private coPass As String
    Private _salesDetails As List(Of SorDetail)
    Private _sot As List(Of SortraObj)
    Public Property itemsPostSuccess As List(Of SortraObj)
    Public Property itemsPostFail As List(Of SortraObj)
    Public Property itemsPostWarning As List(Of SortraObj)
    Private _trnmsg As String
    Private _result As Enums.PostResults
    Public ReadOnly Property TrnMessage As String
        Get
            Return _trnmsg
        End Get
    End Property
    Private Sub AppendTrnMessage(msg As String)
        If _trnmsg IsNot Nothing Then
            _trnmsg &= Environment.NewLine
        End If
        _trnmsg &= msg
    End Sub
    Public Function CreateSalesOrder(Xmlin As String, salesorder As String) As Enums.PostResults
        Return PostSORTOI(Xmlin, salesorder)
    End Function

    'Public Function ReserveStock(salesorder As String) As Enums.PostResults
    '    Return PostSORTRA(salesorder)
    'End Function
    Public Function ReserveStock(xmlin As String) As Enums.PostResults
        Return PostSORTRA(xmlin)
    End Function
    Private Function PostSORTOI(xmlin As String, salesorder As String) As Enums.PostResults
        ' xmlin = ReadFile("c:\temp\SysproQ_Test.xml")
        'xmlin = ReadFile(xmlin)
        'Parse xml in to object as xml
        Dim sortoi As New SORTOI(xmlin, GetLogininfo)
        _result = sortoi.processXmlIn(salesorder)
        AppendTrnMessage(sortoi.TrnMessage)
        Return _result
    End Function

    Private Function PostSORTRA(xmlin As String) As Enums.PostResults
        Dim sr As New SORTRA(GetLogininfo, xmlin)
        If sr.Post() Then
            AppendTrnMessage(sr.TrnMessage)
            Return Enums.PostResults.Success
        Else
            AppendTrnMessage(sr.TrnMessage)
        End If
        Return Enums.PostResults.Fail
    End Function

    Private Function GetLogininfo() As SysproSignInObj
        'username = "ARPOS"
        'uPass = ""
        'company = "T"
        'coPass = ""
        username = "ADMIN"
        uPass = "admin"
        company = "C"
        coPass = ""
        Return New SysproSignInObj(username, uPass, company, coPass)
    End Function

    Private Function ReadFile(path As String) As String
        Dim fr As String = Nothing
        Try
            Using sr As StreamReader = New StreamReader(path)
                fr = sr.ReadToEnd
            End Using
        Catch

        End Try
        Return fr
    End Function

    Public Function CheckSalesOrderExists(salesorder As String) As Boolean
        Dim b As New BLL.Query
        Return b.FillSorDetails(salesorder).Count > 0
    End Function

End Class
