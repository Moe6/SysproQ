Imports SysproQ.Entity
Imports SysproQ.BLL
Imports System.IO
Public Class Base
    Private username As String
    Private uPass As String
    Private company As String
    Private coPass As String
    Private _salesDetails As List(Of SorDetail)
    Private _sot As List(Of SortraObj)

    Public Sub New()

    End Sub

    Public Function PostSORTOI(xmlin As String) As Enums.PostResults
        xmlin = ReadFile("c:\temp\SysproQ_Test.xml")
        'Parse xml in to object as xml
        Dim sortoi As New SORTOI(xmlin, GetLogininfo)
        Return sortoi.processXmlIn()
    End Function

    Private Sub Post(salesorder As String)
        Try

            'If args.Length > 0 Then
            'Fill sales order detials from Syspro
            fillSalesOrderData("005004") '(args(0).ToString)
            If _salesDetails IsNot Nothing Then
                If _salesDetails.Count > 0 Then
                    CreateSortraObject()
                    If _sot IsNot Nothing Then
                        Dim bll As New BLL.SORTRA(GetLogininfo, _sot)
                        If bll.Post() Then
                            MsgBox("Success")
                        Else
                            MsgBox(bll.TrnMessage)
                        End If
                    End If
                Else
                    MsgBox("No sales order lines found for this order.")
                End If

            End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Main()")
        End Try

    End Sub

    Private Function GetLogininfo() As SysproSignInObj
        username = "ADMIN"
        uPass = "admin"
        company = "C"
        coPass = ""
        Return New SysproSignInObj(username, uPass, company, coPass)
    End Function

    Private Sub fillSalesOrderData(so As String)
        'All conversion logic goes here
        Dim bll As New BLL.Query
        _salesDetails = bll.FillSorDetails(so)
    End Sub

    Private Sub CreateSortraObject()
        _sot = New List(Of SortraObj)
        For Each item In _salesDetails
            Dim obj As New SortraObj

            obj.SalesOrder = item.SalesOrder
            obj.Line = item.SalesOrderLine
            obj.StockCode = item.MStockCode
            obj.Qty = item.MOrderQty
            obj.Warehouse = item.MWarehouse
            _sot.Add(obj)
        Next
    End Sub

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
End Class

