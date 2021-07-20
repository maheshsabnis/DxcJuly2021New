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




# C# Programming (6.0+)
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
3. Tuple Type
	- Declaration with Concise syntax for defining a group of multiple Data elements
	- void Process(int a,int b,int c,int d, int e){}
		- Process must be passed with 5 data elements of the type int to execute else it will result into errors
	- The 'lightweight Data Structure'
		- System.ValueTuple
			- System.ValueTuple<T1,T2>
				- .NET frwk 4.7 Update (C# 7.0) and .NET Core 2.2, .NET Core 3.0 with C# 7.3 for the application development
	- Lightweight
		- Managed as 'Single Value Tuple' 
			- (double, int) t1 = (4.1,10); // C# compiler will define identifiers for it internally 
			- void Process(t1), as input parameters
			- (double, string) Process(); as a return type
		- The Value Type for the tuple will be internally handled as 'struct'
		- Members or Field Names for the Tuple
			- var t1 = (Fname: "", Lname:"", Age:33);
				- t1.Fname, t1.Lname, t1.Age
			- (string Fname, string Lname, int Age) t=("M","S",33);
				t.Fname, t.Lname, l.Age

4. Pattern Matching
	- Approach for executing the Logic based on Pattern or Characteristics of the input values
		- http://server/mysite/mycontroller/myaction/p1/p2/p3, p1,p2,p3 are route values
		- http://server/mysite/mycontroller/myaction?name=value, name is key and value is value of the key
		- p1,p2,p3, value are scalars
	- OPerators
		- The 'is' operator aka Property Pattern Matching
			- vaeify the type by internally accessing the typeof() operator and GetType()method to eveluate the pattern
		- SwichCase Pattern Matching
			- Work on Bulk of input data and process it based on Characteristics of it


# Tasks

# VIMP: Show the SOlution once completed by sharing the scren else not done in time then share it using git link
on sabnis_m@hotmail.com

Day 1: 19-july-2021
1.  REad about the C# Language Features for 5 and 6
	- Pattern Matching
	- String Templates
	- Out Variable
2. SIngle File Publish

Day 2: 20-July-2021

Create an Object Collection in the Console Applicaiton Targeted to .NET 5. THis object collection will contains the data as List of Employees, List of Departments. The Employees data will be a Tuple with EmpNo, EmpName, Salary, DeptNo. The Department data will also be a Tuple containing DeptNo,DeptName Perform the following actions. Process the collection based on Pattern Matching as like
	- if the Salary of the Employee is less than 40000, the the Tax will be 10% of salary else 20%
	- If the Employee is from 'Sales' Department then the Additional Travel Allowance of 25% of salary must be added in it and calculate the total Salary 
	- Print all EMployees with their DepartmentNames with Salary and Tax

