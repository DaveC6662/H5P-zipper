/*
 * H5PZipper - A utility to compress H5P content into a valid .h5p file.
 * 
 * Author: Davin Chiupka
 * License: GNU General Public License v3.0 (GPL-3.0)
 * Date: February 12, 2025
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/>.
*/


using System.IO.Compression;

namespace H5Pzipper;

internal class H5Pzipper
{
    /// <summary>
    /// Compresses an H5P folder into a valid .h5p file.
    /// </summary>
    /// <param name="sourceFolder">Path to the folder containing H5P content.</param>
    public static void CreateH5PPackage(string sourceFolder)
    {
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Error: Source folder '{sourceFolder}' does not exist.");
            return;
        }

        var folderName = new DirectoryInfo(sourceFolder).Name;
        var outputZipFile = Path.Combine(sourceFolder, $"{folderName}.h5p");

        if (!outputZipFile.EndsWith(".h5p", StringComparison.OrdinalIgnoreCase))
        {
            outputZipFile += ".h5p";
        }

        if (File.Exists(outputZipFile))
        {
            Console.WriteLine($"Warning: File '{outputZipFile}' already exists. Overwriting...");
            File.Delete(outputZipFile);
        }

        Console.WriteLine($"Compressing '{sourceFolder}' into '{outputZipFile}'...");

        using (var zipToCreate = new FileStream(outputZipFile, FileMode.Create))
        using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create, leaveOpen: false))
        {
            foreach (var filePath in Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories))
            {
                if (filePath.Equals(outputZipFile, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var relativePath = Path.GetRelativePath(sourceFolder, filePath).Replace("\\", "/");
                archive.CreateEntryFromFile(filePath, relativePath, CompressionLevel.Optimal);
                Console.WriteLine($"Added: {relativePath}");
            }
        }

        Console.WriteLine($"\nSuccessfully created: {outputZipFile}\n");
    }

    private static void ShowHelp()
    {
        Console.Clear();
        Console.WriteLine(@"
===============================================================
                        H5P Zipper Help
===============================================================

This tool allows you to compress an extracted H5P folder into 
a valid .h5p package.

USAGE:
1. Extract your H5P file.
2. When prompted, enter the path to the extracted H5P folder.
3. The tool will generate a compressed .h5p file in the same 
   directory as the provided folder.

NOTES:
- Ensure the folder contains a valid H5P structure 
  before compressing.
- The output file will overwrite any existing .h5p file 
  with the same name in the same directory.

For more details, refer to the official H5P documentation:
https://h5p.org/documentation

===============================================================
");
    }

    private static void ShowHeader()
    {
        Console.WriteLine("""

                          ██╗  ██╗███████╗██████╗     ███████╗██╗██████╗ ██████╗ ███████╗██████╗ 
                          ██║  ██║██╔════╝██╔══██╗    ╚══███╔╝██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
                          ███████║███████╗██████╔╝      ███╔╝ ██║██████╔╝██████╔╝█████╗  ██████╔╝
                          ██╔══██║╚════██║██╔═══╝      ███╔╝  ██║██╔═══╝ ██╔═══╝ ██╔══╝  ██╔══██╗
                          ██║  ██║███████║██║         ███████╗██║██║     ██║     ███████╗██║  ██║
                          ╚═╝  ╚═╝╚══════╝╚═╝         ╚══════╝╚═╝╚═╝     ╚═╝     ╚══════╝╚═╝  ╚═╝
                           ======================================================================
                                     H5P Zipper - Create Valid .h5p Packages
                                            By: Davin Chiupka
                          =======================================================================

                          """);
    }

    private static void Main()
    {
        ShowHeader();
        while (true)
        {
            Console.WriteLine("1 - Compress an H5P folder");
            Console.WriteLine("2 - Help");
            Console.WriteLine("3 - Exit");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    Console.Write("\nEnter the folder path containing H5P content: ");
                    var sourceFolder = Console.ReadLine()?.Trim() ?? "";

                    while (!Directory.Exists(sourceFolder))
                    {
                        Console.Write("Invalid folder. Please enter a valid path: ");
                        sourceFolder = Console.ReadLine()?.Trim() ?? "";
                    }

                    CreateH5PPackage(sourceFolder);
                    break;

                case "2":
                    ShowHelp();
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please select a valid number.");
                    break;
            }
        }
    }
}