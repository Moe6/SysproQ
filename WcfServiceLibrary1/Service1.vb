' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Imports SysproQ.Entity.Enums
Imports SysproQ.Entity
Public Class Service1
    Implements IService1

    Private msgHelper As New MessagingHelper
    Private _salesorder As String
    Private _trnMessage As String
    Private _actionType As String

    Private Sub AppendTrnMessage(msg As String)
        If _trnMessage IsNot Nothing Then
            _trnMessage &= Environment.NewLine
        End If
        _trnMessage &= msg
    End Sub

    Private Function TrnMessage() As String Implements IService1.TrnMessage
        Return _trnMessage
    End Function
    Public Function GetData(ByVal value As String) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IService1.GetDataUsingDataContract
        If composite Is Nothing Then
            Throw New ArgumentNullException("composite")
        End If
        If composite.BoolValue Then
            composite.StringValue &= "Suffix"
        End If
        Return composite
    End Function
    'Public Function Post2(Xmlin As String) As String Implements IService1.Post2
    '    Dim f As New SysproQ.BLL.Face
    '    Dim result As PostResults = Nothing
    '    _trnMessage = Nothing
    '    _salesorder = Nothing
    '    Try
    '        ''get sales order into var
    '        If GetSalesOrder(Xmlin) Then
    '            'check if sales order exists - No need to create order again
    '            If f.CheckSalesOrderExists(_salesorder) Then
    '                ''If order exists then jump to reserve stock
    '                AppendTrnMessage("Order already exists.")
    '                'GoTo doReserverStock
    '            Else
    '                result = f.CreateSalesOrder(Xmlin, _salesorder)
    '                'If sales order has been created then run sortra to reserve Stock
    '                If result = PostResults.Success Then
    '                    _trnMessage = f.TrnMessage
    '                    ' AppendTrnMessage(f.TrnMessage)
    '                    'Reserve stock
    '                    '                        If _salesorder IsNot Nothing Then
    '                    'doReserverStock:
    '                    '                            ''Must pass xmlin from client as per requirement,as this has the qties
    '                    '                            ''query Sales order lines from the DB
    '                    '                            ''Match Stockcode on Line numbers
    '                    '                            ''Use Qties from xmlin to reserve quantities
    '                    '                            'result = f.ReserveStock(Xmlin)
    '                    '                            '_trnMessage = f.TrnMessage
    '                    '                            'Select Case result
    '                    '                            '    Case PostResults.Success
    '                    '                            '        AppendTrnMessage("Stock Reserve Success " & _salesorder)
    '                    '                            '    Case PostResults.Fail
    '                    '                            '        AppendTrnMessage("Stock Reserve Failed for " & _salesorder)
    '                    '                            '    Case PostResults.Partly
    '                    '                            '        AppendTrnMessage("Stock reserve partial " & _salesorder)
    '                    '                            'End Select

    '                    '                        Else
    '                    '                            _trnMessage = f.TrnMessage
    '                    '                            AppendTrnMessage("Could not identify sales order")
    '                    '                        End If
    '                Else
    '                    _trnMessage = f.TrnMessage
    '                    AppendTrnMessage("Reserve Stock Failed")
    '                End If
    '            End If
    '        Else
    '            _trnMessage = f.TrnMessage
    '            AppendTrnMessage("Could not Identify the Sales order number.")
    '        End If
    '    Catch ex As Exception
    '        Dim m As String = msgHelper.GetFullMessage(ex)
    '        AppendTrnMessage(m)
    '    End Try
    '    ' AppendTrnMessage("Post failed.")
    '    Return _trnMessage
    'End Function
    Public Function Post2(Xmlin As String) As String Implements IService1.Post2
        Dim f As New SysproQ.BLL.Face
        Dim result As PostResults = Nothing
        _trnMessage = Nothing
        _salesorder = Nothing
        Try
            ''get sales order into var
            If GetSalesOrderAndActionType(Xmlin) Then
                Dim sls = GetSalesOrderLines(Xmlin)
                result = f.CreateSalesOrder(Xmlin, _salesorder, _actionType, sls)
                'If sales order has been created then run sortra to reserve Stock
                If result = PostResults.Success Then
                    _trnMessage = f.TrnMessage
                Else
                    _trnMessage = f.TrnMessage
                    AppendTrnMessage("Reserve Stock Failed")
                End If
            Else
                _trnMessage = f.TrnMessage
                AppendTrnMessage("Could not Identify the Sales order number.")
            End If
        Catch ex As Exception
            Dim m As String = msgHelper.GetFullMessage(ex)
            AppendTrnMessage(m)
        End Try
        ' AppendTrnMessage("Post failed.")
        Return _trnMessage
    End Function
    Private Function GetSalesOrderAndActionType(xmlin As String) As Boolean
        Dim xl As XElement
        xl = XElement.Parse(xmlin)
        _salesorder = xl.Element("SalesOrder").Value
        _actionType = xl.Element("ActionType").Value
        If _salesorder IsNot Nothing Then
            Return True
        End If
        Return False
    End Function

    Private Function GetSalesOrderLines(xmlin As String) As List(Of SoLines)
        Dim foundLines As New List(Of SoLines)
        Dim xele As XElement = XElement.Parse(xmlin)
        Dim xLines = xele.Descendants("StockLine")
        'Loop to get each line in xmlin
        For Each line In xLines
            Dim sl As New SoLines
            sl.StockCode = line.Element("StockCode").Value
            sl.PoLine = line.Element("PoLine")
            Select Case _actionType
                Case "D"
                    sl.LineAction = "D"
                Case "C"
                    sl.LineAction = line.Element("LineAction").Value
                Case "A"
                    sl.LineAction = "A"
                Case Else
                    sl.LineAction = line.Element("LineAction").Value
            End Select

            foundLines.Add(sl)
        Next
        Return foundLines
    End Function

    Private Function CreateMyOwnXmlTemplate() As String
        Dim a = <Order>
                    <OrderHeader>
                        <SalesOrder>50006</SalesOrder>
                        <CustomerPoNumber>C1000</CustomerPoNumber>
                        <Customer>610001</Customer>
                        <ActionType>A</ActionType>
                    </OrderHeader>
                    <OrderDetails>
                        <StockLine>
                            <PoLine>1</PoLine>
                            <StockCode>0101</StockCode>
                            <OrderQty>5</OrderQty>
                            <Price>40000</Price>
                            <City>CityWh</City>
                            <LineAction>A</LineAction>
                        </StockLine>
                        <StockLine>
                            <PoLine>2</PoLine>
                            <StockCode>0103</StockCode>
                            <OrderQty>20</OrderQty>
                            <Price>500000</Price>
                            <City>CityWh</City>
                            <LineAction>A</LineAction>
                        </StockLine>
                    </OrderDetails>
                </Order>
        Return a.ToString
    End Function


End Class


