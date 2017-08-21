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
    Public Function CreateSalesOrder(Xmlin As String, salesorder As String, actiontype As String, sl As List(Of SoLines)) As Enums.PostResults
        Return PostSORTOI(Xmlin, salesorder, actiontype, sl)
    End Function

    'Public Function ReserveStock(xmlin As String) As Enums.PostResults
    '    Return PostSORTRA(xmlin)
    'End Function
    Private Function PostSORTOI(xmlin As String, salesorder As String, actionType As String, slines As List(Of SoLines)) As Enums.PostResults
        'Parse xml in to object as xml
        Dim sortoi As New SORTOI(xmlin, GetLogininfo, actionType, slines)
        _result = sortoi.processXmlIn(salesorder)
        AppendTrnMessage(sortoi.TrnMessage)
        Return _result
    End Function

    'Private Function PostSORTRA(xmlin As String) As Enums.PostResults
    '    Dim sr As New SORTRA(GetLogininfo, xmlin)
    '    If sr.Post() Then
    '        AppendTrnMessage(sr.TrnMessage)
    '        Return Enums.PostResults.Success
    '    Else
    '        AppendTrnMessage(sr.TrnMessage)
    '    End If
    '    Return Enums.PostResults.Fail
    'End Function

    Private Function GetLogininfo() As SysproSignInObj
        'username = "ARPOS"
        'uPass = ""
        'company = "N"
        'coPass = ""
        username = "ADMIN"
        uPass = ""
        company = "A"
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

    Public Function ValidateCustomer(cust As String) As Boolean
        Dim xele As XElement
        If cust IsNot Nothing Then
            cust = Trim(cust)
            If cust <> "" Then
                'Verify customer already exists
                Using dal As New DAL.Query
                    Dim found = dal.FillCustomer(cust)
                    If found IsNot Nothing Then Return True
                End Using
                'If customer does not exist, create new customer
                Using dal As New DAL.Update
                    Dim newcustomer As New ArCustomer(cust)
                    dal.Update(newcustomer)
                    Return dal.Save() = True
                    xele = FormatCustomerFailMessage(dal.TrnMessage)
                    AppendTrnMessage(xele.ToString)
                End Using
            Else
                xele = FormatCustomerFailMessage("Customer was not specified")
                AppendTrnMessage(xele.ToString)
            End If
        Else
            xele = FormatCustomerFailMessage("Customer was not specified")
            AppendTrnMessage(xele.ToString)
        End If
        Return False
    End Function

    Private Function FormatCustomerFailMessage(msg As String) As XElement
        Return <Customer>
                   <Post2Result><%= msg %></Post2Result>
                   <Status><%= "NOK" %></Status>
               </Customer>
    End Function
End Class
