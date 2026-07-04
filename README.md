<!-- Common Project Tags:
api-analysis 
c#
c-sharp 
cli 
cli-tool 
code-analysis
codeanalysis
command-line 
command-line-tool 
console-applications 
csharp 
csharp-language 
desktop-app 
desktop-application 
dotnet 
dotnet-core 
dotnetcore 
netcore 
netframework 
netframework48 
public-apis 
roslyn
roslyn-analyzer
source-code-analysis 
source-files 
summarizer 
textfile 
tool 
tools 
vbnet 
visual-studio 
visualbasic-language 
visualbasicdotnet 
visualstudio 
vs-code
vscode
windows 
windows-app 
windows-application 
windows-applications 
 -->

<div align="center">
  <img src="/Images/App.ico" width="150" alt="CS-VB_PMS_Gen Logo">
  
  <h1>CS-VB Public Members Summary Generator</h1>
  <h1>(CS-VB_PMS_Gen.exe)</h1>

### Command-line utility to generate and integrate a summary of public API members in your C# or VB.NET source-code files.

</div>

------------------

## 👋 Introduction

**CS-VB Public Members Summary Generator** is a command-line interface (CLI) tool designed to analyze C# (`*.cs`) and Visual Basic .NET (`*.vb`) source-code files to identify and document their public API structure. 

The application scans a given directory to process supported source-code files, identifying all publicly accessible members and inserting a clean overview inside a designated `#region` directive on top of each file using a safe, atomic file replacement operation.

## 👌 Features

- Identifies and summarizes public constructors, properties, fields, events, methods, operators, delegates, and enumerations.
- Automatically updates existing summary `#region` directives, or completely removes them if no public members remain in the file.
- Supports and preserves UTF-8, UTF-16, and UTF-32 file encodings in both Little Endian and Big Endian, with or without BOM.
- Files are read and rewritten preserving their exact text encoding, supporting UTF-8, UTF-16, and UTF-32 in both Little Endian and Big Endian byte orders, with or without a Byte Order Mark (BOM).
- Automatically skips empty files, white-space-only files, Visual Studio auto-generated files, and files with unrecognized or unsupported text encodings.
- Performs strict, paranoid validation checks, comparing line counts and content hashes, to guarantee all code outside the target summary `#region` remains byte-for-byte identical and is not modified or lost by mistake.
- Updates your source-code files safely by writing changes to a temporary file first, using an atomic replacement with a temporary `.bak` file to ensure data integrity.
- Supports optional recursive scanning (`-r` or `--recurse` flags).
- Provides a safe dry-run environment (`-t` or`--test` flags) to simulate changes without modifying your actual files.
- Uses [Natural sort order](https://en.wikipedia.org/wiki/Natural_sort_order) to process files, making the program execution sequence predictable and easy to follow.

## 🖼️ Screenshots

![screenshot1](/Images/screenshot1.png)

![screenshot2](/Images/screenshot2.png)

## 📝 Requirements

- Microsoft Windows OS.

## 🤖 Getting Started

Download the latest release by clicking [here](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/releases/latest) and start using it!.

## ⚙️ Usage

```bash
CS-VB_PMS_Gen.exe <directory_path> [options]
```

### Arguments

* **`directory_path`**  
  The path to the root directory containing the `*.cs` or `*.vb` source-code files to process.

### Options

* **`-r, --recursive`**  
  If specified, processes all subdirectories recursively.
* **`-t, --test`**  
  If specified, runs the application in Test Mode (dry-run). It simulates the entire parsing and validation process without writing any changes to disk.
---

### 🚀 Examples

Generate and insert public member summaries only for files in the specified root directory:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution"
```

Recursively update all source files across all subdirectories:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r
```

Run a safe simulation dry-run recursively to validate the execution path without modifying code:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r --test
```

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](/Docs/CHANGELOG.md).

## ⚠️ Disclaimer:

This Work (the repository and the content provided in) is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Work or the use or other dealings in the Work.

This Work has no affiliation, approval or endorsement by the author(s) of the third-party libraries used by this Work.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This work is distributed for educational purposes and without any profit motive. However, if you find value in my efforts and wish to support and motivate my ongoing work, you may consider contributing financially through the following options:

<br></br>
<p align="center"><img src="/Images/github_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Becoming my sponsor on Github:</h3>
<p align="center">You can show me your support by clicking <a href="https://github.com/sponsors/ElektroStudios/">here</a>, <br align="center">contributing any amount you prefer, and unlocking rewards!</br></p>
<br></br>

<p align="center"><img src="/Images/paypal_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Making a Paypal Donation:</h3>
<p align="center">You can donate to me any amount you like via Paypal by clicking <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY">here</a>.</p>
<br></br>

<p align="center"><img src="/Images/envato_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Purchasing software of mine at Envato's Codecanyon marketplace:</h3>
<p align="center">If you are a .NET developer, you may want to explore '<b>DevCase Class Library for .NET</b>', <br align="center">a huge set of APIs that I have on sale. Check out the product by clicking <a href="https://codecanyon.net/item/elektrokit-class-library-for-net/19260282">here</a></br><br align="center"><i>It also contains all piece of reusable code that you can find across the source code of my open source works.</i></p>
<br></br>

<h2 align="center"><u>Your support means the world to me! Thank you for considering it!</u> 👍</h2>
