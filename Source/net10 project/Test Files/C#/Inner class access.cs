// This file contains three outer classes:
//   - TestClassOuterNoModifier
//   - TestClassOuterInternal
//   - TestClassOuterPublic
//
// Within each outer class, there are three inner classes:
//   - TestClassInnerNoModifier
//   - TestClassInnerInternal
//   - TestClassInnerPublic
//
// Within each inner class, there are members with all combinations of access modifiers.
//
// The generator should only generate summary for the public members within the
// inner class 'TestClassInnerPublic' contained in the outer class 'TestClassOuterPublic',
// while ignoring the outer classes 'TestClassOuterInternal' and 'TestClassOuterNoModifier'
// including their members, as both classes are internal.

#pragma warning disable // Disable all warnings in this file to test the generator.

public class TestClassOuterPublic
{
    public class TestClassInnerPublic
    {
        public TestClassInnerPublic(short defaultCtor)
        {
        }

        private TestClassInnerPublic(decimal privateCtor)
        {
        }

        public static TestClassInnerPublic()
        {
        }

        internal TestClassInnerPublic(long internalCtor)
        {
        }

        public TestClassInnerPublic(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerPublic instance)
        {
        }

        public static string operator -(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator ==(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator !=(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static string operator +(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    internal class TestClassInnerInternal
    {
        public TestClassInnerInternal(short defaultCtor)
        {
        }

        private TestClassInnerInternal(decimal privateCtor)
        {
        }

        public static TestClassInnerInternal()
        {
        }

        internal TestClassInnerInternal(long internalCtor)
        {
        }

        public TestClassInnerInternal(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerInternal instance)
        {
        }

        public static string operator -(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator ==(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator !=(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static string operator +(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    class TestClassInnerNoModifier
    {
        public TestClassInnerNoModifier(short defaultCtor)
        {
        }

        private TestClassInnerNoModifier(decimal privateCtor)
        {
        }

        public static TestClassInnerNoModifier()
        {
        }

        internal TestClassInnerNoModifier(long internalCtor)
        {
        }

        public TestClassInnerNoModifier(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerNoModifier instance)
        {
        }

        public static string operator -(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator ==(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator !=(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static string operator +(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }
}

internal class TestClassOuterInternal
{
    public class TestClassInnerPublic
    {
        public TestClassInnerPublic(short defaultCtor)
        {
        }

        private TestClassInnerPublic(decimal privateCtor)
        {
        }

        public static TestClassInnerPublic()
        {
        }

        internal TestClassInnerPublic(long internalCtor)
        {
        }

        public TestClassInnerPublic(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerPublic instance)
        {
        }

        public static string operator -(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator ==(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator !=(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static string operator +(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    internal class TestClassInnerInternal
    {
        public TestClassInnerInternal(short defaultCtor)
        {
        }

        private TestClassInnerInternal(decimal privateCtor)
        {
        }

        public static TestClassInnerInternal()
        {
        }

        internal TestClassInnerInternal(long internalCtor)
        {
        }

        public TestClassInnerInternal(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerInternal instance)
        {
        }

        public static string operator -(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator ==(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator !=(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static string operator +(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    class TestClassInnerNoModifier
    {
        public TestClassInnerNoModifier(short defaultCtor)
        {
        }

        private TestClassInnerNoModifier(decimal privateCtor)
        {
        }

        public static TestClassInnerNoModifier()
        {
        }

        internal TestClassInnerNoModifier(long internalCtor)
        {
        }

        public TestClassInnerNoModifier(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerNoModifier instance)
        {
        }

        public static string operator -(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator ==(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator !=(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static string operator +(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }
}

class TestClassOuterNoModifier
{
    public class TestClassInnerPublic
    {
        public TestClassInnerPublic(short defaultCtor)
        {
        }

        private TestClassInnerPublic(decimal privateCtor)
        {
        }

        public static TestClassInnerPublic()
        {
        }

        internal TestClassInnerPublic(long internalCtor)
        {
        }

        public TestClassInnerPublic(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerPublic instance)
        {
        }

        public static string operator -(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator ==(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static new string operator !=(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }

        public static string operator +(TestClassInnerPublic a, TestClassInnerPublic b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    internal class TestClassInnerInternal
    {
        public TestClassInnerInternal(short defaultCtor)
        {
        }

        private TestClassInnerInternal(decimal privateCtor)
        {
        }

        public static TestClassInnerInternal()
        {
        }

        internal TestClassInnerInternal(long internalCtor)
        {
        }

        public TestClassInnerInternal(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerInternal instance)
        {
        }

        public static string operator -(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator ==(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static new string operator !=(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }

        public static string operator +(TestClassInnerInternal a, TestClassInnerInternal b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }

    class TestClassInnerNoModifier
    {
        public TestClassInnerNoModifier(short defaultCtor)
        {
        }

        private TestClassInnerNoModifier(decimal privateCtor)
        {
        }

        public static TestClassInnerNoModifier()
        {
        }

        internal TestClassInnerNoModifier(long internalCtor)
        {
        }

        public TestClassInnerNoModifier(int publicCtor)
        {
        }



        public string PropertyNoModifier { get; set; }

        public string PropertyNoModifierReadOnly { get; }

        public static string PropertyNoModifierStatic { get; set; }

        public new string PropertyNoModifierNew { get; set; }

        public static new string PropertyNoModifierStaticNew { get; set; }

        private string PropertyPrivate { get; set; }

        private static string PropertyPrivateStatic { get; set; }

        private new string PropertyPrivateNew { get; set; }

        private static new string PropertyPrivateStaticNew { get; set; }

        internal string PropertyInternal { get; set; }

        internal static string PropertyInternalStatic { get; set; }

        internal new string PropertyInternalNew { get; set; }

        internal static new string PropertyInternalStaticNew { get; set; }

        protected string PropertyProtected { get; set; }

        protected static string PropertyProtectedStatic { get; set; }

        protected new string PropertyProtectedNew { get; set; }

        protected static new string PropertyProtectedStaticNew { get; set; }

        protected internal string PropertyProtectedInternal { get; set; }

        protected internal static string PropertyProtectedInternalStatic { get; set; }

        protected internal new string PropertyProtectedInternalNew { get; set; }

        protected internal static new string PropertyProtectedInternalStaticNew { get; set; }

        public string PropertyPublic { get; set; }

        public static string PropertyPublicStatic { get; set; }

        public new string PropertyPublicNew { get; set; }

        public static new string PropertyPublicStaticNew { get; set; }



        private string FieldNoModifierDim;

        private readonly string FieldNoModifierReadOnly;

        private static string FieldNoModifierStatic;

        private new string FieldNoModifierNew;

        private static new string FieldNoModifierStaticNew;

        private string FieldPrivate;

        private static string FieldPrivateStatic;

        private new string FieldPrivateNew;

        private static new string FieldPrivateStaticNew;

        internal string FieldInternal;

        internal static string FieldInternalStatic;

        internal new string FieldInternalNew;

        internal static new string FieldInternalStaticNew;

        protected string FieldProtected;

        protected static string FieldProtectedStatic;

        protected new string FieldProtectedNew;

        protected static new string FieldProtectedStaticNew;

        protected internal string FieldProtectedInternal;

        protected internal static string FieldProtectedInternalStatic;

        protected internal new string FieldProtectedInternalNew;

        protected internal static new string FieldProtectedInternalStaticNew;

        public string FieldPublic;

        public static string FieldPublicStatic;

        public new string FieldPublicNew;

        public static new string FieldPublicStaticNew;



        public event EventHandler EventNoModifier;

        private event EventHandler EventPrivate;

        private static event EventHandler EventPrivateStatic;

        internal event EventHandler EventInternal;

        internal static event EventHandler EventInternalStatic;

        public event EventHandler EventPublic;

        public static event EventHandler EventStatic;



        public void MethodNoModifier()
        {
        }

        public static void MethodNoModifierStatic()
        {
        }

        public new void MethodNoModifierNew()
        {
        }

        public static new void MethodNoModifierStaticNew()
        {
        }

        private void MethodPrivate()
        {
        }

        private static void MethodPrivateStatic()
        {
        }

        private new void MethodPrivateNew()
        {
        }

        private static new void MethodPrivateStaticNew()
        {
        }

        internal void MethodInternal()
        {
        }

        internal static void MethodInternalStatic()
        {
        }

        internal new void MethodInternalNew()
        {
        }

        internal static new void MethodInternalStaticNew()
        {
        }

        protected internal void MethodProtectedInternal()
        {
        }

        protected internal static void MethodProtectedInternalStatic()
        {
        }

        protected internal new void MethodProtectedInternalNew()
        {
        }

        protected internal static new void MethodProtectedInternalStaticNew()
        {
        }

        public void MethodPublic()
        {
        }

        public static void MethodPublicStatic()
        {
        }

        public new void MethodPublicNew()
        {
        }

        public static new void MethodPublicStaticNew()
        {
        }



        public static implicit operator string(TestClassInnerNoModifier instance)
        {
        }

        public static string operator -(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator ==(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static new string operator !=(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }

        public static string operator +(TestClassInnerNoModifier a, TestClassInnerNoModifier b)
        {
        }



        delegate bool DelegateNoModifier();

        new delegate bool DelegateNoModifierhadows();

        private delegate bool DelegatePrivate();

        private new delegate bool DelegatePrivateNew();

        internal delegate bool DelegateInternal();

        internal new delegate bool DelegateInternalNew();

        protected delegate bool DelegateProtected();

        protected new delegate bool DelegateProtectedNew();

        protected internal delegate bool DelegateProtectedInternal();

        protected internal new delegate bool DelegateProtectedInternalNew();

        public delegate bool DelegatePublic();

        public new delegate bool DelegatePublicNew();



        enum EnumNoModifier : short
        {
            Entry
        }

        new enum EnumNoModifierNew : short
        {
            Entry
        }

        private enum EnumPrivate : short
        {
            Entry
        }

        private new enum EnumPrivateNew : short
        {
            Entry
        }

        internal enum EnumInternal : short
        {
            Entry
        }

        internal new enum EnumInternalNew : short
        {
            Entry
        }

        protected enum EnumProtected : short
        {
            Entry
        }

        protected new enum EnumProtectedNew : short
        {
            Entry
        }

        protected internal enum EnumProtectedInternal : short
        {
            Entry
        }

        protected internal new enum EnumProtectedInternalNew : short
        {
            Entry
        }

        public enum EnumPublic : short
        {
            Entry
        }

        public new enum EnumPublicNew : short
        {
            Entry
        }
    }
}

#pragma warning restore  // Re-enable all warnings.