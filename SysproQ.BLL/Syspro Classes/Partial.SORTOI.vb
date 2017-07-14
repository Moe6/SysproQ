Imports System.Collections.Generic
Imports System.Linq
Imports SysproQ.Entity
Imports System.Xml.Linq
Partial Public Class SORTOI
    Private _Xmlin As String
    Private passedXml As XElement
    Private OrderHdr As New List(Of OrderHeaderDataObject)
    Private orderDetails As New List(Of OrderDetailDataObject)
    Private _PostOut As New SysproPostXmlOutResult
    Private _signInInfo As SysproSignInObj
    Private _msg As String
    Private _actionType As String
    Public ReadOnly Property PostResult As Enums.PostResults
    Private _solines As List(Of SoLines)
    Public ReadOnly Property TrnMessage As String
        Get
            Return _msg
        End Get
    End Property
    Public Sub New(Xmlin As String, signIn As SysproSignInObj, actionType As String, slns As List(Of SoLines))
        _Xmlin = Xmlin
        _actionType = actionType
        _solines = slns
        With signIn
            _signInInfo = New SysproSignInObj(.Username, .UserPassWord, .Company, .CompanyPassword)
        End With
    End Sub

    Public Function processXmlIn(salesorder As String) As Enums.PostResults
        passedXml = ParseXmlin()
        'Create the Objects to post to Syspro
        LoadDataIntoSalesObject()
        'Post To Syspro
        Dim p As New SORTOI.ProcessPost(OrderHdr.FirstOrDefault, orderDetails, _solines)
        _PostOut = p.Execute(_signInInfo, salesorder, _actionType)
        AppendTrnMessage(_msg, p.TrnMessage)
        Return Enums.PostResults.Success
    End Function

    Private Function ParseXmlin() As XElement
        Dim foreignXml As XElement
        foreignXml = XElement.Parse(_Xmlin)
        Return foreignXml
    End Function

    Private Sub LoadDataIntoSalesObject()
        Dim doc As XElement = passedXml
        'Dim headerInfo = doc.Descendants("OrderHeader")

        'If headerInfo.Count > 0 Then
        Dim ordHd As New OrderHeaderDataObject
            With ordHd
            'For Each item In headerInfo
            .SalesOrder = doc.Element("SalesOrder").Value
            .CustomerPoNumber = doc.Element("PO").Value
            .Customer = doc.Element("Customer").Value
            .OrderActionType = doc.Element("ActionType").Value
            .OrderDate = Now.Date.ToString("yyyy-MM-dd")
            'Next
        End With
            OrderHdr.Add(ordHd)
        'End If

        'Load Detail    
        Dim detailInfo = doc.Descendants("StockLine")
        For Each detail In detailInfo
            Dim ordDet As New OrderDetailDataObject
            With ordDet
                .StockCode = detail.Element("StockCode").Value
                '.StockDescription = detail.Element("StockDescription").Value
                .OrderQty = detail.Element("Qty").Value
                .Price = detail.Element("Price").Value
                .Warehouse = detail.Element("City").Value
                .LineActionType = detail.Element("LineAction").Value
                .CustomerPoLine = detail.Element("PoLine").Value
            End With
            orderDetails.Add(ordDet)
        Next

    End Sub

    Private Function BillingXmlInTemplate() As XElement
        'THis is the xml format that I expect to be passed by thi billing system as a string
        'Then converted to an xml doc
        'then read the xml doc and pass data to my Order objects 
        'Then  read order classes and pass dat to Syspro Xml BO
        'Then Post to Syspro.

        Return <Order>
                   <OrderHeader>
                       <SalesOrder>50005</SalesOrder>
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

    End Function

End Class
