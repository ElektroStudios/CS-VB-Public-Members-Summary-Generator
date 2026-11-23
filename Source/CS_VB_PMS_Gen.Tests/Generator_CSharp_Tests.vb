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
    Public Class Generator_CSharp_Tests

        <Fact>
        Public Sub GenerateSummary_CSharp_PublicClass_ExtractsPublicMembersOnly()
            Dim code As String =
                "public class Target {" & vbCrLf &
                "    public Target() {}" & vbCrLf &
                "    private Target(int x) {}" & vbCrLf &
                "    public string PropPublic { get; set; }" & vbCrLf &
                "    internal string PropInternal { get; set; }" & vbCrLf &
                "    public void MethodPublic() {}" & vbCrLf &
                "    protected void MethodProtected() {}" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("Target()", result, StringComparison.Ordinal)
            Assert.Contains("PropPublic", result, StringComparison.Ordinal)
            Assert.Contains("MethodPublic", result, StringComparison.Ordinal)
            Assert.DoesNotContain("PropInternal", result, StringComparison.Ordinal)
            Assert.DoesNotContain("MethodProtected", result, StringComparison.Ordinal)
            Assert.Equal(3, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_InternalClass_IsCompletelyIgnored()
            Dim code As String =
                "internal class Target {" & vbCrLf &
                "    public void MethodPublic() {}" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.DoesNotContain("MethodPublic", result, StringComparison.Ordinal)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_NestedPublicClass_ExtractsMembers()
            Dim code As String =
                "public class Outer {" & vbCrLf &
                "    public class Inner {" & vbCrLf &
                "        public void NestedMethod() {}" & vbCrLf &
                "    }" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("Outer.Inner.NestedMethod()", result, StringComparison.Ordinal)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_NestedClassInInternal_IsIgnored()
            Dim code As String =
                "internal class Outer {" & vbCrLf &
                "    public class Inner {" & vbCrLf &
                "        public void NestedMethod() {}" & vbCrLf &
                "    }" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.DoesNotContain("NestedMethod", result, StringComparison.Ordinal)
            Assert.Equal(0, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_Interface_ExtractsAllMembersAsPublic()
            Dim code As String =
                "public interface IWorker {" & vbCrLf &
                "    void DoWork();" & vbCrLf &
                "    string Status { get; }" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("IWorker.DoWork()", result, StringComparison.Ordinal)
            Assert.Contains("IWorker.Status", result, StringComparison.Ordinal)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_Struct_ExtractsPublicMembers()
            Dim code As String =
                "public struct Point {" & vbCrLf &
                "    public int X;" & vbCrLf &
                "    public int Y;" & vbCrLf &
                "    private int Z;" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("Point.X", result, StringComparison.Ordinal)
            Assert.Contains("Point.Y", result, StringComparison.Ordinal)
            Assert.DoesNotContain("Point.Z", result, StringComparison.Ordinal)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_Operators_AreExtracted()
            Dim code As String =
                "public class MathObj {" & vbCrLf &
                "    public static MathObj operator +(MathObj a, MathObj b) => null;" & vbCrLf &
                "    public static implicit operator string(MathObj a) => null;" & vbCrLf &
                "}"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("MathObj.+(MathObj, MathObj)", result, StringComparison.Ordinal)
            Assert.Contains("MathObj.implicit(MathObj)", result, StringComparison.Ordinal)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_Delegates_AreExtracted()
            Dim code As String = "public delegate void MyCallback(string msg);"
            Dim count As Integer = 0
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("MyCallback(string)", result, StringComparison.Ordinal)
            Assert.Equal(1, count)
        End Sub

        <Fact>
        Public Sub GenerateSummary_CSharp_FieldsAndEvents_AreExtracted()
            ' Arrange
            Dim code As String =
                "public class Target {" & vbCrLf &
                "    public int PublicField;" & vbCrLf &
                "    private int PrivateField;" & vbCrLf &
                "    public event System.EventHandler PublicEvent;" & vbCrLf &
                "    public event System.EventHandler CustomEvent { add {} remove {} }" & vbCrLf &
                "}"
            Dim count As Integer = 0

            ' Act
            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            ' Assert
            Assert.Contains("PublicField", result, StringComparison.Ordinal)
            Assert.Contains("PublicEvent", result, StringComparison.Ordinal)
            Assert.Contains("CustomEvent", result, StringComparison.Ordinal)
            Assert.DoesNotContain("PrivateField", result, StringComparison.Ordinal)
            Assert.Equal(3, count)
        End Sub

    End Class
End Namespace
