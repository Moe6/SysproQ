Imports Owin
Imports Microsoft.Owin
Imports System.Web.Services
Imports System.Web.Services.Protocols
<Assembly: OwinStartupAttribute(GetType(Startup))>

Partial Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        ConfigureAuth(app)
    End Sub
End Class
