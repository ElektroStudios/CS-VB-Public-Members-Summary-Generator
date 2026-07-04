// This file contains a public class, an internal class and a class with no access modifier specified (internal).
// Within these classes, there are members with all combinations of access modifiers.
//
// The generator should only generate summary for the public members of the public class,
// while ignoring all members of the internal and no modifier (internal) class.

#pragma warning disable // Disable all warnings in this file to test the generator.

public class TestClassPublic
{
    public TestClassPublic(short defaultCtor)
    {
    }

    private TestClassPublic(decimal privateCtor)
    {
    }

    public static TestClassPublic()
    {
    }

    internal TestClassPublic(long internalCtor)
    {
    }

    public TestClassPublic(int publicCtor)
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



    public static implicit operator string(TestClassPublic instance)
    {
    }

    public static string operator -(TestClassPublic a, TestClassPublic b)
    {
    }

    public static new string operator ==(TestClassPublic a, TestClassPublic b)
    {
    }

    public static new string operator !=(TestClassPublic a, TestClassPublic b)
    {
    }

    public static string operator +(TestClassPublic a, TestClassPublic b)
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


internal class TestClassInternal
{
    public TestClassInternal(short defaultCtor)
    {
    }

    private TestClassInternal(decimal privateCtor)
    {
    }

    public static TestClassInternal()
    {
    }

    internal TestClassInternal(long internalCtor)
    {
    }

    public TestClassInternal(int publicCtor)
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



    public static implicit operator string(TestClassInternal instance)
    {
    }

    public static string operator -(TestClassInternal a, TestClassInternal b)
    {
    }

    public static new string operator ==(TestClassInternal a, TestClassInternal b)
    {
    }

    public static new string operator !=(TestClassInternal a, TestClassInternal b)
    {
    }

    public static string operator +(TestClassInternal a, TestClassInternal b)
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


class TestClassNoModifier
{
    public TestClassNoModifier(short defaultCtor)
    {
    }

    private TestClassNoModifier(decimal privateCtor)
    {
    }

    public static TestClassNoModifier()
    {
    }

    internal TestClassNoModifier(long friendCtor)
    {
    }

    public TestClassNoModifier(int publicCtor)
    {
    }



    public string PropertyNoModifier { get; set; }

    public string PropertyNoModifierReadOnly { get; }

    public static string PropertyNoModifierShared { get; set; }

    public new string PropertyNoModifierShadows { get; set; }

    public static new string PropertyNoModifierSharedShadows { get; set; }

    private string PropertyPrivate { get; set; }

    private static string PropertyPrivateShared { get; set; }

    private new string PropertyPrivateShadows { get; set; }

    private static new string PropertyPrivateSharedShadows { get; set; }

    internal string PropertyFriend { get; set; }

    internal static string PropertyFriendShared { get; set; }

    internal new string PropertyFriendShadows { get; set; }

    internal static new string PropertyFriendSharedShadows { get; set; }

    protected string PropertyProtected { get; set; }

    protected static string PropertyProtectedShared { get; set; }

    protected new string PropertyProtectedShadows { get; set; }

    protected static new string PropertyProtectedSharedShadows { get; set; }

    protected internal string PropertyProtectedFriend { get; set; }

    protected internal static string PropertyProtectedFriendShared { get; set; }

    protected internal new string PropertyProtectedFriendShadows { get; set; }

    protected internal static new string PropertyProtectedFriendSharedShadows { get; set; }

    public string PropertyPublic { get; set; }

    public static string PropertyPublicShared { get; set; }

    public new string PropertyPublicShadows { get; set; }

    public static new string PropertyPublicSharedShadows { get; set; }



    private string FieldNoModifierDim;

    private readonly string FieldNoModifierReadOnly;

    private static string FieldNoModifierShared;

    private new string FieldNoModifierShadows;

    private static new string FieldNoModifierSharedShadows;

    private string FieldPrivate;

    private static string FieldPrivateShared;

    private new string FieldPrivateShadows;

    private static new string FieldPrivateSharedShadows;

    internal string FieldFriend;

    internal static string FieldFriendShared;

    internal new string FieldFriendShadows;

    internal static new string FieldFriendSharedShadows;

    protected string FieldProtected;

    protected static string FieldProtectedShared;

    protected new string FieldProtectedShadows;

    protected static new string FieldProtectedSharedShadows;

    protected internal string FieldProtectedFriend;

    protected internal static string FieldProtectedFriendShared;

    protected internal new string FieldProtectedFriendShadows;

    protected internal static new string FieldProtectedFriendSharedShadows;

    public string FieldPublic;

    public static string FieldPublicShared;

    public new string FieldPublicShadows;

    public static new string FieldPublicSharedShadows;



    public event EventHandler EventNoModifier;

    private event EventHandler EventPrivate;

    private static event EventHandler EventPrivateShared;

    internal event EventHandler EventFriend;

    internal static event EventHandler EventFriendShared;

    public event EventHandler EventPublic;

    public static event EventHandler EventShared;



    public void MethodNoModifier()
    {
    }

    public static void MethodNoModifierShared()
    {
    }

    public new void MethodNoModifierShadows()
    {
    }

    public static new void MethodNoModifierSharedShadows()
    {
    }

    private void MethodPrivate()
    {
    }

    private static void MethodPrivateShared()
    {
    }

    private new void MethodPrivateShadows()
    {
    }

    private static new void MethodPrivateSharedShadows()
    {
    }

    internal void MethodFriend()
    {
    }

    internal static void MethodFriendShared()
    {
    }

    internal new void MethodFriendShadows()
    {
    }

    internal static new void MethodFriendSharedShadows()
    {
    }

    protected internal void MethodProtectedFriend()
    {
    }

    protected internal static void MethodProtectedFriendShared()
    {
    }

    protected internal new void MethodProtectedFriendShadows()
    {
    }

    protected internal static new void MethodProtectedFriendSharedShadows()
    {
    }

    public void MethodPublic()
    {
    }

    public static void MethodPublicShared()
    {
    }

    public new void MethodPublicShadows()
    {
    }

    public static new void MethodPublicSharedShadows()
    {
    }



    public static implicit operator string(TestClassNoModifier instance)
    {
    }

    public static string operator -(TestClassNoModifier a, TestClassNoModifier b)
    {
    }

    public static new string operator ==(TestClassNoModifier a, TestClassNoModifier b)
    {
    }

    public static new string operator !=(TestClassNoModifier a, TestClassNoModifier b)
    {
    }

    public static string operator +(TestClassNoModifier a, TestClassNoModifier b)
    {
    }



    delegate bool DelegateNoModifier();

    new delegate bool DelegateNoModifierhadows();

    private delegate bool DelegatePrivate();

    private new delegate bool PrivateShadowsDelegate();

    internal delegate bool DelegateFriend();

    internal new delegate bool DelegateFriendShadows();

    protected delegate bool DelegateProtected();

    protected new delegate bool DelegateProtectedShadows();

    protected internal delegate bool DelegateProtectedFriend();

    protected internal new delegate bool DelegateProtectedFriendShadows();

    public delegate bool DelegatePublic();

    public new delegate bool DelegatePublicShadows();



    enum EnumNoModifier : short
    {
        Entry
    }

    new enum EnumNoModifierShadows : short
    {
        Entry
    }

    private enum EnumPrivate : short
    {
        Entry
    }

    private new enum EnumPrivateShadows : short
    {
        Entry
    }

    internal enum EnumFriend : short
    {
        Entry
    }

    internal new enum EnumFriendShadows : short
    {
        Entry
    }

    protected enum EnumProtected : short
    {
        Entry
    }

    protected new enum EnumProtectedShadows : short
    {
        Entry
    }

    protected internal enum EnumProtectedFriend : short
    {
        Entry
    }

    protected internal new enum EnumProtectedFriendShadows : short
    {
        Entry
    }

    public enum EnumPublic : short
    {
        Entry
    }

    public new enum EnumPublicShadows : short
    {
        Entry
    }
}

#pragma warning restore  // Re-enable all warnings.