Imports System.Collections.Generic
Imports System.Linq

Public Class eTrackSysproLog

    Public Shared Property TransactionID As Integer = Nothing

    Public Shared Function CreateLog(trnLog As Log) As Boolean
        Dim trnSuccess As Boolean = False
        Using db As New eTrackSysproLogEntities
            db.Logs.Add(trnLog)
            If db.SaveChanges > 0 Then
                TransactionID = GetID(trnLog)
                trnSuccess = True
            End If
        End Using
        Return trnSuccess
    End Function
    
    Public Function GetLog(logID As Integer) As List(Of Log)
        Using db As New eTrackSysproLogEntities
            Return db.Logs.Where(Function(c) c.ID = logID).ToList
        End Using
    End Function

    Private Shared Function GetID(trnLog As Log) As Integer
        Dim id = trnLog.ID
        Return id
    End Function


    Public Function Search(reference As String) As List(Of Log)
        Using db As New eTrackSysproLogEntities
            Return db.Logs.Where(Function(c) c.Reference = reference).ToList
        End Using
    End Function

    Public Function Search(startDate As DateTime, endDate As DateTime) As List(Of Log)
        startDate = New DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0)
        endDate = New DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, 999)
        Using db As New eTrackSysproLogEntities
            Return db.Logs.Where(Function(c) c.TrnTime >= startDate And c.TrnTime <= endDate).ToList
        End Using
    End Function


End Class



