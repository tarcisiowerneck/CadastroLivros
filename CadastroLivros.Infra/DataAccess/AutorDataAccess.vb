Imports System.Data.SqlClient

Namespace WebApiLivros.Infra.Repository

    Public Module AutorDataAccess
        Private Sub SaveLivroAutores(connection As SqlConnection, transaction As SqlTransaction, livroCod As Integer, autores As List(Of String))
            For Each autor In autores
                Dim codAu = SaveAutor(connection, transaction, autor)
                Using cmd As New SqlCommand("INSERT INTO Livro_Autor (Livro_Cod, Autor_CodAu) VALUES (@livroCod, @AutorCod)", connection, transaction)
                    cmd.Parameters.AddWithValue("@livroCod", livroCod)
                    cmd.Parameters.AddWithValue("@AutorCod", codAu)
                    cmd.ExecuteNonQuery()
                End Using
            Next
        End Sub

        Public Sub SaveAutores(connection As SqlConnection, transaction As SqlTransaction, livroCod As Integer, autores As List(Of String))
            Using cmdDelete As New SqlCommand("DELETE FROM Livro_Autor WHERE Livro_Cod = @livroCod", connection, transaction)
                cmdDelete.Parameters.AddWithValue("@livroCod", livroCod)
                cmdDelete.ExecuteNonQuery()
            End Using

            SaveLivroAutores(connection, transaction, livroCod, autores)
        End Sub

        Private Function SaveAutor(connection As SqlConnection, transaction As SqlTransaction, nomeAutor As String) As Integer
            Dim autorId As Integer = GetAutorCod(connection, transaction, nomeAutor)

            If autorId = 0 Then
                autorId = InsertAutor(connection, transaction, nomeAutor)
            End If

            Return autorId
        End Function

        Private Function GetAutorCod(connection As SqlConnection, transaction As SqlTransaction, nomeAutor As String) As Integer
            Using cmd As New SqlCommand("SELECT CodAu FROM Autor WHERE Nome = @Nome", connection, transaction)
                cmd.Parameters.AddWithValue("@Nome", nomeAutor)
                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                    Return Convert.ToInt32(result)
                Else
                    Return 0
                End If
            End Using
        End Function

        Private Function InsertAutor(connection As SqlConnection, transaction As SqlTransaction, nomeAutor As String) As Integer
            Using cmd As New SqlCommand("INSERT INTO Autor (Nome) VALUES (@Nome); SELECT SCOPE_IDENTITY()", connection, transaction)
                cmd.Parameters.AddWithValue("@Nome", nomeAutor)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function
    End Module
End Namespace