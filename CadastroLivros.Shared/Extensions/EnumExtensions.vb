Imports System.ComponentModel
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace WebApiLivros.Shared.Extensions
    Public Module EnumExtensions
        <Extension()>
        Public Function ToDescription(Of EnumType As Structure)(EnumValue As EnumType) As String
            Dim fi As FieldInfo = EnumValue.GetType().GetField(EnumValue.ToString())
            Dim attributes As DescriptionAttribute() = TryCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

            If attributes IsNot Nothing AndAlso attributes.Any() Then Return Enumerable.First(attributes).Description

            Return EnumValue.ToString()
        End Function
    End Module
End Namespace
