#Region " Option Statements "
Option Strict On
Option Explicit On
Option Infer Off
#End Region

#Region " Imports "
Imports System
Imports System.IO
Imports System.Text

Imports CS_VB_Public_Members_Summary_Generator

Imports Xunit
#End Region

Namespace CS_VB_PMS_Gen.Tests
    Public Class EncodingHelperTests
        <Fact>
        Public Sub TryGetUtfFileEncodingKind_UTF16LE_BOM_DetectsSuccessfully()
            Dim tempFile As String = Path.GetTempFileName()
            Try
                File.WriteAllText(tempFile, "Test", New UnicodeEncoding(bigEndian:=False, byteOrderMark:=True))
                Dim detectedKind As UtfEncodingKind = UtfEncodingKind.Unknown
                Dim result As Boolean = EncodingHelper.TryGetUtfFileEncodingKind(tempFile, detectedKind)
                Assert.True(result)
                Assert.Equal(UtfEncodingKind.UTF16LE_Bom, detectedKind)
            Finally
                If File.Exists(tempFile) Then File.Delete(tempFile)
            End Try
        End Sub

        <Fact>
        Public Sub TryGetUtfFileEncodingKind_UTF16BE_BOM_DetectsSuccessfully()
            Dim tempFile As String = Path.GetTempFileName()
            Try
                File.WriteAllText(tempFile, "Test", New UnicodeEncoding(bigEndian:=True, byteOrderMark:=True))
                Dim detectedKind As UtfEncodingKind = UtfEncodingKind.Unknown
                Dim result As Boolean = EncodingHelper.TryGetUtfFileEncodingKind(tempFile, detectedKind)
                Assert.True(result)
                Assert.Equal(UtfEncodingKind.UTF16BE_Bom, detectedKind)
            Finally
                If File.Exists(tempFile) Then File.Delete(tempFile)
            End Try
        End Sub
    End Class
End Namespace
