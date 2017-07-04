Imports SysproQ.Entity
Imports SysproQ.BLL
Module Module1
    Private username As String
    Private uPass As String
    Private company As String
    Private coPass As String
    Private _salesDetails As List(Of SorDetail)
    Private _sot As List(Of SortraObj)
    Sub Main(args() As String)
        Try

            'If args.Length > 0 Then
            'Fill sales order detials from Syspro
            fillSalesOrderData("005002") '(args(0).ToString)
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

End Module
