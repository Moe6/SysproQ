'Imports System.Xml.Linq
Imports System.Linq
Imports System.Xml.Linq

Public Class SysproPostResult

    Private _xd As XDocument
    Private _businessObject As String

    Public Property Successful As Boolean = False
    Public Property ErrorMessages As String = Nothing
    Public Property WarningMessages As String = Nothing
    Public Property DocumentNumber As String = Nothing
    Public Property FinalMessage As String = Nothing
    Public Property GrnKey As New GrnResultKey
    Public Property SoTransferKey As New SalesOrderTransferKey
    Public Property ShipQtyUpdateKey As New SalesOrderShipQtyUpdateKey
    Public Property InvTransferKey As New InventoryTransferKey

    Public Class GrnResultKey
        Public Property JnlYear As Integer
        Public Property JnlMonth As Integer
        Public Property Journal As String
        Public Property EntryNumber As String
        Public Property Warehouse As String
    End Class

    Public Class SalesOrderShipQtyUpdateKey
        Public Property SalesOrder As String
        Public Property SalesOrderLine As String
    End Class

    Public Class SalesOrderTransferKey
        Public Property SalesOrder As String
        Public Property GtrReference As String
        Public Property SourceWarehouse As String
        Public Property TargetWarehouse As String
        Public Property JnlYear As Integer
        Public Property JnlMonth As Integer
        Public Property Journal As String
    End Class

    Public Class InventoryTransferKey
        Public Property JnlYear As Integer
        Public Property JnlMonth As Integer
        Public Property Journal As String
        Public Property EntryNumber As String
        Public Property Warehouse As String
        Public Property GlYear As Integer
        Public Property GlPeriod As Integer
        Public Property GlJournal As Integer
    End Class

    Public Sub New(xmlOut As String, businessObject As String)
        _xd = XDocument.Parse(xmlOut)
        _businessObject = businessObject
        CheckForErrorsAndWarnings()
        ProcessResult()
    End Sub

    Private Sub ProcessResult()
        If Me.Successful Then
            Select Case _businessObject
                Case "PORTOI"
                    DocumentNumber = GetPurchaseOrderDocumentNumber()
                    FinalMessage = "Purchase Order Created - " & Date.Now
                Case "PORTOR"
                    DocumentNumber = GetGrnDocumentNumber()
                    FinalMessage = "GRN Created - " & Date.Now
                    GetGrnResultKey()
                Case "APSTRI"
                    DocumentNumber = GetApInvoiceNumber()
                    FinalMessage = "AP Invoice created - " & Date.Now
                Case "SORTSC"
                    DocumentNumber = GetGrnDocumentNumber()
                    FinalMessage = "Sales order transfer created - " & Date.Now
                    GetSalesOrderTransferResult()
                Case "SORTBO"
                    FinalMessage = "Ship qty updated - " & Date.Now
                    GetShipQtyUpdateResult()
                Case "INVTMN"
                    FinalMessage = "Inventory transferred - " & Date.Now
                    GetInventoryTransferResult()
                Case "SORTIC"
                    DocumentNumber = GetSalesOrderInvoiceNumber()
                    FinalMessage = "Invoice created - " & Date.Now
                Case "SORTOI"
                    DocumentNumber = GetSalesOrderNumber()
                    FinalMessage = "Sales order created - " & Date.Now
            End Select
        End If
    End Sub

#Region "ERRORS AND WARNINGS"

    Private Sub CheckForErrorsAndWarnings()
        Me.ErrorMessages = CheckForErrors(_xd)
        Me.WarningMessages = CheckForWarnings(_xd)
        If Me.ErrorMessages Is Nothing Then
            Me.Successful = True
        End If
    End Sub

    Private Function CheckForErrors(xd As XDocument) As String
        Dim msgErrors As New Text.StringBuilder
        Dim errortags = xd.Descendants("ErrorDescription").ToList
        If errortags.Count > 0 Then
            Dim itemCount As Integer = 0
            For Each et As XElement In errortags
                msgErrors.AppendLine("(" & itemCount & ") " & et.Value.ToString)
                itemCount += 1
            Next
        End If
        If msgErrors.Length > 0 Then
            Return msgErrors.ToString
        Else
            Return Nothing
        End If
    End Function

    Private Function CheckForWarnings(xd As XDocument) As String
        Dim msgWarnings As New Text.StringBuilder
        Dim warningtags = xd.Descendants("WarningDescription").ToList
        If warningtags.Count > 0 Then
            Dim itemCount As Integer = 0
            For Each wt As XElement In warningtags
                msgWarnings.AppendLine("(" & itemCount & ") " & wt.Value.ToString)
                itemCount += 1
            Next
        End If
        If msgWarnings.Length > 0 Then
            Return msgWarnings.ToString
        Else
            Return Nothing
        End If
    End Function

#End Region

#Region "GET TRANSACTION DOCUMENT NUMBERS"

    Private Function GetPurchaseOrderDocumentNumber() As String
        Return _xd.<postpurchaseorders>.<Order>.<Key>.<PurchaseOrder>.Value
    End Function

    Private Function GetGrnDocumentNumber() As String
        Return _xd.<postpurchaseorderreceipts>.<Item>.<Receipt>.<Grn>.Value
    End Function

    Private Function GetSalesOrderNumber() As String
        Return _xd.<SalesOrders>.<Order>.<SalesOrder>.Value
    End Function

    Private Function GetSalesOrderInvoiceNumber() As String
        Return _xd.<postsalesorderinvoice>.<Item>.<Key>.<InvoiceNumber>.Value
    End Function

    Private Function GetApInvoiceNumber() As String
        Return _xd.<postapregisterinvoice>.<Item>.<Key>.<Invoice>.Value.ToString
    End Function

#End Region

#Region "GET TRANSACTION RESULT KEYS"

    Private Sub GetGrnResultKey()
        GrnKey.JnlYear = _xd.<postpurchaseorderreceipts>.<Key>.<JnlYear>.Value
        GrnKey.JnlMonth = _xd.<postpurchaseorderreceipts>.<Key>.<JnlMonth>.Value
        GrnKey.Journal = _xd.<postpurchaseorderreceipts>.<Key>.<Journal>.Value
        GrnKey.EntryNumber = _xd.<postpurchaseorderreceipts>.<Key>.<EntryNumber>.Value
        GrnKey.Warehouse = _xd.<postpurchaseorderreceipts>.<Key>.<Warehouse>.Value
    End Sub

    Private Sub GetSalesOrderTransferResult()
        SoTransferKey.SalesOrder = _xd.<postsalesordertransfer>.<Item>.<Key>.<SalesOrder>.Value
        SoTransferKey.GtrReference = _xd.<postsalesordertransfer>.<Item>.<Key>.<GtrReference>.Value
        SoTransferKey.SourceWarehouse = _xd.<postsalesordertransfer>.<Item>.<Key>.<SourceWarehouse>.Value
        SoTransferKey.TargetWarehouse = _xd.<postsalesordertransfer>.<Item>.<Key>.<TargetWarehouse>.Value
        SoTransferKey.JnlYear = _xd.<postsalesordertransfer>.<Item>.<Key>.<JnlYear>.Value
        SoTransferKey.JnlMonth = _xd.<postsalesordertransfer>.<Item>.<Key>.<JnlMonth>.Value
        SoTransferKey.Journal = _xd.<postsalesordertransfer>.<Item>.<Key>.<Journal>.Value
    End Sub

    Private Sub GetShipQtyUpdateResult()
        ShipQtyUpdateKey.SalesOrder = _xd.<PostSorBackOrderRelease>.<Item>.<Key>.<SalesOrder>.Value
        ShipQtyUpdateKey.SalesOrderLine = _xd.<PostSorBackOrderRelease>.<Item>.<Key>.<SalesOrderLine>.Value
    End Sub

    Private Sub GetInventoryTransferResult()
        InvTransferKey.JnlYear = _xd.<postinvgitwhtransferin>.<Key>.<JnlYear>.Value
        InvTransferKey.JnlMonth = _xd.<postinvgitwhtransferin>.<Key>.<JnlMonth>.Value
        InvTransferKey.Journal = _xd.<postinvgitwhtransferin>.<Key>.<Journal>.Value
        InvTransferKey.EntryNumber = _xd.<postinvgitwhtransferin>.<Key>.<EntryNumber>.Value
        InvTransferKey.Warehouse = _xd.<postinvgitwhtransferin>.<Key>.<Warehouse>.Value
        InvTransferKey.GlYear = _xd.<postinvgitwhtransferin>.<Key>.<GlJournal>.<GlYear>.Value
        InvTransferKey.GlPeriod = _xd.<postinvgitwhtransferin>.<Key>.<GlJournal>.<GlPeriod>.Value
        InvTransferKey.GlJournal = _xd.<postinvgitwhtransferin>.<Key>.<GlJournal>.<GlJournal>.Value
    End Sub

#End Region

End Class