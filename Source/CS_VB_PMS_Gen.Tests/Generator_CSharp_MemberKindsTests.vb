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
    ''' Tests of the detection of members in C# source code: accessibility rules, every kind of member,
    ''' the text written for each signature, and the things that must be ignored.
    ''' </summary>
    Public Class Generator_CSharp_MemberKindsTests

#Region " Private Methods "

        ''' <summary>
        ''' Joins lines with a line break (CRLF).
        ''' </summary>
        Private Shared Function Code(ParamArray lines As String()) As String

            Return String.Join(vbCrLf, lines)

        End Function

        ''' <summary>
        ''' Runs the generator on C# source code.
        ''' </summary>
        Private Shared Function Run(ParamArray lines As String()) As GeneratorOutput

            Return GeneratorOutput.CSharp(Generator_CSharp_MemberKindsTests.Code(lines))

        End Function

#End Region

#Region " Accessibility "

        ''' <summary>
        ''' Only members declared "public" are listed; any other accessibility (and the default one) is ignored.
        ''' </summary>
        <Fact>
        Public Sub Accessibility_OnlyPublicMembersAreListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public int A;",
                "    private int B;",
                "    protected int Cc;",
                "    internal int D;",
                "    protected internal int E;",
                "    private protected int F;",
                "    int G;",
                "}")

            Assert.Equal(GeneratorOutput.Expected("int C.A"), output.Members("Fields"))
            Assert.Equal(1, output.Count)

        End Sub

        ''' <summary>
        ''' A public type nested in a non-public type is not reachable, so none of its members is listed.
        ''' </summary>
        <Theory>
        <InlineData("internal")>
        <InlineData("private")>
        <InlineData("protected")>
        <InlineData("")>
        Public Sub Accessibility_PublicMembersOfNonPublicContainers_AreIgnored(modifier As String)

            Dim output As GeneratorOutput = Run(
                "public class Outer {",
                $"    {modifier} class Inner {{",
                "        public void M() {}",
                "        public class Deeper { public void N() {} }",
                "    }",
                "}")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub Accessibility_NonPublicTopLevelTypes_AreIgnored()

            Dim output As GeneratorOutput = Run(
                "public class A { public void M() {} }",
                "internal class Hidden { public void M() {} }",
                "class AlsoHidden { public void M() {} }",
                "public class B { public void M() {} }")

            Assert.Equal(GeneratorOutput.Expected("void A.M()", "void B.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Accessibility_InterfaceMembers_AreListedWithoutModifier_AndInternalInterfacesAreIgnored()

            Dim output As GeneratorOutput = Run(
                "public interface I { void X(); int Y { get; } event System.EventHandler E; }",
                "internal interface H { void Z(); }")

            Assert.Equal(GeneratorOutput.Expected("void I.X()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int I.Y"), output.Members("Properties"))
            Assert.Equal(GeneratorOutput.Expected("System.EventHandler I.E"), output.Members("Events"))

        End Sub

        <Fact>
        Public Sub Accessibility_ExplicitInterfaceImplementations_AreIgnored()

            Dim output As GeneratorOutput = Run(
                "public class C : I {",
                "    void I.X() {}",
                "    int I.P { get { return 0; } }",
                "    public void Y() {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.Y()"), output.Members("Methods"))
            Assert.Equal(1, output.Count)

        End Sub

        <Fact>
        Public Sub Accessibility_PartialTypeWithoutModifier_IsGivenTheBenefitOfTheDoubt()

            ' The access modifier of a partial type can be specified in only one of its parts, declared in another file.
            Dim output As GeneratorOutput = Run(
                "namespace N {",
                "    partial class C { public void M() {} }",
                "    partial struct S { public int F; }",
                "    public class O { partial class I { public void Nested() {} } }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.M()", "void O.I.Nested()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int S.F"), output.Members("Fields"))

        End Sub

        <Fact>
        Public Sub Accessibility_PartialTypeWithExplicitNonPublicModifier_IsStillIgnored()

            Dim output As GeneratorOutput = Run(
                "internal partial class A { public void M() {} }",
                "public class O {",
                "    private partial class B { public void N() {} }",
                "    protected partial class P { public void Q() {} }",
                "}")

            Assert.Equal(String.Empty, output.Text)

        End Sub

        <Fact>
        Public Sub Accessibility_NonPartialTypeWithoutModifier_IsStillIgnored()

            Dim output As GeneratorOutput = Run("class C { public void M() {} }")

            Assert.Equal(String.Empty, output.Text)

        End Sub

#End Region

#Region " Containers "

        <Fact>
        Public Sub Containers_FileScopedNamespace_IsSupported()

            Dim output As GeneratorOutput = Run("namespace N;", "public class C { public void M() {} }")

            Assert.Equal(GeneratorOutput.Expected("void C.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_BlockNamespaces_AreNotPartOfTheName()

            Dim output As GeneratorOutput = Run("namespace A.B { namespace C { public class K { public void M() {} } } }")

            Assert.Equal(GeneratorOutput.Expected("void K.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_StaticClass_IsSupported()

            Dim output As GeneratorOutput = Run("public static class C { public static void M() {} public static int F; }")

            Assert.Equal(GeneratorOutput.Expected("void C.M()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int C.F"), output.Members("Fields"))

        End Sub

        ''' <summary>
        ''' The members of every part of a partial class are listed together.
        ''' </summary>
        <Fact>
        Public Sub Containers_PartialClass_MergesTheMembersOfAllParts()

            Dim output As GeneratorOutput = Run(
                "public partial class C { public void A() {} }",
                "public partial class C { public void B() {} }")

            Assert.Equal(GeneratorOutput.Expected("void C.A()", "void C.B()"), output.Members("Methods"))
            Assert.Equal(2, output.Count)

        End Sub

        ''' <summary>
        ''' The same signature declared twice is listed once (and counted once).
        ''' </summary>
        <Fact>
        Public Sub Containers_DuplicatedSignatures_AreListedOnce()

            Dim output As GeneratorOutput = Run(
                "public partial class C { public void M() {} }",
                "public partial class C { public void M() {} }")

            Assert.Equal(GeneratorOutput.Expected("void C.M()"), output.Members("Methods"))
            Assert.Equal(1, output.Count)

        End Sub

        <Fact>
        Public Sub Containers_NestedTypes_UseTheDottedNameOfTheirContainers()

            Dim output As GeneratorOutput = Run(
                "public class Outer {",
                "    public class Inner {",
                "        public void M() {}",
                "        public class Deep { public int F; }",
                "    }",
                "    public interface I { void X(); }",
                "    public struct S { public int A; }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void Outer.Inner.M()", "void Outer.I.X()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int Outer.Inner.Deep.F", "int Outer.S.A"), output.Members("Fields"))

        End Sub

        ''' <summary>
        ''' The generic parameters of the containers are not part of the name.
        ''' </summary>
        <Fact>
        Public Sub Containers_GenericTypes_AreNamedWithoutTheirTypeParameters()

            Dim output As GeneratorOutput = Run("public class O<T> { public class I<U> { public void M() {} } public void N() {} }")

            Assert.Equal(GeneratorOutput.Expected("void O.I.M()", "void O.N()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_Struct_ListsItsPublicMembers()

            Dim output As GeneratorOutput = Run(
                "public struct P {",
                "    public int X; private int Y;",
                "    public P(int x) { X = x; Y = 0; }",
                "    public void M() {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("P(int)"), output.Members("Constructors"))
            Assert.Equal(GeneratorOutput.Expected("int P.X"), output.Members("Fields"))
            Assert.Equal(GeneratorOutput.Expected("void P.M()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Containers_SeveralTopLevelTypes_AreAllListed()

            Dim output As GeneratorOutput = Run(
                "public class A { public void M() {} }",
                "public class B { public void M() {} }",
                "public struct S { public int F; }",
                "public interface I { void X(); }")

            Assert.Equal(GeneratorOutput.Expected("void A.M()", "void B.M()", "void I.X()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("int S.F"), output.Members("Fields"))

        End Sub

#End Region

#Region " Constructors "

        <Fact>
        Public Sub Constructors_AreListedWithoutTheirContainerName()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public C() {}",
                "    public C(int a, string b) {}",
                "    private C(double d) {}",
                "    protected C(char c) {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("C()", "C(int, string)"), output.Members("Constructors"))

        End Sub

        <Fact>
        Public Sub Constructors_DestructorsAndStaticConstructors_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    static C() {}",
                "    ~C() {}",
                "    public void M() {}",
                "}")

            Assert.Empty(output.Members("Constructors"))
            Assert.Equal(1, output.Count)

        End Sub

        <Fact>
        Public Sub Constructors_NestedTypes_AreListedWithTheNamesOfTheirContainers()

            Dim output As GeneratorOutput = Run("public class O { public class I { public I(int x) {} } }")

            Assert.Equal(GeneratorOutput.Expected("O.I(int)"), output.Members("Constructors"))

        End Sub
        <Fact>
        Public Sub Constructors_RecordPrimaryConstructor_IsListed()

            Dim output As GeneratorOutput = Run(
                "public record R(int A, string B);",
                "public record struct RS(double D);")

            Assert.Equal(GeneratorOutput.Expected("R(int, string)", "RS(double)"), output.Members("Constructors"))

        End Sub

        <Fact>
        Public Sub Constructors_ClassAndStructPrimaryConstructors_AreListed_AndTheirParametersAreNotProperties()

            Dim output As GeneratorOutput = Run(
                "public class C(int a, string b) { }",
                "public struct S(long l) { }")

            Assert.Equal(GeneratorOutput.Expected("C(int, string)", "S(long)"), output.Members("Constructors"))
            Assert.Empty(output.Members("Properties"))

        End Sub

        <Fact>
        Public Sub Constructors_NestedPrimaryConstructor_IsListedWithTheNamesOfItsContainers()

            Dim output As GeneratorOutput = Run("public class O { public record I(int x); }")

            Assert.Equal(GeneratorOutput.Expected("O.I(int)"), output.Members("Constructors"))

        End Sub

        <Fact>
        Public Sub Constructors_NonPublicPrimaryConstructors_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "internal record R(int A);",
                "record R2(int A);",
                "public class O { private record P(int A); }",
                "internal class H(int a) { public H(string s) : this(0) {} }")

            Assert.Equal(String.Empty, output.Text)

        End Sub

#End Region

#Region " Properties "

        <Fact>
        Public Sub Properties_AllKinds_AreListedOnceWithTheirType()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public int A { get; } = 5;",
                "    public int B { get; private set; }",
                "    public int D { private get; set; }",
                "    public int E { get; init; }",
                "    public int Expr => 1;",
                "    private int Hidden { get; set; }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("int C.A", "int C.B", "int C.D", "int C.E", "int C.Expr"), output.Members("Properties"))
            Assert.Equal(5, output.Count)

        End Sub

        <Fact>
        Public Sub Properties_GenericAndNullableTypes_KeepTheirSyntax()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public System.Collections.Generic.List<int> L { get; set; }",
                "    public int? N { get; set; }",
                "    public string[] Arr { get; set; }",
                "    public (int, string) Tup { get; set; }",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "System.Collections.Generic.List<int> C.L", "int? C.N", "string[] C.Arr", "(int, string) C.Tup"),
                output.Members("Properties"))

        End Sub

#End Region

#Region " Fields "

        <Fact>
        Public Sub Fields_ConstStaticReadOnlyAndVolatile_AreListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public const int K = 1;",
                "    public static readonly string S = """";",
                "    public volatile int V;",
                "}")

            Assert.Equal(GeneratorOutput.Expected("int C.K", "string C.S", "int C.V"), output.Members("Fields"))

        End Sub

        ''' <summary>
        ''' A declaration with several variables produces one entry per variable.
        ''' </summary>
        <Fact>
        Public Sub Fields_MultipleDeclarators_ProduceOneEntryEach()

            Dim output As GeneratorOutput = Run("public class C { public int A, B = 2, Cc; }")

            Assert.Equal(GeneratorOutput.Expected("int C.A", "int C.B", "int C.Cc"), output.Members("Fields"))
            Assert.Equal(3, output.Count)

        End Sub

        <Fact>
        Public Sub Fields_ArraysAndGenericTypes_KeepTheirSyntax()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public int[] A;",
                "    public int[][] B;",
                "    public string[,] D;",
                "    public System.Func<int, int> F;",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected("int[] C.A", "int[][] C.B", "string[,] C.D", "System.Func<int, int> C.F"),
                output.Members("Fields"))

        End Sub

#End Region

#Region " Events "

        <Fact>
        Public Sub Events_FieldLikeCustomAndStatic_AreListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public event System.EventHandler A, B;",
                "    public static event System.Action<int, string> S;",
                "    public event System.EventHandler Custom { add {} remove {} }",
                "    private event System.EventHandler Hidden;",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "System.EventHandler C.A", "System.EventHandler C.B", "System.Action<int, string> C.S", "System.EventHandler C.Custom"),
                output.Members("Events"))

        End Sub

#End Region

#Region " Methods "

        <Fact>
        Public Sub Methods_ModifiersDoNotChangeTheEntry()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public override string ToString() { return """"; }",
                "    public virtual void V() {}",
                "    public new void N() {}",
                "    public static void S() {}",
                "    public extern void Ex();",
                "    public sealed override int GetHashCode() { return 0; }",
                "    public abstract class A { public abstract void Ab(); }",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "string C.ToString()", "void C.V()", "void C.N()", "void C.S()", "void C.Ex()", "int C.GetHashCode()", "void C.A.Ab()"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_Parameters_ListOnlyTheirTypes()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public void M<U>(U u, ref int r, out string o, params int[] ps) { o = null; }",
                "    public int Sum(int a, int b = 5) { return a + b; }",
                "    public void In(in int x) {}",
                "    public void None() {}",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected("void C.M(U, int, string, int[])", "int C.Sum(int, int)", "void C.In(int)", "void C.None()"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_ExtensionMethod_DoesNotShowTheThisKeyword()

            Dim output As GeneratorOutput = Run("public static class E { public static int Len(this string s, int x = 5) { return 0; } }")

            Assert.Equal(GeneratorOutput.Expected("int E.Len(string, int)"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_ComplexTypes_KeepTheirSyntax()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public T Max<T>(T a, T b) where T : System.IComparable<T> { return a; }",
                "    public Dictionary<string, List<int>> D(IEnumerable<KeyValuePair<int, string>> x) { return null; }",
                "    public (int, string) Tup() { return (1, """"); }",
                "    public int? Nul() { return 1; }",
                "    public int[][] J(int[] a, string[,,] b) { return null; }",
                "    public unsafe int* P(int* q, byte[] b) { return q; }",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "T C.Max(T, T)",
                    "Dictionary<string, List<int>> C.D(IEnumerable<KeyValuePair<int, string>>)",
                    "(int, string) C.Tup()",
                    "int? C.Nul()",
                    "int[][] C.J(int[], string[,,])",
                    "int* C.P(int*, byte[])"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_AsyncMethods_DoNotShowTheAsyncKeyword()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public async System.Threading.Tasks.Task A() {}",
                "    public async Task<List<int>> B() { return null; }",
                "    public ValueTask<int> V() { return default; }",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected("System.Threading.Tasks.Task C.A()", "Task<List<int>> C.B()", "ValueTask<int> C.V()"),
                output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Methods_NullableReferenceAnnotations_AreKept()

            Dim output As GeneratorOutput = Run(
                "#nullable enable",
                "public class C { public string? M(string? s) { return s; } public System.Collections.Generic.List<string?> L; }")

            Assert.Equal(GeneratorOutput.Expected("string? C.M(string?)"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("System.Collections.Generic.List<string?> C.L"), output.Members("Fields"))

        End Sub

        <Fact>
        Public Sub Methods_LocalFunctionsAndLambdas_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public void M() {",
                "        void Local() {}",
                "        System.Func<int> f = () => 1;",
                "    }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.M()"), output.Members("Methods"))
            Assert.Equal(1, output.Count)

        End Sub

        ''' <summary>
        ''' The methods whose name is in the list of prohibited names ("GetType") are not listed.
        ''' </summary>
        <Fact>
        Public Sub Methods_ProhibitedNames_AreNotListed()

            Dim output As GeneratorOutput = Run("public class C { public new System.Type GetType() { return null; } public void Other() {} }")

            Assert.Equal(GeneratorOutput.Expected("void C.Other()"), output.Members("Methods"))

        End Sub

#End Region

#Region " Operators "

        <Fact>
        Public Sub Operators_AllKinds_AreListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public static bool operator true(C c) { return true; }",
                "    public static bool operator false(C c) { return false; }",
                "    public static explicit operator C(int i) { return null; }",
                "    public static implicit operator int(C c) { return 0; }",
                "    public static C operator ++(C c) { return c; }",
                "    public static bool operator ==(C a, C b) { return true; }",
                "    public static bool operator !=(C a, C b) { return false; }",
                "    internal static C operator -(C a, C b) { return a; }",
                "}")

            Assert.Equal(
                GeneratorOutput.Expected(
                    "bool C.true(C)", "bool C.false(C)", "C C.explicit(int)", "int C.implicit(C)", "C C.++(C)", "bool C.==(C, C)", "bool C.!=(C, C)"),
                output.Members("Operators"))

        End Sub

#End Region

#Region " Delegates And Enums "

        <Fact>
        Public Sub Delegates_TopLevelNestedAndGeneric_AreListed()

            Dim output As GeneratorOutput = Run(
                "public delegate int TopD(string s);",
                "public delegate T Fn<T, U>(U u, ref int r) where T : class;",
                "public class C { public delegate void D(); private delegate void Hidden(); }")

            Assert.Equal(GeneratorOutput.Expected("int TopD(string)", "T Fn(U, int)", "void C.D()"), output.Members("Delegates"))

        End Sub

        <Fact>
        Public Sub Enums_ShowTheirUnderlyingType_AndDefaultToInt()

            Dim output As GeneratorOutput = Run(
                "public enum Top { One }",
                "public class C {",
                "    [System.Flags] public enum F : long { A = 1, B = 2 }",
                "    public enum Plain { X }",
                "    public enum Small : byte { Y }",
                "    internal enum Hidden { Z }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("Top : int", "C.F : long", "C.Plain : int", "C.Small : byte"), output.Members("Enumerations"))

        End Sub

        <Fact>
        Public Sub Enums_MembersOfTheEnumeration_AreNotListed()

            Dim output As GeneratorOutput = Run("public enum E { First, Second, Third }")

            Assert.Equal(1, output.Count)
            Assert.DoesNotContain("First", output.Text)

        End Sub

#End Region

#Region " Things To Ignore "

        ''' <summary>
        ''' Commented-out code, documentation comments and text inside string literals are not members.
        ''' </summary>
        <Fact>
        Public Sub Ignored_CommentsAndStringLiterals()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    // public void Fake() {}",
                "    /* public void Fake2() {} */",
                "    public string S = ""public void Fake3() {}"";",
                "    /// <summary>public void Fake4() {}</summary>",
                "    public void Real() {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.Real()"), output.Members("Methods"))
            Assert.Equal(GeneratorOutput.Expected("string C.S"), output.Members("Fields"))
            Assert.Equal(2, output.Count)

        End Sub

        <Fact>
        Public Sub Ignored_VerbatimAndRawStringLiterals()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public string V = @""public void FakeA() {}"";",
                "    public string R = """"""public void FakeB() {}"""""";",
                "    public void Real() {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.Real()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Ignored_MembersInsideMethodBodies()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "    public void M() {",
                "        var anonymous = new { Public = 1 };",
                "    }",
                "}")

            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " EditorBrowsable "

        ''' <summary>
        ''' Members marked with EditorBrowsable(Never) are not listed, whatever the way the attribute is written;
        ''' any other state (Always, Advanced) is listed.
        ''' </summary>
        <Fact>
        Public Sub EditorBrowsable_Never_HidesMembersOfEveryKind_AndTheOtherStatesDoNot()

            Dim output As GeneratorOutput = Run(
                "using System.ComponentModel;",
                "public class C {",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public int P { get; set; }",
                "    [EditorBrowsable(EditorBrowsableState.Advanced)] public int Q { get; set; }",
                "    [EditorBrowsable(EditorBrowsableState.Always)] public int Always { get; set; }",
                "    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] public int R;",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public event System.EventHandler Ev;",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public C() {}",
                "    [EditorBrowsableAttribute(EditorBrowsableState.Never)] public void Z() {}",
                "    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)] public void Z2() {}",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public static C operator +(C a, C b) { return a; }",
                "}")

            Assert.Equal(GeneratorOutput.Expected("int C.Q", "int C.Always"), output.Members("Properties"))
            Assert.Equal(2, output.Count)

        End Sub

        <Fact>
        Public Sub EditorBrowsable_Never_HidesDelegatesAndEnums()

            Dim output As GeneratorOutput = Run(
                "using System.ComponentModel;",
                "public class C {",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public delegate void D();",
                "    [EditorBrowsable(EditorBrowsableState.Never)] public enum E { A }",
                "    public void Visible() {}",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.Visible()"), output.Members("Methods"))
            Assert.Equal(1, output.Count)

        End Sub

#End Region

#Region " Syntax Variants "

        ''' <summary>
        ''' The preprocessor is applied before the analysis: code of an inactive "#if" branch is not part of the summary
        ''' (no symbol such as DEBUG is defined).
        ''' </summary>
        <Fact>
        Public Sub Preprocessor_InactiveBranches_AreNotListed()

            Dim output As GeneratorOutput = Run(
                "public class C {",
                "#if DEBUG",
                "    public void OnlyInDebug() {}",
                "#else",
                "    public void NotDebug() {}",
                "#endif",
                "}")

            Assert.Equal(GeneratorOutput.Expected("void C.NotDebug()"), output.Members("Methods"))

        End Sub

        <Fact>
        Public Sub Regions_DirectivesInsideTheCode_DoNotAffectTheResult()

            Dim output As GeneratorOutput = Run(
                "#region Stuff",
                "public class C {",
                "#region Inner",
                "    public void M() {}",
                "#endregion",
                "}",
                "#endregion")

            Assert.Equal(GeneratorOutput.Expected("void C.M()"), output.Members("Methods"))

        End Sub

#End Region

    End Class

End Namespace
