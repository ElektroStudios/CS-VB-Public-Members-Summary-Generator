
' Test public class with an inner interface.
' The generator should include the public members of the inner interface in the summary.

#Disable Warning ' Disable all warnings in this file to test the generator.

Public Class TestClassPublicWithInnerInterface

    Public Interface TestInterfacePublic

        Sub TestMethodPublicNoModifier()

        Shadows Sub TestMethodShadows()

        Overloads Sub TestMethodOverloads()

    End Interface

End Class

#Enable Warning ' Re-enable all warnings.
