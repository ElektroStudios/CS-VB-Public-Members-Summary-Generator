#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports CS_VB_Public_Members_Summary_Generator

#End Region

#Region " GeneratorOutput "

''' <summary>
''' The result of a call to <c>Generator.GenerateSummaryRegion</c>, split in sections
''' so the tests can verify each kind of member separately and without depending on the sorting rules of the current culture.
''' </summary>
Friend NotInheritable Class GeneratorOutput

#Region " Constants "

    ''' <summary>
    ''' The titles of the sections, in the order in which the generator writes them.
    ''' </summary>
    Public Shared ReadOnly SectionNames As String() = {
        "Constructors", "Properties", "Fields", "Events", "Methods", "Operators", "Delegates", "Enumerations"
    }

    ''' <summary>
    ''' The character used to underline the titles of the sections.
    ''' </summary>
    Public Shared ReadOnly UnderlineChar As Char = ChrW(&H2594)

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets the language of the analyzed source code.
    ''' </summary>
    Public ReadOnly Property Language As SourceLanguage

    ''' <summary>
    ''' Gets the complete text returned by the generator.
    ''' </summary>
    Public ReadOnly Property Text As String

    ''' <summary>
    ''' Gets the members count reported by the generator.
    ''' </summary>
    Public ReadOnly Property Count As Integer

    ''' <summary>
    ''' Gets the comment token of the language ("//" or "'").
    ''' </summary>
    Public ReadOnly Property CommentToken As String

    ''' <summary>
    ''' Gets the names of the sections found in the text, in the order in which they appear.
    ''' </summary>
    Public ReadOnly Property FoundSections As New List(Of String)

    ''' <summary>
    ''' Gets the member lines of every section (without the comment token), in the order in which they appear.
    ''' </summary>
    Public ReadOnly Property Sections As New Dictionary(Of String, List(Of String))

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GeneratorOutput"/> class.
    ''' </summary>
    Private Sub New(language As SourceLanguage, text As String, count As Integer)

        Me.Language = language
        Me.Text = text
        Me.Count = count
        Me.CommentToken = If(language = SourceLanguage.CSharp, "//", "'")

        Me.Parse()

    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Runs the generator on a source code and splits its result in sections.
    ''' </summary>
    '''
    ''' <param name="code">
    ''' The source code.
    ''' </param>
    '''
    ''' <param name="language">
    ''' The language of the source code.
    ''' </param>
    '''
    ''' <returns>
    ''' The parsed result.
    ''' </returns>
    Public Shared Function Generate(code As String, language As SourceLanguage) As GeneratorOutput

        Dim count As Integer = -1
        Dim text As String = Generator.GenerateSummaryRegion(code, language, count)

        Return New GeneratorOutput(language, text, count)

    End Function

    ''' <summary>
    ''' Runs the generator on C# source code.
    ''' </summary>
    Public Shared Function CSharp(code As String) As GeneratorOutput

        Return GeneratorOutput.Generate(code, SourceLanguage.CSharp)

    End Function

    ''' <summary>
    ''' Runs the generator on Visual Basic source code.
    ''' </summary>
    Public Shared Function VisualBasic(code As String) As GeneratorOutput

        Return GeneratorOutput.Generate(code, SourceLanguage.VisualBasic)

    End Function

    ''' <summary>
    ''' Returns the member lines of a section, sorted with an ordinal comparison.
    ''' An empty array is returned when the section does not exist.
    ''' </summary>
    Public Function Members(sectionName As String) As String()

        If Not Me.Sections.ContainsKey(sectionName) Then
            Return Array.Empty(Of String)()
        End If

        Return Me.Sections(sectionName).OrderBy(Function(line As String) line, StringComparer.Ordinal).ToArray()

    End Function

    ''' <summary>
    ''' Returns every member line of every section.
    ''' </summary>
    Public Function AllMembers() As List(Of String)

        Dim result As New List(Of String)
        For Each sectionName As String In Me.FoundSections
            result.AddRange(Me.Sections(sectionName))
        Next sectionName

        Return result

    End Function

    ''' <summary>
    ''' Returns the specified lines sorted with an ordinal comparison, to be compared with the result of <see cref="Members"/>.
    ''' </summary>
    Public Shared Function Expected(ParamArray lines As String()) As String()

        Return lines.OrderBy(Function(line As String) line, StringComparer.Ordinal).ToArray()

    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Splits the text in sections.
    ''' </summary>
    Private Sub Parse()

        Dim lines As String() = Me.Text.Split(ControlChars.Lf).Select(Function(line As String) line.TrimEnd(ControlChars.Cr)).ToArray()
        Dim currentSection As String = Nothing
        Dim prefix As String = $"{Me.CommentToken} "

        For i As Integer = 0 To lines.Length - 1

            Dim line As String = lines(i)

            If line.Length = 0 OrElse line.StartsWith("#", StringComparison.Ordinal) OrElse line.IndexOf(GeneratorOutput.UnderlineChar) >= 0 Then
                Continue For
            End If

            If i + 1 < lines.Length AndAlso lines(i + 1).IndexOf(GeneratorOutput.UnderlineChar) >= 0 Then
                currentSection = line.Substring(line.LastIndexOf(" "c) + 1)
                Me.FoundSections.Add(currentSection)
                Me.Sections(currentSection) = New List(Of String)
                Continue For
            End If

            If currentSection Is Nothing Then
                Throw New InvalidOperationException($"Unexpected line before the first section title: {line}")
            End If

            Me.Sections(currentSection).Add(If(line.StartsWith(prefix, StringComparison.Ordinal), line.Substring(prefix.Length), line))

        Next i

    End Sub

#End Region

End Class

#End Region
