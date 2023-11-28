Imports System.Data.SqlClient

Namespace WebApiLivros.Infra.Repository

    Public Module AssuntoDataAccess
        Private Sub SaveLivroAssuntos(connection As SqlConnection, transaction As SqlTransaction, livroCod As Integer, assuntos As List(Of String))
            For Each assunto In assuntos
                Dim codAs = SaveAssunto(connection, transaction, assunto)
                Using cmd As New SqlCommand("INSERT INTO Livro_Assunto (Livro_Cod, Assunto_CodAs) VALUES (@livroCod, @AssuntoCod)", connection, transaction)
                    cmd.Parameters.AddWithValue("@livroCod", livroCod)
                    cmd.Parameters.AddWithValue("@AssuntoCod", codAs)
                    cmd.ExecuteNonQuery()
                End Using
            Next
        End Sub

        Public Sub SaveAssuntos(connection As SqlConnection, transaction As SqlTransaction, livroCod As Integer, assuntos As List(Of String))
            Using cmdDelete As New SqlCommand("DELETE FROM Livro_Assunto WHERE Livro_Cod = @livroCod", connection, transaction)
                cmdDelete.Parameters.AddWithValue("@livroCod", livroCod)
                cmdDelete.ExecuteNonQuery()
            End Using

            SaveLivroAssuntos(connection, transaction, livroCod, assuntos)
        End Sub

        Private Function SaveAssunto(connection As SqlConnection, transaction As SqlTransaction, descricaoAssunto As String) As Integer
            Dim assuntoId As Integer = GetAssuntoCod(connection, transaction, descricaoAssunto)

            If assuntoId = 0 Then
                assuntoId = InsertAssunto(connection, transaction, descricaoAssunto)
            End If

            Return assuntoId
        End Function

        Private Function GetAssuntoCod(connection As SqlConnection, transaction As SqlTransaction, descricaoAssunto As String) As Integer
            Using cmd As New SqlCommand("SELECT CodAs FROM Assunto WHERE Descricao = @Descricao", connection, transaction)
                cmd.Parameters.AddWithValue("@Descricao", descricaoAssunto)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                    Return Convert.ToInt32(result)
                Else
                    Return 0
                End If
            End Using
        End Function

        Private Function InsertAssunto(connection As SqlConnection, transaction As SqlTransaction, descricaoAssunto As String) As Integer
            Using cmd As New SqlCommand("INSERT INTO Assunto (Descricao) VALUES (@Descricao); SELECT SCOPE_IDENTITY()", connection, transaction)
                cmd.Parameters.AddWithValue("@Descricao", descricaoAssunto)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function
    End Module
End Namespace
