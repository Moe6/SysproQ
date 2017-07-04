Imports SysproQ.Base
Imports SysproQ.Entity.Enums
Module Module1
    Sub Main()
        Dim b As New Base()
        Dim result As PostResults
        result = b.PostSORTOI("")
        Select Case result
            Case PostResults.Success
                MsgBox("Success")
            Case PostResults.Fail
                MsgBox("Fail")
        End Select

    End Sub
End Module
