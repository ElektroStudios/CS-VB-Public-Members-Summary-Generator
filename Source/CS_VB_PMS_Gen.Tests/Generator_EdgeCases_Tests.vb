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
    Public Class Generator_EdgeCases_Tests

        <Fact>
        Public Sub GenerateSummary_EditorBrowsableNever_IsOmitted_CSharp()
            Dim code As String =
                "using System.ComponentModel;" & vbCrLf &
                "public class Target {" & vbCrLf &
                "    [EditorBrowsable(EditorBrowsableState.Never)]" & vbCrLf &
                "    public void HiddenMethod() {}" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.DoesNotContain("HiddenMethod", result)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_EditorBrowsableNever_IsOmitted_VB()
            Dim code As String =
                "Imports System.ComponentModel" & vbCrLf &
                "Public Class Target" & vbCrLf &
                "    <EditorBrowsable(EditorBrowsableState.Never)>" & vbCrLf &
                "    Public Sub HiddenMethod()" & vbCrLf & "    End Sub" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.DoesNotContain("HiddenMethod", result)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_ProhibitedMembers_AreOmitted()
            Dim code As String =
                "Public Class Target" & vbCrLf &
                "    Public Shadows Function [GetType]() As Type" & vbCrLf &
                "    Return Nothing" & vbCrLf &
                "    End Function" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.DoesNotContain("GetType", result)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_SyntaxError_ThrowsInvalidOperationException()
            Dim code As String = "public class {" ' Missing class name
            Dim count As Integer = 0

            Assert.Throws(Of InvalidOperationException)(
                Function() Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count))
        End Sub

    End Class
End Namespace
