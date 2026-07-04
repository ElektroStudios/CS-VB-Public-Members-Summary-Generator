#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

#If NETCOREAPP Then
Imports System.Reflection

#Else
Imports System.Collections.Generic
Imports System.Linq
Imports System.Diagnostics

Imports DevCase.Runtime.TypeComparers

#End If

Imports Microsoft.CodeAnalysis

Imports Cs = Microsoft.CodeAnalysis.CSharp
Imports CsSyntax = Microsoft.CodeAnalysis.CSharp.Syntax
Imports CsExtensions = Microsoft.CodeAnalysis.CSharp.CSharpExtensions
Imports CsSyntaxExtensions = Microsoft.CodeAnalysis.CSharp.SyntaxExtensions

Imports Vb = Microsoft.CodeAnalysis.VisualBasic
Imports VbSyntax = Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VbExtensions = Microsoft.CodeAnalysis.VisualBasic.VisualBasicExtensions
Imports VbSyntaxExtensions = Microsoft.CodeAnalysis.VisualBasic.SyntaxExtensions

#End Region

Public Module Program

#Region " Fields "

    ''' <summary>
    ''' The set of VB.NET source-code file name patterns to ignore during processing. 
    ''' <para></para>
    ''' Any file whose name contains any of these patterns (case-insensitive) will be skipped.
    ''' <para></para>
    ''' Use cases for ignoring files that shouldn't be modified include:
    ''' <para></para>
    '''   - Auto-generated designer files (e.g., Form1.Designer.cs, Form1.Designer.vb).
    ''' <para></para>
    '''   - Auto-generated assembly attribute files (i.e., *.AssemblyAttributes.cd, *.AssemblyAttributes.vb).
    ''' <para></para>
    '''   - Assembly metadata files (i.e., AssemblyInfo.cd, AssemblyInfo.vb).
    ''' </summary>
    Private ReadOnly ProhibitedFileNamePatterns As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        ".Designer",
        ".AssemblyAttributes",
        "AssemblyInfo"
    }

    ''' <summary>
    ''' The file extension for supported C# source-code files. Only C# files with this extension will be processed.
    ''' </summary>
    Friend Const CsFileExtension As String = ".cs"

    ''' <summary>
    ''' The file extension for supported VB.NET source-code files. Only VB.NET files with this extension will be processed.
    ''' </summary>
    Friend Const VbFileExtension As String = ".vb"

#If NETCOREAPP Then
    ''' <summary>
    ''' The suffix used for temporary files created during the atomic file replacement process.
    ''' </summary>
    Private ReadOnly GeneratedTempFileSuffix As String = $"{Assembly.GetEntryAssembly().GetName().Name}.tmp"

    ''' <summary>
    ''' The suffix used for backup files created during the atomic file replacement process.
    ''' </summary>
    Private ReadOnly GeneratedBackupFileSuffix As String = $"{Assembly.GetEntryAssembly().GetName().Name}.bak"
#Else
    ''' <summary>
    ''' The suffix used for temporary files created during the atomic file replacement process.
    ''' </summary>
    Private ReadOnly GeneratedTempFileSuffix As String = $"{My.Application.Info.AssemblyName}.tmp"

    ''' <summary>
    ''' The suffix used for backup files created during the atomic file replacement process.
    ''' </summary>
    Private ReadOnly GeneratedBackupFileSuffix As String = $"{My.Application.Info.AssemblyName}.bak"
#End If

    ''' <summary>
    ''' The set of line separator strings used for splitting source code into lines, 
    ''' covering different newline conventions across platforms.
    ''' </summary>
    Private ReadOnly NewLineSeparators As String() = {
        Constants.vbCrLf,
        Constants.vbLf,
        Constants.vbCr
    }

    ''' <summary>
    ''' The regular expression used to identify a '<b>Public Members Summary</b>' #Region directive in a source-code.
    ''' <para></para>
    ''' Typo tolerance is included to allow for common misspellings such as "summry" or "sumary", and 
    ''' the regex is case-insensitive to match variations in capitalization.
    ''' </summary>
    Private ReadOnly RegexPublicMembersSummaryRegion As New Regex("(?i)public\s+members?\s+summ?a?ry?", RegexOptions.Compiled)

    ''' <summary>
    ''' The <see cref="CultureInfo"/> instance representing the "en-US" culture.
    ''' </summary>
    Private ReadOnly CultureInfoEnUs As New CultureInfo("en-US")

    ''' <summary>
    ''' The UTF-8 encoding instance used for console output, configured to not emit a BOM (Byte Order Mark).
    ''' </summary>
    Private ReadOnly ConsoleEncoding As New UTF8Encoding(encoderShouldEmitUTF8Identifier:=False)

#End Region

#Region " Entry Point "

    ''' <summary>
    ''' The main entry point of the application.
    ''' </summary>
    ''' 
    ''' <param name="args">
    ''' The command-line arguments passed to the application.
    ''' <para></para>
    ''' The first argument (args(0)) is expected to be the path to the 
    ''' source directory containing VB files to process.
    ''' </param>
    <DebuggerStepperBoundary>
    Public Sub Main(args As String())

        Thread.CurrentThread.CurrentCulture = Program.CultureInfoEnUs
        Thread.CurrentThread.CurrentUICulture = Program.CultureInfoEnUs

        Console.OutputEncoding = Program.ConsoleEncoding
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White

#If NETCOREAPP Then
        Dim versionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(Environment.ProcessPath)
        Dim version As String = versionInfo.ProductVersion
        Dim assemblyTitle As String = versionInfo.FileDescription
#Else
        Dim version As String = My.Application.Info.Version.ToString(fieldCount:=3)
        Dim assemblyTitle As String = My.Application.Info.Title
#End If

        Dim consoletitle As String = $"{assemblyTitle} {version} ─ by ElektroStudios"
#If DEBUG Then
        Console.Title = consoletitle
#End If
        ConsoleHelper.WriteColoredTextLine(" " & consoletitle, ConsoleColor.Cyan)
        Console.WriteLine("╭───────────────────────────────────────────────────────────────────────────────────╮")
        Console.WriteLine("│ Purpose:                                                                          │")
        Console.WriteLine("│   This application analyzes C# and VB.NET source-code files (*.cs and *.vb) to    │")
        Console.WriteLine("│   generate a specialized summary of public members that is directly inserted on   │")
        Console.WriteLine("│   top of each source file through a new '#region' directive, providing a clear    │")
        Console.WriteLine("│   and updated overview for the publicly accessible members of your API.           │")
        Console.WriteLine("│                                                                                   │")
        Console.WriteLine("│   The summary generator supports the following kind of public members:            │")
        Console.WriteLine("│     - Ctors, properties, fields, events, methods, operators, delegates and enums. │")
        Console.WriteLine("│                                                                                   │")
        Console.WriteLine("│   Source files are read and rewritten preserving their exact UTF encoding,        │")
        Console.WriteLine("│   supporting UTF-8, UTF-16, and UTF-32, with or without a Byte Order Mark (BOM).  │")
        Console.WriteLine("│                                                                                   │")
        Console.WriteLine("│   To prevent data loss, files with unknown encodings are automatically skipped.   │")
        Console.WriteLine("│                                                                                   │")
        Console.WriteLine("│ [!] Disclaimer:                                                                   │")
        Console.WriteLine("│   This application is shared 'as-is', without any warranty; Use at your own risk. │")
        Console.WriteLine("│   Ensure to make a backup of your CS/VB files before using this application.      │")
        Console.WriteLine("╰───────────────────────────────────────────────────────────────────────────────────╯")
        Console.WriteLine()

        If args.Length < 1 Then
            Program.ShowUsage()
            ConsoleHelper.ExitWithMessage("[ ERROR ] Missing required argument(s). See usage above.", exitCode:=1, ConsoleColor.Red)
        End If

        Dim isTestMode As Boolean = args.Contains("-t", StringComparer.OrdinalIgnoreCase) OrElse
                                           args.Contains("--test", StringComparer.OrdinalIgnoreCase)

        Dim isRecursiveSearch As Boolean = args.Contains("-r", StringComparer.OrdinalIgnoreCase) OrElse
                                           args.Contains("--recurse", StringComparer.OrdinalIgnoreCase)

        Dim currentSearchOption As SearchOption = If(isRecursiveSearch, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)


        Dim totalUpdatedFiles As Integer = 0
        Dim totalSkippedFiles As Integer = 0
        Dim totalFailedFiles As Integer = 0

        Dim sourceDirPath As String = args(0)

        ConsoleHelper.WriteColoredTextLine($"Gathering files from specified directory path...", ConsoleColor.Cyan)
        Console.WriteLine()
        Try
            ' Convert to absolute path first and then apply the Long Path prefix if required.
            Dim sourceDirPathExtended As String = Path.GetFullPath(sourceDirPath)
            sourceDirPathExtended = FileSystemHelper.GetExtendedPath(sourceDirPathExtended)

            If Not Directory.Exists(sourceDirPath) Then
                Dim exitMsg As String = $"[ ERROR ] The specified directory path does not exist: {sourceDirPath}"
                ConsoleHelper.ExitWithMessage(exitMsg, exitCode:=3, ConsoleColor.Red)
            End If

#If NETCOREAPP Then
            Dim filePathComparer As StringComparer = StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.NumericOrdering)
#Else
            Dim filePathComparer As New StringNaturalComparer()
#End If

            Dim sourceFiles As New SortedSet(Of String)(
                FileSystemHelper.EnumerateSourceFiles(sourceDirPathExtended, currentSearchOption), filePathComparer)

            Dim totalCsFiles As Integer = 0
            Dim totalVbFiles As Integer = 0

            For Each filePath As String In sourceFiles

                Dim extension As String = Path.GetExtension(filePath)
                If extension.Equals(Program.CsFileExtension, StringComparison.OrdinalIgnoreCase) Then
                    totalCsFiles += 1

                ElseIf extension.Equals(Program.VbFileExtension, StringComparison.OrdinalIgnoreCase) Then
                    totalVbFiles += 1

                End If

#If DEBUG Then
                Thread.CurrentThread.Join(0) ' Prevents ContextSwitchDeadlock on long-running iterations.
#End If
            Next filePath

            Dim totalSourceFileCount As Integer =
                If(sourceFiles Is Nothing, 0, sourceFiles.Count)

            If totalSourceFileCount = 0 Then
                Dim exitMsg As String = $"[ ERROR ] No supported files were found in source directory: {sourceDirPath}"
                ConsoleHelper.ExitWithMessage(exitMsg, exitCode:=4, ConsoleColor.Red)
            End If

            ConsoleHelper.WriteColoredTextLine($"Source Directory Path : {sourceDirPath}", ConsoleColor.DarkCyan)
            ConsoleHelper.WriteColoredTextLine($"Search Option         : {currentSearchOption}", ConsoleColor.DarkCyan)
            ConsoleHelper.WriteColoredTextLine($"Supported Files Found : {totalSourceFileCount:N0} source-code files (*.cs: {totalCsFiles:N0}, *.vb: {totalVbFiles:N0})", ConsoleColor.DarkCyan)
            ConsoleHelper.WriteColoredTextLine($"Test Mode Enabled     : {isTestMode}", ConsoleColor.DarkCyan)
            Console.WriteLine()

#If DEBUG Then
            ConsoleHelper.WriteColoredTextLine("Press 'Y' key to start processing CS/VB files, or 'Escape' key to exit...", ConsoleColor.Yellow)
            ConsoleHelper.WriteColoredTextLine("[!] This message only appears in DEBUG mode to prevent accidental execution.", ConsoleColor.Yellow)
            Do
                Dim keyInfo As ConsoleKeyInfo = Console.ReadKey(intercept:=True)
                If keyInfo.Key = ConsoleKey.Y Then
                    Exit Do
                ElseIf keyInfo.Key = ConsoleKey.Escape Then
                    Environment.Exit(0)
                End If
            Loop
            Console.WriteLine()
#End If

            ' Iterate through each source file, generate the public members summary, and insert it into the file.
            For i As Integer = 0 To totalSourceFileCount - 1

                Dim currentFile As String = sourceFiles(i)
                ConsoleHelper.WriteColoredTextLine($"[{i + 1:N0} of {totalSourceFileCount:N0}] Processing file: {FileSystemHelper.GetNormalPath(currentFile)} ...", ConsoleColor.Cyan)

                Try
                    Dim fileName As String = Path.GetFileName(currentFile)
                    Dim fileExtension As String = Path.GetExtension(fileName)
#If NETCOREAPP Then
                    Dim prohibitedFileNameMatchedPattern As String =
                        Program.ProhibitedFileNamePatterns.FirstOrDefault(
                            Function(pattern As String) fileName.Contains($"{pattern}{fileExtension}", StringComparison.OrdinalIgnoreCase))
#Else
                    Dim prohibitedFileNameMatchedPattern As String =
                        Program.ProhibitedFileNamePatterns.FirstOrDefault(
                            Function(pattern As String) fileName.IndexOf($"{pattern}{fileExtension}", StringComparison.OrdinalIgnoreCase) >= 0)
#End If

                    If prohibitedFileNameMatchedPattern IsNot Nothing Then
                        ConsoleHelper.WriteColoredTextLine($"[SKIPPED] The file name matches a prohibited pattern: ""{prohibitedFileNameMatchedPattern}{fileExtension}""", ConsoleColor.DarkYellow)
                        Console.WriteLine()
                        totalSkippedFiles += 1
                        Continue For
                    End If

                    Dim detectedEncodingKind As UtfEncodingKind = UtfEncodingKind.Unknown
                    If Not EncodingHelper.TryGetUtfFileEncodingKind(currentFile, detectedEncodingKind) OrElse
                           detectedEncodingKind = UtfEncodingKind.UTF7 Then
#If DEBUG Then
                        ConsoleHelper.WriteColoredTextLine($"[ DEBUG ] Detected encoding kind: {detectedEncodingKind}", ConsoleColor.Magenta)
#End If
                        ConsoleHelper.WriteColoredTextLine($"[SKIPPED] Unsupported or unknown file encoding.", ConsoleColor.Yellow)
                        Console.WriteLine()
                        totalSkippedFiles += 1
                        Continue For
                    End If

                    Dim detectedEncoding As Encoding = EncodingHelper.GetEncodingFromUtfEncodingKind(detectedEncodingKind)
#If DEBUG Then
                    Dim hasBOM As Boolean = detectedEncoding.GetPreamble().Length > 0
                    ConsoleHelper.WriteColoredTextLine($"[ DEBUG ] Detected encoding: {detectedEncoding.BodyName}{If(hasBOM, " (with BOM)", "")}", ConsoleColor.Magenta)
#End If

                    Dim sourceCode As String = File.ReadAllText(currentFile, detectedEncoding)

                    If String.IsNullOrWhiteSpace(sourceCode) Then
                        ConsoleHelper.WriteColoredTextLine($"[SKIPPED] The file is empty or contains only white-spaces.", ConsoleColor.DarkGray)
                        Console.WriteLine()
                        totalSkippedFiles += 1
                        Continue For
                    End If

                    Dim regionFound As Boolean = False
                    Dim isMalformedRegion As Boolean = False
                    Dim spanStart As Integer = -1
                    Dim spanEnd As Integer = -1
                    Dim newRegionStart As Integer = -1

                    Dim resultSummary As String = Nothing
                    Dim publicMembersCount As Integer = 0

                    Select Case fileExtension.ToLower()

                        Case Program.CsFileExtension.ToLower()
                            ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Generating summary for C# file...", ConsoleColor.Gray)

                            resultSummary = Generator.GenerateSummaryRegion(sourceCode, SourceLanguage.CSharp, publicMembersCount)?.TrimEnd()

                            ' C# AST Parsing
                            ' --------------
                            Dim syntaxTree As SyntaxTree = Cs.CSharpSyntaxTree.ParseText(sourceCode)
                            Dim root As SyntaxNode = syntaxTree.GetRoot()

                            Dim regionDirectives As IEnumerable(Of CsSyntax.RegionDirectiveTriviaSyntax) =
                                root.DescendantTrivia().
                                     Where(Function(t As SyntaxTrivia) t.IsKind(Cs.SyntaxKind.RegionDirectiveTrivia)).
                                     Select(Function(t As SyntaxTrivia) CType(t.GetStructure(), CsSyntax.RegionDirectiveTriviaSyntax))

                            Dim summaryRegionCount As Integer = 0
                            For Each region As CsSyntax.RegionDirectiveTriviaSyntax In regionDirectives
                                Dim fullRegionText As String = region.ToString()

                                If Program.RegexPublicMembersSummaryRegion.IsMatch(fullRegionText) Then
                                    If summaryRegionCount >= 1 Then
                                        Dim errMsg As String =
                                            $"[ ERROR ] Multiple 'Public Members Summary' regions detected in file." & Environment.NewLine &
                                             "          This may indicate a malformed file structure. " & Environment.NewLine &
                                             "          Operation aborted, no changes have been made to the file."
                                        ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                                        Console.WriteLine()
                                        totalFailedFiles += 1
                                        Exit For
                                    End If
                                    Dim related As IReadOnlyList(Of CsSyntax.DirectiveTriviaSyntax) = region.GetRelatedDirectives()
                                    Dim relatedCount As Integer = related.Count

                                    If (relatedCount > 0) AndAlso related(relatedCount - 1).IsKind(Cs.SyntaxKind.EndRegionDirectiveTrivia) Then
                                        spanStart = region.ParentTrivia.SpanStart
                                        spanEnd = related(relatedCount - 1).ParentTrivia.Span.End
                                        regionFound = True
                                    Else
                                        isMalformedRegion = True
                                    End If
                                    summaryRegionCount += 1
                                End If
                            Next region

                            If summaryRegionCount > 1 Then
                                Continue For
                            End If

                            If Not regionFound AndAlso Not isMalformedRegion Then
                                Dim firstToken As SyntaxToken = root.GetFirstToken()
                                Dim positionFound As Boolean = False

                                If firstToken.HasLeadingTrivia Then
                                    For Each trivia As SyntaxTrivia In firstToken.LeadingTrivia
                                        If trivia.IsKind(Cs.SyntaxKind.SingleLineCommentTrivia) OrElse
                                           trivia.IsKind(Cs.SyntaxKind.MultiLineCommentTrivia) OrElse
                                           trivia.IsKind(Cs.SyntaxKind.WhitespaceTrivia) OrElse
                                           trivia.IsKind(Cs.SyntaxKind.EndOfLineTrivia) Then
                                            Continue For
                                        Else
                                            newRegionStart = trivia.FullSpan.Start
                                            positionFound = True
                                            Exit For
                                        End If
                                    Next
                                End If

                                If Not positionFound Then
                                    newRegionStart = firstToken.SpanStart
                                End If
                            End If
                            ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Generated summary contains {publicMembersCount:N0} public members.", ConsoleColor.Gray)

                        Case Program.VbFileExtension.ToLower()
                            ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Generating summary for VB.NET file...", ConsoleColor.Gray)

                            resultSummary = Generator.GenerateSummaryRegion(sourceCode, SourceLanguage.VisualBasic, publicMembersCount)?.TrimEnd()

                            ' VB.NET AST Parsing
                            ' ------------------
                            Dim syntaxTree As SyntaxTree = Vb.VisualBasicSyntaxTree.ParseText(sourceCode)
                            Dim root As SyntaxNode = syntaxTree.GetRoot()

                            Dim regionDirectives As IEnumerable(Of VbSyntax.RegionDirectiveTriviaSyntax) =
                                root.DescendantTrivia().
                                     Where(Function(t As SyntaxTrivia) t.IsKind(Vb.SyntaxKind.RegionDirectiveTrivia)).
                                     Select(Function(t As SyntaxTrivia) CType(t.GetStructure(), VbSyntax.RegionDirectiveTriviaSyntax))

                            Dim summaryRegionCount As Integer = 0
                            For Each region As VbSyntax.RegionDirectiveTriviaSyntax In regionDirectives
                                Dim fullRegionText As String = region.ToString()

                                If Program.RegexPublicMembersSummaryRegion.IsMatch(fullRegionText) Then
                                    Dim related As IReadOnlyList(Of VbSyntax.DirectiveTriviaSyntax) = region.GetRelatedDirectives()
                                    Dim relatedCount As Integer = related.Count

                                    If summaryRegionCount >= 1 Then
                                        Dim errMsg As String =
                                            $"[ ERROR ] Multiple 'Public Members Summary' regions detected in file." & Environment.NewLine &
                                             "          This may indicate a malformed file structure. " & Environment.NewLine &
                                             "          Operation aborted, no changes have been made to the file."
                                        ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                                        Console.WriteLine()
                                        totalFailedFiles += 1
                                        Exit For
                                    End If

                                    If (relatedCount > 0) AndAlso related(relatedCount - 1).IsKind(Vb.SyntaxKind.EndRegionDirectiveTrivia) Then
                                        spanStart = region.ParentTrivia.SpanStart
                                        spanEnd = related(relatedCount - 1).ParentTrivia.Span.End
                                        regionFound = True
                                    Else
                                        isMalformedRegion = True
                                    End If
                                    summaryRegionCount += 1
                                End If
                            Next region

                            If summaryRegionCount > 1 Then
                                Continue For
                            End If

                            If Not regionFound AndAlso Not isMalformedRegion Then
                                Dim firstToken As SyntaxToken = root.GetFirstToken()
                                Dim positionFound As Boolean = False

                                If firstToken.HasLeadingTrivia Then
                                    For Each trivia As SyntaxTrivia In firstToken.LeadingTrivia
                                        If trivia.IsKind(Vb.SyntaxKind.CommentTrivia) OrElse
                                           trivia.IsKind(Vb.SyntaxKind.WhitespaceTrivia) OrElse
                                           trivia.IsKind(Vb.SyntaxKind.EndOfLineTrivia) Then
                                            Continue For
                                        Else
                                            newRegionStart = trivia.FullSpan.Start
                                            positionFound = True
                                            Exit For
                                        End If
                                    Next
                                End If

                                If Not positionFound Then
                                    newRegionStart = firstToken.SpanStart
                                End If
                            End If
                            ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Generated summary contains {publicMembersCount:N0} public members.", ConsoleColor.Gray)

                        Case Else
                            ConsoleHelper.WriteColoredTextLine($"[SKIPPED] Unexpected file extension: {fileExtension.ToLower()}", ConsoleColor.DarkGray)
                            Console.WriteLine()
                            totalSkippedFiles += 1
                            Continue For

                    End Select

                    Dim modifiedCode As String = String.Empty
                    Dim originalStripped As String = String.Empty
                    Dim modifiedStripped As String = String.Empty
                    Dim areRegionsIdentical As Boolean = False

                    If regionFound Then
                        ' Scenario A: Public Members Summary region found in file. Determine exact boundaries and replace.

                        ConsoleHelper.WriteColoredTextLine(
                            $"[ INFO  ] Summary region was found in file. Reasoning replacement...", ConsoleColor.Gray)

                        If (publicMembersCount = 0) OrElse String.IsNullOrEmpty(resultSummary) Then
                            Dim msg As String =
                                "[WARN] The new generated summary region is empty, which means no valid public members could be identified in the file. " & Environment.NewLine &
                                "       The existing summary region is considered invalid / obsolete and will be removed from file."
                            ConsoleHelper.WriteColoredTextLine(msg, ConsoleColor.Yellow)
                        End If

                        Dim length As Integer = spanEnd - spanStart
                        originalStripped = sourceCode.Remove(spanStart, length)
                        modifiedCode = originalStripped.Insert(spanStart, resultSummary)

                        newRegionStart = spanStart
                        modifiedStripped = modifiedCode.Remove(newRegionStart, resultSummary.Length)

                        ' Validate whether the existing region content is identical to the generated one,
                        ' ignoring order and whitespace, to potentially skip unnecessary file writes.
                        Dim existingRegionText As String = sourceCode.Substring(spanStart, length)
                        Dim existingRegionLines As String() = existingRegionText.Split(Program.NewLineSeparators, StringSplitOptions.None)
                        Dim generatedRegionLines As String() = resultSummary.Split(Program.NewLineSeparators, StringSplitOptions.None)

                        If (existingRegionLines.Length > 1) AndAlso (generatedRegionLines.Length > 1) Then
                            Dim existingSet As New HashSet(Of String)(StringComparer.Ordinal)
                            For lineIdx As Integer = 1 To existingRegionLines.Length - 1
                                existingSet.Add(existingRegionLines(lineIdx).Trim())
                            Next

                            Dim generatedSet As New HashSet(Of String)(StringComparer.Ordinal)
                            For lineIdx As Integer = 1 To generatedRegionLines.Length - 1
                                generatedSet.Add(generatedRegionLines(lineIdx).Trim())
                            Next

                            If existingSet.SetEquals(generatedSet) Then
                                areRegionsIdentical = True
                            End If
                        End If

                        If areRegionsIdentical Then
                            ConsoleHelper.WriteColoredTextLine(
                                $"[SKIPPED] The existing summary region in file is up to date (identical members and signatures than the new generated one).", ConsoleColor.DarkGray)
                            Console.WriteLine()
                            totalSkippedFiles += 1
                            Continue For
                        End If

                    ElseIf isMalformedRegion Then

                        Dim errMsg As String = String.Empty
                        Select Case fileExtension.ToLower()

                            Case Program.CsFileExtension.ToLower()
                                errMsg = "[ ERROR ] A matching starting '#region' directive was found, but its closing '#endregion' is missing. " &
                                         "The file structure may be fundamentally malformed. " & Environment.NewLine &
                                         "          Operation aborted, no changes have been made to the file."

                            Case Program.VbFileExtension.ToLower()
                                errMsg = "[ ERROR ] A matching starting '#Region' directive was found, but its closing '#End Region' is missing. " &
                                         "The file structure may be fundamentally malformed. " & Environment.NewLine &
                                         "          Operation aborted, no changes have been made to the file."
                        End Select

                        ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                        Console.WriteLine()
                        totalFailedFiles += 1
                        Continue For

                    Else
                        ' Scenario B: Public Members Summary region not found in file. Smart Insertion on top of the file.

                        ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Summary region was not found in file. Reasoning insertion...", ConsoleColor.White)

                        If String.IsNullOrWhiteSpace(resultSummary) Then
                            ConsoleHelper.WriteColoredTextLine($"[SKIPPED] No public members were detected to generate a new summary. There is nothing to update.", ConsoleColor.DarkGray)
                            Console.WriteLine()
                            totalSkippedFiles += 1
                            Continue For
                        End If

                        originalStripped = sourceCode

                        ' Create the insertion string with exactly one NewLine at the end for perfect aesthetic spacing
                        Dim insertionString As String = $"{resultSummary}{Environment.NewLine}{Environment.NewLine}"

                        modifiedCode = sourceCode.Insert(newRegionStart, insertionString)

                        ' Remove exactly what was inserted to maintain HashSet validation integrity
                        modifiedStripped = modifiedCode.Remove(newRegionStart, insertionString.Length)

                    End If

                    If Not String.IsNullOrEmpty(resultSummary) Then
                        ConsoleHelper.WriteColoredTextLine(
                            $"[ INFO  ] Summary region virtually inserted into source-code. Validating changes...", ConsoleColor.Gray)
                    Else
                        ConsoleHelper.WriteColoredTextLine(
                            $"[ INFO  ] Summary region virtually removed from source-code. Validating changes...", ConsoleColor.Gray)
                    End If

                    ' Paranoid validation to ensure that only the target region was modified.

                    Dim originalLines As String() = originalStripped.Split(Program.NewLineSeparators, StringSplitOptions.None)
                    Dim modifiedLines As String() = modifiedStripped.Split(Program.NewLineSeparators, StringSplitOptions.None)

                    Dim originalLinesLength As Integer = If(originalLines?.Length, 0)
                    Dim modifiedLinesLength As Integer = If(modifiedLines?.Length, 0)

                    ' Content hash check to ensure that all lines outside the target region are identical, regardless of order.
                    Dim hashOriginal As New HashSet(Of String)(StringComparer.Ordinal)
                    For lineIndex As Integer = 0 To originalLines.Length - 1
                        hashOriginal.Add($"{lineIndex}::{originalLines(lineIndex)}")
                    Next
                    Dim hashModified As New HashSet(Of String)(StringComparer.Ordinal)
                    For lineIndex As Integer = 0 To modifiedLines.Length - 1
                        hashModified.Add($"{lineIndex}::{modifiedLines(lineIndex)}")
                    Next

                    If (originalLinesLength = 0 OrElse modifiedLinesLength = 0) OrElse
                       (originalLinesLength <> modifiedLinesLength) Then
                        Dim msg As String =
                                "[ ERROR ] Validation not passed: Line count mismatch detected outside the target region: " & Environment.NewLine &
                               $"          Original [{originalLinesLength:N0} lines] vs Modified [{modifiedLinesLength:N0} lines]. " & Environment.NewLine &
                                "          Operation aborted, no changes have been made to the file."

                        ConsoleHelper.WriteColoredTextLine(msg, ConsoleColor.Red)
                        Console.WriteLine()
                        totalFailedFiles += 1
                        Continue For
                    End If

                    If Not hashOriginal.SetEquals(hashModified) Then
                        Dim msg As String =
                                "[ ERROR ] Validation not passed: Content hash mismatch detected outside the target region. " & Environment.NewLine &
                                "          Operation aborted, no changes have been made to the file."

                        ConsoleHelper.WriteColoredTextLine(msg, ConsoleColor.Red)
                        Console.WriteLine()
                        totalFailedFiles += 1
                        Continue For
                    End If
                    ConsoleHelper.WriteColoredTextLine(
                            "[ INFO  ] Validation passed: " &
                           $"Both original and modified code have {hashOriginal.Count:N0} lines outside the target region " &
                            "with identical byte-for-byte content.",
                            ConsoleColor.Gray)

                    ' Create the temporary file in the SAME directory than the current file is, 
                    ' and use File.Replace() to ensure an atomic replacement operation.
                    Dim destDirectory As String = If(isTestMode,
                                                     Path.GetTempPath(),
                                                     Path.GetDirectoryName(currentFile))

                    Dim destFilename As String = Path.GetFileName(currentFile)

                    If Not currentFile.EndsWith(destFilename, StringComparison.Ordinal) Then
                        Dim errMsg As String =
                            $"[ ERROR ] Failed to extract the destination filename from the current file path." & Environment.NewLine &
                            $"          Original filepath : ""{currentFile}""" & Environment.NewLine &
                            $"          Returned filename : ""{destFilename}""" & Environment.NewLine &
                             "          Operation aborted, no changes have been made to the file."

                        ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                        Console.WriteLine()
                        totalFailedFiles += 1
                        Continue For
                    End If

                    Dim tempFileName As String = $"{destFilename}.{Program.GeneratedTempFileSuffix}"
                    Dim bakFileName As String = $"{destFilename}.{Program.GeneratedBackupFileSuffix}"
                    Dim tempFilePath As String = FileSystemHelper.GetExtendedPath(Path.Combine(destDirectory, tempFileName))
                    Dim bakFilePath As String = FileSystemHelper.GetExtendedPath(Path.Combine(destDirectory, bakFileName))

#If DEBUG Then
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Is Test Mode   : " & isTestMode, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Current File   : " & currentFile, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Dest Directory : " & destDirectory, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Dest Filename  : " & destFilename, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Temp Filename  : " & tempFileName, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Bak  Filename  : " & bakFileName, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Temp Filepath  : " & tempFilePath, ConsoleColor.Magenta)
                    ConsoleHelper.WriteColoredTextLine("[ DEBUG ] Bak  Filepath  : " & bakFilePath, ConsoleColor.Magenta)
#End If

                    ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Writing changes to temporary file...", ConsoleColor.Gray)
                    File.WriteAllText(tempFilePath, modifiedCode, detectedEncoding)
                    Try
                        ConsoleHelper.WriteColoredTextLine($"[ INFO  ] Replacing original file with temporary one...", ConsoleColor.Gray)
                        If Not isTestMode Then
                            File.Replace(tempFilePath, currentFile, bakFilePath)
                        End If

                    Catch ex As Exception
                        Dim errMsg As String =
                            $"[ ERROR ] An error occurred while replacing the file. Error code: 0x{ex.HResult:X8}. Error message: " & Environment.NewLine &
                            $"          {ex.Message}>" & Environment.NewLine &
                             "          Please, ensure that the file not get corrupted." & Environment.NewLine &
                             "          A backup of the unmodified original file may be available at: " & bakFilePath

                        ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                        Console.WriteLine()
                        totalFailedFiles += 1
                        Continue For
                    End Try

                    If File.Exists(bakFilePath) Then
                        File.Delete(bakFilePath)
                    End If

                    If isTestMode Then
                        If File.Exists(tempFilePath) Then
                            File.Delete(tempFilePath)
                        End If
                    End If

                    ' Calculate the line number (1-based index).
                    Dim textBeforeInsertion As String = sourceCode.Substring(0, newRegionStart)
                    Dim lineNumber As Integer = textBeforeInsertion.Split(Program.NewLineSeparators, StringSplitOptions.None).Length
                    If Not String.IsNullOrEmpty(resultSummary) Then
                        ConsoleHelper.WriteColoredTextLine(
                            $"[SUCCESS] Summary was successfully inserted into file at line {lineNumber:N0}.{If(isTestMode, " (Test Mode)", "")}", ConsoleColor.Green)
                    Else
                        ConsoleHelper.WriteColoredTextLine(
                            $"[SUCCESS] Summary region was successfully removed from file at line {lineNumber:N0}.{If(isTestMode, " (Test Mode)", "")}", ConsoleColor.Green)
                    End If

                    totalUpdatedFiles += 1
                    Console.WriteLine()

                Catch ex As Exception
                    Dim errMsg As String =
                        $"[ ERROR ] An error occurred while processing the file. Error code: 0x{ex.HResult:X8}. Error message: " & Environment.NewLine &
                        $"          {ex.Message}>" & Environment.NewLine &
                         "          Operation aborted, no changes have been made to the file."

                    ConsoleHelper.WriteColoredTextLine(errMsg, ConsoleColor.Red)
                    Console.WriteLine()
                    totalFailedFiles += 1
                    Continue For

                Finally
#If DEBUG Then
                    Thread.CurrentThread.Join(0) ' Prevents ContextSwitchDeadlock on long-running iterations.
#End If
                End Try

            Next i

        Catch ex As Exception
            Console.WriteLine()
            ConsoleHelper.ExitWithMessage($"FATAL ERROR 0x{ex.HResult:X8}: {ex.Message}", exitCode:=ex.HResult, ConsoleColor.Red)

        End Try

        Dim exitCode As Integer = If(totalFailedFiles = 0, 0, 1)
        Dim exitColor As ConsoleColor = If(totalFailedFiles = 0, ConsoleColor.Green, ConsoleColor.Red)
        ConsoleHelper.ExitWithMessage($"All files have been processed. Modified: {totalUpdatedFiles:N0}; Skipped: {totalSkippedFiles:N0}; Failed: {totalFailedFiles:N0}.{If(isTestMode, " >>> TEST MODE IS ENABLED (NO FILES HAVE BEEN MODIFIED)", "")}", exitCode, exitColor)
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Prints command-line usage information to the console. 
    ''' <para></para>
    ''' Called whenever a mandatory or optional argument is missing or invalid.
    ''' </summary>
    <DebuggerStepThrough>
    Private Sub ShowUsage()

        Dim executableName As String = $"{Process.GetCurrentProcess().ProcessName}.exe"

        ConsoleHelper.WriteColoredTextLine("Usage:", ConsoleColor.DarkCyan)
        Console.WriteLine($"  {executableName} <directory_path> [-r|--recursive]")
        Console.WriteLine()
        ConsoleHelper.WriteColoredTextLine("Arguments:", ConsoleColor.DarkCyan)
        Console.WriteLine("  directory_path      Path to the root directory containing *.cs / *.vb files.")
        Console.WriteLine("  -r, --recursive     Optional. Processes all subdirectories recursively.")
        Console.WriteLine("  -t, --test          Optional. Runs the application in test mode without modifying any files.")
        Console.WriteLine()
        ConsoleHelper.WriteColoredTextLine("Examples:", ConsoleColor.DarkCyan)
        Console.WriteLine($"  {executableName} ""C:\MySolution""")
        Console.WriteLine($"  {executableName} ""C:\MySolution"" -r")
        Console.WriteLine($"  {executableName} ""C:\MySolution"" -r --test")
        Console.WriteLine()
    End Sub

#End Region

End Module
