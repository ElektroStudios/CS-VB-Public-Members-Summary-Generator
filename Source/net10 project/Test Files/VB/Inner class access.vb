' This file contains three outer classes:
'   - TestClassOuterNoModifier
'   - TestClassOuterPublic
'   - TestClassOuterFriend
'
' Within each outer class, there are three inner classes:
'   - TestClassInnerNoModifier
'   - TestClassInnerPublic
'   - TestClassInnerFriend
'
' Within each inner class, there are members with all combinations of access modifiers.
'
' The generator should only generate summary for the public members within the 
' inner classes 'TestClassInnerPublic' and 'TestClassInnerNoModifier' (as both are public) 
' contained in the outer classes 'TestClassOuterPublic' and 'TestClassOuterNoModifier', 
' while ignoring the outer class 'TestClassOuterFriend' and its inner classes.

#Disable Warning ' Disable all warnings in this file to test the generator.

#Region " TestClassOuter Public "

Public Class TestClassOuterPublic

#Region " Inner TestClass Public "

    Public Class TestClassInnerPublic

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerPublic) As String : End Operator

        Shared Operator -(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Operator +(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass Friend "

    Friend Class TestClassInnerFriend

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerFriend) As String : End Operator

        Shared Operator -(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Operator +(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass NoModifier "

    Class TestClassInnerNoModifier

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerNoModifier) As String : End Operator

        Shared Operator -(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Operator +(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

End Class

#End Region

#Region " TestClassOuter Friend "

Friend Class TestClassOuterFriend

#Region " Inner TestClass Public "

    Public Class TestClassInnerPublic

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerPublic) As String : End Operator

        Shared Operator -(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Operator +(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass Friend "

    Friend Class TestClassInnerFriend

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerFriend) As String : End Operator

        Shared Operator -(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Operator +(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass NoModifier "

    Class TestClassInnerNoModifier

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerNoModifier) As String : End Operator

        Shared Operator -(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Operator +(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

End Class

#End Region

#Region " TestClassOuter NoModifier "

Class TestClassOuterNoModifier

#Region " Inner TestClass Public "

    Public Class TestClassInnerPublic

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerPublic) As String : End Operator

        Shared Operator -(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

        Public Shared Operator +(a As TestClassInnerPublic, b As TestClassInnerPublic) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass Friend "

    Friend Class TestClassInnerFriend

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerFriend) As String : End Operator

        Shared Operator -(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

        Public Shared Operator +(a As TestClassInnerFriend, b As TestClassInnerFriend) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

#Region " Inner TestClass NoModifier "

    Class TestClassInnerNoModifier

#Region " Constructors "

        Sub New(defaultCtor As Short) : End Sub

        Private Sub New(privateCtor As Decimal) : End Sub

        Shared Sub New() : End Sub

        Friend Sub New(friendCtor As Long) : End Sub

        Public Sub New(publicCtor As Integer) : End Sub

#End Region

#Region " Properties "

        Property PropertyNoModifier As String

        ReadOnly Property PropertyNoModifierReadOnly As String

        Shared Property PropertyNoModifierShared As String

        Shadows Property PropertyNoModifierShadows As String

        Shared Shadows Property PropertyNoModifierSharedShadows As String

        Private Property PropertyPrivate As String

        Private Shared Property PropertyPrivateShared As String

        Private Shadows Property PropertyPrivateShadows As String

        Private Shared Shadows Property PropertyPrivateSharedShadows As String

        Friend Property PropertyFriend As String

        Friend Shared Property PropertyFriendShared As String

        Friend Shadows Property PropertyFriendShadows As String

        Friend Shared Shadows Property PropertyFriendSharedShadows As String

        Protected Property PropertyProtected As String

        Protected Shared Property PropertyProtectedShared As String

        Protected Shadows Property PropertyProtectedShadows As String

        Protected Shared Shadows Property PropertyProtectedSharedShadows As String

        Protected Friend Property PropertyProtectedFriend As String

        Protected Friend Shared Property PropertyProtectedFriendShared As String

        Protected Friend Shadows Property PropertyProtectedFriendShadows As String

        Protected Friend Shared Shadows Property PropertyProtectedFriendSharedShadows As String

        Public Property PropertyPublic As String

        Public Shared Property PropertyPublicShared As String

        Public Shadows Property PropertyPublicShadows As String

        Public Shared Shadows Property PropertyPublicSharedShadows As String

#End Region

#Region " Fields "

        Dim FieldNoModifierDim As String

        ReadOnly FieldNoModifierReadOnly As String

        Shared FieldNoModifierShared As String

        Shadows FieldNoModifierShadows As String

        Shared Shadows FieldNoModifierSharedShadows As String

        Private FieldPrivate As String

        Private Shared FieldPrivateShared As String

        Private Shadows FieldPrivateShadows As String

        Private Shared Shadows FieldPrivateSharedShadows As String

        Friend FieldFriend As String

        Friend Shared FieldFriendShared As String

        Friend Shadows FieldFriendShadows As String

        Friend Shared Shadows FieldFriendSharedShadows As String

        Protected FieldProtected As String

        Protected Shared FieldProtectedShared As String

        Protected Shadows FieldProtectedShadows As String

        Protected Shared Shadows FieldProtectedSharedShadows As String

        Protected Friend FieldProtectedFriend As String

        Protected Friend Shared FieldProtectedFriendShared As String

        Protected Friend Shadows FieldProtectedFriendShadows As String

        Protected Friend Shared Shadows FieldProtectedFriendSharedShadows As String

        Public FieldPublic As String

        Public Shared FieldPublicShared As String

        Public Shadows FieldPublicShadows As String

        Public Shared Shadows FieldPublicSharedShadows As String

#End Region

#Region " Events "

        Event EventNoModifier As EventHandler

        Private Event EventPrivate As EventHandler

        Private Shared Event EventPrivateShared As EventHandler

        Friend Event EventFriend As EventHandler

        Friend Shared Event EventFriendShared As EventHandler

        Public Event EventPublic As EventHandler

        Shared Event EventShared As EventHandler

#End Region

#Region " Methods "

        Sub MethodNoModifier() : End Sub

        Shared Sub MethodNoModifierShared() : End Sub

        Shadows Sub MethodNoModifierShadows() : End Sub

        Shared Shadows Sub MethodNoModifierSharedShadows() : End Sub

        Private Sub MethodPrivate() : End Sub

        Private Shared Sub MethodPrivateShared() : End Sub

        Private Shadows Sub MethodPrivateShadows() : End Sub

        Private Shared Shadows Sub MethodPrivateSharedShadows() : End Sub

        Friend Sub MethodFriend() : End Sub

        Friend Shared Sub MethodFriendShared() : End Sub

        Friend Shadows Sub MethodFriendShadows() : End Sub

        Friend Shared Shadows Sub MethodFriendSharedShadows() : End Sub

        Protected Friend Sub MethodProtectedFriend() : End Sub

        Protected Friend Shared Sub MethodProtectedFriendShared() : End Sub

        Protected Friend Shadows Sub MethodProtectedFriendShadows() : End Sub

        Protected Friend Shared Shadows Sub MethodProtectedFriendSharedShadows() : End Sub

        Public Sub MethodPublic() : End Sub

        Public Shared Sub MethodPublicShared() : End Sub

        Public Shadows Sub MethodPublicShadows() : End Sub

        Public Shared Shadows Sub MethodPublicSharedShadows() : End Sub

#End Region

#Region " Operators "

        Public Shared Widening Operator CType(instance As TestClassInnerNoModifier) As String : End Operator

        Shared Operator -(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Shared Shadows Operator =(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Shadows Operator <>(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

        Public Shared Operator +(a As TestClassInnerNoModifier, b As TestClassInnerNoModifier) As String : End Operator

#End Region

#Region " Delegates "

        Delegate Function DelegateNoModifier() As Boolean

        Shadows Delegate Function DelegateNoModifierhadows() As Boolean

        Private Delegate Function DelegatePrivate() As Boolean

        Private Shadows Delegate Function PrivateShadowsDelegate() As Boolean

        Friend Delegate Function DelegateFriend() As Boolean

        Friend Shadows Delegate Function DelegateFriendShadows() As Boolean

        Protected Delegate Function DelegateProtected() As Boolean

        Protected Shadows Delegate Function DelegateProtectedShadows() As Boolean

        Protected Friend Delegate Function DelegateProtectedFriend() As Boolean

        Protected Friend Shadows Delegate Function DelegateProtectedFriendShadows() As Boolean

        Public Delegate Function DelegatePublic() As Boolean

        Public Shadows Delegate Function DelegatePublicShadows() As Boolean

#End Region

#Region " Enums "

        Enum EnumNoModifier As Short : Entry : End Enum

        Shadows Enum EnumNoModifierShadows As Short : Entry : End Enum

        Private Enum EnumPrivate As Short : Entry : End Enum

        Private Shadows Enum EnumPrivateShadows As Short : Entry : End Enum

        Friend Enum EnumFriend As Short : Entry : End Enum

        Friend Shadows Enum EnumFriendShadows As Short : Entry : End Enum

        Protected Enum EnumProtected As Short : Entry : End Enum

        Protected Shadows Enum EnumProtectedShadows As Short : Entry : End Enum

        Protected Friend Enum EnumProtectedFriend As Short : Entry : End Enum

        Protected Friend Shadows Enum EnumProtectedFriendShadows As Short : Entry : End Enum

        Public Enum EnumPublic As Short : Entry : End Enum

        Public Shadows Enum EnumPublicShadows As Short : Entry : End Enum

#End Region

    End Class

#End Region

End Class

#End Region

#Enable Warning ' Re-enable all warnings.