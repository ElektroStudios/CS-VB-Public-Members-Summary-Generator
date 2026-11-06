#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

Imports CS_VB_Public_Members_Summary_Generator

Imports Xunit

#End Region

Namespace CS_VB_PMS_Gen.Tests

    ''' <summary>
    ''' Tests of the structure of the text produced by <c>Generator.GenerateSummaryRegion</c>:
    ''' header, footer, sections, order, count and general contract of the method.
    ''' The detection of each kind of member is tested in <see cref="Generator_CSharp_MemberKindsTests"/> and <see cref="Generator_VB_MemberKindsTests"/>.
    ''' </summary>
    Public Class GeneratorTests

#Region " Constants "

        ''' <summary>
        ''' A C# source that contains one public member of each of the eight kinds.
        ''' </summary>
        Private Const CSharpAllKinds As String =
            "public class Target {" & vbCrLf &
            "    public Target() {}" & vbCrLf &
            "    public int Prop { get; set; }" & vbCrLf &
            "    public int Field;" & vbCrLf &
            "    public event System.EventHandler Occurred;" & vbCrLf &
            "    public void Method() {}" & vbCrLf &
            "    public static Target operator +(Target a, Target b) { return a; }" & vbCrLf &
            "    public delegate void Callback(int x);" & vbCrLf &
            "    public enum Kind { A, B }" & vbCrLf &
            "}"

        ''' <summary>
        ''' A Visual Basic source that contains one public member of each of the eight kinds.
        ''' </summary>
        Private Const VisualBasicAllKinds As String =
            "Public Class Target" & vbCrLf &
            "    Public Sub New()" & vbCrLf &
            "    End Sub" & vbCrLf &
            "    Public Property Prop As Integer" & vbCrLf &
            "    Public Field As Integer" & vbCrLf &
            "    Public Event Occurred As System.EventHandler" & vbCrLf &
            "    Public Sub Method()" & vbCrLf &
            "    End Sub" & vbCrLf &
            "    Public Shared Operator +(a As Target, b As Target) As Target" & vbCrLf &
            "        Return a" & vbCrLf &
            "    End Operator" & vbCrLf &
            "    Public Delegate Sub Callback(x As Integer)" & vbCrLf &
            "    Public Enum Kind" & vbCrLf &
            "        A" & vbCrLf &
            "        B" & vbCrLf &
            "    End Enum" & vbCrLf &
            "End Class"

        ''' <summary>
        ''' The regular expression that the first line of the region of C# must match.
        ''' </summary>
        Private Const CSharpHeaderPattern As String =
            "^#region Public Members Summary \(auto-generated with CS-VB_PMS_Gen on (?<date>[A-Z][a-z]+ \d{1,2}, \d{4} @ \d{2}:\d{2}:\d{2})\)$"

        ''' <summary>
        ''' The regular expression that the first line of the region of Visual Basic must match.
        ''' </summary>
        Private Const VisualBasicHeaderPattern As String =
            "^#Region "" Public Members Summary \(auto-generated with CS-VB_PMS_Gen on (?<date>[A-Z][a-z]+ \d{1,2}, \d{4} @ \d{2}:\d{2}:\d{2})\) ""$"

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Splits a text in lines, whatever the line break is.
        ''' </summary>
        Private Shared Function SplitLines(text As String) As String()

            Return text.Replace(vbCrLf, vbLf).Split(ControlChars.Lf)

        End Function

        ''' <summary>
        ''' Returns the text without its first line (the one that has the date and time).
        ''' </summary>
        Private Shared Function WithoutHeader(text As String) As String

            Return String.Join(vbLf, GeneratorTests.SplitLines(text).Skip(1))

        End Function

#End Region

#Region " Original Tests (kept) "

        <Fact>
        Public Sub GenerateSummaryRegion_CSharp_DetectsPublicMembersOnly()
            Dim code As String = "public class MyClass { public void PublicMethod() {} private void Hidden() {} public string PublicProp { get; set; } }"
            Dim count As Integer = 0

            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count)

            Assert.Contains("PublicMethod", result)
            Assert.Contains("PublicProp", result)
            Assert.DoesNotContain("Hidden", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummaryRegion_VisualBasic_DetectsPublicMembersOnly()
            Dim code As String =
                "Public Class MyTestClass" & vbCrLf &
                "    Public Sub PublicMethod()" & vbCrLf &
                "    End Sub" & vbCrLf &
                "    Private Sub Hidden()" & vbCrLf &
                "    End Sub" & vbCrLf &
                "    Public Property PublicProp As String" & vbCrLf &
                "End Class"
            Dim count As Integer = 0

            Dim result As String = Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count)

            Assert.Contains("PublicMethod", result)
            Assert.Contains("PublicProp", result)
            Assert.DoesNotContain("Hidden", result)
            Assert.Equal(2, count)
        End Sub

        <Fact>
        Public Sub GenerateSummaryRegion_WhitespaceCode_ReturnsEmptyString()
            Dim count As Integer = 0

            Dim result As String = Generator.GenerateSummaryRegion("   ", SourceLanguage.CSharp, count)

            Assert.Equal(String.Empty, result)
            Assert.Equal(0, count)
        End Sub

#End Region

#Region " Header And Footer "

        <Fact>
        Public Sub Header_CSharp_HasTheExpectedFormat()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)
            Dim firstLine As String = GeneratorTests.SplitLines(output.Text)(0)

            Assert.Matches(GeneratorTests.CSharpHeaderPattern, firstLine)

        End Sub

        <Fact>
        Public Sub Header_VisualBasic_HasTheExpectedFormat()

            Dim output As GeneratorOutput = GeneratorOutput.VisualBasic(GeneratorTests.VisualBasicAllKinds)
            Dim firstLine As String = GeneratorTests.SplitLines(output.Text)(0)

            Assert.Matches(GeneratorTests.VisualBasicHeaderPattern, firstLine)

        End Sub

        ''' <summary>
        ''' The header carries the date and time of the generation, formatted with the English (United States) culture.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub Header_TimestampIsTheCurrentLocalTime(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)
            Dim pattern As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpHeaderPattern, GeneratorTests.VisualBasicHeaderPattern)

            Dim before As Date = Date.Now.AddSeconds(-2)
            Dim output As GeneratorOutput = GeneratorOutput.Generate(code, language)
            Dim after As Date = Date.Now.AddSeconds(2)

            Dim headerMatch As Match = Regex.Match(GeneratorTests.SplitLines(output.Text)(0), pattern)
            Assert.True(headerMatch.Success)

            Dim generatedAt As Date = Date.ParseExact(headerMatch.Groups("date").Value, "MMMM d, yyyy @ HH:mm:ss", CultureInfo.GetCultureInfo("en-US"))
            Assert.InRange(generatedAt, before, after)

        End Sub

        ''' <summary>
        ''' The month name in the header is always English, whatever the culture of the current thread is.
        ''' </summary>
        <Theory>
        <InlineData("es-ES")>
        <InlineData("de-DE")>
        <InlineData("ja-JP")>
        <InlineData("ar-SA")>
        Public Sub Header_IsIndependentOfTheCurrentCulture(cultureName As String)

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            Dim culture As CultureInfo
            Try
                culture = CultureInfo.GetCultureInfo(cultureName)
            Catch ex As CultureNotFoundException
                Return ' Invariant globalization mode: the culture is not available on this machine.
            End Try

            Try
                Thread.CurrentThread.CurrentCulture = culture
                Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)

                Assert.Matches(GeneratorTests.CSharpHeaderPattern, GeneratorTests.SplitLines(output.Text)(0))
            Finally
                Thread.CurrentThread.CurrentCulture = originalCulture
            End Try

        End Sub

        <Fact>
        Public Sub Footer_CSharp_IsEndRegion()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)
            Dim lines As String() = GeneratorTests.SplitLines(output.Text)

            ' The text ends with a line break, so the last element is empty.
            Assert.Equal(String.Empty, lines(lines.Length - 1))
            Assert.Equal("#endregion", lines(lines.Length - 2))
            Assert.Equal(String.Empty, lines(lines.Length - 3))

        End Sub

        <Fact>
        Public Sub Footer_VisualBasic_IsEndRegion()

            Dim output As GeneratorOutput = GeneratorOutput.VisualBasic(GeneratorTests.VisualBasicAllKinds)
            Dim lines As String() = GeneratorTests.SplitLines(output.Text)

            Assert.Equal(String.Empty, lines(lines.Length - 1))
            Assert.Equal("#End Region", lines(lines.Length - 2))
            Assert.Equal(String.Empty, lines(lines.Length - 3))

        End Sub

        <Fact>
        Public Sub Header_IsFollowedByABlankLine()

            Assert.Equal(String.Empty, GeneratorTests.SplitLines(GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds).Text)(1))
            Assert.Equal(String.Empty, GeneratorTests.SplitLines(GeneratorOutput.VisualBasic(GeneratorTests.VisualBasicAllKinds).Text)(1))

        End Sub

        ''' <summary>
        ''' The text uses the line break of the platform on every line, and never mixes line breaks.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub Text_UsesOnlyTheNewLineOfThePlatform(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)

            Dim text As String = GeneratorOutput.Generate(code, language).Text

            Assert.Equal(text, text.Replace(vbCrLf, vbLf).Replace(vbLf, Environment.NewLine))

        End Sub

#End Region

#Region " Sections "

        <Fact>
        Public Sub Sections_CSharp_AreWrittenInTheDocumentedOrder()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)

            Assert.Equal(GeneratorOutput.SectionNames, output.FoundSections.ToArray())

        End Sub

        <Fact>
        Public Sub Sections_VisualBasic_AreWrittenInTheDocumentedOrder()

            Dim output As GeneratorOutput = GeneratorOutput.VisualBasic(GeneratorTests.VisualBasicAllKinds)

            Assert.Equal(GeneratorOutput.SectionNames, output.FoundSections.ToArray())

        End Sub

        <Fact>
        Public Sub Sections_CSharp_HaveTheExpectedMembers()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)

            Assert.Equal(GeneratorOutput.Expected("Target()"), output.Members("Constructors"))
            Assert.Equal(GeneratorOutput.Expected("int Target.Prop"), output.Members("Properties"))
            Assert.Equal(GeneratorOutput.Expected("int Target.Field"), output.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("System.EventHandler Target.Occurred"), output.Members("Events"))
            Assert.Equal(GeneratorOutput.Expected("void Target.Method()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("Target Target.+(Target, Target)"), output.Members("Operators"))
            Assert.Equal(GeneratorOutput.Expected("void Target.Callback(int)"), output.Members("Delegates"))
            Assert.Equal(GeneratorOutput.Expected("Target.Kind : int"), output.Members("Enumerations"))
            Assert.Equal(8, output.Count)

        End Sub

        <Fact>
        Public Sub Sections_VisualBasic_HaveTheExpectedMembers()

            Dim output As GeneratorOutput = GeneratorOutput.VisualBasic(GeneratorTests.VisualBasicAllKinds)

            Assert.Equal(GeneratorOutput.Expected("Target.New()"), output.Members("Constructors"))
            Assert.Equal(GeneratorOutput.Expected("Target.Prop As Integer"), output.Members("Properties"))
            Assert.Equal(GeneratorOutput.Expected("Target.Field As Integer"), output.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("Target.Occurred As System.EventHandler"), output.Members("Events"))
            Assert.Equal(GeneratorOutput.Expected("Target.Method()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("Target.+(Target, Target) As Target"), output.Members("Operators"))
            Assert.Equal(GeneratorOutput.Expected("Target.Callback(Integer)"), output.Members("Delegates"))
            Assert.Equal(GeneratorOutput.Expected("Target.Kind As Integer"), output.Members("Enumerations"))
            Assert.Equal(8, output.Count)

        End Sub

        ''' <summary>
        ''' A kind of member that is not present in the code does not produce a section (not even an empty one).
        ''' </summary>
        <Fact>
        Public Sub Sections_WithoutMembers_AreNotWritten()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp("public class C { public void M() {} }")

            Assert.Equal(New String() {"Methods"}, output.FoundSections.ToArray())
            Assert.DoesNotContain("Constructors", output.Text)
            Assert.DoesNotContain("Enumerations", output.Text)

        End Sub

        ''' <summary>
        ''' Every section title is written as a comment, and is followed by a line of 16 "▔" characters (also a comment) and the members.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub Sections_TitlesAreCommentsFollowedByAnUnderline(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)
            Dim token As String = If(language = SourceLanguage.CSharp, "//", "'")
            Dim expectedUnderline As String = $"{token} {New String(GeneratorOutput.UnderlineChar, 16)}"

            Dim lines As String() = GeneratorTests.SplitLines(GeneratorOutput.Generate(code, language).Text)
            Dim underlineCount As Integer = 0

            For i As Integer = 0 To lines.Length - 1
                If lines(i).IndexOf(GeneratorOutput.UnderlineChar) >= 0 Then
                    underlineCount += 1
                    Assert.Equal(expectedUnderline, lines(i))
                    Assert.StartsWith($"{token} ", lines(i - 1), StringComparison.Ordinal)
                    Assert.EndsWith(GeneratorOutput.SectionNames(underlineCount - 1), lines(i - 1), StringComparison.Ordinal)
                End If
            Next i

            Assert.Equal(8, underlineCount)

        End Sub

        ''' <summary>
        ''' Every section is followed by exactly one blank line, and no other blank line is written inside the region.
        ''' </summary>
        <Fact>
        Public Sub Sections_AreSeparatedByExactlyOneBlankLine()

            Dim lines As String() = GeneratorTests.SplitLines(GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds).Text)

            ' Lines: header, blank, then 8 sections of (title, underline, member, blank), then "#endregion", then the trailing empty element.
            Assert.Equal(2 + 8 * 4 + 1 + 1, lines.Length)
            For i As Integer = 0 To 7
                Assert.Equal(String.Empty, lines(2 + i * 4 + 3))
            Next i

        End Sub

        ''' <summary>
        ''' Within a section, the members are sorted (the generator uses a sorted set with the default comparer).
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub Members_AreSortedWithinEachSection(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String
            If language = SourceLanguage.CSharp Then
                code = "public class C { public void Zeta() {} public void alpha() {} public void Beta() {} public void Alpha() {} public void _under() {} public void M1() {} public void M10() {} public void M2() {} }"
            Else
                code = "Public Class C" & vbCrLf & "Public Sub Zeta()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub alpha()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub Beta()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub Alpha()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub _under()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub M1()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub M10()" & vbCrLf & "End Sub" & vbCrLf & "Public Sub M2()" & vbCrLf & "End Sub" & vbCrLf & "End Class"
            End If

            Dim methods As List(Of String) = GeneratorOutput.Generate(code, language).Sections("Methods")

            Assert.Equal(8, methods.Count)
            For i As Integer = 1 To methods.Count - 1
                Assert.True(Comparer(Of String).Default.Compare(methods(i - 1), methods(i)) < 0,
                            $"'{methods(i - 1)}' should be before '{methods(i)}'.")
            Next i

        End Sub

#End Region

#Region " Count "

        <Fact>
        Public Sub Count_EqualsTheNumberOfMemberLinesWritten()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp(GeneratorTests.CSharpAllKinds)

            Assert.Equal(output.AllMembers().Count, output.Count)

        End Sub

        <Fact>
        Public Sub Count_IsNotAffectedByTheValueOfTheArgumentBeforeTheCall()

            Dim count As Integer = 12345

            Generator.GenerateSummaryRegion("public class C { public void M() {} public int F; }", SourceLanguage.CSharp, count)

            Assert.Equal(2, count)

        End Sub

        <Fact>
        Public Sub Count_ForNoMembers_IsZeroAndTheTextIsEmpty()

            Dim output As GeneratorOutput = GeneratorOutput.CSharp("public class C { private void M() {} }")

            Assert.Equal(String.Empty, output.Text)

        End Sub

#End Region

#Region " Arguments "

        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub NullSourceCode_ReturnsEmptyString(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim count As Integer = 0

            Assert.Equal(String.Empty, Generator.GenerateSummaryRegion(Nothing, language, count))

        End Sub

        <Theory>
        <InlineData("")>
        <InlineData(" ")>
        <InlineData("   ")>
        <InlineData(vbCrLf)>
        <InlineData(vbTab & vbLf & " " & vbCr)>
        Public Sub BlankSourceCode_ReturnsEmptyString(code As String)

            Dim count As Integer = 0

            Assert.Equal(String.Empty, Generator.GenerateSummaryRegion(code, SourceLanguage.CSharp, count))
            Assert.Equal(String.Empty, Generator.GenerateSummaryRegion(code, SourceLanguage.VisualBasic, count))

        End Sub

        <Theory>
        <InlineData(2)>
        <InlineData(-1)>
        <InlineData(99)>
        Public Sub InvalidLanguage_ThrowsInvalidEnumArgumentException(invalidValue As Integer)

            Dim count As Integer = 0
            Dim invalidLanguage As SourceLanguage = CType(invalidValue, SourceLanguage)

            Dim ex As InvalidEnumArgumentException = Assert.Throws(Of InvalidEnumArgumentException)(
                Function() Generator.GenerateSummaryRegion("public class C { public void M() {} }", invalidLanguage, count))

            Assert.Equal("language", ex.ParamName)

        End Sub

#End Region

#Region " Contract "

        ''' <summary>
        ''' Two runs on the same code produce the same region, except for the date and time of its header.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub Generation_IsDeterministic(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)

            Dim first As String = GeneratorOutput.Generate(code, language).Text
            Dim second As String = GeneratorOutput.Generate(code, language).Text

            Assert.Equal(GeneratorTests.WithoutHeader(first), GeneratorTests.WithoutHeader(second))

        End Sub

        ''' <summary>
        ''' The kind of line breaks of the analyzed code (CRLF, LF or CR) does not change the result.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub LineBreakStyleOfTheCode_DoesNotChangeTheResult(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)
            Dim lfCode As String = code.Replace(vbCrLf, vbLf)
            Dim crCode As String = code.Replace(vbCrLf, vbCr)

            Dim expected As String = GeneratorTests.WithoutHeader(GeneratorOutput.Generate(code, language).Text)

            Assert.Equal(expected, GeneratorTests.WithoutHeader(GeneratorOutput.Generate(lfCode, language).Text))
            Assert.Equal(expected, GeneratorTests.WithoutHeader(GeneratorOutput.Generate(crCode, language).Text))

        End Sub

        ''' <summary>
        ''' The region generated for a file has the same members when it is generated again from the code that already contains it:
        ''' the existing region is a comment/trivia and must not be counted as a member.
        ''' </summary>
        <Theory>
        <InlineData("CSharp")>
        <InlineData("VisualBasic")>
        Public Sub CodeThatAlreadyHasTheRegion_ProducesTheSameMembers(languageName As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim code As String = If(language = SourceLanguage.CSharp, GeneratorTests.CSharpAllKinds, GeneratorTests.VisualBasicAllKinds)

            Dim first As GeneratorOutput = GeneratorOutput.Generate(code, language)
            Dim second As GeneratorOutput = GeneratorOutput.Generate(first.Text & vbCrLf & vbCrLf & code, language)

            Assert.Equal(first.Count, second.Count)
            Assert.Equal(GeneratorTests.WithoutHeader(first.Text), GeneratorTests.WithoutHeader(second.Text))

        End Sub

        ''' <summary>
        ''' Non-ASCII identifiers (accents, CJK) are kept as they are.
        ''' </summary>
        <Fact>
        Public Sub UnicodeIdentifiers_AreKept()

            Dim cs As GeneratorOutput = GeneratorOutput.CSharp("public class Ñandú { public int Año; public void 日本語() {} }")
            Dim vb As GeneratorOutput = GeneratorOutput.VisualBasic("Public Class Ñandú" & vbCrLf & "Public Año As Integer" & vbCrLf & "Public Sub 日本語()" & vbCrLf & "End Sub" & vbCrLf & "End Class")

            Assert.Equal(GeneratorOutput.Expected("int Ñandú.Año"), cs.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("void Ñandú.日本語()"), cs.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("Ñandú.Año As Integer"), vb.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("Ñandú.日本語()"), vb.Members("Methods"))

        End Sub

        ''' <summary>
        ''' A source with many members is processed completely.
        ''' </summary>
        <Fact>
        Public Sub BigSource_IsProcessedCompletely()

            Dim csCode As New StringBuilder("public class Big {")
            Dim vbCode As New StringBuilder("Public Class Big")
            vbCode.AppendLine()
            For i As Integer = 0 To 1999
                csCode.Append($" public void M{i}(int a, string b) {{}}")
                vbCode.AppendLine($"    Public Sub M{i}(a As Integer, b As String)")
                vbCode.AppendLine("    End Sub")
            Next i
            csCode.Append(" }")
            vbCode.AppendLine("End Class")

            Assert.Equal(2000, GeneratorOutput.CSharp(csCode.ToString()).Count)
            Assert.Equal(2000, GeneratorOutput.VisualBasic(vbCode.ToString()).Count)

        End Sub

        ''' <summary>
        ''' Code with a syntax error is not summarized (an incomplete result would be misleading): the generator reports the error.
        ''' </summary>
        <Theory>
        <InlineData("CSharp", "public class C { public void M( { }")>
        <InlineData("CSharp", "public class {")>
        <InlineData("VisualBasic", "Public Class C" & vbCrLf & "Public Sub M(" & vbCrLf & "End Class")>
        <InlineData("VisualBasic", "Public Class" & vbCrLf & "End Class")>
        Public Sub SyntaxErrors_ThrowInvalidOperationException(languageName As String, code As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)
            Dim count As Integer = 0

            Dim ex As InvalidOperationException = Assert.Throws(Of InvalidOperationException)(
                Function() Generator.GenerateSummaryRegion(code, language, count))

            Assert.Contains("Syntax compilation error", ex.Message)

        End Sub

        ''' <summary>
        ''' A code that only has comments, usings or empty types has no members, so no region is generated.
        ''' </summary>
        <Theory>
        <InlineData("CSharp", "// nothing here")>
        <InlineData("CSharp", "using System;")>
        <InlineData("CSharp", "public class C { }")>
        <InlineData("CSharp", "namespace N { }")>
        <InlineData("VisualBasic", "' nothing here")>
        <InlineData("VisualBasic", "Imports System")>
        <InlineData("VisualBasic", "Public Class C" & vbCrLf & "End Class")>
        <InlineData("VisualBasic", "Namespace N" & vbCrLf & "End Namespace")>
        Public Sub CodeWithoutMembers_ReturnsEmptyString(languageName As String, code As String)

            Dim language As SourceLanguage = CType([Enum].Parse(GetType(SourceLanguage), languageName), SourceLanguage)

            Assert.Equal(String.Empty, GeneratorOutput.Generate(code, language).Text)

        End Sub

#End Region

    End Class

End Namespace
