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
    ''' Tests of the detection of members in Visual Basic source code: accessibility rules, every kind of member,
    ''' the text written for each signature, and the things that must be ignored.
    ''' </summary>
    Public Class Generator_VB_MemberKindsTests

#Region " Private Methods "

        ''' <summary>
        ''' Runs the generator on Visual Basic source code made of the specified lines.
        ''' </summary>
        Private Shared Function Run(ParamArray lines As String()) As GeneratorOutput

            Return GeneratorOutput.VisualBasic(String.Join(vbCrLf, lines))

        End Function

#End Region

#Region " Accessibility "

        ''' <summary>
        ''' Only members declared "Public" are listed; any other accessibility (and the default one) is ignored.
        ''' </summary>
        <Fact>
        Public Sub Accessibility_OnlyPublicMembersAreListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public A As Integer",
                "    Private B As Integer",
                "    Protected Cc As Integer",
                "    Friend D As Integer",
                "    Protected Friend E As Integer",
                "    Private Protected G As Integer",
                "    Dim F As Integer",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.A As Integer"), output.Members("Fields"))
            Assert.Equal(1, output.Count)

        End Sub

        ''' <summary>
        ''' A public type nested in a non-public type is not reachable, so none of its members is listed.
        ''' </summary>
        <Theory>
        <InlineData("Friend")>
        <InlineData("Private")>
        <InlineData("Protected")>
        <InlineData("Protected Friend")>
        Public Sub Accessibility_PublicMembersOfNonPublicContainers_AreIgnored(modifier As String)

            Dim output As GeneratorOutput = Run(
                "Public Class Outer",
                $"    {modifier} Class Inner",
                "        Public Sub M()",
                "        End Sub",
                "        Public Class Deeper",
                "            Public Sub N()",
                "            End Sub",
                "        End Class",
                "    End Class",
                "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub Accessibility_NonPublicTopLevelTypes_AreIgnored()

            Dim output As GeneratorOutput = Run(
                "Public Class A",
                "    Public Sub M()",
                "    End Sub",
                "End Class",
                "Friend Class Hidden",
                "    Public Sub M()",
                "    End Sub",
                "End Class",
                "Friend Module HiddenModule",
                "    Public Sub M()",
                "    End Sub",
                "End Module",
                "Public Class B",
                "    Public Sub M()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("A.M()", "B.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Accessibility_InterfaceMembers_AreListedWithoutModifier_AndFriendInterfacesAreIgnored()

            Dim output As GeneratorOutput = Run(
                "Public Interface I",
                "    Sub X()",
                "    Function Y(a As Integer) As String",
                "    Property P As Integer",
                "    Event E As EventHandler",
                "End Interface",
                "Friend Interface H",
                "    Sub Z()",
                "End Interface")

            Assert.Equal(GeneratorOutput.Expected("I.X()", "I.Y(Integer) As String"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("I.P As Integer"), output.Members("Properties"))
            Assert.Equal(GeneratorOutput.Expected("I.E As EventHandler"), output.Members("Events"))

        End Sub

        ''' <summary>
        ''' A public method that implements an interface member is a normal public method; a private one is not listed.
        ''' </summary>
        <Fact>
        Public Sub Accessibility_ImplementsClause_DoesNotChangeTheAccessibility()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Implements IDisposable",
                "    Public Sub Dispose() Implements IDisposable.Dispose",
                "    End Sub",
                "    Private Sub Priv() Implements IFoo.Bar",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Dispose()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Accessibility_PartialTypeWithoutModifier_IsGivenTheBenefitOfTheDoubt()

            ' The access modifier of a partial type can be specified in only one of its parts, declared in another file.
            Dim output As GeneratorOutput = Run(
                "Namespace N",
                "    Partial NotInheritable Class C",
                "        Public Shared Function F(path As String) As String",
                "            Return path",
                "        End Function",
                "    End Class",
                "    Partial Structure S",
                "        Public Sub M()",
                "        End Sub",
                "    End Structure",
                "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("C.F(String) As String"), output.Members("Methods").Where(Function(m As String) m.StartsWith("C.", StringComparison.Ordinal)).ToArray())
            Assert.Equal(GeneratorOutput.Expected("S.M()"), output.Members("Methods").Where(Function(m As String) m.StartsWith("S.", StringComparison.Ordinal)).ToArray())

        End Sub

        <Fact>
        Public Sub Accessibility_PartialTypeWithExplicitFriendOrPrivate_IsStillIgnored()

            Dim output As GeneratorOutput = Run(
                "Partial Friend Class A",
                "    Public Sub M()",
                "    End Sub",
                "End Class",
                "Public Class O",
                "    Private Partial Class B",
                "        Public Sub N()",
                "        End Sub",
                "    End Class",
                "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub Accessibility_NonPartialTypeWithoutModifier_IsStillFriend()

            Dim output As GeneratorOutput = Run(
                "NotInheritable Class C",
                "    Public Sub M()",
                "    End Sub",
                "End Class")

            Assert.Equal(String.Empty, output.Text)

        End Sub

#End Region

#Region " Containers "

        <Fact>
        Public Sub Containers_Namespaces_AreNotPartOfTheName()

            Dim output As GeneratorOutput = Run(
                "Namespace A.B",
                "    Public Class C",
                "        Public Sub M()",
                "        End Sub",
                "    End Class",
                "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("C.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_Module_ListsItsPublicMembers()

            Dim output As GeneratorOutput = Run(
                "Public Module M",
                "    Public Sub S()",
                "    End Sub",
                "    Public Function F() As String",
                "        Return """"",
                "    End Function",
                "    Friend Sub H()",
                "    End Sub",
                "End Module")

            Assert.Equal(GeneratorOutput.Expected("M.S()", "M.F() As String"), output.Members("Methods"))

        End Sub

        ''' <summary>
        ''' The members of every part of a partial class are listed together.
        ''' </summary>
        <Fact>
        Public Sub Containers_PartialClass_MergesTheMembersOfAllParts()

            Dim output As GeneratorOutput = Run(
                "Partial Public Class C",
                "    Public Sub A()",
                "    End Sub",
                "End Class",
                "Partial Public Class C",
                "    Public Sub B()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.A()", "C.B()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_NestedTypes_UseTheDottedNameOfTheirContainers()

            Dim output As GeneratorOutput = Run(
                "Public Class O",
                "    Public Class I",
                "        Public Sub M()",
                "        End Sub",
                "        Public Class Deep",
                "            Public Sub N()",
                "            End Sub",
                "        End Class",
                "    End Class",
                "    Friend Class H",
                "        Public Sub X()",
                "        End Sub",
                "    End Class",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("O.I.M()", "O.I.Deep.N()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_Structure_ListsItsPublicMembers()

            Dim output As GeneratorOutput = Run(
                "Public Structure S",
                "    Public X As Integer",
                "    Public Sub New(x As Integer)",
                "        Me.X = x",
                "    End Sub",
                "    Public Sub M()",
                "    End Sub",
                "End Structure")

            Assert.Equal(GeneratorOutput.Expected("S.New(Integer)"), output.Members("Constructors"))
            Assert.Equal(GeneratorOutput.Expected("S.X As Integer"), output.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("S.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_GenericClass_IsNamedWithoutItsTypeParameters()

            Dim output As GeneratorOutput = Run(
                "Public Class G(Of T As Class)",
                "    Public Sub M(x As T)",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("G.M(T)"), output.Members("Methods"))

        End Sub

#End Region

#Region " Constructors "

        <Fact>
        Public Sub Constructors_AreListedAsNew_WithTheTypesOfTheirParameters()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Sub New()",
                "    End Sub",
                "    Public Sub New(a As Integer, b As String)",
                "    End Sub",
                "    Private Sub New(d As Double)",
                "    End Sub",
                "    Friend Sub New(c As Char)",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.New()", "C.New(Integer, String)"), output.Members("Constructors"))

        End Sub

        <Fact>
        Public Sub Constructors_ParamArray_IsListedWithItsArrayType()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Sub New(a As Integer, ParamArray rest As String())",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.New(Integer, String())"), output.Members("Constructors"))

        End Sub

#End Region

#Region " Properties "

        <Fact>
        Public Sub Properties_AllKinds_AreListedWithTheirType()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Property A As Integer",
                "    Public ReadOnly Property B As Integer",
                "        Get",
                "            Return 1",
                "        End Get",
                "    End Property",
                "    Public WriteOnly Property W As Integer",
                "        Set(value As Integer)",
                "        End Set",
                "    End Property",
                "    Public Property P(i As Integer, j As String) As Integer",
                "        Get",
                "            Return 1",
                "        End Get",
                "        Set(value As Integer)",
                "        End Set",
                "    End Property",
                "    Public Property Q As Integer",
                "        Get",
                "            Return 1",
                "        End Get",
                "        Private Set(value As Integer)",
                "        End Set",
                "    End Property",
                "    Private Property Hidden As Integer",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected("C.A As Integer", "C.B As Integer", "C.W As Integer", "C.P As Integer", "C.Q As Integer"),
                output.Members("Properties"))

        End Sub

        <Fact>
        Public Sub Properties_DefaultProperty_IsListedByItsName()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Default Public ReadOnly Property Item(i As Integer) As Integer",
                "        Get",
                "            Return 0",
                "        End Get",
                "    End Property",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Item As Integer"), output.Members("Properties"))

        End Sub

#End Region

#Region " Fields "

        <Fact>
        Public Sub Fields_MultipleDeclarators_ProduceOneEntryEach()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public A, B As Integer",
                "    Public Const K1 As Integer = 1, K2 As Integer = 2",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.A As Integer", "C.B As Integer", "C.K1 As Integer", "C.K2 As Integer"), output.Members("Fields"))

        End Sub

        <Fact>
        Public Sub Fields_ConstSharedReadOnlyAndWithEvents_AreListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Const K As String = ""x""",
                "    Public Shared ReadOnly R As Date = Nothing",
                "    Public WithEvents W As Object",
                "    Public Nul As Integer?",
                "    Public Tup As (Integer, String)",
                "    Public Dict As New Dictionary(Of String, List(Of Integer))",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "C.K As String", "C.R As Date", "C.W As Object", "C.Nul As Integer?", "C.Tup As (Integer, String)",
                    "C.Dict As Dictionary(Of String, List(Of Integer))"),
                output.Members("Fields"))

        End Sub

        <Fact>
        Public Sub Fields_ArrayTypes_AreListedWithTheirArrayType()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public A As Integer()",
                "    Public B As String(,)",
                "    Public D As List(Of Integer())",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected("C.A As Integer()", "C.B As String(,)", "C.D As List(Of Integer())"),
                output.Members("Fields"))

        End Sub

#End Region

#Region " Events "

        <Fact>
        Public Sub Events_AsClauseParameterListAndCustom_AreListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Event A As EventHandler",
                "    Public Event B(x As Integer)",
                "    Public Event NoParams()",
                "    Public Shared Event S As Action",
                "    Public Custom Event Cu As EventHandler",
                "        AddHandler(value As EventHandler)",
                "        End AddHandler",
                "        RemoveHandler(value As EventHandler)",
                "        End RemoveHandler",
                "        RaiseEvent(sender As Object, e As EventArgs)",
                "        End RaiseEvent",
                "    End Event",
                "    Private Event Hidden As EventHandler",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected("C.A As EventHandler", "C.B(Integer)", "C.NoParams()", "C.S As Action", "C.Cu As EventHandler"),
                output.Members("Events"))

        End Sub

#End Region

#Region " Methods "

        <Fact>
        Public Sub Methods_ModifiersDoNotChangeTheEntry()

            Dim output As GeneratorOutput = Run(
                "Public MustInherit Class C",
                "    Public Overrides Function ToString() As String",
                "        Return """"",
                "    End Function",
                "    Public Overridable Sub V()",
                "    End Sub",
                "    Public MustOverride Sub Ab()",
                "    Public Shadows Sub Sh()",
                "    End Sub",
                "    Public Overloads Sub Ov()",
                "    End Sub",
                "    Public NotOverridable Overrides Function Equals(o As Object) As Boolean",
                "        Return True",
                "    End Function",
                "    Public Shared Sub St()",
                "    End Sub",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "C.ToString() As String", "C.V()", "C.Ab()", "C.Sh()", "C.Ov()", "C.Equals(Object) As Boolean", "C.St()"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_Parameters_ListOnlyTheirTypes()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Sub M(a As Integer, ByRef b As String, ByVal c As Date, Optional d As Integer = 1, ParamArray e As Integer())",
                "    End Sub",
                "    Public Function G(Of T As {Class, New}, U)(x As T, y As List(Of U)) As Dictionary(Of T, U)",
                "        Return Nothing",
                "    End Function",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected("C.M(Integer, String, Date, Integer, Integer())", "C.G(T, List(Of U)) As Dictionary(Of T, U)"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_ExtensionMethod_IsListedAsNormalMethod()

            Dim output As GeneratorOutput = Run(
                "Imports System.Runtime.CompilerServices",
                "Public Module E",
                "    <Extension>",
                "    Public Function Len2(s As String) As Integer",
                "        Return 0",
                "    End Function",
                "End Module")

            Assert.Equal(GeneratorOutput.Expected("E.Len2(String) As Integer"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_ComplexTypes_KeepTheirSyntax()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Async Function A() As Task(Of Integer)",
                "        Return 1",
                "    End Function",
                "    Public Function Arr() As Integer()()",
                "        Return Nothing",
                "    End Function",
                "    Public Sub P(x As Integer?, y As String(), z As Integer(,))",
                "    End Sub",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected("C.A() As Task(Of Integer)", "C.Arr() As Integer()()", "C.P(Integer?, String(), Integer(,))"),
                output.Members("Methods"))

        End Sub

        ''' <summary>
        ''' A method with an explicit identifier in brackets is listed without the brackets.
        ''' </summary>
        <Fact>
        Public Sub Methods_BracketedIdentifiers_AreListedWithoutBrackets()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Sub [Select]()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Select()"), output.Members("Methods"))

        End Sub

        ''' <summary>
        ''' The methods whose name is in the list of prohibited names ("GetType") are not listed.
        ''' </summary>
        <Fact>
        Public Sub Methods_ProhibitedNames_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Shadows Function [GetType]() As Type",
                "        Return Nothing",
                "    End Function",
                "    Public Sub Other()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Other()"), output.Members("Methods"))

        End Sub

#End Region

#Region " Operators "

        <Fact>
        Public Sub Operators_AllKinds_AreListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Shared Operator +(a As C, b As C) As C",
                "        Return a",
                "    End Operator",
                "    Public Shared Operator =(a As C, b As C) As Boolean",
                "        Return True",
                "    End Operator",
                "    Public Shared Operator <>(a As C, b As C) As Boolean",
                "        Return True",
                "    End Operator",
                "    Public Shared Operator IsTrue(a As C) As Boolean",
                "        Return True",
                "    End Operator",
                "    Public Shared Operator IsFalse(a As C) As Boolean",
                "        Return True",
                "    End Operator",
                "    Public Shared Narrowing Operator CType(i As Integer) As C",
                "        Return Nothing",
                "    End Operator",
                "    Public Shared Operator Like(a As C, b As String) As Boolean",
                "        Return True",
                "    End Operator",
                "    Friend Shared Operator -(a As C, b As C) As C",
                "        Return a",
                "    End Operator",
                "End Class")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "C.+(C, C) As C", "C.=(C, C) As Boolean", "C.<>(C, C) As Boolean", "C.IsTrue(C) As Boolean",
                    "C.IsFalse(C) As Boolean", "C.CType(Integer) As C", "C.Like(C, String) As Boolean"),
                output.Members("Operators"))

        End Sub

#End Region

#Region " Delegates And Enums "

        ''' <summary>
        ''' A delegate declared inside a type is listed with the name of its container.
        ''' </summary>
        <Fact>
        Public Sub Delegates_NestedInClass_AreListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Delegate Sub D(x As Integer)",
                "    Public Delegate Function F(s As String) As Integer",
                "    Private Delegate Sub Hidden()",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.D(Integer)", "C.F(String) As Integer"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub Enums_ShowTheirUnderlyingType_AndDefaultToInteger()

            Dim output As GeneratorOutput = Run(
                "Public Enum Top",
                "    One",
                "End Enum",
                "Public Class C",
                "    <Flags>",
                "    Public Enum F As Long",
                "        A = 1",
                "    End Enum",
                "    Public Enum Plain",
                "        X",
                "    End Enum",
                "    Friend Enum Hidden",
                "        Y",
                "    End Enum",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("Top As Integer", "C.F As Long", "C.Plain As Integer"), output.Members("Enumerations"))

        End Sub

        <Fact>
        Public Sub Enums_InsideNamespace_AreListed()

            Dim output As GeneratorOutput = Run(
                "Namespace N",
                "    Public Enum E",
                "        A",
                "    End Enum",
                "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("E As Integer"), output.Members("Enumerations"))

        End Sub

        <Fact>
        Public Sub Delegates_AtFileLevelWithoutModifier_AreFriend_AndIgnored()

            Dim output As GeneratorOutput = Run(
                "Delegate Sub Hidden()",
                "Friend Delegate Sub AlsoHidden()",
                "Public Delegate Sub Shown()")

            Assert.Equal(GeneratorOutput.Expected("Shown()"), output.Members("Delegates"))
            Assert.Equal(1, output.Count)

        End Sub

        <Fact>
        Public Sub Delegates_InsideNamespaceWithoutModifier_AreFriend_AndIgnored()

            Dim output As GeneratorOutput = Run(
                "Namespace N",
                "    Delegate Sub Hidden()",
                "    Public Delegate Sub Shown()",
                "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("Shown()"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub Delegates_NestedWithoutModifier_ArePublic_AndListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Delegate Sub Nested()",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Nested()"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub Enums_AtFileLevelWithoutModifier_AreFriend_AndIgnored()

            Dim output As GeneratorOutput = Run(
                "Enum Hidden",
                "    A",
                "End Enum",
                "Friend Enum AlsoHidden",
                "    A",
                "End Enum",
                "Public Enum Shown",
                "    A",
                "End Enum")

            Assert.Equal(GeneratorOutput.Expected("Shown As Integer"), output.Members("Enumerations"))
            Assert.Equal(1, output.Count)

        End Sub

        <Fact>
        Public Sub Enums_InsideNamespaceWithoutModifier_AreFriend_AndIgnored()

            Dim output As GeneratorOutput = Run(
                "Namespace N",
                "    Enum Hidden",
                "        A",
                "    End Enum",
                "    Public Enum Shown",
                "        A",
                "    End Enum",
                "End Namespace")

            Assert.Equal(GeneratorOutput.Expected("Shown As Integer"), output.Members("Enumerations"))

        End Sub

        <Fact>
        Public Sub Enums_NestedWithoutModifier_ArePublic_AndListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Enum Nested",
                "        A",
                "    End Enum",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Nested As Integer"), output.Members("Enumerations"))

        End Sub

#End Region

#Region " Things To Ignore "

        ''' <summary>
        ''' Commented-out code, documentation comments and text inside string literals are not members.
        ''' </summary>
        <Fact>
        Public Sub Ignored_CommentsAndStringLiterals()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    ' Public Sub Fake()",
                "    REM Public Sub Fake2()",
                "    Public S As String = ""Public Sub Fake3()""",
                "    ''' <summary>Public Sub Fake4()</summary>",
                "    Public Sub Real()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Real()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("C.S As String"), output.Members("Fields"))
            Assert.Equal(2, output.Count)

        End Sub

        <Fact>
        Public Sub Ignored_MembersInsideMethodBodies()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "    Public Sub M()",
                "        Dim f As Func(Of Integer) = Function() 1",
                "        Dim Public1 As Integer = 1",
                "    End Sub",
                "End Class")

            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " EditorBrowsable "

        ''' <summary>
        ''' Members marked with EditorBrowsable(Never) are not listed, whatever the way the attribute is written;
        ''' any other state (Always, Advanced) is listed.
        ''' </summary>
        <Fact>
        Public Sub EditorBrowsable_Never_HidesProperties_Fields_Constructors_AndMethods()

            Dim output As GeneratorOutput = Run(
                "Imports System.ComponentModel",
                "Public Class C",
                "    <EditorBrowsable(EditorBrowsableState.Never)>",
                "    Public Property P As Integer",
                "    <EditorBrowsable(EditorBrowsableState.Advanced)>",
                "    Public Property Q As Integer",
                "    <EditorBrowsable(EditorBrowsableState.Always)>",
                "    Public Property Always As Integer",
                "    <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>",
                "    Public R As Integer",
                "    <EditorBrowsableAttribute(EditorBrowsableState.Never)>",
                "    Public Sub New()",
                "    End Sub",
                "    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>",
                "    Public Sub Z()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.Q As Integer", "C.Always As Integer"), output.Members("Properties"))
            Assert.Equal(2, output.Count)

        End Sub

#End Region

#Region " Syntax Variants "

        ''' <summary>
        ''' The preprocessor is applied before the analysis: code of an inactive "#If" branch is not part of the summary
        ''' (no symbol such as DEBUG is defined).
        ''' </summary>
        <Fact>
        Public Sub Preprocessor_InactiveBranches_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "Public Class C",
                "#If DEBUG Then",
                "    Public Sub OnlyInDebug()",
                "    End Sub",
                "#Else",
                "    Public Sub NotDebug()",
                "    End Sub",
                "#End If",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.NotDebug()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub OptionStatementsAndImports_DoNotAffectTheResult()

            Dim output As GeneratorOutput = Run(
                "Option Strict On",
                "Option Explicit On",
                "Option Infer Off",
                "Imports System",
                "Imports System.Collections.Generic",
                "Public Class C",
                "    Public Sub M()",
                "    End Sub",
                "End Class")

            Assert.Equal(GeneratorOutput.Expected("C.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Regions_DirectivesInsideTheCode_DoNotAffectTheResult()

            Dim output As GeneratorOutput = Run(
                "#Region "" Outer """,
                "Public Class C",
                "#Region "" Inner """,
                "    Public Sub M()",
                "    End Sub",
                "#End Region",
                "End Class",
                "#End Region")

            Assert.Equal(GeneratorOutput.Expected("C.M()"), output.Members("Methods"))

        End Sub

        ''' <summary>
        ''' Visual Basic is case-insensitive: keywords written in any case are recognized.
        ''' </summary>
        <Fact>
        Public Sub Keywords_AreCaseInsensitive()

            Dim output As GeneratorOutput = Run(
                "PUBLIC CLASS C",
                "    public sub M()",
                "    end sub",
                "    PRIVATE sub H()",
                "    END SUB",
                "END CLASS")

            Assert.Equal(GeneratorOutput.Expected("C.M()"), output.Members("Methods"))

        End Sub

#End Region

    End Class

End Namespace
