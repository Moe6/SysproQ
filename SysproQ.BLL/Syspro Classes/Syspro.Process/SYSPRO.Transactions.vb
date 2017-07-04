Public Class Transactions
    
    Private Function ProcessPost(bo As IBusinessObjects, reference As String, username As String, validateOnly As Boolean) As Log
        'initialise transaction log
        Dim trnLog As New Log(username, bo.CreateXmlIn, bo.CreateXmlParams(validateOnly), bo.GetName, validateOnly)
        With trnLog
            'set the reference value of the document in question
            .Reference = reference
            'process the post
            Using service As New WebService(New Entity.SysproUtilityObject)
                'post to SYSPRO via web WebService 
                .XmlOut = service.Post(.BusinessObject, .XmlIn, .XmlParam)
                'check for exceptions
                If Not String.IsNullOrWhiteSpace(service.ExceptionError) Then
                    .IsException = True
                    .Note = String.Format("{0} POST Exception:{1}", .BusinessObject, service.ExceptionError)
                End If
            End Using
            'process the result from the post
            If Not CBool(.IsException) Then
                'check if anything was returned from the web WebService
                If .XmlOut IsNot Nothing Then
                    'read the XML out
                    .PostResult = New SysproPostResult(.XmlOut, bo.GetName)
                    If .PostResult.Successful Then
                        'set the transaction log status to successful
                        .Successful = True
                    Else
                        'unsuccessful post
                        .Note = String.Format("SYSPRO POST ERROR:{0}{1}", Environment.NewLine, .PostResult.ErrorMessages)
                    End If
                Else
                    'nothing returned from the web WebService
                    .Note = String.Format("Nothing was returned from the WebService for the {0} post transaction", bo.GetName)
                End If
            End If
        End With
        'save the post log
        eTrackSysproLog.CreateLog(trnLog)
        'return boolean result 
        Return trnLog
    End Function

    Private Function action_Post(bo As IBusinessObjects, reference As String, trnUser As String, validateOnly As Boolean) As Log
        'process actual post and get log
        Dim postTrnLog = ProcessPost(bo, reference, trnUser, validateOnly)
        'check for errors/note 
        If Not CBool(postTrnLog.Successful) Then AppendTrnMessage(postTrnLog.Note, postTrnLog.Note)
        'return log 
        Return postTrnLog
    End Function

    Public Function PostWithValidation(bo As IBusinessObjects, reference As String, trnUser As String) As Log
        'create an instance of the log to return
        Dim postTrnLog As New Log
        'process validation post
        Dim validateTrnLog = action_Post(bo, reference, trnUser, True)
        If validateTrnLog.Successful Then
            'process actual post 
            postTrnLog = action_Post(bo, reference, trnUser, False)
        Else
            'update the return object with the validation log note
            AppendTrnMessage(postTrnLog.Note, validateTrnLog.Note)
        End If
        Return postTrnLog
    End Function

    Public Function PostWithoutValidation(bo As IBusinessObjects, reference As String, trnUser As String) As Log
        Return action_Post(bo, reference, trnUser, False)
    End Function

End Class

