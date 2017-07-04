Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports SysproQ.BLL.Face
Imports SysproQ.Entity.Enums
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://gyxitex.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class QWebService
    Inherits System.Web.Services.WebService

    Public _salesorder As String

    <ValidateInput(False)>
    <WebMethod()>
    Public Function Post2(Xmlin As String) As String
        Dim f As New SysproQ.BLL.Face
        Dim result As PostResults = Nothing
        Try
            ''get sales order into var
            'Xmlin = CreateMyOwnXmlTemplate()
            If GetSalesOrder(Xmlin) Then
                '_salesOrder = "005013"
                'check if sales order exists - No need to create order again
                If f.CheckSalesOrderExists(_salesorder) Then
                    GoTo doReserverStock
                Else
                    result = f.CreateSalesOrder(Xmlin)
                    'If sales order has been created then run sortra to reserve Stock
                    If result = PostResults.Success Then
                        'Reserve stock
                        If _salesorder IsNot Nothing Then
doReserverStock:
                            '_salesOrder = "005012"
                            result = f.ReserveStock(_salesorder)
                            Select Case result
                                Case PostResults.Success
                                    ' MsgBox("Stock Reserve Success", ServiceNotification)
                                    Return "Stock Reserve Success"
                                Case PostResults.Fail
                                    'MsgBox("Stock Reserve Fail", ServiceNotification)
                                    Return "Stock Reserve Failed"
                                Case PostResults.Partly
                                    ' MsgBox("Stock Reserve Partial", ServiceNotification)
                                    Return "Stock reserve partial"
                            End Select
                        Else
                            ' MsgBox("Could not identify sales order", ServiceNotification)
                            Return "Could not identify sales order"
                        End If
                    Else
                        'MsgBox("Sales order failed to create", ServiceNotification)
                        Return "Sales order failed to create"
                    End If
                End If
            Else
                Return "Could not ID the Salses order number."
                ' MsgBox("Could not ID the Salses order number.")
            End If

        Catch ex As Exception
            Return ex.Message.ToString
            'MsgBox(ex.Message & vbNewLine & vbNewLine & ex.StackTrace, ServiceNotification, "SysproQ Error")
        End Try
        Return "Other."
    End Function

    Private Function GetSalesOrder(xmlin As String) As Boolean
        Dim xl As XElement
        xl = XElement.Parse(xmlin)
        _salesorder = xl.Element("SalesOrder").Value
        If _salesorder IsNot Nothing Then
            Return True
        End If
        'Dim headerInfo = xl.Descendants("OrderHeader")
        'If headerInfo.Count > 0 Then
        '    _salesOrder = xl.Element("SalesOrder").Value
        '    Return True
        'End If
        Return False
    End Function

    Private Function CreateMyOwnXmlTemplate() As String
        Dim a = <Order>
                    <OrderHeader>
                        <SalesOrder>50006</SalesOrder>
                        <CustomerPoNumber>C1000</CustomerPoNumber>
                        <Customer>610001</Customer>
                        <OrderActionType>A</OrderActionType>
                    </OrderHeader>
                    <OrderDetails>
                        <StockLine>
                            <CustomerPoLine>1</CustomerPoLine>
                            <StockCode>0101</StockCode>
                            <OrderQty>5</OrderQty>
                            <Price>40000</Price>
                        </StockLine>
                        <StockLine>
                            <CustomerPoLine>2</CustomerPoLine>
                            <StockCode>0103</StockCode>
                            <OrderQty>20</OrderQty>
                            <Price>500000</Price>
                        </StockLine>
                    </OrderDetails>
                </Order>

        Return a.ToString
    End Function

End Class