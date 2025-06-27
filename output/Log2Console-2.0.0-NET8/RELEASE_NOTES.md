# Log2Console 2.0.0 - .NET 8 Edition

## Release Information
- **Version**: 2.0.0
- **Release Date**: June 2024
- **Framework**: .NET 8 (Desktop Runtime required)
- **Original Creator**: Rémy Baudet (Statyk7)
- **Migration**: Upgraded from .NET Framework 4.0 to .NET 8

## What's New in 2.0.0

### 🚀 Major Framework Upgrade
- **Upgraded to .NET 8** for modern performance and long-term support
- **Modern SDK-style project files** with improved build performance
- **Enhanced compatibility** with current Windows versions (Windows 10/11)
- **Future-proof architecture** ensuring continued support and updates

### 🔧 Technical Improvements
- **JSON-based settings** replacing obsolete binary serialization for better security
- **Windows Compatibility Pack** integration for legacy Windows API support
- **Conditional compilation** for features not available in modern .NET
- **Improved error handling** and stability
- **Enhanced memory management** and performance optimizations

### 📊 Enhanced Logging Support
- **Improved Serilog support** with structured logging capabilities
- **Modern JSON formatting** for better log parsing and analysis
- **Enhanced NLog compatibility** with .NET 8+ applications
- **Backward compatibility** maintained for existing log4net configurations
- **Better Unicode support** for international applications

### 🛠️ Development Improvements
- **Modern C# language features** enabled where applicable
- **Improved build system** with faster compilation times
- **Better IntelliSense** and debugging experience
- **Enhanced code analysis** and warning detection

## System Requirements

### Minimum Requirements
- **Operating System**: Windows 10 version 1809 or later
- **Runtime**: .NET 8 Desktop Runtime
- **Memory**: 512 MB RAM minimum, 1 GB recommended
- **Disk Space**: 50 MB for application

### Recommended Requirements
- **Operating System**: Windows 11 or Windows 10 version 21H2+
- **Runtime**: Latest .NET 8 Desktop Runtime
- **Memory**: 2 GB RAM or more
- **Disk Space**: 100 MB for application and logs

## Installation Instructions

### 1. Install .NET 8 Desktop Runtime
Before running Log2Console 2.0.0, ensure you have the .NET 8 Desktop Runtime installed:
- Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- Select: ".NET Desktop Runtime 8.0.x" for your architecture (x64 recommended)

### 2. Run Log2Console
- Extract all files from the package to a folder
- Run `Log2Console.exe`
- Your existing settings should be automatically migrated to the new JSON format

## Compatibility Notes

### ✅ Fully Compatible
- **UDP Receivers** - Full compatibility with existing configurations
- **TCP Receivers** - Enhanced performance with .NET 8
- **File Receivers** - Improved file watching and parsing
- **Event Log Receivers** - Windows Event Log integration maintained
- **CSV Receivers** - Enhanced parsing capabilities

### ⚠️ Limited Compatibility (.NET 8+ Only)
- **.NET Remoting Receivers** - Only available when connecting to .NET Framework applications
- **MSMQ Receivers** - Limited to Windows environments with MSMQ installed

### 📝 Configuration Migration
- Settings files are automatically migrated from binary format to JSON
- Backup of original settings is created during first run
- All receiver configurations are preserved
- UI preferences and layouts are maintained

## New Features for Modern Applications

### Enhanced Serilog Integration
```csharp
// Example: ASP.NET Core with Serilog to Log2Console
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.Udp("localhost", 7071, new JsonFormatter());
});
```

### Improved Structured Logging
- Better handling of complex objects in log messages
- Enhanced property display and filtering
- Improved search capabilities across structured data

## Breaking Changes

### Removed Features
- **Silverlight support** - Obsolete technology no longer supported
- **Some legacy .NET Framework specific APIs** - Replaced with modern equivalents

### Changed Behavior
- **Settings storage format** - Now uses JSON instead of binary serialization
- **Performance characteristics** - Generally improved, but some legacy optimizations may behave differently

## Known Issues

1. **First-time startup** may take slightly longer due to settings migration
2. **Very old log formats** (pre-2010) may need format adjustments
3. **Some UI themes** may appear slightly different due to .NET 8 rendering changes

## Migration Guide

### From Log2Console 1.x
1. Install .NET 8 Desktop Runtime
2. Run the new version - settings will be automatically migrated
3. Verify your receivers are working correctly
4. Update any custom configurations if necessary

### For Developers
- Update logging libraries to latest versions for best compatibility
- Consider using Serilog for new applications
- Test receiver configurations with the new version

## Support and Resources

### Documentation
- [Serilog Configuration Guide](../docs/SerilogConfiguration.md)
- [NLog Configuration Guide](../docs/NLog.md)
- [Original Documentation](../docs/)

### Getting Help
- Check the documentation for configuration examples
- Review the GitHub repository for known issues
- Ensure .NET 8 Desktop Runtime is properly installed

## Credits and Acknowledgments

### Original Creator
- **Rémy Baudet (Statyk7)** - Original creator and designer of Log2Console
- **Copyright**: © Rémy Baudet 2007-2010

### .NET 8 Migration
- **.NET 8 Upgrade**: 2024
- **Compatibility maintained** with all original features
- **Performance improvements** while preserving functionality

### Special Thanks
- **Microsoft .NET Team** - For excellent migration tools and documentation
- **Community contributors** - For feedback and testing
- **Original Log2Console users** - For years of usage and feature requests

---

**Log2Console continues to be an essential tool for .NET developers, now with modern .NET 8 performance and long-term support.**

For technical support or issues, please refer to the documentation or check the project repository.
