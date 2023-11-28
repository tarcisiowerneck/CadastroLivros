Imports System.Data
Imports System.Data.SqlClient
Imports WebApiLivros.Domain.Entities
Imports WebApiLivros.Domain.Interfaces
Imports WebApiLivros.Domain.Models

Namespace WebApiLivros.Infra.Repository

    Public Class LivroDataAccess
        Implements ILivroDataAccess

        Private connectionString As String

        Public Sub New(connection As String)
            connectionString = connection
        End Sub


        Public Function GetAll() As List(Of Livro) Implements ILivroDataAccess.GetAll
            Dim livros As New List(Of Livro)()

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Livro"
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim livro As New Livro() With {
                                .Cod = Convert.ToInt32(reader("Cod")),
                                .Titulo = reader("Titulo").ToString(),
                                .Editora = reader("Editora").ToString(),
                                .Edicao = Convert.ToInt32(reader("Edicao")),
                                .AnoPublicacao = reader("AnoPublicacao").ToString(),
                                .Preco = Convert.ToDecimal(reader("Preco"))
                            }
                            livros.Add(livro)
                        End While
                    End Using
                End Using
            End Using

            Return livros
        End Function

        Public Function GetList() As List(Of LivroListModel) Implements ILivroDataAccess.GetList
            Dim livros As New List(Of LivroListModel)()

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT Cod, Titulo FROM Livro"

                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim livro As New LivroListModel(
                                Convert.ToInt32(reader("Cod")),
                                reader("Titulo").ToString()
                                )
                            livros.Add(livro)
                        End While
                    End Using
                End Using
            End Using

            Return livros
        End Function

        Public Function GetByCod(cod As Integer) As Livro Implements ILivroDataAccess.GetByCod
            Dim livro As New Livro()

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim queryLivro As String = "SELECT * FROM Livro WHERE Cod = @CodigoLivro"
                Using commandLivro As New SqlCommand(queryLivro, connection)
                    commandLivro.Parameters.AddWithValue("@CodigoLivro", cod)

                    Using readerLivro As SqlDataReader = commandLivro.ExecuteReader()
                        If readerLivro.Read() Then
                            livro.Cod = Convert.ToInt32(readerLivro("Cod"))
                            livro.Titulo = readerLivro("Titulo").ToString()
                            livro.Editora = readerLivro("Editora").ToString()
                            livro.Edicao = Convert.ToInt32(readerLivro("Edicao"))
                            livro.AnoPublicacao = readerLivro("AnoPublicacao").ToString()
                            livro.Preco = Convert.ToDecimal(readerLivro("Preco"))
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using

                Dim queryAutores As String = "SELECT A.Nome FROM Autor A " &
                                         "INNER JOIN Livro_Autor LA ON A.CodAu = LA.Autor_CodAu " &
                                         "WHERE LA.Livro_Cod = @CodigoLivro"

                Using commandAutores As New SqlCommand(queryAutores, connection)
                    commandAutores.Parameters.AddWithValue("@CodigoLivro", cod)

                    Using readerAutores As SqlDataReader = commandAutores.ExecuteReader()
                        While readerAutores.Read()
                            livro.Autores.Add(readerAutores("Nome").ToString())
                        End While
                    End Using
                End Using

                Dim queryAssuntos As String = "SELECT ASU.Descricao FROM Assunto ASU " &
                                          "INNER JOIN Livro_Assunto LASU ON ASU.CodAs = LASU.Assunto_CodAs " &
                                          "WHERE LASU.Livro_Cod = @CodigoLivro"

                Using commandAssuntos As New SqlCommand(queryAssuntos, connection)
                    commandAssuntos.Parameters.AddWithValue("@CodigoLivro", cod)

                    Using readerAssuntos As SqlDataReader = commandAssuntos.ExecuteReader()
                        While readerAssuntos.Read()
                            livro.Assuntos.Add(readerAssuntos("Descricao").ToString())
                        End While
                    End Using
                End Using
            End Using

            Return livro
        End Function

        Public Function GetReportData() As DataTable Implements ILivroDataAccess.GetReportData
            Dim dataTable As New DataTable()

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM RelatorioLivros"

                Using adapter As New SqlDataAdapter(query, connection)
                    adapter.Fill(dataTable)
                End Using
            End Using

            Return dataTable
        End Function

        Public Sub Remove(cod As Integer) Implements ILivroDataAccess.Remove
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        Using cmd As New SqlCommand("DELETE FROM Livro_Autor WHERE Livro_Cod = @LivroId", connection, transaction)
                            cmd.Parameters.AddWithValue("@LivroId", cod)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmd As New SqlCommand("DELETE FROM Livro_Assunto WHERE Livro_Cod = @LivroId", connection, transaction)
                            cmd.Parameters.AddWithValue("@LivroId", cod)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmd As New SqlCommand("DELETE FROM Livro WHERE Cod = @LivroId", connection, transaction)
                            cmd.Parameters.AddWithValue("@LivroId", cod)
                            cmd.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception("Erro ao excluir o livro", ex)
                    End Try
                End Using
            End Using
        End Sub

        Public Sub Save(livro As Livro) Implements ILivroDataAccess.Save
            If livro.Cod = 0 Then
                InsertLivro(livro)
            Else
                UpdateLivro(livro)
            End If
        End Sub

        Private Sub InsertLivro(livro As Livro)
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        Using cmd As New SqlCommand("INSERT INTO Livro (Titulo, Editora, Edicao, AnoPublicacao, Preco) VALUES (@Titulo, @Editora, @Edicao, @AnoPublicacao, @Preco); SELECT SCOPE_IDENTITY()", connection, transaction)
                            cmd.Parameters.AddWithValue("@Titulo", livro.Titulo)
                            cmd.Parameters.AddWithValue("@Editora", livro.Editora)
                            cmd.Parameters.AddWithValue("@Edicao", livro.Edicao)
                            cmd.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao)
                            cmd.Parameters.AddWithValue("@Preco", livro.Preco)

                            livro.Cod = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        SaveAutores(connection, transaction, livro.Cod, livro.Autores)

                        SaveAssuntos(connection, transaction, livro.Cod, livro.Assuntos)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception("Erro ao inserir o livro", ex)
                    End Try
                End Using
            End Using
        End Sub

        Private Sub UpdateLivro(livro As Livro)
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        Using cmd As New SqlCommand("UPDATE Livro SET Titulo = @Titulo, Editora = @Editora, Edicao = @Edicao, AnoPublicacao = @AnoPublicacao, Preco = @Preco WHERE Cod = @LivroId", connection, transaction)
                            cmd.Parameters.AddWithValue("@LivroId", livro.Cod)
                            cmd.Parameters.AddWithValue("@Titulo", livro.Titulo)
                            cmd.Parameters.AddWithValue("@Editora", livro.Editora)
                            cmd.Parameters.AddWithValue("@Edicao", livro.Edicao)
                            cmd.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao)
                            cmd.Parameters.AddWithValue("@Preco", livro.Preco)
                            cmd.ExecuteNonQuery()
                        End Using

                        SaveAutores(connection, transaction, livro.Cod, livro.Autores)

                        SaveAssuntos(connection, transaction, livro.Cod, livro.Assuntos)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw New Exception("Erro ao atualizar o livro", ex)
                    End Try
                End Using
            End Using
        End Sub

    End Class
End Namespace