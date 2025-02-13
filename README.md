# H5P Zipper

## Overview
H5Pzipper is a lightweight utility for compressing H5P content folders into valid `.h5p` files. This tool ensures correct file paths for H5P validation and creates a properly formatted package.

## Features
- Converts an H5P folder into a `.h5p` package.
- Ensures correct forward slashes for ZIP file validation.
- Overwrites existing `.h5p` files if necessary.
- Supports command-line usage for ease of operation.

## License
This project is licensed under the **GNU General Public License v3.0 (GPL-3.0)**.

## Installation
### Windows (Standalone Executable)
Download the latest release from [GitHub Releases](https://github.com/DaveC6662/H5P-zipper/releases) and run the executable.

### Building from Source
Ensure you have **.NET SDK 6.0+** installed.

Clone the repository:
```sh
git clone https://github.com/DaveC6662/H5P-zipper.git
cd H5Pzipper
```
Build the project:
```sh
dotnet build -c Release
```

## Usage
### Running the Program
To compress an H5P folder, run the executable from a terminal:
```sh
H5Pzipper.exe
```
Enter the path to your H5P content folder when prompted.

Alternatively, run it via command-line with a folder path:
```sh
H5Pzipper.exe "C:\path\to\h5p-folder"
```

### Linux/macOS
For Linux/macOS, build a self-contained binary:
```sh
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -o ./release-linux
```
Run the binary:
```sh
./release-linux/H5Pzipper
```

## Contributing
Pull requests are welcome! If you encounter issues, feel free to open a ticket in the [Issues](https://github.com/DaveC6662/H5P-zipper/issues) section.

## Author
**Davin Chiupka**

## License
This project is licensed under the [GNU GPL v3](https://www.gnu.org/licenses/gpl-3.0.html).

