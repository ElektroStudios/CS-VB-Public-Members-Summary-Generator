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
  
  <h1>CS-VB Public Members Summary Generator<br>(CS-VB_PMS_Gen.exe)</h1>

### Command-line utility to generate and integrate a summary of public API members<br>in your C# or VB.NET source-code files.

</div>

------------------

<p align="center">
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/network/members"><img src="https://img.shields.io/github/forks/ElektroStudios/CS-VB-Public-Members-Summary-Generator.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%F0%9F%8D%B4%20Forks" alt="Forks"></a>&nbsp;
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/graphs/contributors"><img src="https://img.shields.io/github/contributors/ElektroStudios/CS-VB-Public-Members-Summary-Generator.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%F0%9F%91%A5%20Contributors" alt="Contributors"></a>&nbsp;
    <a href="https://github.com/ElektroStudios"><img src="https://img.shields.io/github/followers/ElektroStudios.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%F0%9F%91%A4%20Followers" alt="Followers"></a>&nbsp;
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/watchers"><img src="https://img.shields.io/github/watchers/ElektroStudios/CS-VB-Public-Members-Summary-Generator.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%F0%9F%91%80%20Watchers" alt="Watchers"></a>
    <a href="https://github.com/sponsors/ElektroStudios"><img src="https://img.shields.io/github/sponsors/ElektroStudios.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%F0%9F%92%96%20Sponsors" alt="Sponsors"></a>&nbsp;
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/stargazers"><img src="https://img.shields.io/github/stars/ElektroStudios/CS-VB-Public-Members-Summary-Generator.svg?style=social&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPjwvc3ZnPg%3D%3D&label=%E2%AD%90%20Stars" alt="Stars"></a>
  <br>
  <br>
    <a href="https://ci.appveyor.com/project/ElektroStudios/CS-VB-Public-Members-Summary-Generator"><img src="https://ci.appveyor.com/api/projects/status/github/ElektroStudios/CS-VB-Public-Members-Summary-Generator?svg=true" alt="AppVeyor CI"></a>
    <a href="https://sonarcloud.io/summary/new_code?id=ElektroStudios_CS-VB-Public-Members-Summary-Generator"><img src="https://sonarcloud.io/api/project_badges/measure?project=ElektroStudios_CS-VB-Public-Members-Summary-Generator&metric=alert_status" alt="SonarCloud Quality Gate"></a>
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/releases/latest"><img src="https://img.shields.io/github/v/release/ElektroStudios/CS-VB-Public-Members-Summary-Generator" alt="Latest Release"></a>
    <a href="https://learn.microsoft.com/en-us/dotnet/visual-basic/"><img src="https://img.shields.io/badge/language-VB.NET-purple.svg?logo=visualbasic" alt="Language"></a>
    <a href="https://learn.microsoft.com/en-us/windows/apps/"><img src="https://img.shields.io/badge/platform-Windows-lightgray.svg" alt="Platform"></a>
    <a href="LICENSE"><img src="https://img.shields.io/github/license/ElektroStudios/CS-VB-Public-Members-Summary-Generator" alt="License"></a>
  <br>
    <a href="https://api.github.com/repos/ElektroStudios/CS-VB-Public-Members-Summary-Generator/zipball"><img src="https://img.shields.io/github/repo-size/ElektroStudios/CS-VB-Public-Members-Summary-Generator" alt="Repo size"></a>
    <a href="https://somsubhra.github.io/github-release-stats/?username=ElektroStudios&repository=CS-VB-Public-Members-Summary-Generator"><img src="https://img.shields.io/github/downloads/ElektroStudios/CS-VB-Public-Members-Summary-Generator/total.svg?label=total%20downloads" alt="Total Downloads"></a>
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/commits"><img src="https://img.shields.io/github/last-commit/ElektroStudios/CS-VB-Public-Members-Summary-Generator" alt="Last commit"></a>
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/pulls"><img src="https://img.shields.io/github/issues-pr/ElektroStudios/CS-VB-Public-Members-Summary-Generator" alt="Pull Requests"></a>
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/issues?q=is%3Aissue+is%3Aopen"><img src="https://img.shields.io/github/issues/ElektroStudios/CS-VB-Public-Members-Summary-Generator?color=blue" alt="Open Issues"></a><a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/issues?q=is%3Aissue+is%3Aclosed"><img src="https://img.shields.io/github/issues-closed/ElektroStudios/CS-VB-Public-Members-Summary-Generator?label=&color=28a745" alt="Closed Issues"></a>
    <a href="https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/pulse"><img src="https://img.shields.io/badge/maintained-yes-green.svg" alt="Maintenance"></a>
  <br>
  <br>
    <a href="https://en.wikipedia.org/wiki/Spanish_Empire"><img src="https://img.shields.io/badge/Made_in-Spain_%F0%9F%87%AA%F0%9F%87%B8-AA151B?style=flat" alt="Made in Spain"></a>
</p>

------------------

## 👋 Introduction

**CS-VB Public Members Summary Generator** is a command-line interface (CLI) tool designed to analyze C# (`*.cs`) and Visual Basic .NET (`*.vb`) source-code files to identify and document their public API structure. 

The application scans a given directory to process supported source-code files, identifying all publicly accessible members and inserting a clean overview inside a designated `#region` directive on top of each file using a safe, atomic file replacement operation.

## 💡 Motivation

Navigating large .NET codebases can quickly become an overwhelming task when developers need to understand the exposed interface of multiple source files. While full-scale documentation tools exist, they often generate heavy external files or require complex configuration pipelines just to answer a simple question: *What does this specific class actually expose to the outside world?* Checking public constructors, properties, or methods usually forces developers to scroll through thousands of lines of implementation logic or rely constantly on IDE object browsers.

This utility was developed to solve that exact friction point by bringing high-level visibility directly into the source files. By automatically parsing code structures and injecting a clean, concise summary of public members into a dedicated header `#region`, it allows developers to grasp a file's entire public API surface at a single glance. Designed with strict safety constraints to guarantee code preservation, it serves as an immediate productivity booster for team code reviews, legacy refactoring, and API surface auditing.

##### ⚡ The Real Question
###### Why spend valuable time scrolling through massive implementation details or fighting IDE navigation lag just to discover a class interface, when you can automatically map and embed a clean public API summary at the top of every C# and VB.NET source file?

## 🖼️ Screenshots

![screenshot1](/Images/screenshot1.png)

![screenshot2](/Images/screenshot2.png)

![screenshot3](/Images/screenshot3.png)

![screenshot4](/Images/screenshot4.png)

## 🤖 Features

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

## 📝 Requirements

- Microsoft Windows OS.

## 🚀 Getting Started

Choose the installation method that best fits your workflow. You can either use the tool as a standalone portable executable or install it as a .NET Tool via NuGet.

### Option A: Standalone Executable (Portable)

This is the standard approach if you want a ready-to-use console application without relying on the .NET SDK.

1. Navigate to the [Releases page](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/releases/latest).
2. Download the latest `.zip` archive.
3. Extract the contents to your preferred directory.
4. Open your terminal and run the executable directly.

### Option B: .NET  Tool (NuGet)

If you are a developer with the .NET SDK installed, you can install the package globally. This is highly recommended as it allows you to invoke the tool directly from Visual Studio in the directory of your current solution.

Open your terminal and run the following command:

```bash
dotnet tool install --global CS-VB_Public_Members_Summary_Generator
```

Once installed successfully, you can invoke the tool from anywhere using its command name `CS-VB_PMS_Gen`.

## ⚙️ Usage

```bash
CS-VB_PMS_Gen.exe <directory_path> [options]
```

| Mandatory&nbsp;Arguments | Description |
| :--- | :--- |
| **<code><directory_path></code>** | The path to the root directory containing the `*.cs` or `*.vb` source-code files to process. |

| Options | Description |
| :--- | :--- |
| **<code>&#8209;r</code>**,&nbsp;**<code>&#8209;&#8209;recursive</code>** | Recursively process source-code files in all subdirectories. |
| **<code>&#8209;t</code>**,&nbsp;**<code>&#8209;&#8209;test</code>** | Runs the application in Test Mode (dry-run), simulating the entire process without modifying actual files. |

### 👌 Examples

Generate and insert public member summaries only for files in the specified root directory:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution"
```

Recursively generate and insert public member summaries for files within the specified root directory and its subdirectories:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r
```

Run a safe simulation dry-run recursively to validate the execution path without modifying actual files:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r --test
```

---

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](/Docs/CHANGELOG.md).

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution

This work is distributed for educational purposes and without any profit motive. However, if you find value in my efforts and wish to support and motivate my ongoing work, you may consider contributing financially through the following options:

| Platform | How to Support |
| :---: | :--- |
| <a href="https://github.com/sponsors/ElektroStudios/"><img src="/Images/github_circle.png" width="64"></a> | **[Become my sponsor on GitHub](https://github.com/sponsors/ElektroStudios/)**<br>You can show me your support by contributing any amount you prefer, and unlocking rewards! |
| <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY"><img src="/Images/paypal_circle.png" width="64"></a> | **[Make a PayPal Donation](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY)**<br>You can donate to me any amount you like via PayPal. |
| <a href="https://codecanyon.net/item/elektrokit-class-library-for-net/19260282"><img src="/Images/envato_circle.png" width="64"></a> | **[Purchase my software at Envato's CodeCanyon](https://codecanyon.net/item/elektrokit-class-library-for-net/19260282)**<br>If you are a .NET developer, you may want to explore **DevCase Class Library for .NET**, a huge set of APIs I have on sale. *It also contains all pieces of reusable code that you can find across the source code of my open-source works.* |

<br>
<div align="center">
  <b>Your support means the world to me! Thank you for considering it! 🤗💗</b>
</div>

------------------

## 🏆 Credits

This work relies on the following technologies, libraries or resources: 

 - [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
 - [.NET Core](https://dotnet.microsoft.com/en-us/download/dotnet-core)

## ⚠️ Disclaimer

This software and its associated repository are provided strictly on an "as is" basis, without warranties of any kind, whether express or implied. This includes, but is not limited to, any implied warranties of merchantability, reliability, or fitness for a particular purpose.

The authors and copyright holders assume no liability for any direct, indirect, incidental, or consequential damages—including data loss or system errors—arising from the use, misuse, or inability to use this software. You are solely responsible for determining the appropriateness of using this tool and assume all associated risks.

Furthermore, this project operates entirely independently. The utilization of any third-party libraries or components within this software does not imply any affiliation with, or endorsement or approval by, their respective original authors.

This software may interact with third-party services, websites, or platforms. It is the user's sole responsibility to ensure that such use complies with the applicable terms of service, laws, and regulations. The authors do not endorse, and are not responsible for, any misuse of this software to violate third-party terms of service or applicable law.

By using this software, you agree to indemnify and hold harmless the authors from any claims, damages, or liabilities arising from your use or misuse of it.

This project is licensed under the **Apache License, Version 2.0**. See the  [License](./LICENSE) file for details.