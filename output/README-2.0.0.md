# Log2Console 2.0.0 - .NET 8 Edition Release

## 📦 Release Information
- **Version**: 2.0.0 (.NET 8)
- **Release Date**: June 27, 2025
- **Original Creator**: Rémy Baudet (Statyk7)
- **Framework Upgrade**: Migrated from .NET Framework 4.0 to .NET 8

## 🎯 What's Included

### MSI Installer
- **File**: `Log2Console-2.0.0-NET8.msi` (610 KB)
- **Features**: Complete installation with Start Menu and Desktop shortcuts
- **Requirements**: .NET 8 Desktop Runtime
- **Installation**: Double-click to install, includes uninstaller

### Portable Distribution
- **File**: `Log2Console-2.0.0-NET8.zip` (6.5 MB)
- **Contents**: Complete portable application with all dependencies
- **Usage**: Extract and run `Log2Console.exe`

### Folder Distribution
- **Folder**: `Log2Console-2.0.0-NET8/`
- **Contents**: Ready-to-run application with documentation

## 🚀 Major Improvements

### Framework Modernization
- ✅ **Upgraded to .NET 8 LTS** for long-term support and performance
- ✅ **Modern SDK-style projects** for improved build performance
- ✅ **Enhanced compatibility** with Windows 10/11
- ✅ **Future-proof architecture** ensuring continued support

### Technical Enhancements
- ✅ **JSON-based settings** (replacing obsolete binary serialization)
- ✅ **Windows Compatibility Pack** for legacy API support
- ✅ **Conditional compilation** for .NET 8+ compatibility
- ✅ **Maintained backward compatibility** for existing configurations

### Enhanced Logging Support
- ✅ **Improved Serilog integration** with JSON formatting
- ✅ **UDP/TCP receiver enhancements** for modern .NET applications
- ✅ **Cross-platform logging compatibility** (.NET Core, .NET 5+)

## 📋 System Requirements

### Minimum Requirements
- **Operating System**: Windows 10 (version 1809) or later
- **Runtime**: .NET 8 Desktop Runtime
- **Memory**: 512 MB RAM
- **Storage**: 50 MB available space

### Recommended
- **Operating System**: Windows 11
- **Memory**: 1 GB RAM or more
- **Storage**: 100 MB available space

## 🛠 Installation Options

### Option 1: MSI Installer (Recommended)
1. Download `Log2Console-2.0.0-NET8.msi`
2. Double-click to install
3. Follow the installation wizard
4. Launch from Start Menu or Desktop

### Option 2: Portable Application
1. Download `Log2Console-2.0.0-NET8.zip`
2. Extract to your preferred location
3. Run `Log2Console.exe`

### .NET 8 Runtime Installation
If .NET 8 Desktop Runtime is not installed:
1. Visit: https://dotnet.microsoft.com/download/dotnet/8.0
2. Download ".NET Desktop Runtime 8.0.x" for your architecture
3. Install and restart

## 📖 Quick Start Guide

### For Serilog Users
```csharp
// Install: dotnet add package Serilog.Sinks.Udp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Udp("localhost", 7071, new JsonFormatter())
    .CreateLogger();

Log.Information("Hello from Log2Console!");
```

### Log2Console Configuration
1. Open Log2Console
2. Go to **Settings → Receivers**
3. Click **Add** → Select **UDP Receiver**
4. Set **Port**: `7071`
5. Click **OK** and **Start**

## 🏗 Build Information

### Development Environment
- **Build Tool**: .NET 8 SDK
- **Installer**: WiX Toolset v6.0.1
- **Target Framework**: net8.0-windows
- **Architecture**: AnyCPU

### Project Structure
```
src/
├── Log2Console/          # Main application
├── ICSharpCode.TextEditor/   # Text editor component
├── RichTextBoxLinks/     # Enhanced RichTextBox
├── Test/                 # Test projects
└── LogFmwk.sln          # Solution file
```

## 📄 Documentation

### Included Files
- `README.md` - Main documentation
- `LICENSE.md` - BSD License with proper attribution
- `RELEASE_NOTES.md` - Detailed change log
- `UPGRADE_SUMMARY.md` - Technical upgrade details

### Online Resources
- [Original Project Attribution](https://github.com/log2console) (if available)
- [.NET 8 Documentation](https://docs.microsoft.com/dotnet)
- [Serilog Documentation](https://serilog.net)

## 🙏 Credits

### Original Work
- **Creator**: Rémy Baudet (Statyk7)
- **Years**: 2007-2010
- **License**: BSD License
- **Achievement**: Created an essential tool for .NET developers

### .NET 8 Upgrade
- **Year**: 2025
- **Scope**: Complete framework modernization
- **Goal**: Preserve functionality while adding modern compatibility
- **Result**: Zero-compromise upgrade maintaining all original features

## 📋 Technical Notes

### Compatibility
- **Legacy**: Maintains compatibility with existing log4net configurations
- **Modern**: Full support for .NET Core, .NET 5+, and .NET 8 applications
- **Cross-platform**: Works with logs from Linux and macOS .NET applications

### Performance
- **Startup**: Faster application startup with .NET 8
- **Memory**: Improved memory management
- **Responsiveness**: Enhanced UI responsiveness

### Security
- **Serialization**: Modern JSON serialization replaces obsolete binary format
- **Dependencies**: Updated to latest secure versions
- **Runtime**: Benefits from .NET 8 security improvements

---

**Log2Console 2.0.0** - Bringing a classic .NET tool into the modern era while honoring its original creator's vision.

*Original creator: Rémy Baudet (Statyk7) | .NET 8 upgrade: 2025*
