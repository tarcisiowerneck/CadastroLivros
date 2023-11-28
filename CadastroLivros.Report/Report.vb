Imports Microsoft.Reporting.NETCore
Imports System.Data
Imports System.Reflection

Namespace CadastroLivros.Report
    Friend Class Report
        Public Shared Sub Load(ByVal report As LocalReport, ByVal livros As DataTable)

            Dim items As New List(Of ReportItem)()

            For Each row As DataRow In livros.Rows
                Dim ri As New ReportItem()

                ri.Cod = Convert.ToInt32(row("Cod"))
                ri.Titulo = Convert.ToString(row("Titulo"))
                ri.Editora = Convert.ToString(row("Editora"))
                ri.Edicao = Convert.ToInt32(row("Edicao"))
                ri.AnoPublicacao = Convert.ToString(row("AnoPublicacao"))
                ri.Preco = Convert.ToDecimal(row("Preco"))

                items.Add(ri)
            Next

            Dim parameters = {New ReportParameter("Title", "Livros")}
            Dim rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("CadastroLivros.Report.Report.rdlc")
            report.LoadReportDefinition(rs)
            report.DataSources.Add(New ReportDataSource("Items", items.ToArray()))
            report.SetParameters(parameters)
        End Sub
    End Class
End Namespace
