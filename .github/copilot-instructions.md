# B2S Backglass Server - Copilot Instructions

## Repository Overview

This repository contains the B2S (Backglass 2nd Screen) Server system for displaying directB2S backglasses in Virtual Pinball. It's a Windows-focused .NET Framework 4.8 project written primarily in VB.NET with some C# components.

### High-Level Information
- **Purpose**: COM server and tools for Virtual Pinball backglass display and management
- **Size**: ~50MB repository with multiple Visual Studio solutions
- **Languages**: VB.NET (primary), C# (secondary)
- **Target Framework**: .NET Framework 4.8 
- **Platform**: Windows x86/AnyCPU
- **Build System**: MSBuild
- **Current Version**: 2.1.4 (see `b2sbackglassserver/b2sbackglassserver/Classes/B2SVersionInfo.vb`)

## Build Instructions

### Prerequisites
- MSBuild (Visual Studio Build Tools 2019+ or Visual Studio)
- .NET Framework 4.8 Developer Pack
- Windows development environment

### Critical Dependency - B2SServerPluginInterface
**ALWAYS build B2SServerPluginInterface.dll first before building the main server solution.** The main B2SBackglassServer project has a hard dependency on this external library.

#### Build B2SServerPluginInterface (Required First Step)
```powershell
# Clone the dependency repository
git clone https://github.com/DirectOutput/B2SServerPluginInterface.git

# Patch for .NET Framework 4.8 compatibility
$csproj = "B2SServerPluginInterface/B2SServerPluginInterface/B2SServerPluginInterface.csproj"
(Get-Content $csproj) -replace '<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>', '<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>' | Set-Content $csproj

# Build the interface
msbuild B2SServerPluginInterface/B2SServerPluginInterface.sln /t:Rebuild /p:Configuration=Debug /p:UICulture=en-US

# Copy to Plugin directory
Copy-Item "B2SServerPluginInterface/B2SServerPluginInterface/bin/Debug/B2SServerPluginInterface.dll" "b2sbackglassserver/b2sbackglassserver/Plugin/"
```

### Build Order and Commands
Build these solutions in order. Each takes 1-5 seconds to complete:

1. **B2S Screen Resolution Identifier**:
   ```cmd
   msbuild b2s_screenresidentifier/B2S_ScreenResIdentifier.sln /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /p:UICulture=en-US
   ```

2. **B2S SetUp Tool**:
   ```cmd
   msbuild B2S_SetUp/B2S_SetUp.sln /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /p:UICulture=en-US
   ```

3. **Main B2S Backglass Server** (requires B2SServerPluginInterface.dll):
   ```cmd
   msbuild b2sbackglassserver/B2SBackglassServer.sln /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /p:UICulture=en-US
   ```

4. **B2S Register Application**:
   ```cmd
   msbuild b2sbackglassserverregisterapp/B2SBackglassServerRegisterApp.sln /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /p:UICulture=en-US
   ```

5. **B2S Window Punch**:
   ```cmd
   msbuild B2SWindowPunch/B2SWindowPunch.sln /t:Rebuild /p:Configuration=Debug /p:UICulture=en-US
   ```

### Supported Build Configurations
- **Debug/Release** (use Debug for development)
- **Platform**: Any CPU (standardized across all projects)
- **Build time**: Complete rebuild takes ~10 seconds total

### Clean Build Process
```cmd
# Clean individual projects by rebuilding with /t:Clean
msbuild [solution].sln /t:Clean /p:Configuration=Debug /p:UICulture=en-US
```

### Validation Steps
- All builds complete without errors (warnings are acceptable)
- Generated executables appear in respective `bin/Debug` or `bin/x86/Debug` directories
- B2SServerPluginInterface.dll must exist in `b2sbackglassserver/b2sbackglassserver/Plugin/` before building main server
- No unit tests exist in this repository

## Project Architecture

### Main Components
1. **b2sbackglassserver/** - Core COM server (DLL) and standalone executable (EXE)
   - `B2SBackglassServer.vbproj` - COM server library
   - `B2SBackglassServerEXE.vbproj` - Standalone executable
   - Key files: `Server.vb`, `Classes/B2SVersionInfo.vb`

2. **b2s_screenresidentifier/** - Screen resolution management tool
   - VB.NET Windows Forms application
   - Manages display settings and configurations

3. **B2S_SetUp/** - Setup utility (C# project)
   - Configuration and installation helper

4. **b2sbackglassserverregisterapp/** - COM registration utility
   - Registers B2S server in Windows registry

5. **B2SWindowPunch/** - Window management utility (C#)
   - Creates transparent regions in overlapping windows

6. **B2STools/** - Command line tools and scripts
   - `B2SInit.cmd` - Initialization script
   - Various `.cmd` files for configuration

7. **leds/** - LED display implementations
   - `BetterLed/` and `dream7segments/` subdirectories

### Configuration Files
- Each project has its own `.config` files in project directories
- `ScreenResTemplate.txt` - Example screen resolution configuration
- `Plugins.txt` - Plugin directory marker file
- Version info centralized in `Classes/B2SVersionInfo.vb`

### CI/CD Pipeline
- **GitHub Actions**: `.github/workflows/b2s-backglass.yml`
- Builds all configurations (Debug/Release) using Any CPU platform
- Automatically updates version numbers from VB version constants
- Creates release artifacts with all executables and documentation
- **Prerelease workflow**: `.github/workflows/prerelease.yml` for tagged releases

### Development Notes
- **Code Style**: Legacy VB.NET style with some C# components
- **COM Interop**: Heavy use of Windows COM for Visual Pinball integration
- **Plugin System**: Uses Microsoft MEF (Managed Extensibility Framework)
- **Registry Communication**: Server-to-backglass communication via Windows Registry
- **Threading**: GUI work split between threads (noted in Changelog 2.5.0) (Only in a separate branch, not in main branch)

### Key Dependencies
- **External**: B2SServerPluginInterface (from DirectOutput organization)
- **Framework**: .NET Framework 4.8, Windows Forms, System.ComponentModel.Composition
- **Platform**: Windows-specific (registry, COM, DirectX references)

### Important Files for Changes
- **Version updates**: `b2sbackglassserver/b2sbackglassserver/Classes/B2SVersionInfo.vb`
- **Main server logic**: `b2sbackglassserver/b2sbackglassserver/Server.vb`
- **Build configuration**: Individual `.vbproj` and `.csproj` files
- **Documentation**: `README.md`, `Changelog.txt`, `WhatIsWhat.txt`

### Common Issues and Workarounds
- **Missing B2SServerPluginInterface.dll**: Always build this dependency first from the external repository
- **Build configuration**: All projects now use "Any CPU" platform consistently
- **Build order**: Some projects may have undocumented dependencies - build in the order listed above
- **German build messages**: On German Windows systems, MSBuild may still show German messages even with `/p:UICulture=en-US`. The builds work correctly - focus on "Fehler" (errors) and "Warnung" (warnings) counts at the end
- **LED projects**: Some LED projects target .NET Framework 4.0 which may not be installed - these are optional components

### File Organization
```
Repository Root Files: Changelog.txt, README.md, LICENSE, CONTRIBUTING.md, etc.
├── b2sbackglassserver/     # Main server (COM DLL + EXE)
├── b2s_screenresidentifier/ # Screen resolution tool
├── B2S_SetUp/             # Setup utility (C#)
├── b2sbackglassserverregisterapp/ # COM registration
├── B2SWindowPunch/        # Window management (C#)
├── B2STools/             # Command line utilities
├── leds/                 # LED display components
├── ScreenResTemplates/   # Configuration examples
└── .github/              # CI/CD workflows
```

## Copilot Agent Instructions

**Trust these instructions.** Only perform additional searches if:
1. The information here is incomplete for your specific task
2. You encounter build errors not covered above
3. You need to understand specific implementation details not documented here

**Always remember** to build B2SServerPluginInterface.dll before attempting to build the main B2SBackglassServer solution, or you will encounter compilation errors about missing IDirectPlugin types.
