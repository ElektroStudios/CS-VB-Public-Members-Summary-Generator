# CS-VB Public Members Summary Generator Change Log 📋

## v1.1 *(current)* 🆕

### Fixes
- Visual Basic: Types, delegates and enums declared without an access modifier at file or namespace level are `Friend`, so they are no longer listed as public.
- Visual Basic: Static constructors (`Shared Sub New`) are no longer listed.
- Visual Basic: `EditorBrowsable(Never)` is now honored for events, operators, delegates and enums.
- Visual Basic: Array types declared on the identifier of a field or parameter (`Names() As String`, `ParamArray e() As Integer`) are now listed with their array brackets.
- Visual Basic: Top-level and namespace-level delegates, and events declared without `As` clause or parameters, no longer make the whole file fail.
- C#: Records and record structs are now listed with their own name, including the properties generated from their positional parameters.
- The line breaks of the generated region now match the line breaks of the file, so files no longer end up with mixed line endings.
- When the summary region is removed from a file, the blank lines that were added with it are removed too, restoring the original file.
- Files containing more than one summary region are now left untouched and counted only as failed.
- The temporary file is now removed when replacing the original file fails.
- Source files inside bin/obj subdirectories and more kind of generated files (*.g.cs, *.g.i.cs, *.generated.*) are now skipped.

### Improvements
- Visual Basic: `Declare` statements (P/Invoke) are now listed as methods.
- C#: Primary constructors of records, classes and structs are now listed as constructors.
- C#: Public indexers are now listed as properties.

## v1.0
Initial Release.