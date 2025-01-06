# 🛠️ File Bundler CLI

## 📋 Overview

The **File Bundler CLI** is a command-line application built in C# that bundles multiple code files into a single file.

It supports various programming languages and includes options to customize the output, such as sorting files, removing empty lines, and adding metadata.

## 🚀 Features

- Combine multiple code files into a single file.
- Support for a variety of languages, including `C#`, `SQL`, `Java`, `Python`, and more.
- Options for file sorting by name or type.
- Remove empty lines for cleaner output.
- Add author metadata to the bundled file.
- Automatically include file paths as comments.
- Customizable response file generation.

## 🔧 Supported Languages

The CLI supports the following languages:
- `cs` (C#)
- `sql` (SQL)
- `vb` (VB.NET)
- `java` (Java)
- `py` (Python)
- `c` (C)
- `cpp` (C++)
- `js` (JavaScript)
- `html` (HTML)
- `txt` (Plain Text)
- `all` (All supported file types)

## 📦 Installation

1. Clone this repository.
2. Ensure you have .NET SDK installed on your machine.
3. Build the project using the following command:
   ```
   dotnet build
   ```
   
## 🕹️ How to Use

### 🔗 Basic Commands

**1. Bundle Command**

The `bundle` command allows you to combine files into a single output file.

**Syntax:**
```
bundle --output <output-file> --language <language> [options]
```

**Options:**
**Option**	**Alias**	 **Description**
`--output`	   `-o`	     File path and name for the output file.
`--language`	 `-l`      Language to include (cs, java, etc.). Required.
`--note`	     `-n`	     Add file paths and names as comments in the output.
`--sort`	     `-s`	     Sort files by `name` or `type`.
`--remove`     `-r`	     Remove empty lines from the files.
`--author`	   `-a`	     Include the author's name in the output file metadata.

**Example:**
```
bundle -o bundled.cs -l cs -n -s name -r -a "Your Name"
```

**2. Create Response File**
The `create-rsp` command generates a response file for running predefined `bundle` commands.

**Syntax:**
  ```
    create-rsp
  ```

The CLI will prompt for:

Response file name.
Bundle file name.
Language to include.
Whether to include file names as comments.
File sorting preference.
Whether to remove empty lines.
Author's name.
Example:
bash
Copy code
create-rsp
You will then input the required details interactively.

### 🌟 Example Run:
To bundle all `.cs` files in the current directory and its subdirectories:

```
  bundle --output combined.cs --language cs --note --sort type --remove --author "Jane Doe"
```

To create a response file for a custom bundle:
```
  create-rsp
```
## 🧠 Code Explanation

### Key Components:

**1. File Gathering:**

- The program scans the current directory and its subdirectories to collect all files.
- Filters out system directories like `bin` and `debug`.
  
**2. Sorting:**

- Sorts files alphabetically by name or by file type based on the `--sort` option.
  
**3. Language Filtering:**

- Includes only files with extensions matching the specified `--language` option.
- If all is selected, `all` supported languages are included.
  
**4. Output File:**

- Writes the filtered and processed content to the specified output file.
- Includes optional metadata like author name and file paths.

**5. Response File:**

- Generates `.rsp` files for pre-configured commands.
  
### Error Handling:

Graceful error handling ensures the program reports issues (e.g., file write errors) without crashing.

## 💻 Example Directory Structure
Assume the following directory structure:

```
/project-directory
  ├── file1.cs
  ├── file2.java
  ├── subdirectory
  │   ├── file3.py
  │   ├── file4.cs
  └── bin
      ├── temp.cs
```

Running:

```
bundle -o combined.txt -l cs -s name -r
```

Will produce:

```
// Subdirectory/file4.cs
class Example { ... }
// file1.cs
class Main { ... }
```

## ❗ Notes

- Ensure the CLI has permission to write to the output file path.
- Unsupported languages are skipped silently.
- Empty lines are only removed if the `--remove` option is specified.

## 🛠️ Dependencies

- `.NET 6.0` or higher
- `System.CommandLine` library

## 🤝 Contributing
Feel free to submit issues or pull requests for enhancements or bug fixes!
