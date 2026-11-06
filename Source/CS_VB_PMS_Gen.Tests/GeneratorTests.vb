#Region " Option Statements "
Option Strict On
Option Explicit On
Option Infer Off
#End Region

#Region " Imports "
Imports System

Imports CS_VB_Public_Members_Summary_Generator

Imports Xunit
#End Region

Namespace CS_VB_PMS_Gen.Tests

    Public Class GeneratorTests

        <Fact>
        Public Sub GenerateSummaryRegion_CSharp_DetectsPublicMembersOnly()
            ' Arrange
            Dim code As String = "public class MyClass { public void PublicMethod() {} private void Hidden() {} public string PublicProp { get; set; } }"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            ' Assert
            Assert.Contains("PublicMethod", result)
            Assert.Contains("PublicProp", result)
            Assert.DoesNotContain("Hidden", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummaryRegion_VisualBasic_DetectsPublicMembersOnly()
            ' Arrange
            Dim code As String =
                "Public Class MyTestClass" & vbCrLf &
                "    Public Sub PublicMethod()" & vbCrLf &
                "    End Sub" & vbCrLf &
                "    Private Sub Hidden()" & vbCrLf &
                "    End Sub" & vbCrLf &
                "    Public Property PublicProp As String" & vbCrLf &
                "End Class"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            ' Assert
            Assert.Contains("PublicMethod", result)
            Assert.Contains("PublicProp", result)
            Assert.DoesNotContain("Hidden", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummaryRegion_EmptyCode_ReturnsEmptyString()
            ' Arrange
            Dim code As String = "   "
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            ' Assert
            Assert.Equal(String.Empty, result)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummaryRegion_EditorBrowsableNever_IsIgnored()
            ' Arrange
            Dim code As String =
                "using System.ComponentModel;" & vbCrLf &
                "public class MyClass {" & vbCrLf &
                "    [EditorBrowsable(EditorBrowsableState.Never)]" & vbCrLf &
                "    public void HiddenMethod() {}" & vbCrLf &
                "}"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            ' Assert
            Assert.DoesNotContain("HiddenMethod", result)
            Assert.Equal(0, count)
        End Sub

    End Class

End Namespace
