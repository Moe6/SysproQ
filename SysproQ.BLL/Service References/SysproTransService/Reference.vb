﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace SysproTransService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", ConfigurationName:="SysproTransService.transactionclassSoap")>  _
    Public Interface transactionclassSoap
        
        'CODEGEN: Generating message contract since the wrapper name (Build_With_String) of message Build_With_String does not match the default value (Build)
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Build_With_String", ReplyAction:="*")>  _
        Function Build(ByVal request As SysproTransService.Build_With_String) As SysproTransService.Build_With_String1
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Build_With_String", ReplyAction:="*")>  _
        Function BuildAsync(ByVal request As SysproTransService.Build_With_String) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_String1)
        
        'CODEGEN: Generating message contract since the wrapper name (Build_With_ByteArray) of message Build_With_ByteArray does not match the default value (BuildW)
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Build_With_ByteArray", ReplyAction:="*")>  _
        Function BuildW(ByVal request As SysproTransService.Build_With_ByteArray) As SysproTransService.Build_With_ByteArray1
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Build_With_ByteArray", ReplyAction:="*")>  _
        Function BuildWAsync(ByVal request As SysproTransService.Build_With_ByteArray) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_ByteArray1)
        
        'CODEGEN: Generating message contract since the wrapper name (Post_With_String) of message Post_With_String does not match the default value (Post)
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Post_With_String", ReplyAction:="*")>  _
        Function Post(ByVal request As SysproTransService.Post_With_String) As SysproTransService.Post_With_String1
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Post_With_String", ReplyAction:="*")>  _
        Function PostAsync(ByVal request As SysproTransService.Post_With_String) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_String1)
        
        'CODEGEN: Generating message contract since the wrapper name (Post_With_ByteArray) of message Post_With_ByteArray does not match the default value (PostW)
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Post_With_ByteArray", ReplyAction:="*")>  _
        Function PostW(ByVal request As SysproTransService.Post_With_ByteArray) As SysproTransService.Post_With_ByteArray1
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://www.syspro.com/ns/transaction/Post_With_ByteArray", ReplyAction:="*")>  _
        Function PostWAsync(ByVal request As SysproTransService.Post_With_ByteArray) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_ByteArray1)
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Build_With_String", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Build_With_String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public UserId As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=1)>  _
        Public BusinessObject As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=2)>  _
        Public XMLIn As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn As String)
            MyBase.New
            Me.UserId = UserId
            Me.BusinessObject = BusinessObject
            Me.XMLIn = XMLIn
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Build_With_StringResponse", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Build_With_String1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public Build_With_StringResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Build_With_StringResult As String)
            MyBase.New
            Me.Build_With_StringResult = Build_With_StringResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Build_With_ByteArray", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Build_With_ByteArray
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public UserId As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=1)>  _
        Public BusinessObject As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=2)>  _
        Public XMLIn() As Byte
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn() As Byte)
            MyBase.New
            Me.UserId = UserId
            Me.BusinessObject = BusinessObject
            Me.XMLIn = XMLIn
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Build_With_ByteArrayResponse", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Build_With_ByteArray1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public Build_With_ByteArrayResult() As Byte
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Build_With_ByteArrayResult() As Byte)
            MyBase.New
            Me.Build_With_ByteArrayResult = Build_With_ByteArrayResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Post_With_String", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Post_With_String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public UserId As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=1)>  _
        Public BusinessObject As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=2)>  _
        Public XMLParameters As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=3)>  _
        Public XMLIn As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters As String, ByVal XMLIn As String)
            MyBase.New
            Me.UserId = UserId
            Me.BusinessObject = BusinessObject
            Me.XMLParameters = XMLParameters
            Me.XMLIn = XMLIn
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Post_With_StringResponse", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Post_With_String1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public Post_With_StringResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Post_With_StringResult As String)
            MyBase.New
            Me.Post_With_StringResult = Post_With_StringResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Post_With_ByteArray", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Post_With_ByteArray
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public UserId As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=1)>  _
        Public BusinessObject As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=2)>  _
        Public XMLParameters() As Byte
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=3)>  _
        Public XMLIn() As Byte
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters() As Byte, ByVal XMLIn() As Byte)
            MyBase.New
            Me.UserId = UserId
            Me.BusinessObject = BusinessObject
            Me.XMLParameters = XMLParameters
            Me.XMLIn = XMLIn
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="Post_With_ByteArrayResponse", WrapperNamespace:="http://www.syspro.com/ns/transaction/", IsWrapped:=true)>  _
    Partial Public Class Post_With_ByteArray1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://www.syspro.com/ns/transaction/", Order:=0)>  _
        Public Post_With_ByteArrayResult() As Byte
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Post_With_ByteArrayResult() As Byte)
            MyBase.New
            Me.Post_With_ByteArrayResult = Post_With_ByteArrayResult
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface transactionclassSoapChannel
        Inherits SysproTransService.transactionclassSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class transactionclassSoapClient
        Inherits System.ServiceModel.ClientBase(Of SysproTransService.transactionclassSoap)
        Implements SysproTransService.transactionclassSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_Build(ByVal request As SysproTransService.Build_With_String) As SysproTransService.Build_With_String1 Implements SysproTransService.transactionclassSoap.Build
            Return MyBase.Channel.Build(request)
        End Function
        
        Public Function Build(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn As String) As String
            Dim inValue As SysproTransService.Build_With_String = New SysproTransService.Build_With_String()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLIn = XMLIn
            Dim retVal As SysproTransService.Build_With_String1 = CType(Me,SysproTransService.transactionclassSoap).Build(inValue)
            Return retVal.Build_With_StringResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_BuildAsync(ByVal request As SysproTransService.Build_With_String) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_String1) Implements SysproTransService.transactionclassSoap.BuildAsync
            Return MyBase.Channel.BuildAsync(request)
        End Function
        
        Public Function BuildAsync(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn As String) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_String1)
            Dim inValue As SysproTransService.Build_With_String = New SysproTransService.Build_With_String()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLIn = XMLIn
            Return CType(Me,SysproTransService.transactionclassSoap).BuildAsync(inValue)
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_BuildW(ByVal request As SysproTransService.Build_With_ByteArray) As SysproTransService.Build_With_ByteArray1 Implements SysproTransService.transactionclassSoap.BuildW
            Return MyBase.Channel.BuildW(request)
        End Function
        
        Public Function BuildW(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn() As Byte) As Byte()
            Dim inValue As SysproTransService.Build_With_ByteArray = New SysproTransService.Build_With_ByteArray()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLIn = XMLIn
            Dim retVal As SysproTransService.Build_With_ByteArray1 = CType(Me,SysproTransService.transactionclassSoap).BuildW(inValue)
            Return retVal.Build_With_ByteArrayResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_BuildWAsync(ByVal request As SysproTransService.Build_With_ByteArray) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_ByteArray1) Implements SysproTransService.transactionclassSoap.BuildWAsync
            Return MyBase.Channel.BuildWAsync(request)
        End Function
        
        Public Function BuildWAsync(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLIn() As Byte) As System.Threading.Tasks.Task(Of SysproTransService.Build_With_ByteArray1)
            Dim inValue As SysproTransService.Build_With_ByteArray = New SysproTransService.Build_With_ByteArray()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLIn = XMLIn
            Return CType(Me,SysproTransService.transactionclassSoap).BuildWAsync(inValue)
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_Post(ByVal request As SysproTransService.Post_With_String) As SysproTransService.Post_With_String1 Implements SysproTransService.transactionclassSoap.Post
            Return MyBase.Channel.Post(request)
        End Function
        
        Public Function Post(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters As String, ByVal XMLIn As String) As String
            Dim inValue As SysproTransService.Post_With_String = New SysproTransService.Post_With_String()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLParameters = XMLParameters
            inValue.XMLIn = XMLIn
            Dim retVal As SysproTransService.Post_With_String1 = CType(Me,SysproTransService.transactionclassSoap).Post(inValue)
            Return retVal.Post_With_StringResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_PostAsync(ByVal request As SysproTransService.Post_With_String) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_String1) Implements SysproTransService.transactionclassSoap.PostAsync
            Return MyBase.Channel.PostAsync(request)
        End Function
        
        Public Function PostAsync(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters As String, ByVal XMLIn As String) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_String1)
            Dim inValue As SysproTransService.Post_With_String = New SysproTransService.Post_With_String()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLParameters = XMLParameters
            inValue.XMLIn = XMLIn
            Return CType(Me,SysproTransService.transactionclassSoap).PostAsync(inValue)
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_PostW(ByVal request As SysproTransService.Post_With_ByteArray) As SysproTransService.Post_With_ByteArray1 Implements SysproTransService.transactionclassSoap.PostW
            Return MyBase.Channel.PostW(request)
        End Function
        
        Public Function PostW(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters() As Byte, ByVal XMLIn() As Byte) As Byte()
            Dim inValue As SysproTransService.Post_With_ByteArray = New SysproTransService.Post_With_ByteArray()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLParameters = XMLParameters
            inValue.XMLIn = XMLIn
            Dim retVal As SysproTransService.Post_With_ByteArray1 = CType(Me,SysproTransService.transactionclassSoap).PostW(inValue)
            Return retVal.Post_With_ByteArrayResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function SysproTransService_transactionclassSoap_PostWAsync(ByVal request As SysproTransService.Post_With_ByteArray) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_ByteArray1) Implements SysproTransService.transactionclassSoap.PostWAsync
            Return MyBase.Channel.PostWAsync(request)
        End Function
        
        Public Function PostWAsync(ByVal UserId As String, ByVal BusinessObject As String, ByVal XMLParameters() As Byte, ByVal XMLIn() As Byte) As System.Threading.Tasks.Task(Of SysproTransService.Post_With_ByteArray1)
            Dim inValue As SysproTransService.Post_With_ByteArray = New SysproTransService.Post_With_ByteArray()
            inValue.UserId = UserId
            inValue.BusinessObject = BusinessObject
            inValue.XMLParameters = XMLParameters
            inValue.XMLIn = XMLIn
            Return CType(Me,SysproTransService.transactionclassSoap).PostWAsync(inValue)
        End Function
    End Class
End Namespace
