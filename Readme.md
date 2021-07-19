# .NET 5 Object Model

Microsoft.Net.App
- A Complete Object Model for .NET 5 Applications. This contains APIs for
	- Collections
	- Text Encoding
		- Xml
		- JSON
		- Binary
	- Communicaiton Object Model
		- System.Net
- Support for Single File Publish (Like SDK Style)
	- A Single EXE with Compact aka Trimmed Deployment for the Application
``` javascript
  <PublishSingleFile>true</PublishSingleFile>  
  <RuntimeIdentifier>[RID]</RuntimeIdentifier>  
  <PublishReadyToRun>true</PublishReadyToRun>  
   <PublishTrimmed>true</PublishTrimmed>
```
	- The MSBuild (with Visual Studio on WIndows OS or VS on Mac ) with the INtegration of "Microsoft.Net.App" will generate a Single .EXE file with all project dependencies
	- [RID] is the Target Platform whrn the App will be Deployed
		- win-x64, win-x86, win-arm, win-arm64 (Windows portable for 7,8,10)
		- win7-<PROCESSOR>, WIndows 7 and Windows Srever 2008 R2
		- win81-<PROCESOR>, WIndow8.1 anD Windows Server 2012 R2
		- win10-<PROCESSOR>, Windows 10 and WIndows Servetr2016
		- linux-x64
		- rhel-x64
		- tizen
		-osx.10.10-x64
	- REady-To-Run (R2R) will enable the Ahead-of-Time (AoT) compilation for the application provide the improved binary for startup performance by reducting the amount of work the JIT needs to laod and start the applicaiton
		- JIT does not have the platfort, stepcif Binary Embeded instead it depends on the coreclr+clrjit+clrcompression 
		- R2R, contains the embeded binary to run the application
	- Publish Trimmed will make sure that, only required depednencies for the application code will be embeded into the output executable. THis is also knows as 'assembly trimming'. Supported for Seld-Contaioned apps 
#=====================================================================================================================
Learning .NET 5
1. C# Language Changes
2. ASP.NET Core 5 Features with API Changes
	- Code Changes
	- Swagger
	- OpenID Methods
	- API Proxy COde
	- Microsoft Identity Platform (.NET 5)
		- User / Role Based Secueity with Policy BAsed Authentication
		- Azure AD Integration (Recommended if app is deployed on Azure)
			- OpenIdConnect
		- Federated Security for Local / On-Premised AD
	- Host BUilder Modification
3. Blazor
4. gRPC an Alternative to API
5. Migrating .NET App to .NET Core 

======================================================================================================================




# C# Programming
1. Discard
	- A Strict Scope for Local variable for the method
		- This variable will be immediately disposed when the scope is complets its execution
		- Use Discard when the method depends on local declarations
2. Deconstruction 
	- Simplified approach of variable declarations
		- e.g.
			- DEclaring multiple variable
				- var x = "ddd";
				- var y = "sss";
			- C# 7.0+
				- var (identifiers seperated by comma) = (initial values separated by comma)

# Tasks
Day 1: 19-july-2021
1.  REad about the C# Language Features for 5 and 6
	- Pattern Matching
	- String Templates
	- Out Variable
2. SIngle File Publish