using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

string[] lang = new string[] { "cs", "sql", "vb", "java", "py", "c", "cpp", "js", "html", "txt", "all" };
var rootCommand = new RootCommand("Root command for file Bundler CLI");
var bundleCommand = new Command("bundle", "Bundle code files to a single file");
var outputOption = new Option<FileInfo>("--output", "File path and name");
var languageOption = new Option<string>("--language", "An option that must be one of the values of a static list")
    .FromAmong("cs", "sql", "vb", "java", "py", "c", "cpp", "js", "html", "txt", "all");
languageOption.IsRequired = true;
var noteOption = new Option<bool>("--note", "File path and name");
var sortOption = new Option<string>("--sort", "The order of copying the code files, according to the alphabet of the file name or according to the type of code.")
    .FromAmong("name", "type");
var removeOption = new Option<bool>("--remove", "Do delete empty lines.");
var authorOption = new Option<string>("--author", "Registering the name of the creator of the file.");
var createRspCommand = new Command("create-rsp", "Create response file");

outputOption.AddAlias("-o");
languageOption.AddAlias("-l");
noteOption.AddAlias("-n");
sortOption.AddAlias("-s");
removeOption.AddAlias("-r");
authorOption.AddAlias("-a");

rootCommand.AddCommand(createRspCommand);
rootCommand.AddCommand(bundleCommand);
bundleCommand.AddOption(outputOption);
bundleCommand.AddOption(languageOption);
bundleCommand.AddOption(noteOption);
bundleCommand.AddOption(sortOption);
bundleCommand.AddOption(removeOption);
bundleCommand.AddOption(authorOption);

bundleCommand.SetHandler((output, language, note, sort, remove, author) =>
{
    string outputPath = output.FullName;

    DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
    FileInfo[] files = directoryInfo.GetFiles();
    DirectoryInfo[] directory = directoryInfo.GetDirectories("*.", SearchOption.AllDirectories);

    foreach (DirectoryInfo dir in directory)
    {
        if (dir.Name == "bin" || dir.Name == "debug")
            continue;
        FileInfo[] tempFile = dir.GetFiles();
        FileInfo[] file2 = new FileInfo[files.Length];
        for (int k = 0; k < files.Length; k++)
        {
            file2[k] = files[k];
        }
        files = new FileInfo[file2.Length + tempFile.Length];
        int i = 0;
        for (i = 0; i < file2.Length; i++)
        {
            files[i] = file2[i];
        }
        for (int j = 0; j < tempFile.Length && i < files.Length; i++, j++)
        {
            files[i] = tempFile[j];
        }
    }

    if (sort == "type")
    {
        files = files.OrderBy(file => Path.GetExtension(file.Name)).ToArray();
    }
    else
    {
        files = files.OrderBy(file => Path.GetFileNameWithoutExtension(file.Name)).ToArray();
    }

    int count = 0;

    foreach (FileInfo file in files)
    {
        if (language == "all")
        {
            bool flag = false;
            for (int i = 0; i < lang.Length; i++)
            {
                if (file.Extension == $".{lang[i]}")
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(outputPath))
                    {
                        int countRemove = 0;
                        string[] fileContent = File.ReadAllLines(file.FullName);
                        if (remove)
                        {
                            fileContent = File.ReadAllLines(file.FullName).Where(line => line != string.Empty).ToArray();
                        }
                        if (author != "" && count < 1)
                        {
                            writer.WriteLine($"Author: {author} \n");
                            count++;
                        }
                        if (note)
                        {
                            writer.WriteLine("//" + file.FullName + "\n" + file.Name + "\n");
                        }
                        for (int i = 0; i < fileContent.Length - countRemove; i++)
                        {
                            writer.WriteLine(fileContent[i]);
                        }
                        writer.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to file: {ex.Message}");
                }
            }
        }

        else

        if (file.Extension == $".{language}")
        {
            try
            {
                using (StreamWriter writer = File.AppendText(outputPath))
                {
                    int countRemove = 0;
                    string[] fileContent = File.ReadAllLines(file.FullName);
                    if (remove)
                    {
                        fileContent = File.ReadAllLines(file.FullName).Where(line => line != string.Empty).ToArray();
                    }
                    if (author != "" && count < 1)
                    {
                        writer.WriteLine($"Author: {author} \n");
                        count++;
                    }
                    if (note)
                    {
                        writer.WriteLine("//" + file.FullName + "\n" + file.Name + "\n");
                    }
                    for (int i = 0; i < fileContent.Length - countRemove; i++)
                    {
                        writer.WriteLine(fileContent[i]);
                    }
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }

    Console.WriteLine("Operation completed successfully.");
}, outputOption, languageOption, noteOption, sortOption, removeOption, authorOption);


createRspCommand.SetHandler(() =>
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Enter name response-file");
    Console.ResetColor();
    string name = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Enter name of bundle file");
    Console.ResetColor();
    string bundleName = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose which language you want to include");
    Console.ResetColor();
    string language = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose if you want to include the name of the file before the");
    Console.ResetColor();
    string note = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Do you want to sort by type of files?");
    Console.ResetColor();
    string sort = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Do you want remove empty lines?");
    Console.ResetColor();
    string remove = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Do you want enter the name of the author? if you want enter the name if not enter no");
    Console.ResetColor();
    string author = Console.ReadLine();
    using (StreamWriter writer = File.AppendText(name))
    {
        writer.WriteLine($"bundle \n -o \n {bundleName} \n -l \n {language}");
        if (note == "yes")
        {
            writer.WriteLine("-n");
        }
        if (sort == "yes")
        {
            writer.WriteLine("-s \n type");
        }
        if (remove == "yes")
        {
            writer.WriteLine(" -r");
        }
        if (author != "no")
        {
            writer.WriteLine($"-a \n {author}");
        }
    };
});

rootCommand.InvokeAsync(args);