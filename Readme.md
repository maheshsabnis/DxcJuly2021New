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
			- Microsoft.AspNetCore.Identity.EntityFramneworkCore
				- An integration between ASP.NET COre Identity and EF Core
					- IdentityUser
						- Used to Define Schema for USer in Database, AspNetUsers Table
						- Id, Name, Email, PasswordHash, etc.
					- IdentityRole
						- Used to define sschema for Application Roles, AspNetRoles Table  
					- IdentityClaims, IdentityTokens, etc
				- IdentityDbContext
					- Derived from IdentituDbContext<IdentityUser,IdentityRole, IdentityClaims, IdentityTokens> 
						- This class is used to to generate Identity database using EF Core Code-First Migration
				- UserManager<IdentityUser>
					- USer Management
						- CReate, Update, Delete, Read USers
						- Assign Role to / Roles (.NET Core) to USer
				- RoleManager<IdentityRole>
					- Role Management CRUD
				- SignInManasger<IdentityUser>
					- Mange the USer SignIn using USer+Password 
			- Microsoft.AspNetCore.Identity.UI
				- The Package that represets the Razor Library that contains pages for Security / Identity
	`		- The AddAuthorization() Service method will allo to define Authriozation Policies for the Application
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
# ASP.NET Core (MVC) 5 Project Strcture
- Folders
	- Controllers
		- MVC and API Controllers
	- View
		- Contains MVC Views
	- Data
		- DbContext class and Default Migrations classe for ASP.NET Core Identity.
		- THis will be uesed to CReate ASP.NET Core Identity Database and Tables
	- Models
		- Applicaton Widw models 
		- The ErrorViewModel class, the custom class for Exception Management
	- Areas
		- Folder that contains the default Identity Razor Pages
		- The Razor View Library (a dll file taht contains Razor Views used for Identity Management)
	- wwwroot 
		- Conatains 'Client-Side' files e.g. Bootstrap, jQuery and any other JS, CSS and Image files to be rendereds (aka loaded) in browser with response for Views
- Files
	- appsettings.json (Say bye to Web.Config)
		- Application Level Setings
			- e.g. ConnectionString, Token Secrets, Loggining, Host COnfig, Redis COnfig, etc. 
	- Startup.cs
		- The class that is used to provide, application Startup configuration******
	- Program.cs
		- Class fot Hosting Initialization 
- Dependenceis
	- Microsoft.NetCore.App
		- .NET Core Libreries
	- Microsoft.AspNetCore.App
		- ASP.NET Core Eco-System

# Programming With ASP.NET Core 5
1. Use EF Core to Generate DAL
	- dotnet add package Microsoft.EntityFrameworkCore -v 5.0.8
	- dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.8
	- dotnet add package Microsoft.EntityFrameworkCore.Relational -v 5.0.8
	- dotnet add package Microsoft.EntityFrameworkCore.Tools -v 5.0.8
	- dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.8
2. Defualt Repositry Pattern to create Business Services
3. If using MVC then define Action Filters on Controller
    - Controllers
		- The Request Facilitator of MVC Applications
		- Listen to the HTTP Request and based on the Request Type (HTTP GET/POST/PUT/DELETE) + Action Name (What is supposed to be executed), the Execution is performed
			- The 'Controller' is base class for MVC Controller
				- This class implements IActionFilter Interface to load and execute action filters for MVC COntroller Only for Exception, Logging, Result, etc.
				- The 'Controller' is responsible to Execute View Resources aka ViewResult, PartialViewResult
				- The 'Controller' is responsible to execute Action Method and decide the Response Type based on Action Merthod
					- View, Json
				- The Controller class can maintain State of the data across actoin Methods of Same controller or action methods of Across Controllers, using 'TempData'. The ViewData and ViewBag are used to maintain state across the Action Method and View  
		- The ControllerBase class is the Common BAse class for APIs and MVC Controllers
			- This class managed
				- Routing, Model Validations, Identity of User, HttpContext to Manage Request and Response
		- IActionResult, the common contract interface for Managing the HttpResponse from the ASP.NET Core Apps
			- ViewResult,JsonResult, PartialViewResult, ReditectToActionResult
			- FileResult, 
			- Results for APIs, OkResult, ObObjectResult, NotFoundResult, BadRequestResult, etc.
	- Exception, Result (Optional)
	- The View can directly be injected using the dependencies
		- @inject directive, used to resolve and load the dependency type on view and execute it
				- Load View
				- Execute Logic on View
				- Generate Data
				- Pre-Render the HTML based on Data3
				- Send the HTML Serialied response to Client along with Data
4. Use Tag Helpers for Views 
	- They are the Interactive UI scaffolded by MVC in ASP.NET Core
		- View is Derived from RazorViewPage<TModel> class
			- TModel is a Model class passed to the View while scaffolding it (aka generating it)
				- Type of TModel is depending on the ViewTemplate and Model class passed to the Template
				- If Template is List, then TModel will be IEnumetable[Model] class
				- If Template is create, then TModel will be an empty Model class
				- If Template is Edit, then TModel will be the Model with Valued to be edited
				- If Template is Delete, the TModel will be the Model with values to be deleted
				- If Template is Details, the TModel will be the Readonly Model with values to be viewed
				- If Template is Empty(With Model), the TModel will be the Model with values to be shown onn View
			- Each view has the 'Model' property of the type TModel
	- Standard Tag Helpers
		- They are the Pre-Compiled HTML Attributes used to set the Execution Behavior of HTML elements when the View is rendered in Browser	   
			- asp-controller, generate HTTP Request for the Controller
			- asp-action, generate HTTP reques for Action Method
			- asp-for, bind the scalar property of teh Model classs with HTML Elements
			- asp-items, generate  HTML elements by iterating over the collection property from Model class
				- asp-items="IEnumerable"
					- Microsoft.AspNetCore.Mvc.Rendering.SelectList
			- asp-validation-for,executes server-side Model validation using JavaScript code on the client (browser)
			- asp-route for roputing
	- Create Custom Tag Helper
5. ASP.NET Core Request Processing
6. Identity
	- Individual Identities for User Based Security
	- Role BAsed Security
		- Customize the Identity Pages
	- Policy BAsed Authorization
7. APIs
	- The 'ControllerBase' is base class for API Controller
		- Request Processing Object Models
			- HttpRequest, HttpResponse, HttpContext and RouteData
		- Security
			- ClaimsPrincipal Object to Handle Secure Calls to API
		- Http Status Code Response Methods
			- OkResult, OkObjectResult, NotFoundResult, ConflictResult, etc.
	- ApiControllerAttribute class applied on API Controller class
		- Used to Map Http Requests to HTTP Action Methods based on Http request Type e.g. Get return type will mapp to action method with HttpGet Attribute applied on it.
		- For Http Post and Http Put request, the JSON data from Http Request Body will be mapped with CLR Object
	- Parameter Binders 
	- HTTP Method Mappers
		- The class to map the Http Request to Currsponding method of API Controller
``` csharp
//
    // Summary:
    //     Identifies an action that supports the HTTP GET method.
    public class HttpGetAttribute : HttpMethodAttribute
    {
        //
        // Summary:
        //     Creates a new Microsoft.AspNetCore.Mvc.HttpGetAttribute.
        public HttpGetAttribute();
        //
        // Summary:
        //     Creates a new Microsoft.AspNetCore.Mvc.HttpGetAttribute with the given route
        //     template.
        //
        // Parameters:
        //   template:
        //     The route template. May not be null.
        public HttpGetAttribute(string template);
    }
```

	- The ' HttpGetAttribute(string template);', the template means the OpenAPI method name template, that will be used by the Route and MapControllers() middlewares to map the request to action method
	- API Design Challanges
		- What if the Browser CLient tries to Access the API?
			- The Browser client from the different Domain with receive the CORS error
			- To rectify this error add the CORS policy service in COmfogureServices() method pof Startup class
			- Add the CORS Middleware in COmnfigure() method before the routing Middleware
		- What if the ApiController Attribute is removed from API Controller class?
			- TO make Post and Put Request from Client app that is posting data send in Http Request, use the Model Parameter Binders in Post and Put Methods 
		- What if the client is trying to make post request using QueryString, FormData, Route values?
			- The FormQuery Model Parameter binder, that is used to map the QuerySrting Data with CLR Object
			- The FromRoute Model Parameter binder, that is used to map the Route Value to CLR object
	- OpenAPI Ids
		- What if the Managed cline wants to access API methods for HTTP Calls using Service Proxy Stub? OR eventthe TypeScript clientts wants to access the API with Simple Coding standard?
		- The OpsnAPI Specification 3.0 allows developers to create a simple API Documentation by configuring HTTP Merthods names as developer friendly methods 
    - Managing Responses from the API
		- Validation of Models
		- Process BAsed Exceptions
			- Implement the custom Middleware that will be added in the Http Pipeline
		- Security Implemnation
		- Managing the Model Design for performing Read/Write Operations
	- Middlewares	
		- Class that is constructor injected with 'RequestDelegate' delegate object
			- This delegate invokes a method that is accepting the input parameter as 'HttpContext'
			- InvokeAsync(HttpContext context) {  /* Logic for Middleware */}
		- Create an extension class
			- This class will register a middleware class in the Pipline using IApplicationBuilder
	- Swagger Service
	- Proxy Class for Client
	- Token Based Authentication
8. ASP.NET Core API Request Processing
9. Blazor
	- Hosting Model
		- Server-Side Blazor Apps
		- Web Assembly
	- State Management
	- Lifecycle Hooks
	- USing Naviugations with ROuting
	- Custom Components
	- Templates
	- Data Binding
	- Event Binding
	- Razor Veiw Component
10. Microsoft Identity Platform
	- Secure ASP.Net Core App
	- Secure ASP.NET Core API
	- Secure Blazor Apps
11. Deployment


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

Day 3: 21-July-2021
Complete the Client Application CS_EF_DbFirst with FOllowing Modifications
	1. Create a Service class for Product to complete CRUD operations on Product Table
	2. Create a new class called 'Client', this class will have followign methods
		- CreateCategoryProduct(<INPUT-OBJECT-WITH-ONE-TO-MANY-RELATIOSHIPS>)
			- This method will create a category and multiple Products for teh category based on one-to-many relationship across Category-Product table. (Hint: Use AddRange() for adding multiple records) 
		- ProductByCatName(string subCatName)
			- THis method will return a List of Products based on SubCategoryName as
				- ProductRowId, ProductId, ProductName SubCategoryName(Only 4 columns ) (Hint:Tuple)
Day 4: 22-july-2021

1. Create a StoreProcedure for Inserting Employee information in Employee table. THis Stored Procedure will have all columns of Employee Table as input parameters. Perform the Insert operations using EF Core Database First Approach. The Stored Procedure will retuen the Count of the Number of records in EMployee Table based on the DeptNo.
2. Take an experience of Code-First Approach with Many-to-Many relations acros tables as per the classes of your choice.
Day 5 : 23-Ju;y-2021,Conceptual Session on ASP.NET COre 5
Day 6: 
1. Modify the ASP.NET Core 5 app by adding Edit,Delete methods in DepartmentController alomg with Views. Add EmployeeController and in Create View of Employee, show DropDown for DeptNo that shows List of DeptName HIML Use asp-items for shoing List of Department on Create view for Employees
2. Create a Controller that will show Departments and EMployees Table in a Single View. When you select DeptNo from Departments Table, the EMployee Table should show Employees for selected Department 

Day 7: Date: 27-july-2021
1. Modify the Custom Exception Filter to Log Exceptions with ControllerName, ActioName and Excception Mressage in Database table called RequetLog which is having columns as ReuqestId, COntrollerName, ActionName, RequestDateTime, ErrorMessage
	- Use EF Core Approach to perform this Database Operations

Day 8: Date:28-july-2021
1. When a View result for any exception based on action method, the error page will be displayed. When we comeback from erro page to the view that has thrown exception, the view should show the original data and the entry caused the error, must show error message beside it. 

Day 9: Date 29-July-2021
1. Create a SearchAPI that will contain a method which will provided the Listing of EMployees based on DeptName
2. Create a API that will contain a Post method to Create Department and EMployees with one-to-Many relations, means that for a single department multiple Employees will be created. This Post method will accept one deprtment object and multiple Employees for the dfeepartment.  
 Post(Dept,List<Employee>)

Day 10: Date : 30-July-2021
1. Create a Middleware that will listen to excepitons thrown by the API COntroller's Action Method. The Middleware will perform the Following
	- Its should listen to different exceptions 
		- DatabaseException
		- Application Levele excepotions e.g. Custom Exceptions.
	- The Status code Must be different for different exception
		- E.g. If DB Exception, then for Insert Fail, 600, Update Fail, 601, Unique Fail, 602, Not Found, 603
	- The Exception Message must containe the Following Infromation in Response
		- Decorated Exception Message (Hide the actual Exception), Status Code, Data Value that caused exception
	- The Middleware mujst log the Exception details in Database 		


Day 17: Date: 10-Aug-2021
1. When the Application Starts, create a Administrator Role User using Code.The RoleController and RoleUSerManagerCOntroller can be accessed using the Administrator role only
	- User NAme: admin@myapp.com, Password:P@ssw0rd-1235


USE [Company]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 7/26/2021 12:36:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Department](
	[DeptNo] [int] NOT NULL,
	[DeptName] [varchar](100) NOT NULL,
	[Location] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




 USE [Company]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 7/26/2021 12:36:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[EmpNo] [int] NOT NULL,
	[EmpName] [varchar](100) NOT NULL,
	[Designation] [varchar](100) NOT NULL,
	[Salary] [int] NOT NULL,
	[DeptNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([DeptNo])
REFERENCES [dbo].[Department] ([DeptNo])
GO

