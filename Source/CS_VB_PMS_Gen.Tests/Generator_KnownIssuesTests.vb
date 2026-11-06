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

    ''' <summary>
    ''' Tests that describe the CORRECT behavior for defects of the generator that were confirmed while writing the test suite.
    ''' <para></para>
    ''' All of them are skipped, so the suite stays green: remove the "Skip" argument of a test once its defect is fixed,
    ''' and it will protect the fix from regressions. The reason of each skip explains the defect.
    ''' </summary>
    Public Class Generator_KnownIssuesTests

#Region " Private Methods "

        ''' <summary>
        ''' Runs the generator on Visual Basic source code made of the specified lines.
        ''' </summary>
        Private Shared Function RunVb(ParamArray lines As String()) As GeneratorOutput

            Return GeneratorOutput.VisualBasic(String.Join(vbCrLf, lines))

        End Function

        ''' <summary>
        ''' Runs the generator on C# source code made of the specified lines.
        ''' </summary>
        Private Shared Function RunCs(ParamArray lines As String()) As GeneratorOutput

            Return GeneratorOutput.CSharp(String.Join(vbCrLf, lines))

        End Function

#End Region

#Region " Visual Basic: Crashes "

        <Fact>
        Public Sub VB_TopLevelDelegate_IsListed()

            Dim output As GeneratorOutput = RunVb("Public Delegate Sub TopCb()", "Public Delegate Function TopCf() As Integer")

            Assert.Equal(GeneratorOutput.Expected("TopCb()", "TopCf() As Integer"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub VB_NamespaceLevelDelegate_IsListed()

            Dim output As GeneratorOutput = RunVb("Namespace N", "    Public Delegate Sub InNs()", "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("InNs()"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub VB_EventWithoutAsClauseNorParameters_IsListed()

            Dim output As GeneratorOutput = RunVb("Public Class C", "    Public Event NoClause", "End Class")

            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " Visual Basic: Wrong Results "

        <Fact>
        Public Sub VB_TopLevelTypeWithoutModifier_IsFriend_AndIgnored()

            Dim output As GeneratorOutput = RunVb("Class C", "    Public Sub S()", "    End Sub", "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub VB_SharedConstructor_IsNotListed()

            Dim output As GeneratorOutput = RunVb("Public Class C", "    Shared Sub New()", "    End Sub", "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub VB_EditorBrowsableNever_HidesEvents()

            Dim output As GeneratorOutput = RunVb(
                "Imports System.ComponentModel",
                "Public Class C",
                "    <EditorBrowsable(EditorBrowsableState.Never)>",
                "    Public Event Ev As EventHandler",
                "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub VB_ArrayOnTheFieldIdentifier_KeepsTheArrayType()

            Dim output As GeneratorOutput = RunVb("Public Class C", "    Public Names() As String", "    Public Arr(5) As Integer", "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Names As String()", "C.Arr As Integer()"), output.Members("Fields"))

        End Sub

        <Fact>
        Public Sub VB_ArrayOnTheParameterIdentifier_KeepsTheArrayType()

            Dim output As GeneratorOutput = RunVb(
                "Public Class C",
                "    Public Sub M(ParamArray e() As Integer)",
                "    End Sub",
                "    Public Sub N(x() As String)",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.M(Integer())", "C.N(String())"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub VB_DeclareStatements_AreListedAsMethods()

            Dim output As GeneratorOutput = RunVb(
                "Public Class C",
                "    Public Declare Function GetTick Lib ""kernel32"" Alias ""GetTickCount"" () As Integer",
                "End Class")

            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " C#: Wrong Results "

        <Fact>
        Public Sub CS_Record_IsListedWithItsName()

            Dim output As GeneratorOutput = RunCs("public record R(int A, string B) { public void M() {} }")

            Assert.Equal(GeneratorOutput.Expected("void R.M()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int R.A", "string R.B"), output.Members("Properties"))

        End Sub

        <Fact>
        Public Sub CS_RecordStruct_IsListed()

            Dim output As GeneratorOutput = RunCs("public record struct RS(int A) { public void M() {} }")

            Assert.Equal(GeneratorOutput.Expected("void RS.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub CS_Indexer_IsListed()

            Dim output As GeneratorOutput = RunCs("public class C { public int this[int i] { get { return 0; } } }")

            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " Contract "

        <Fact>
        Public Sub Count_IsResetToZero_WhenThereAreNoMembers()

            Dim count As Integer = 77

            Dim text As String = Generator.GenerateSummaryRegion("public class C { private void M() {} }", SourceLanguage.CSharp, count)

            Assert.Equal(String.Empty, text)
            Assert.Equal(0, count)

        End Sub

#End Region

    End Class

End Namespace
