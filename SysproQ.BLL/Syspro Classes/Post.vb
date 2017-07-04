Imports SysproQ.Entity
Imports SYSPROWCFServicesClientLibrary40
Imports System.IO
Public Class Post
    Private Property Username As String
    Private Property UserPW As String
    Private Property Company As String
    Private Property CompanyPW As String
    Private _businessObject As String
    Private _xmlIn As String
    Private _xmlParams As String
    Private _xmlOut As String
    'Public Qry As New SysproQueryService.queryclass
    'Public Tran As New SysproTransService.transactionclass
    'Public Util As New SysproUtilitiesService.Logon_With_String()
    Public _wcf As SYSPROWCFServicesClient
    Public _wcfPrima As SYSPROWCFServicesPrimitiveClient

    Public _transactionXmlOut As New SysproPostXmlOutResult
    Public msgHelper As New MessagingHelper

    Private signinInfo As SysproSignInObj
    Private _sorDetails As List(Of SorDetail)
    Private _postedOrder As List(Of SorDetail)
    Public ReadOnly Property PostResult As SysproPostXmlOutResult
        Get
            Return _transactionXmlOut
        End Get
    End Property

    Private _trnMessage As String = Nothing
    Public ReadOnly Property TrnMessage As String
        Get
            Return _trnMessage
        End Get
    End Property
    Private Sub AppendTrnMessage(msg As String)
        If _trnMessage IsNot Nothing Then
            _trnMessage &= Environment.NewLine
        End If
        _trnMessage &= msg
    End Sub

    Public Sub New(loginInfo As SysproSignInObj, ByVal businessObj As String, ByVal xmlIn As String,
                   ByVal xmlParam As String)
        _businessObject = businessObj
        _xmlIn = xmlIn
        _xmlParams = xmlParam
        _xmlOut = Nothing
        _transactionXmlOut = New SysproPostXmlOutResult
        signinInfo = loginInfo
    End Sub
    Public Sub New(loginInfo As SysproSignInObj, ByVal businessObj As String, ByVal xmlIn As String,
                   ByVal xmlParam As String, soDetails As List(Of SorDetail))
        _businessObject = businessObj
        _xmlIn = xmlIn
        _xmlParams = xmlParam
        _xmlOut = Nothing
        _transactionXmlOut = New SysproPostXmlOutResult
        signinInfo = loginInfo
        _sorDetails = soDetails
    End Sub
    Public Function Excecute() As Boolean
        Try
            With signinInfo
                _wcf = New SYSPROWCFServicesClient("net.tcp://localhost:20000/SYSPROWCFService/Rest", SYSPROWCFBinding.NetTcp, .Username, .UserPassWord, .Company, .CompanyPassword)
            End With

            _transactionXmlOut.XmlOut = _wcf.TransactionPost(_businessObject, _xmlParams, _xmlIn)

        Catch ex As Exception
            AppendTrnMessage("Syspro Posting Failure" & vbCrLf & vbCrLf & "Exception: " & msgHelper.GetFullMessage(ex))
        Finally
            ' Logoff(sysproUser)
        End Try
        'End If
        Return _transactionXmlOut.XmlOut IsNot Nothing
    End Function

    Private Function Logon(utilityObj As SysproUtilityObject) As String
        Dim sysproUser As String = Nothing
        Try
            With utilityObj
                'Using c As New SysproUtilitiesService.utilitiesclassSoapClient
                '    sysproUser = c.Logon(.Username, .UserPW, .Company, .CompanyPW, 0, 0, 0, "")
                'End Using
                'Create the WCF Instance
                _wcf = New SYSPROWCFServicesClient("net.tcp://localhost:20000/SYSPROWCFService/Rest", SYSPROWCFBinding.NetTcp, .Username, .UserPW, .Company, .CompanyPW)
                'Using wsUtils As New SysproUtilitiesService.utilitiesclass
                '    sysproUser = wsUtils.Logon(.Username, .UserPW, .Company, .CompanyPW, 0, 0, 0, "")
                'End Using
            End With
        Catch ex As Exception
            AppendTrnMessage("Syspro Logon Failure for " & utilityObj.Username &
                             utilityObj.UserPW & utilityObj.Company & utilityObj.CompanyPW & vbCrLf & vbCrLf &
                             "Exception: " & msgHelper.GetFullMessage(ex))
            sysproUser = Nothing
        End Try
        Return sysproUser
    End Function

    'Private Function Logoff(sysproUser As String) As Boolean
    '    'Dim trnSuccess As Boolean = False
    '    'Try
    '    '    Using wsUtils As New SysproUtilitiesService.utilitiesclassSoapClient
    '    '        Dim result As String = wsUtils.Logoff(sysproUser)
    '    '    End Using
    '    '    'Using wsUtils As New SysproUtilitiesService.utilitiesclass

    '    '    '    Dim result As String = wsUtils.Logoff(sysproUser)
    '    '    'End Using
    '    '    trnSuccess = True
    '    'Catch ex As Exception
    '    '    AppendTrnMessage("Syspro Logoff Failure" & vbCrLf & vbCrLf & "Exception: " & msgHelper.Getfullmessage(ex))
    '    '    sysproUser = Nothing
    '    'End Try
    '    'Return trnSuccess
    'End Function

    Public Function ConfirmXmlOut(Optional salesorder As String = "") As SysproPostXmlOutResult
        Dim returnObj As SysproPostXmlOutResult = Nothing
        If _transactionXmlOut.XmlOut IsNot Nothing Then
            returnObj = New SysproPostXmlOutResult
            returnObj.XmlOut = _transactionXmlOut.XmlOut

            If _businessObject = "SORTOI" Then
                'Query db to check if sales order items were succefully reserved
                If CheckSalesOrderPost(salesorder) Then
                    returnObj.Successful = True
                    If _trnMessage Is Nothing Then
                        'Error would have occured in posting
                        GetOtherErrors(returnObj)
                    End If
                Else
                    'Error would have occured in posting
                    GetOtherErrors(returnObj)
                End If
            Else
                'read xml data
                Dim xDoc = XDocument.Parse(_transactionXmlOut.XmlOut)
                'get any errors if exists
                Dim errortags = xDoc.Descendants("ErrorDescription")
                If errortags.Count > 0 Then
                    returnObj.ErrorsFound = True
                    _transactionXmlOut.ErrorsFound = True
                    For Each et As XElement In errortags
                        _transactionXmlOut.ErrorMessages.Add(Trim(et.Value.ToString))
                        AppendTrnMessage(Trim(et.Value.ToString))
                        returnObj.ErrorMessages.Add(Trim(et.Value.ToString))
                    Next
                End If
                'get any warnings if exists
                Dim warningtags = xDoc.Descendants("WarningMessages")
                If warningtags.Count > 0 Then
                    returnObj.WarningsFound = True
                    _transactionXmlOut.WarningsFound = True
                    For Each wt In warningtags
                        _transactionXmlOut.WarningMessages.Add(wt.Value.ToString)
                        AppendTrnMessage(Trim(wt.Value.ToString))
                        returnObj.WarningMessages.Add(wt.Value.ToString)
                    Next
                End If

                'SORTRA Success and Failure check for lines reserved
                If _businessObject = "SORTRA" Then
                    processSuccessMessages(xDoc, returnObj)
                    processErrorMessages(xDoc, returnObj)
                End If
            End If
        End If
        Return returnObj
    End Function

    Private Sub processErrorMessages(xdoc As XDocument, ByRef returnObj As SysproPostXmlOutResult)
        Dim itemsFound = xdoc.Descendants("Item")
        If itemsFound.Count > 0 Then
            For Each item In itemsFound
                Try
                    Dim vStatus As String = item.Element("ValidationStatus").Element("Status").Value
                    If vStatus = "Failed" Then
                        _transactionXmlOut.ErrorsFound = True
                        returnObj.ErrorsFound = True
                        Dim stockcode As String = ""
                        Dim LineNumber As String = ""
                        Dim errorDescription As String = ""
                        'Get line Number
                        LineNumber = item.Element("ValidationStatus").Element("SalesOrderDetails").Element("SalesOrderLine").Value
                        errorDescription = item.Element("salesorderline").Element("ErrorDescription").Value
                        'get the stockcode value from the order details object
                        Dim ln As Decimal = CDec(LineNumber)
                        If _sorDetails IsNot Nothing Then
                            Dim d = _sorDetails.Where(Function(c) c.SalesOrderLine = ln).FirstOrDefault
                            If d IsNot Nothing Then
                                stockcode = d.MStockCode
                            End If
                        End If
                        AppendTrnMessage("Line " & LineNumber & " Stockcode: " & stockcode & " Failed with Error: " & errorDescription)
                        returnObj.ErrorMessages.Add("Line " & LineNumber & " Stockcode: " & stockcode & " Failed with Error: " & errorDescription)
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Sub processSuccessMessages(xdoc As XDocument, ByRef returnObj As SysproPostXmlOutResult)
        Dim itemsFound = xdoc.Descendants("Item")
        If itemsFound.Count > 0 Then
            For Each item In itemsFound
                Try
                    Dim vStatus As String = item.Element("ValidationStatus").Element("Status").Value
                    If vStatus = "Successful" Then
                        _transactionXmlOut.Successful = True
                        returnObj.Successful = True
                        Dim stockcode As String = ""
                        Dim LineNumber As String = ""
                        'Get line Number
                        LineNumber = item.Element("ValidationStatus").Element("SalesOrderDetails").Element("SalesOrderLine").Value
                        'get the stockcode value from the order details object
                        Dim ln As Decimal = CDec(LineNumber)
                        If _sorDetails IsNot Nothing Then
                            Dim d = _sorDetails.Where(Function(c) c.SalesOrderLine = ln).FirstOrDefault
                            If d IsNot Nothing Then
                                stockcode = d.MStockCode
                            End If
                        End If
                        AppendTrnMessage("Line " & LineNumber & " Stockcode: " & stockcode & " was Successfully Reserved.")
                        returnObj.OtherMessages.Add("Line " & LineNumber & " Stockcode: " & stockcode & " was Successfully Reserved.")
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Function GetTransationReferenceValue(xDoc As XDocument, bo As Enums.BusinessObjectUsed) As String
        Dim reference As String = Nothing
        Select Case bo
            Case Enums.BusinessObjectUsed.PORTOI
                'purchase order number
                reference = xDoc.<postpurchaseorders>.<Order>.<Key>.<PurchaseOrder>.Value
            Case Enums.BusinessObjectUsed.PORTOR
                'grn number 
                reference = xDoc.<postpurchaseorderreceipts>.<Item>.<Receipt>.<Grn>.Value
            Case Enums.BusinessObjectUsed.SORTIC
                'invoice number
                reference = xDoc.<postsalesorderinvoice>.<Item>.<Key>.<InvoiceNumber>.Value
            Case Enums.BusinessObjectUsed.SORTOI
                'reserve stock
                reference = xDoc.<PostSorAllocateReserved>.<item>.<key>.<SalesOrder>.Value
        End Select
        Return reference
    End Function

    Private Function ParseXmlin(xmlin As String) As XElement
        Dim parsedXml As XElement
        parsedXml = XElement.Parse(xmlin)
        Return parsedXml
    End Function

    Private Function CheckSalesOrderPost(salesorder As String) As Boolean
        Dim b As New BLL.Query
        Dim result As Boolean
        Dim msg As String

        'Fill the created sales order
        _postedOrder = b.FillSorDetails(salesorder)
        If _postedOrder.Count > 0 Then
            'Need to fomrat headers
            For Each item In _postedOrder
                result = True
                If item.QtyReserved > 0 Then
                    If item.MOrderQty = item.QtyReserved Then 'OK
                        result = True
                        msg = ("Stock code " & item.MStockCode & " Quantity " & CInt(item.QtyReserved) & " Reserve Successful")
                        Dim x = FormatResult(item, msg, "OK")
                        AppendTrnMessage(x.ToString)
                    Else 'POK
                        msg = ("Stock code " & item.MStockCode & " Quantity " & CInt(item.QtyReserved) & " Reserve Partial")
                        Dim x = FormatResult(item, msg, "POK")
                        AppendTrnMessage(x.ToString)
                    End If
                ElseIf item.MBackOrderQty > 0 Then
                    msg = ("Stock code " & item.MStockCode & " Quantity " & CInt(item.MBackOrderQty) & " Reserve Fail")
                    Dim x = FormatResult(item, msg, "NOK")
                    AppendTrnMessage(x.ToString)
                End If
            Next
        Else
            AppendTrnMessage("Could not identify items for created  order.")
        End If
        Return result
    End Function

    Private Function FormatResult(item As SorDetail, msg As String, result As String) As XElement
        Return <StockLine>
                   <Line><%= item.SalesOrderLine %></Line>
                   <Post2Result><%= msg %></Post2Result>
                   <StockCode><%= item.MStockCode %></StockCode>
                   <QtyReserved><%= item.QtyReserved %></QtyReserved>
                   <QtyFailed><%= item.MBackOrderQty %></QtyFailed>
                   <Status><%= result %></Status>
               </StockLine>
    End Function

    Private Sub GetOtherErrors(ByRef returnObj As SysproPostXmlOutResult)
        'read xml data
        Dim xDoc = XDocument.Parse(_transactionXmlOut.XmlOut)
        'get any errors if exists
        Dim errortags = xDoc.Descendants("ErrorDescription")
        If errortags.Count > 0 Then
            returnObj.ErrorsFound = True
            _transactionXmlOut.ErrorsFound = True
            For Each et As XElement In errortags
                _transactionXmlOut.ErrorMessages.Add(Trim(et.Value.ToString))
                AppendTrnMessage(Trim(et.Value.ToString))
                returnObj.ErrorMessages.Add(Trim(et.Value.ToString))
            Next
        End If
        'get any warnings if exists
        Dim warningtags = xDoc.Descendants("WarningMessages")
        If warningtags.Count > 0 Then
            returnObj.WarningsFound = True
            _transactionXmlOut.WarningsFound = True
            For Each wt In warningtags
                _transactionXmlOut.WarningMessages.Add(wt.Value.ToString)
                AppendTrnMessage(Trim(wt.Value.ToString))
                returnObj.WarningMessages.Add(wt.Value.ToString)
            Next
        End If
    End Sub

End Class

Public Class SysproPostXmlOutResult

    Public Sub New()
        ErrorsFound = False
        WarningsFound = False
        Successful = False
        ErrorMessages = New List(Of String)
        WarningMessages = New List(Of String)
        OtherMessages = New List(Of String)
        Reference = Nothing
        itemsRejectedWithWarnings = Nothing
        itemsProcessedWithWarnings = Nothing
        ItemsProcessed = Nothing
    End Sub

    Public Property XmlOut As String
    Public Property ErrorsFound As Boolean
    Public Property ErrorMessages As List(Of String)
    Public Property WarningsFound As Boolean
    Public Property WarningMessages As List(Of String)
    Public Property OtherMessages As List(Of String)
    Public Property itemsRejectedWithWarnings As String
    Public Property itemsProcessedWithWarnings As String
    Public Property Successful As Boolean
    Public Property Reference As String
    Public Property ItemsProcessed As String

End Class

Public Class SysproUtilityObject
    Public Property Username As String
    Public Property UserPW As String
    Public Property Company As String
    Public Property CompanyPW As String
    'Private Sub New()
    'End Sub
    Public Sub New(SignInInfo As SysproSignInObj)
        With SignInInfo
            Me.Username = .Username
            Me.UserPW = .UserPassWord
            Me.Company = .Company
            Me.CompanyPW = .CompanyPassword
        End With

    End Sub
End Class

Public Class SysproSignInObj
    Public Property Username As String
    Public Property UserPassWord As String
    Public Property Company As String
    Public Property CompanyPassword As String
    Public Sub New(ByVal username As String, ByVal userpassword As String, ByVal company As String, ByVal companypassword As String)
        Me.Username = username
        Me.UserPassWord = userpassword
        Me.Company = company
        Me.CompanyPassword = companypassword
    End Sub
End Class
