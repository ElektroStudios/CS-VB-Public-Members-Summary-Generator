#Region " Option Statements "
Option Strict On
Option Explicit On
Option Infer Off
#End Region

#Region " Imports "
Imports System
Imports System.IO

Imports CS_VB_Public_Members_Summary_Generator

Imports Xunit
#End Region

Namespace CS_VB_PMS_Gen.Tests
    Public Class FileSystemHelperTests
        <Theory>
        <InlineData("C:\Test\Folder", "\\?\C:\Test\Folder")>
        <InlineData("\\Server\Share\File.txt", "\\?\UNC\Server\Share\File.txt")>
        <InlineData("Relative\Path.txt", "Relative\Path.txt")>
        <InlineData("\\?\C:\AlreadyExtended.txt", "\\?\C:\AlreadyExtended.txt")>
        Public Sub GetExtendedPath_ReturnsExpectedResult(input As String, expected As String)
            Dim result As String = FileSystemHelper.GetExtendedPath(input)
            Assert.Equal(expected, result)
        End Sub

        <Theory>
        <InlineData("\\?\C:\Test\Folder", "C:\Test\Folder")>
        <InlineData("\\?\UNC\Server\Share", "\\Server\Share")>
        <InlineData("C:\Normal\Path.txt", "C:\Normal\Path.txt")>
        Public Sub GetNormalPath_ReturnsExpectedResult(input As String, expected As String)
            Dim result As String = FileSystemHelper.GetNormalPath(input)
            Assert.Equal(expected, result)
        End Sub
    End Class
End Namespace
