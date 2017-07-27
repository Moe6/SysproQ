Imports SysproQ.DAL
Imports SysproQ.Entity

Public Class Query
    Public Function FillSorDetails(so As String) As List(Of SorDetail)
        Using dal As New DAL.Query
            Return dal.FillSalesOrderDetails(so)
        End Using
    End Function
    Public Function FillSorMaster(so As String) As SorMaster
        Using dal As New DAL.Query
            Return dal.FillOrderMaster(so)
        End Using
    End Function
End Class
