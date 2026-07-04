' =============================================================================================
' PUBLIC MEMBERS SUMMARY GENERATOR API — VISIBILITY ORDERING CRITERIA & RATIONALE
'
' I.e., What I would like to see first in a summary on top of a file when read through it.
' =============================================================================================
'
' 1st: 🏗️ Constructors
' ---------------------------------------------------------------------------------------------
' Rationale: Mandatory entry point to instantiate the object. They dictate how the lifecycle
'            begins, making them the most relevant members to be listed first.
'
' 2nd: 🔧 Properties
' ---------------------------------------------------------------------------------------------
' Rationale: Represent the public state of the object. Developers typically read or modify 
'            properties to configure the object's behavior immediately after instantiation.
'
' 3rd: 🏷️ Fields
' ---------------------------------------------------------------------------------------------
' Rationale: Like properties, they represent state used for post-instantiation configuration,
'            but they are listed underneath due to having lower architectural visibility.
'
' 4th: ⚡ Events
' ---------------------------------------------------------------------------------------------
' Rationale: Establish the communication contract. Subscribing to notifications usually happens
'            before invoking methods to ensure no signals are missed during interactions.
'
' 5th: 🧠 Methods
' ---------------------------------------------------------------------------------------------
' Rationale: Represent the core actions and operations to perform. These are invoked once 
'            the object's state has been properly configured and event listeners are attached.
'
' 6th: 📐 Operators
' ---------------------------------------------------------------------------------------------
' Rationale: Define specialized behavior for expressions. Evaluated during post-instantiation
'            interactions but are less frequent than standard method calls.
'
' 7th: 📜 Delegates
' ---------------------------------------------------------------------------------------------
' Rationale: Structural type definitions for callbacks and functional signatures rather than
'            executable instance members. They act as supportive types for handlers used above.
'
' 8th: 🔢 Enumerations
' ---------------------------------------------------------------------------------------------
' Rationale: Nested type constants. They are placed the last because they primarily serve as
'            supportive types for member parameters or method return types.
'
' =============================================================================================

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

#If Not NETCOREAPP Then
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
#End If

Imports System.ComponentModel
Imports System.Globalization
Imports System.Text
Imports System.Reflection

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

''' <summary>
''' Provides functionality to parse # and VB.NET source code and generate a summary of all public members.
''' <para></para>
''' Utilizes Roslyn AST analysis to reflect on code structure.
''' </summary>
Friend Module Generator

#Region " Fields "

    ''' <summary>
    ''' The set of member names that are not allowed to be included in the API surface summary output, 
    ''' typically because they are inherited from System.Object and do not provide meaningful information.
    ''' </summary>
    Private ReadOnly ProhibitedMemberNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        "GetType",
        "[GetType]"
    }

    ''' <summary>
    ''' The name of the base object type in C#,
    ''' used for filtering out members inherited from <see cref="Object"/>.
    ''' </summary>
    Friend Const ObjectTypeNameCS As String = "object"

    ''' <summary>
    ''' The default underlying base type for enumerations in C#, 
    ''' used when no explicit base type is specified in the source code.
    ''' </summary>
    Friend Const EnumDefaultBaseTypeCS As String = "int"

    ''' <summary>
    ''' The name of the base object type in VB.NET, 
    ''' used for filtering out members inherited from <see cref="Object"/>.
    ''' </summary>
    Friend Const ObjectTypeNameVB As String = "Object"

    ''' <summary>
    ''' The default underlying base type for enumerations in VB.NET, 
    ''' used when no explicit base type is specified in the source code.
    ''' </summary>
    Friend Const EnumDefaultBaseTypeVB As String = "Integer"

    ''' <summary>
    ''' The culture info used for consistent date formatting in the generated region header.
    ''' </summary>
    Private ReadOnly SummaryCulture As New CultureInfo("en-US")

    ''' <summary>
    ''' The name of the assembly containing this code, used for attribution in the generated region header.
    ''' </summary>
    Private ReadOnly AttributionName As String = Assembly.GetExecutingAssembly().GetName().Name

    ''' <summary>
    ''' The set of special characters to trim from VB.NET member names, 
    ''' used to clean up names for display in the summary region.
    ''' </summary>
    Private ReadOnly VbNameTrimSpecialChars As Char() = {
        "["c, "]"c
    }

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Parses the provided C# or VB.NET source code to generate and return a  
    ''' formatted '#region' directive string containing a summary of the 
    ''' publicly accessible members grouped by kind.
    ''' </summary>
    ''' 
    ''' <param name="sourceCode">
    ''' The raw C# or VB.NET source code string to analyze.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language of the source code being analyzed.
    ''' </param>
    ''' 
    ''' <param name="refPublicMembersCount">
    ''' When this method returns, contains the total count of publicly accessible members detected in the source-code.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A formatted '#region' directive string containing the summarized public members, 
    ''' or an empty string if no accessible public members are found.
    ''' </returns>
    ''' 
    ''' <exception cref="InvalidOperationException">
    ''' Thrown when the source code contains syntax errors or unsupported constructs.
    ''' </exception>
    <DebuggerStepperBoundary>
    Public Function GenerateSummaryRegion(sourceCode As String, language As SourceLanguage,
                                    ByRef refPublicMembersCount As Integer) As String

        If String.IsNullOrWhiteSpace(sourceCode) Then
            Return String.Empty
        End If

        Dim ctors, props, fields, events, methods, operators, delegates, enums As New SortedSet(Of String)
        Dim publicMembersCount As Integer = 0

        Select Case language
            Case SourceLanguage.CSharp
                Generator.ParseCSharpCode(sourceCode, ctors, props, fields, events, methods, operators, delegates, enums)

            Case SourceLanguage.VisualBasic
                Generator.ParseVisualBasicCode(sourceCode, ctors, props, fields, events, methods, operators, delegates, enums)

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))
        End Select

        ' If all collections are empty, return an empty string to avoid generating an empty region.
        If ctors.Count = 0 AndAlso
           props.Count = 0 AndAlso
           fields.Count = 0 AndAlso
           events.Count = 0 AndAlso
           methods.Count = 0 AndAlso
           operators.Count = 0 AndAlso
           delegates.Count = 0 AndAlso
           enums.Count = 0 Then

            Return String.Empty
        End If

        Dim generationTime As String =
            Date.Now.ToString("MMMM d, yyyy @ HH:mm:ss", Generator.SummaryCulture)

        Dim regionStart As String
        Dim regionEnd As String
        Dim commentToken As String

        Select Case language
            Case SourceLanguage.CSharp
                regionStart = $"#region Public Members Summary (auto-generated with {Generator.AttributionName} on {generationTime})"
                regionEnd = "#endregion"
                commentToken = "//"

            Case SourceLanguage.VisualBasic
                regionStart = $"#Region "" Public Members Summary (auto-generated with {Generator.AttributionName} on {generationTime}) """
                regionEnd = "#End Region"
                commentToken = "'"

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))
        End Select

        Dim sb As New StringBuilder()

        sb.AppendLine(regionStart)
        sb.AppendLine()

        If ctors.Count > 0 Then
            sb.AppendLine($"{commentToken} 🏗️ Constructors")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each ctor As String In ctors
                sb.AppendLine(ctor)
                publicMembersCount += 1
            Next
            sb.AppendLine()
        End If

        If props.Count > 0 Then
            sb.AppendLine($"{commentToken} 🔧 Properties")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each prop As String In props
                sb.AppendLine(prop)
                publicMembersCount += 1
            Next
            sb.AppendLine()
        End If

        If fields.Count > 0 Then
            sb.AppendLine($"{commentToken} 🏷️ Fields")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each field As String In fields
                sb.AppendLine(field)
                publicMembersCount += 1
            Next
            sb.AppendLine()
        End If

        If events.Count > 0 Then
            sb.AppendLine($"{commentToken} ⚡ Events")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each [event] As String In events
                sb.AppendLine([event])
                publicMembersCount += 1
            Next [event]
            sb.AppendLine()
        End If

        If methods.Count > 0 Then
            sb.AppendLine($"{commentToken} 🧠 Methods")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each method As String In methods
                sb.AppendLine(method)
                publicMembersCount += 1
            Next method
            sb.AppendLine()
        End If

        If operators.Count > 0 Then
            sb.AppendLine($"{commentToken} 📐 Operators")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each [operator] As String In operators
                sb.AppendLine([operator])
                publicMembersCount += 1
            Next [operator]
            sb.AppendLine()
        End If

        If delegates.Count > 0 Then
            sb.AppendLine($"{commentToken} 📜 Delegates")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each [delegate] As String In delegates
                sb.AppendLine([delegate])
                publicMembersCount += 1
            Next [delegate]
            sb.AppendLine()
        End If

        If enums.Count > 0 Then
            sb.AppendLine($"{commentToken} 🔢 Enumerations")
            sb.AppendLine($"{commentToken} ▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔▔")
            For Each [enum] As String In enums
                sb.AppendLine([enum])
                publicMembersCount += 1
            Next [enum]
            sb.AppendLine()
        End If

        sb.AppendLine(regionEnd)

        refPublicMembersCount = publicMembersCount
        Return sb.ToString()
    End Function

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Parses the provided C# source code to extract publicly accessible members and populate the corresponding lists.
    ''' </summary>
    ''' 
    ''' <param name="sourceCode">
    ''' The raw C# source code string to analyze.
    ''' </param>
    ''' 
    ''' <param name="refCtors">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' constructor entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible constructors are found.
    ''' </param>
    ''' 
    ''' <param name="refProps">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' property entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible properties are found.
    ''' </param>
    ''' 
    ''' <param name="refFields">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' field entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible fields are found.
    ''' </param>
    ''' 
    ''' <param name="refEvents">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' event entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible events are found.
    ''' </param>
    ''' 
    ''' <param name="refMethods">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' method entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible methods are found.
    ''' </param>
    ''' 
    ''' <param name="refOperators">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' operator entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible operators are found.
    ''' </param>
    ''' 
    ''' <param name="refDelegates">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' delegate entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible delegates are found.
    ''' </param>
    ''' 
    ''' <param name="refEnums">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' enumeration entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible enumerations are found.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ParseCSharpCode(sourceCode As String,
                          ByRef refCtors As SortedSet(Of String),
                          ByRef refProps As SortedSet(Of String),
                          ByRef refFields As SortedSet(Of String),
                          ByRef refEvents As SortedSet(Of String),
                          ByRef refMethods As SortedSet(Of String),
                          ByRef refOperators As SortedSet(Of String),
                          ByRef refDelegates As SortedSet(Of String),
                          ByRef refEnums As SortedSet(Of String))

        If refCtors Is Nothing Then
            refCtors = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refProps Is Nothing Then
            refProps = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refFields Is Nothing Then
            refFields = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refEvents Is Nothing Then
            refEvents = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refMethods Is Nothing Then
            refMethods = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refOperators Is Nothing Then
            refOperators = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refDelegates Is Nothing Then
            refDelegates = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refEnums Is Nothing Then
            refEnums = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        Dim syntaxTree As SyntaxTree = Cs.CSharpSyntaxTree.ParseText(sourceCode)
        Dim diagnostics As IEnumerable(Of Diagnostic) = syntaxTree.GetDiagnostics()
        Dim diagnostic As Diagnostic

        For Each diagnostic In diagnostics
            If diagnostic.Severity = DiagnosticSeverity.Error Then
                Throw New InvalidOperationException($"Syntax compilation error discovered: {diagnostic.GetMessage()}")
            End If
        Next diagnostic

        Dim language As SourceLanguage = SourceLanguage.CSharp
        Dim root As SyntaxNode = syntaxTree.GetRoot()

        ' 1. Process Constructors (🏗️)
        Dim ctorStatements As IEnumerable(Of CsSyntax.ConstructorDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.ConstructorDeclarationSyntax)()

        For Each ctorStatement As CsSyntax.ConstructorDeclarationSyntax In ctorStatements

            If Generator.IsPubliclyAccessible(ctorStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(ctorStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(ctorStatement, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(ctorStatement, language)
                Dim params As String = Generator.GetParameterTypes(ctorStatement.ParameterList, language)

                refCtors.Add($"// {containingTypeName}({params})")
            End If
        Next ctorStatement

        ' 2. Process Properties (🔧)
        Dim propertyStatements As IEnumerable(Of CsSyntax.PropertyDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.PropertyDeclarationSyntax)()

        For Each propStatement As CsSyntax.PropertyDeclarationSyntax In propertyStatements

            ' CRITICAL: Check if the property belongs to an interface (intrinsically public in C#).
            Dim isInterfaceMember As Boolean =
                (propStatement.Parent IsNot Nothing AndAlso propStatement.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(propStatement.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(propStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(propStatement, language) Then

                Dim propType As String =
                    If(propStatement.Type IsNot Nothing,
                       propStatement.Type.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim containingTypeName As String = Generator.GetContainingTypeName(propStatement, language)

                refProps.Add($"// {propType} {containingTypeName}.{propStatement.Identifier.Text}")
            End If
        Next propStatement

        ' 3. Process Fields (🏷️)
        Dim fieldStatements As IEnumerable(Of CsSyntax.FieldDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.FieldDeclarationSyntax)()

        For Each fieldDeclaration As CsSyntax.FieldDeclarationSyntax In fieldStatements

            ' Interfaces cannot contain instance fields in C#. We strictly rely on the public modifier.
            If Generator.IsPubliclyAccessible(fieldDeclaration.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(fieldDeclaration, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(fieldDeclaration, language) Then

                Dim fieldType As String =
                    If(fieldDeclaration.Declaration.Type IsNot Nothing,
                       fieldDeclaration.Declaration.Type.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim containingTypeName As String = Generator.GetContainingTypeName(fieldDeclaration, language)

                For Each declarator As CsSyntax.VariableDeclaratorSyntax In fieldDeclaration.Declaration.Variables
                    Dim identifierName As String = declarator.Identifier.Text
                    refFields.Add($"// {fieldType} {containingTypeName}.{identifierName}")
                Next declarator

            End If
        Next fieldDeclaration

        ' 4. Process Events (⚡)
        ' Process standard events (event EventHandler MyEvent;)
        Dim eventFieldStatements As IEnumerable(Of CsSyntax.EventFieldDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.EventFieldDeclarationSyntax)()

        For Each eventField As CsSyntax.EventFieldDeclarationSyntax In eventFieldStatements

            Dim isInterfaceMember As Boolean =
                (eventField.Parent IsNot Nothing AndAlso eventField.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(eventField.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(eventField, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(eventField, language) Then

                Dim eventType As String =
                    If(eventField.Declaration.Type IsNot Nothing,
                       eventField.Declaration.Type.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim containingTypeName As String = Generator.GetContainingTypeName(eventField, language)

                For Each declarator As CsSyntax.VariableDeclaratorSyntax In eventField.Declaration.Variables
                    refEvents.Add($"// {eventType} {containingTypeName}.{declarator.Identifier.Text}")
                Next declarator
            End If
        Next eventField

        ' Process custom accessor events (event EventHandler MyEvent { add; remove; })
        Dim eventStatements As IEnumerable(Of CsSyntax.EventDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.EventDeclarationSyntax)()

        For Each eventStmt As CsSyntax.EventDeclarationSyntax In eventStatements

            Dim isInterfaceMember As Boolean =
                (eventStmt.Parent IsNot Nothing AndAlso eventStmt.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(eventStmt.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(eventStmt, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(eventStmt, language) Then

                Dim eventType As String =
                    If(eventStmt.Type IsNot Nothing,
                       eventStmt.Type.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim containingTypeName As String = Generator.GetContainingTypeName(eventStmt, language)

                refEvents.Add($"// {eventType} {containingTypeName}.{eventStmt.Identifier.Text}")
            End If
        Next eventStmt

        ' 5. Process Methods (🧠)
        Dim methodStatements As IEnumerable(Of CsSyntax.MethodDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.MethodDeclarationSyntax)()

        For Each methodStatement As CsSyntax.MethodDeclarationSyntax In methodStatements

            Dim isInterfaceMember As Boolean =
                (methodStatement.Parent IsNot Nothing AndAlso methodStatement.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            Dim methodName As String = methodStatement.Identifier.Text

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(methodStatement.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(methodStatement, language) AndAlso
           Not Generator.ProhibitedMemberNames.Contains(methodName) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(methodStatement, language) Then

                Dim returnType As String =
                    If(methodStatement.ReturnType IsNot Nothing,
                       methodStatement.ReturnType.ToString().Trim(),
                       "void")

                Dim containingTypeName As String = Generator.GetContainingTypeName(methodStatement, language)
                Dim params As String = Generator.GetParameterTypes(methodStatement.ParameterList, language)

                refMethods.Add($"// {returnType} {containingTypeName}.{methodName}({params})")
            End If
        Next methodStatement

        ' 6. Process Operators (📐)
        ' Process standard operators (+, -, etc.)
        Dim operatorStatements As IEnumerable(Of CsSyntax.OperatorDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.OperatorDeclarationSyntax)()

        For Each opStmt As CsSyntax.OperatorDeclarationSyntax In operatorStatements

            Dim isInterfaceMember As Boolean =
                (opStmt.Parent IsNot Nothing AndAlso opStmt.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(opStmt.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(opStmt, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(opStmt, language) Then

                Dim returnType As String =
                    If(opStmt.ReturnType IsNot Nothing,
                       opStmt.ReturnType.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim containingTypeName As String = Generator.GetContainingTypeName(opStmt, language)
                Dim operatorName As String = opStmt.OperatorToken.Text
                Dim params As String = Generator.GetParameterTypes(opStmt.ParameterList, language)

                refOperators.Add($"// {returnType} {containingTypeName}.{operatorName}({params})")
            End If
        Next opStmt

        ' Process conversion operators (implicit / explicit)
        Dim convOperatorStatements As IEnumerable(Of CsSyntax.ConversionOperatorDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.ConversionOperatorDeclarationSyntax)()

        For Each convOpStmt As CsSyntax.ConversionOperatorDeclarationSyntax In convOperatorStatements

            Dim isInterfaceMember As Boolean =
                (convOpStmt.Parent IsNot Nothing AndAlso convOpStmt.Parent.IsKind(Cs.SyntaxKind.InterfaceDeclaration))

            If (isInterfaceMember OrElse Generator.IsPubliclyAccessible(convOpStmt.Modifiers, language)) AndAlso
               Generator.IsParentChainAccessible(convOpStmt, language) AndAlso
               Not Generator.HasEditorBrowsableNeverAttribute(convOpStmt, language) Then

                Dim conversionKeyword As String = convOpStmt.ImplicitOrExplicitKeyword.Text
                Dim containingTypeName As String = Generator.GetContainingTypeName(convOpStmt, language)

                Dim returnType As String =
                    If(convOpStmt.Type IsNot Nothing,
                       convOpStmt.Type.ToString().Trim(),
                       Generator.ObjectTypeNameCS)

                Dim params As String = Generator.GetParameterTypes(convOpStmt.ParameterList, language)

                refOperators.Add($"// {returnType} {containingTypeName}.{conversionKeyword}({params})")
            End If
        Next convOpStmt

        ' 7. Process Delegates (📜)
        Dim delegateStatements As IEnumerable(Of CsSyntax.DelegateDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.DelegateDeclarationSyntax)()

        For Each delegateStatement As CsSyntax.DelegateDeclarationSyntax In delegateStatements

            If Generator.IsPubliclyAccessible(delegateStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(delegateStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(delegateStatement, language) Then

                Dim returnType As String =
                    If(delegateStatement.ReturnType IsNot Nothing,
                       delegateStatement.ReturnType.ToString().Trim(),
                       "void")

                Dim containingTypeName As String = Generator.GetContainingTypeName(delegateStatement, language)
                Dim prefix As String = If(Not String.IsNullOrEmpty(containingTypeName), $"{containingTypeName}.", "")
                Dim delegateName As String = delegateStatement.Identifier.Text
                Dim params As String = Generator.GetParameterTypes(delegateStatement.ParameterList, language)

                refDelegates.Add($"// {returnType} {prefix}{delegateName}({params})")
            End If
        Next delegateStatement

        ' 8. Process Enums (🔢)
        Dim enumStatements As IEnumerable(Of CsSyntax.EnumDeclarationSyntax) =
            root.DescendantNodes().OfType(Of CsSyntax.EnumDeclarationSyntax)()

        For Each enumStatement As CsSyntax.EnumDeclarationSyntax In enumStatements

            If Generator.IsPubliclyAccessible(enumStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(enumStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(enumStatement, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(enumStatement, language)

                Dim prefix As String =
                    If(Not String.IsNullOrEmpty(containingTypeName),
                     $"{containingTypeName}.",
                      "")

                Dim enumName As String = enumStatement.Identifier.Text
                Dim enumBaseType As String = Generator.EnumDefaultBaseTypeCS

                If enumStatement.BaseList IsNot Nothing AndAlso enumStatement.BaseList.Types.Count > 0 Then
                    enumBaseType = enumStatement.BaseList.Types.First().Type.ToString().Trim()
                End If

                refEnums.Add($"// {prefix}{enumName} : {enumBaseType}")
            End If
        Next enumStatement

    End Sub

    ''' <summary>
    ''' Parses the provided VB.NET source code to extract publicly accessible members and populate the corresponding lists.
    ''' </summary>
    ''' 
    ''' <param name="sourceCode">
    ''' The raw VB.NET source code string to analyze.
    ''' </param>
    ''' 
    ''' <param name="refCtors">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' constructor entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible constructors are found.
    ''' </param>
    ''' 
    ''' <param name="refProps">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' property entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible properties are found.
    ''' </param>
    ''' 
    ''' <param name="refFields">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' field entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible fields are found.
    ''' </param>
    ''' 
    ''' <param name="refEvents">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' event entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible events are found.
    ''' </param>
    ''' 
    ''' <param name="refMethods">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' method entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible methods are found.
    ''' </param>
    ''' 
    ''' <param name="refOperators">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' operator entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible operators are found.
    ''' </param>
    ''' 
    ''' <param name="refDelegates">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' delegate entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible delegates are found.
    ''' </param>
    ''' 
    ''' <param name="refEnums">
    ''' When this method returns, contains an alphabetically sorted list of 
    ''' enumeration entries extracted from the source code, formatted as strings;
    ''' Or an empty list if no publicly accessible enumerations are found.
    ''' </param>
    <DebuggerStepThrough>
    Private Sub ParseVisualBasicCode(sourceCode As String,
                               ByRef refCtors As SortedSet(Of String),
                               ByRef refProps As SortedSet(Of String),
                               ByRef refFields As SortedSet(Of String),
                               ByRef refEvents As SortedSet(Of String),
                               ByRef refMethods As SortedSet(Of String),
                               ByRef refOperators As SortedSet(Of String),
                               ByRef refDelegates As SortedSet(Of String),
                               ByRef refEnums As SortedSet(Of String))

        If refCtors Is Nothing Then
            refCtors = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refProps Is Nothing Then
            refProps = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refFields Is Nothing Then
            refFields = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refEvents Is Nothing Then
            refEvents = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refMethods Is Nothing Then
            refMethods = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refOperators Is Nothing Then
            refOperators = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refDelegates Is Nothing Then
            refDelegates = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        If refEnums Is Nothing Then
            refEnums = New SortedSet(Of String)(StringComparer.OrdinalIgnoreCase)
        End If

        Dim syntaxTree As SyntaxTree = Vb.VisualBasicSyntaxTree.ParseText(sourceCode)
        Dim diagnostics As IEnumerable(Of Diagnostic) = syntaxTree.GetDiagnostics()
        Dim diagnostic As Diagnostic

        For Each diagnostic In diagnostics
            If diagnostic.Severity = DiagnosticSeverity.Error Then
                Throw New InvalidOperationException($"Syntax compilation error discovered: {diagnostic.GetMessage()}")
            End If
        Next diagnostic

        Dim language As SourceLanguage = SourceLanguage.VisualBasic

        Dim root As SyntaxNode = syntaxTree.GetRoot()

        ' 1. Process Constructors (🏗️)
        Dim subNewStatements As IEnumerable(Of VbSyntax.SubNewStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.SubNewStatementSyntax)()

        For Each subNewStatement As VbSyntax.SubNewStatementSyntax In subNewStatements
            If Generator.IsPubliclyAccessible(subNewStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(subNewStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(subNewStatement, language) Then

                Dim params As String = Generator.GetParameterTypes(subNewStatement.ParameterList, language)

                Dim containingTypeName As String = Generator.GetContainingTypeName(subNewStatement, language)
                refCtors.Add($"' {containingTypeName}.New({params})")
            End If
        Next subNewStatement

        ' 2. Process Properties (🔧)
        Dim propertyStatements As IEnumerable(Of VbSyntax.PropertyStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.PropertyStatementSyntax)()

        For Each propStatement As VbSyntax.PropertyStatementSyntax In propertyStatements
            If Generator.IsPubliclyAccessible(propStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(propStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(propStatement, language) Then

                Dim propType As String =
                    If((propStatement.AsClause IsNot Nothing) AndAlso (VbSyntaxExtensions.Type(propStatement.AsClause) IsNot Nothing),
                        VbSyntaxExtensions.Type(propStatement.AsClause).ToString().Trim(),
                        Generator.ObjectTypeNameVB)

                Dim containingTypeName As String = Generator.GetContainingTypeName(propStatement, language)
                refProps.Add($"' {containingTypeName}.{propStatement.Identifier.Text.Trim(Generator.VbNameTrimSpecialChars)} As {propType}")
            End If
        Next propStatement

        ' 3. Process Fields (🏷️)
        Dim fieldStatements As IEnumerable(Of VbSyntax.FieldDeclarationSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.FieldDeclarationSyntax)()

        For Each fieldDeclaration As VbSyntax.FieldDeclarationSyntax In fieldStatements

            ' STRICT VB.NET RULE: A field (class variable) is ONLY public to another assembly or type if
            ' it explicitly contains the "Public" keyword in its declaration. So we ignore the
            ' default behavior of Generator.IsPubliclyAccessible() method.
            Dim isPubliclyAccessible As Boolean =
                fieldDeclaration.Modifiers.Any(Function(token As SyntaxToken) token.IsKind(Vb.SyntaxKind.PublicKeyword))

            If isPubliclyAccessible AndAlso
               Generator.IsParentChainAccessible(fieldDeclaration, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(fieldDeclaration, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(fieldDeclaration, language)

                For Each declarator As VbSyntax.VariableDeclaratorSyntax In fieldDeclaration.Declarators
                    Dim fieldType As String = Generator.ObjectTypeNameVB
                    Dim asClause As VbSyntax.AsClauseSyntax = declarator.AsClause

                    If asClause IsNot Nothing Then
                        ' Capture the base type without expanding complex constructor arguments
                        Dim typeSyntax As VbSyntax.TypeSyntax = Nothing

                        If TypeOf asClause Is VbSyntax.SimpleAsClauseSyntax Then
                            Dim simpleAs As VbSyntax.SimpleAsClauseSyntax = DirectCast(asClause, VbSyntax.SimpleAsClauseSyntax)
                            typeSyntax = simpleAs.Type
                        ElseIf TypeOf asClause Is VbSyntax.AsNewClauseSyntax Then
                            Dim asNew As VbSyntax.AsNewClauseSyntax = DirectCast(asClause, VbSyntax.AsNewClauseSyntax)
                            Dim newExpression As VbSyntax.ExpressionSyntax = asNew.NewExpression

                            If TypeOf newExpression Is VbSyntax.ObjectCreationExpressionSyntax Then
                                Dim objectCreation As VbSyntax.ObjectCreationExpressionSyntax =
                                    DirectCast(newExpression, VbSyntax.ObjectCreationExpressionSyntax)
                                typeSyntax = objectCreation.Type
                            End If
                        End If

                        If typeSyntax IsNot Nothing Then
                            fieldType = typeSyntax.ToString().Trim()
                        End If
                    End If

                    For Each name As VbSyntax.ModifiedIdentifierSyntax In declarator.Names
                        Dim identifierName As String = name.Identifier.Text
                        Dim fieldSummary As String = $"' {containingTypeName}.{identifierName.Trim(Generator.VbNameTrimSpecialChars)} As {fieldType}"
                        refFields.Add(fieldSummary)
                    Next
                Next
            End If
        Next fieldDeclaration

        ' 4. Process Events (⚡)
        Dim eventStatements As IEnumerable(Of VbSyntax.EventStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.EventStatementSyntax)()

        For Each eventStatement As VbSyntax.EventStatementSyntax In eventStatements

            If Generator.IsPubliclyAccessible(eventStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(eventStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(eventStatement, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(eventStatement, language)
                Dim eventName As String = eventStatement.Identifier.Text
                Dim eventSummary As String

                ' Typed event:
                ' Public Event X As EventHandler
                If eventStatement.AsClause IsNot Nothing AndAlso
                   eventStatement.AsClause.Type IsNot Nothing Then

                    Dim eventType As String = eventStatement.AsClause.Type.ToString().Trim()
                    eventSummary = $"' {containingTypeName}.{eventName.Trim(Generator.VbNameTrimSpecialChars)} As {eventType}"

                    ' Delegate-style event:
                    ' Public Event X(sender As Object)
                ElseIf eventStatement.ParameterList IsNot Nothing Then
                    Dim params As String = Generator.GetParameterTypes(eventStatement.ParameterList, language)
                    eventSummary = $"' {containingTypeName}.{eventName.Trim(Generator.VbNameTrimSpecialChars)}({params})"

                Else
                    Throw New InvalidOperationException($"Unsupported event declaration syntax detected: {eventStatement}")

                End If

                refEvents.Add(eventSummary)
            End If
        Next eventStatement

        ' 5. Process Methods (🧠)
        Dim methodStatements As IEnumerable(Of VbSyntax.MethodStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.MethodStatementSyntax)()

        For Each methodStatement As VbSyntax.MethodStatementSyntax In methodStatements
            Dim methodName As String = methodStatement.Identifier.Text

            ' Filter based on accessibility AND ensure it is not a System.Object member
            If Generator.IsPubliclyAccessible(methodStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(methodStatement, language) AndAlso
           Not Generator.ProhibitedMemberNames.Contains(methodName) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(methodStatement, language) Then

                Dim params As String = Generator.GetParameterTypes(methodStatement.ParameterList, language)
                Dim containingTypeName As String = Generator.GetContainingTypeName(methodStatement, language)

                If methodStatement.SubOrFunctionKeyword.IsKind(Vb.SyntaxKind.SubKeyword) Then
                    refMethods.Add($"' {containingTypeName}.{methodName.Trim(Generator.VbNameTrimSpecialChars)}({params})")
                Else
                    Dim returnType As String =
                        If((methodStatement.AsClause IsNot Nothing) AndAlso (methodStatement.AsClause.Type IsNot Nothing),
                            methodStatement.AsClause.Type.ToString().Trim(),
                           Generator.ObjectTypeNameVB)

                    refMethods.Add($"' {containingTypeName}.{methodName.Trim(Generator.VbNameTrimSpecialChars)}({params}) As {returnType}")
                End If
            End If
        Next methodStatement

        ' 6. Process Operators (📐)
        Dim operatorStatements As IEnumerable(Of VbSyntax.OperatorStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.OperatorStatementSyntax)()

        For Each operatorStatement As VbSyntax.OperatorStatementSyntax In operatorStatements

            If Generator.IsPubliclyAccessible(operatorStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(operatorStatement, language) AndAlso
               Not Generator.HasEditorBrowsableNeverAttribute(operatorStatement, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(operatorStatement, language)
                Dim params As String = Generator.GetParameterTypes(operatorStatement.ParameterList, language)
                Dim returnType As String = Generator.ObjectTypeNameVB
                If operatorStatement.AsClause IsNot Nothing AndAlso
                   operatorStatement.AsClause.Type IsNot Nothing Then

                    returnType = operatorStatement.AsClause.Type.ToString().Trim()
                End If

                Dim operatorName As String
                If operatorStatement.OperatorToken.IsKind(Vb.SyntaxKind.CTypeKeyword) Then

                    If operatorStatement.Modifiers.Any(Function(m) m.IsKind(Vb.SyntaxKind.WideningKeyword)) Then
                        operatorName = "CType"

                    ElseIf operatorStatement.Modifiers.Any(Function(m) m.IsKind(Vb.SyntaxKind.NarrowingKeyword)) Then
                        operatorName = "CType"

                    Else
                        operatorName = "CType"

                    End If
                Else

                    operatorName = operatorStatement.OperatorToken.Text
                End If

                refOperators.Add($"' {containingTypeName}.{operatorName}({params}) As {returnType}")
            End If
        Next operatorStatement

        ' 7. Process Delegates (📜)
        Dim delegateStatements As IEnumerable(Of VbSyntax.DelegateStatementSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.DelegateStatementSyntax)()

        For Each delegateStatement As VbSyntax.DelegateStatementSyntax In delegateStatements
            If Generator.IsPubliclyAccessible(delegateStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(delegateStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(delegateStatement, language) Then

                Dim delegateName As String = delegateStatement.Identifier.Text
                Dim params As String = Generator.GetParameterTypes(delegateStatement.ParameterList, language)
                Dim containingTypeName As String = Generator.GetContainingTypeName(delegateStatement, language)
                Dim prefix As String = If(Not String.IsNullOrEmpty(containingTypeName), $"{containingTypeName}.", "")

                If delegateStatement.SubOrFunctionKeyword.IsKind(Vb.SyntaxKind.SubKeyword) Then
                    refDelegates.Add($"' {prefix}{delegateName}({params})")

                Else
                    Dim returnType As String =
                        If((delegateStatement.AsClause IsNot Nothing) AndAlso (delegateStatement.AsClause.Type IsNot Nothing),
                          delegateStatement.AsClause.Type.ToString().Trim(),
                          Generator.ObjectTypeNameVB)

                    refDelegates.Add($"' {prefix}{delegateName.Trim(Generator.VbNameTrimSpecialChars)}({params}) As {returnType}")
                End If

            End If

        Next delegateStatement

        ' 8. Process Enums (🔢)
        Dim enumBlocks As IEnumerable(Of VbSyntax.EnumBlockSyntax) =
            root.DescendantNodes().OfType(Of VbSyntax.EnumBlockSyntax)()

        For Each enumBlock As VbSyntax.EnumBlockSyntax In enumBlocks
            Dim enumStatement As VbSyntax.EnumStatementSyntax = enumBlock.EnumStatement

            If Generator.IsPubliclyAccessible(enumStatement.Modifiers, language) AndAlso
               Generator.IsParentChainAccessible(enumStatement, language) AndAlso
           Not Generator.HasEditorBrowsableNeverAttribute(enumStatement, language) Then

                Dim containingTypeName As String = Generator.GetContainingTypeName(enumStatement, language)
                Dim enumName As String = enumStatement.Identifier.Text

                ' Base type parsing via UnderlyingType
                Dim enumBaseType As String =
                    If((enumStatement.UnderlyingType IsNot Nothing) AndAlso (VbSyntaxExtensions.Type(enumStatement.UnderlyingType) IsNot Nothing),
                      VbSyntaxExtensions.Type(enumStatement.UnderlyingType).ToString().Trim(),
                      Generator.EnumDefaultBaseTypeVB)

                refEnums.Add($"' {If(Not String.IsNullOrEmpty(containingTypeName), $"{containingTypeName}.", "")}{enumName.Trim(Generator.VbNameTrimSpecialChars)} As {enumBaseType}")
            End If
        Next enumBlock

    End Sub

    ''' <summary>
    ''' Determines whether the provided list of syntax modifiers indicates that a member is publicly accessible.
    ''' </summary>
    ''' 
    ''' <param name="modifiers">
    ''' The list of syntax tokens representing the member's modifiers.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language context to interpret the syntax node (e.g., C# or VB.NET).
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if the member is considered publicly accessible; 
    ''' otherwise, <see langword="False"/>.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function IsPubliclyAccessible(modifiers As SyntaxTokenList, language As SourceLanguage) As Boolean

        Select Case language

            Case SourceLanguage.CSharp
                For Each token As SyntaxToken In modifiers
                    If token.IsKind(Cs.SyntaxKind.PublicKeyword) Then
                        Return True
                    End If
                Next
                Return False

            Case SourceLanguage.VisualBasic
                For Each token As SyntaxToken In modifiers
                    If token.IsKind(Vb.SyntaxKind.PrivateKeyword) OrElse
                       token.IsKind(Vb.SyntaxKind.ProtectedKeyword) OrElse
                       token.IsKind(Vb.SyntaxKind.FriendKeyword) OrElse
                       token.IsKind(Vb.SyntaxKind.DimKeyword) Then
                        Return False
                    End If
                Next
                Return True

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))

        End Select

    End Function

    ''' <summary>
    ''' Validates accessibility across the parent type hierarchy of 
    ''' the provided <see cref="SyntaxNode"/> to ensure that 
    ''' no enclosing type restricts visibility of the member.
    ''' </summary>
    ''' 
    ''' <param name="node">
    ''' The syntax node to evaluate.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language context to interpret the syntax node (e.g., C# or VB.NET).
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True"/> if all parent types are accessible; 
    ''' otherwise <see langword="False"/>.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function IsParentChainAccessible(node As SyntaxNode, language As SourceLanguage) As Boolean

        Dim current As SyntaxNode = node.Parent

        Select Case language

            Case SourceLanguage.CSharp
                While current IsNot Nothing

                    If TypeOf current Is CsSyntax.ClassDeclarationSyntax Then
                        Dim classDeclaration As CsSyntax.ClassDeclarationSyntax = CType(current, CsSyntax.ClassDeclarationSyntax)
                        If Not Generator.IsPubliclyAccessible(classDeclaration.Modifiers, SourceLanguage.CSharp) Then
                            Return False
                        End If

                    ElseIf TypeOf current Is CsSyntax.StructDeclarationSyntax Then
                        Dim structureDeclaration As CsSyntax.StructDeclarationSyntax = CType(current, CsSyntax.StructDeclarationSyntax)
                        If Not Generator.IsPubliclyAccessible(structureDeclaration.Modifiers, SourceLanguage.CSharp) Then
                            Return False
                        End If

                    ElseIf TypeOf current Is CsSyntax.InterfaceDeclarationSyntax Then
                        Dim interfaceDeclaration As CsSyntax.InterfaceDeclarationSyntax = CType(current, CsSyntax.InterfaceDeclarationSyntax)
                        If Not Generator.IsPubliclyAccessible(interfaceDeclaration.Modifiers, SourceLanguage.CSharp) Then
                            Return False
                        End If

                    End If

                    current = current.Parent
                End While

            Case SourceLanguage.VisualBasic
                While current IsNot Nothing

                    If TypeOf current Is VbSyntax.ClassBlockSyntax Then
                        Dim classBlock As VbSyntax.ClassBlockSyntax = CType(current, VbSyntax.ClassBlockSyntax)
                        If Not Generator.IsPubliclyAccessible(classBlock.ClassStatement.Modifiers, SourceLanguage.VisualBasic) Then
                            Return False
                        End If

                    ElseIf TypeOf current Is VbSyntax.StructureBlockSyntax Then
                        Dim structBlock As VbSyntax.StructureBlockSyntax = CType(current, VbSyntax.StructureBlockSyntax)
                        If Not Generator.IsPubliclyAccessible(structBlock.StructureStatement.Modifiers, SourceLanguage.VisualBasic) Then
                            Return False
                        End If

                    ElseIf TypeOf current Is VbSyntax.InterfaceBlockSyntax Then
                        Dim interfaceBlock As VbSyntax.InterfaceBlockSyntax = CType(current, VbSyntax.InterfaceBlockSyntax)
                        If Not Generator.IsPubliclyAccessible(interfaceBlock.InterfaceStatement.Modifiers, SourceLanguage.VisualBasic) Then
                            Return False
                        End If

                    ElseIf TypeOf current Is VbSyntax.ModuleBlockSyntax Then
                        Dim moduleBlock As VbSyntax.ModuleBlockSyntax = CType(current, VbSyntax.ModuleBlockSyntax)
                        If Not Generator.IsPubliclyAccessible(moduleBlock.ModuleStatement.Modifiers, SourceLanguage.VisualBasic) Then
                            Return False
                        End If
                    End If

                    current = current.Parent
                End While

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))

        End Select

        Return True
    End Function

    ''' <summary>
    ''' Determines whether the specified syntax node is decorated with the <c>EditorBrowsable(EditorBrowsableState.Never)</c> attribute.
    ''' </summary>
    ''' 
    ''' <param name="node">
    ''' The syntax node to evaluate.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language context to interpret the syntax node (e.g., C# or VB.NET).
    ''' </param>
    ''' 
    ''' <returns>
    ''' <see langword="True" /> if the node has the <c>EditorBrowsable(EditorBrowsableState.Never)</c> attribute; 
    ''' otherwise, <see langword="False" />.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function HasEditorBrowsableNeverAttribute(node As SyntaxNode, language As SourceLanguage) As Boolean

        Const EditorBrowsableAttributeName As String = "EditorBrowsable"
        Const EditorBrowsableStateNeverValue As String = "EditorBrowsableState.Never"

        Select Case language

            Case SourceLanguage.CSharp
                If TypeOf node IsNot CsSyntax.MemberDeclarationSyntax Then
                    Return False
                End If

                Dim memberNode As CsSyntax.MemberDeclarationSyntax = DirectCast(node, CsSyntax.MemberDeclarationSyntax)
                Dim attributeLists As SyntaxList(Of CsSyntax.AttributeListSyntax) = memberNode.AttributeLists

                Dim attributeList As CsSyntax.AttributeListSyntax
                For Each attributeList In attributeLists

                    Dim attribute As CsSyntax.AttributeSyntax

                    For Each attribute In attributeList.Attributes
                        Dim attributeName As String = attribute.Name.ToString()

                        If attributeName.Contains(EditorBrowsableAttributeName) Then

                            If attribute.ArgumentList IsNot Nothing Then

                                Dim argument As CsSyntax.AttributeArgumentSyntax

                                For Each argument In attribute.ArgumentList.Arguments
                                    Dim argumentValue As String = argument.ToString()

                                    If argumentValue.Contains(EditorBrowsableStateNeverValue) Then
                                        Return True
                                    End If

                                Next argument

                            End If ' attribute.ArgumentList

                        End If ' attributeName.Contains

                    Next attribute

                Next attributeList

            Case SourceLanguage.VisualBasic
                Dim attributeLists As SyntaxList(Of VbSyntax.AttributeListSyntax)

                ' Explicitly cast based on the actual VB.NET Roslyn syntax type
                If TypeOf node Is VbSyntax.MethodStatementSyntax Then
                    attributeLists = DirectCast(node, VbSyntax.MethodStatementSyntax).AttributeLists

                ElseIf TypeOf node Is VbSyntax.SubNewStatementSyntax Then
                    attributeLists = DirectCast(node, VbSyntax.SubNewStatementSyntax).AttributeLists

                ElseIf TypeOf node Is VbSyntax.PropertyStatementSyntax Then
                    attributeLists = DirectCast(node, VbSyntax.PropertyStatementSyntax).AttributeLists

                ElseIf TypeOf node Is VbSyntax.FieldDeclarationSyntax Then
                    attributeLists = DirectCast(node, VbSyntax.FieldDeclarationSyntax).AttributeLists

                Else
                    Return False
                End If

                Dim attributeList As VbSyntax.AttributeListSyntax
                For Each attributeList In attributeLists

                    Dim attribute As VbSyntax.AttributeSyntax

                    For Each attribute In attributeList.Attributes
                        Dim attributeName As String = attribute.Name.ToString()

                        If attributeName.Contains(EditorBrowsableAttributeName) Then

                            If attribute.ArgumentList IsNot Nothing Then

                                Dim argument As VbSyntax.ArgumentSyntax

                                For Each argument In attribute.ArgumentList.Arguments
                                    Dim argumentValue As String = argument.ToString()

                                    If argumentValue.Contains(EditorBrowsableStateNeverValue) Then
                                        Return True
                                    End If

                                Next argument

                            End If ' attribute.ArgumentList

                        End If ' attributeName.Contains

                    Next attribute

                Next attributeList

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))

        End Select

        Return False
    End Function

    ''' <summary>
    ''' Traverses the syntax tree upwards from the given syntax node to construct a fully qualified string of containing type names.
    ''' <para></para>
    ''' Only includes Classes, Structures, Modules, and Interfaces in the resulting name, while ignoring Namespaces.
    ''' <para></para>
    ''' For example, if the node is a method declared inside a namespace "MyNamespace", within a class "OuterClass", 
    ''' which contains a nested class "InnerClass", the method will return "OuterClass.InnerClass" instead of "MyNamespace.OuterClass.InnerClass".
    ''' </summary>
    ''' 
    ''' <param name="node">
    ''' The syntax node for which to determine the containing type name hierarchy.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language context to interpret the syntax node (e.g., C# or VB.NET).
    ''' </param>
    ''' 
    ''' <returns>
    ''' Returns a string representing the hierarchy of containing types for the given syntax node, 
    ''' or an empty string if the node is an <see cref="System.Enum"/> declared at the namespace level without a containing type.
    ''' </returns>
    ''' 
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <paramref name="node"/> is null.
    ''' </exception>
    <DebuggerStepThrough>
    Private Function GetContainingTypeName(node As SyntaxNode, language As SourceLanguage) As String

#If NETCOREAPP Then
        ArgumentNullException.ThrowIfNull(node, NameOf(node))
#Else
        If node Is Nothing Then
            Throw New ArgumentNullException(NameOf(node))
        End If
#End If

        Dim current As SyntaxNode = node.Parent
        Dim typeNames As New List(Of String)()

        Select Case language

            Case SourceLanguage.CSharp

                While current IsNot Nothing
                    Dim name As String = String.Empty

                    If TypeOf current Is CsSyntax.ClassDeclarationSyntax Then
                        name = DirectCast(current, CsSyntax.ClassDeclarationSyntax).Identifier.Text

                    ElseIf TypeOf current Is CsSyntax.StructDeclarationSyntax Then
                        name = DirectCast(current, CsSyntax.StructDeclarationSyntax).Identifier.Text

                    ElseIf TypeOf current Is CsSyntax.InterfaceDeclarationSyntax Then
                        name = DirectCast(current, CsSyntax.InterfaceDeclarationSyntax).Identifier.Text

                    End If

                    If Not String.IsNullOrEmpty(name) Then
                        typeNames.Insert(0, name)
                    End If

                    current = current.Parent
                End While

            Case SourceLanguage.VisualBasic
                ' Traverse up, but only collect names from Classes, Structures, Modules, or Interfaces.
                ' We ignore NamespaceBlockSyntax to avoid including the namespace in the output.
                While current IsNot Nothing
                    Dim name As String = String.Empty

                    If TypeOf current Is VbSyntax.ClassBlockSyntax Then
                        name = DirectCast(current, VbSyntax.ClassBlockSyntax).ClassStatement.Identifier.Text

                    ElseIf TypeOf current Is VbSyntax.StructureBlockSyntax Then
                        name = DirectCast(current, VbSyntax.StructureBlockSyntax).StructureStatement.Identifier.Text

                    ElseIf TypeOf current Is VbSyntax.InterfaceBlockSyntax Then
                        name = DirectCast(current, VbSyntax.InterfaceBlockSyntax).InterfaceStatement.Identifier.Text

                    ElseIf TypeOf current Is VbSyntax.ModuleBlockSyntax Then
                        name = DirectCast(current, VbSyntax.ModuleBlockSyntax).ModuleStatement.Identifier.Text

                    End If

                    If Not String.IsNullOrEmpty(name) Then
                        typeNames.Insert(0, name)
                    End If

                    current = current.Parent
                End While

                If typeNames.Count = 0 Then
                    If TypeOf node Is VbSyntax.EnumStatementSyntax Then
                        ' Because a enum can be declared at namespace level without a containing type,
                        ' we return an empty string to indicate no containing type.
                        Return String.Empty
                    End If

                    Throw New InvalidOperationException($"Unable to determine containing type for node '{node.RawKind}'.")
                End If

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))

        End Select

        Return String.Join(".", typeNames)
    End Function

    ''' <summary>
    ''' Converts a syntax node representing a method's parameter list, 
    ''' into a comma-separated string of parameter types.
    ''' </summary>
    ''' 
    ''' <param name="parameterListNode">
    ''' The syntax node representing the parameter list of a member declaration.
    ''' <para></para>
    ''' This value should be either a <see cref="CSSyntax.ParameterListSyntax"/> 
    ''' or a <see cref="VBSyntax.ParameterListSyntax"/> depending on the language context.
    ''' </param>
    ''' 
    ''' <param name="language">
    ''' The language context to interpret the parameter list (e.g., C# or VB.NET).
    ''' </param>
    ''' 
    ''' <returns>
    ''' A string containing the parameter types separated by commas, 
    ''' or an empty string if there are no parameters.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function GetParameterTypes(parameterListNode As SyntaxNode, language As SourceLanguage) As String

        Dim types As New List(Of String)()

        Select Case language

            Case SourceLanguage.CSharp
                Dim parameterList As CsSyntax.ParameterListSyntax =
                    DirectCast(parameterListNode, CsSyntax.ParameterListSyntax)

                If parameterList Is Nothing OrElse
                   parameterList.Parameters.Count = 0 Then

                    Return String.Empty
                End If

                For Each param As CsSyntax.ParameterSyntax In parameterList.Parameters
                    types.Add(If(param.Type IsNot Nothing,
                                 param.Type.ToString().Trim(),
                                 Generator.ObjectTypeNameCS))
                Next param

            Case SourceLanguage.VisualBasic
                Dim parameterList As VbSyntax.ParameterListSyntax =
                    DirectCast(parameterListNode, VbSyntax.ParameterListSyntax)

                If parameterList Is Nothing OrElse
                   parameterList.Parameters.Count = 0 Then

                    Return String.Empty
                End If

                For Each param As VbSyntax.ParameterSyntax In parameterList.Parameters
                    types.Add(If(param.AsClause IsNot Nothing AndAlso param.AsClause.Type IsNot Nothing,
                                 param.AsClause.Type.ToString().Trim(),
                                 Generator.ObjectTypeNameVB))
                Next param

            Case Else
                Throw New InvalidEnumArgumentException(NameOf(language), language, GetType(SourceLanguage))

        End Select

        Return String.Join(", ", types)
    End Function

#End Region

End Module
