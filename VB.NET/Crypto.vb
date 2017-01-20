Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class Crypto
    Public Function Encrypt(ByVal text As String, ByVal key As String) As String
        Try
            Dim crp As New TripleDESCryptoServiceProvider
            Dim uEncode As New UnicodeEncoding
            Dim bytPlainText() As Byte = uEncode.GetBytes(text)
            Dim stmCipherText As New MemoryStream
            Dim slt() As Byte = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
            Dim pdb As New Rfc2898DeriveBytes(key, slt)
            Dim bytDerivedKey() As Byte = pdb.GetBytes(24)

            crp.Key = bytDerivedKey
            crp.IV = pdb.GetBytes(8)

            Dim csEncrypted As New CryptoStream(stmCipherText, crp.CreateEncryptor(), CryptoStreamMode.Write)

            csEncrypted.Write(bytPlainText, 0, bytPlainText.Length)
            csEncrypted.FlushFinalBlock()
            Return Convert.ToBase64String(stmCipherText.ToArray())
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class

