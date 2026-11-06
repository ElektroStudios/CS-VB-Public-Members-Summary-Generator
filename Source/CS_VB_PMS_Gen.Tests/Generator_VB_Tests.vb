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
    Public Class Generator_VB_Tests

        <Fact>
        Public Sub GenerateSummary_VB_PublicClass_ExtractsPublicMembersOnly()
            Dim code As String =
                "Public Class Target" & vbCrLf &
                "    Public Sub New()" & vbCrLf & "    End Sub" & vbCrLf &
                "    Private Sub New(x As Integer)" & vbCrLf & "    End Sub" & vbCrLf &
                "    Public Property PropPublic As String" & vbCrLf &
                "    Friend Property PropFriend As String" & vbCrLf &
                "    Public Sub MethodPublic()" & vbCrLf & "    End Sub" & vbCrLf &
                "    Protected Sub MethodProtected()" & vbCrLf & "    End Sub" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("Target.New()", result)
            Assert.Contains("PropPublic As String", result)
            Assert.Contains("MethodPublic()", result)
            Assert.DoesNotContain("PropFriend", result)
            Assert.DoesNotContain("MethodProtected", result)
            Assert.Equal(3, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_FriendClass_IsCompletelyIgnored()
            Dim code As String =
                "Friend Class Target" & vbCrLf &
                "    Public Sub MethodPublic()" & vbCrLf & "    End Sub" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.DoesNotContain("MethodPublic", result)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_Module_ExtractsPublicMembers()
            Dim code As String =
                "Public Module Utils" & vbCrLf &
                "    Public Sub Helper()" & vbCrLf & "    End Sub" & vbCrLf &
                "End Module"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("Utils.Helper()", result)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_Fields_EvaluatesCorrectly()
            Dim code As String =
                "Public Class Data" & vbCrLf &
                "    Public FieldA As Integer" & vbCrLf &
                "    Dim FieldB As Integer" & vbCrLf & ' Dim is implicitly Private in classes
                "    Private FieldC As Integer" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("Data.FieldA As Integer", result)
            Assert.DoesNotContain("FieldB", result)
            Assert.DoesNotContain("FieldC", result)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_Operators_AreExtracted()
            Dim code As String =
                "Public Class MathObj" & vbCrLf &
                "    Public Shared Operator +(a As MathObj, b As MathObj) As MathObj" & vbCrLf & "    Return Nothing" & vbCrLf & "    End Operator" & vbCrLf &
                "    Public Shared Widening Operator CType(a As MathObj) As String" & vbCrLf & "    Return Nothing" & vbCrLf & "    End Operator" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("MathObj.+(MathObj, MathObj) As MathObj", result)
            Assert.Contains("MathObj.CType(MathObj) As String", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_Events_AreExtracted()
            Dim code As String =
                "Public Class UI" & vbCrLf &
                "    Public Event Clicked As EventHandler" & vbCrLf &
                "    Public Event Hovered(x As Integer, y As Integer)" & vbCrLf &
                "End Class"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("UI.Clicked As EventHandler", result)
            Assert.Contains("UI.Hovered(Integer, Integer)", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_Enums_AreExtracted()
            Dim code As String =
                "Public Enum State As Byte" & vbCrLf &
                "    Off = 0" & vbCrLf &
                "    [On] = 1" & vbCrLf &
                "End Enum"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("State As Byte", result)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_SpecialCharacters_AreTrimmedFromIdentifiers()
            ' Arrange
            Dim code As String =
                "Public Class Target" & vbCrLf &
                "    Public Sub [DoWork]()" & vbCrLf &
                "    End Sub" & vbCrLf &
                "    Public Property [String] As String" & vbCrLf &
                "End Class"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            ' Assert
            Assert.Contains("DoWork()", result)
            Assert.DoesNotContain("[DoWork]", result)

            Assert.Contains("String As String", result)
            Assert.DoesNotContain("[String]", result)

            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_EnumWithoutBaseType_DefaultsToInteger()
            ' Arrange
            Dim code As String =
                "Public Enum DefaultEnum" & vbCrLf &
                "    ValueOne" & vbCrLf &
                "End Enum"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            ' Assert
            Assert.Contains("DefaultEnum As Integer", result)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_VB_NestedPublicClassInFriendClass_IsIgnored()
            ' Arrange
            Dim code As String =
                "Friend Class OuterFriend" & vbCrLf &
                "    Public Class InnerPublic" & vbCrLf &
                "        Public Sub PublicMethod()" & vbCrLf &
                "        End Sub" & vbCrLf &
                "    End Class" & vbCrLf &
                "End Class"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            ' Assert
            Assert.DoesNotContain("PublicMethod", result)
            Assert.Equal(0, count)
        End Sub

    End Class
End Namespace
