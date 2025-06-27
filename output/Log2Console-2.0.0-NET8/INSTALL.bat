@echo off
echo.
echo ===============================================
echo   Log2Console 2.0.0 - .NET 8 Edition
echo   Installation and Setup Script
echo ===============================================
echo.
echo Original Creator: Rémy Baudet (Statyk7)
echo .NET 8 Upgrade: 2024
echo.

REM Check if .NET 8 Desktop Runtime is installed
echo Checking for .NET 8 Desktop Runtime...
dotnet --list-runtimes | findstr "Microsoft.WindowsDesktop.App 8." >nul
if %errorlevel% equ 0 (
    echo ✓ .NET 8 Desktop Runtime found
) else (
    echo ✗ .NET 8 Desktop Runtime not found
    echo.
    echo Please install .NET 8 Desktop Runtime from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    echo Select: ".NET Desktop Runtime 8.0.x" for your architecture
    echo.
    pause
    exit /b 1
)

echo.
echo Creating desktop shortcut...
set "desktop=%USERPROFILE%\Desktop"
set "target=%~dp0Log2Console.exe"
set "shortcut=%desktop%\Log2Console 2.0.exe.lnk"

REM Create shortcut using PowerShell
powershell -Command "$WshShell = New-Object -comObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('%shortcut%'); $Shortcut.TargetPath = '%target%'; $Shortcut.WorkingDirectory = '%~dp0'; $Shortcut.Description = 'Log2Console 2.0 - .NET 8 Edition'; $Shortcut.Save()"

if exist "%shortcut%" (
    echo ✓ Desktop shortcut created successfully
) else (
    echo ! Could not create desktop shortcut
)

echo.
echo ===============================================
echo   Installation Complete!
echo ===============================================
echo.
echo Next steps:
echo 1. Run Log2Console from the desktop shortcut
echo 2. Configure receivers for your logging framework
echo 3. Refer to RELEASE_NOTES.md for configuration examples
echo.
echo For Serilog users:
echo   - Add Serilog.Sinks.Udp package to your project
echo   - Configure UDP output to localhost:7071
echo   - See docs/SerilogConfiguration.md for examples
echo.
echo Would you like to start Log2Console now? (Y/N)
set /p start="Enter your choice: "
if /i "%start%"=="Y" (
    echo Starting Log2Console...
    start "" "%target%"
)

echo.
echo Thank you for using Log2Console!
pause
