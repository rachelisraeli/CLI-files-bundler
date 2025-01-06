# File Bundler CLI

The **File Bundler CLI** is a command-line application built in C# that bundles multiple code files into a single file.

It supports various programming languages and includes options to customize the output, such as sorting files, removing empty lines, and adding metadata.

## Features

- Combine multiple code files into a single file.
- Support for a variety of languages.
- Options for file sorting by name or type.
- Remove empty lines for cleaner output.
- Add author metadata to the bundled file.
- Automatically include file paths as comments.
- Filters out system directories like bin and debug.
- Customizable response file generation.

## Supported Languages

The CLI supports the following languages:

`cs` (C#), `sql` (SQL), `vb` (VB.NET), `java` (Java), `py` (Python) `c` (C),
`cpp` (C++), `js` (JavaScript), `html` (HTML), `txt` (Plain Text), `all` (All supported file types)

## Installation

1. Clone this repository.
2. Ensure you have .NET SDK installed on your machine.
3. Build the project using the following command:
   
   ```
   dotnet build
   ```
   
## How to Use

### Basic Commands

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

### Example Run:
To bundle all `.cs` files in the current directory and its subdirectories:

   ```
     bundle --output combined.cs --language cs --note --sort type --remove --author "Jane Doe"
   ```

To create a response file for a custom bundle:
```
  create-rsp
```
## Code Explanation
  
### Error Handling:

Graceful error handling ensures the program reports issues (e.g., file write errors) without crashing.


## Dependencies

- `.NET 6.0` or higher
- `System.CommandLine` library
