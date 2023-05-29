# ASP.Net Core Notes

## Content
- [General introduction and features](#general)
    - [Pros and features](#pros-and-features)
- [Architecture](#architecture)
    - [Choosing between traditonal web apps and SPAs](#choosing-between-traditional-web-apps-and-and-single-page-apps-spas)
    - [Architectural principles: common design principles](#architectural-principles-common-design-principles)
- [MVC Web Applications](#building-mvc-web-apps)
    - [.net core pipeline](#net-core-pipeline)
    - [mvc architecture](#mvc-archictecture)
    - [models and databases](#models-and-databases)
        - [entity framework](#entity-framework)
        - [data annotations](#data-annotations)
        - [dbcontext and unit of work](#dbcontext-and-unit-of-work)
        - [migrations](#migrations)
    - [controllers and views](#controllers-and-views)
        - [controllers](#controllers)
        - [views](#views)
        - [connecting controllers with views](#connecting-controllers-with-views)
        - [crud operations in views and controllers](#crud-operations-in-views-and-controllers-process-overview)
    - [other features](#other-features)
- [Naming conventions](#conventions)
- [Resources](#resources)
    - [microsoft documentations](#microsoft-learn-documentations)
    - [external sites and blogs](#external-blogs-and-sites)
    - [stackoverflow question and answers](#stackoverflow-answers)

</br>

## General
.Net Core was initially released in June 2016 as an improved framework on the previous .NET MVC framework (2009) that preceded the Webforms framework(2002).

### Pros and Features:
- fast and open source compared to previous frameworks
- cross platform and no longer dependent on windows
- built in dependency injection[(what is that ??)](https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/) that allows apps to loosely couple
with interfaces rather than specific implementations, making them easier to extend, maintain and test
- new versions easy update and are easy to keep up with
- cloud friendly (low memory and high-throughput - reliable, robust) and compatible with all cloud platforms
- improved performace compared to previous frameworks
- easily tested with automated tests: the [loose coupling](https://en.wikipedia.org/wiki/Loose_coupling) and support for dependency injection makes it easy to swap infrastructure concerns with fake implementations for test purposes.
Also ships with a TestServer that can be used to host apps in memory
- supports both traditional and single page application (spa) behaviors.
    - *Traditional web apps* depends on the server for all navigations, queries and updates the app might need with little client-side behavior. Each new user interaction sends a new web request, and requires page to reload to get results. typically followed in classic mvc frameworks
    - *single page applications(SPAs)* involve very few dynamically generated server-side page loads. many SPAs are initialized with a static html file that loads the needed javascript functionalities for the interactivity, and make heavy use of web apis for their data needs. implemented in asp.net core with Blazor WebAssembly. 
    - many apps implement both behaviors and asp.net core supports both the provision of mvc and api functionalities in the same application

</br>

## Architecture
### Choosing Between Traditional Web Apps and  and Single Page Apps (SPAs)
Traditional web apps perform most of the app logic on the server side while SPAs do so on the client side.

Use traditional web apps when:
- app's client side requirements are simple or read-only
- app need to function in browsers without javascript support

Use SPA when:
- app must expose a rich user interface with many features
- team is familiar with javascript, typescript or blazor webassemply development
- application must expose an api for other clients

Considerations for using spa:
- SPA frameworks require greater architectural and security expertise. 
- susceptible to frequent updates and new client frameworks than traditonal web apps
- more challenging to configure automated build and deployment processes and utilize deployment options like containers

</br>

### Architectural Principles: Common design principles
> "If builders built buildings the way programmers wrote programs, then the first woodpecker that came along would destroy civilization".
> \- *Gerald Weinberg*

When architecting and designing software applications, maintainability should be a core consideration.

#### **Sepration of concerns**
- asserts that software should be separated based on the kind of work it performs.
- apps can be built to follow this principle by separating core business behavior from infrastructure and user-interface logic.
- ideally, business rules and logic should reside in a separate project, which should not depend on other projects in the application. i.e. having ui, business logic and database divisions within an application
- ensures that business model is easy to test and can evolve without being [tightly-coupled](https://glossary.cncf.io/tightly-coupled-architectures/) to low-level implementation details
- achieved by the establishment of boundaries such as methods, objects, components and services to define core behavior within an application.
- [more here](https://www.castsoftware.com/pulse/how-to-implement-design-pattern-separation-of-concerns#)

#### **Encapsulation**
- principle of insulating different parts of an application from each other.
- app layers and components should be able to change their internal implementation without breaking their dependencies
- for example, in classes or objects, encapsulation is achieved by limiting outside access to the class's internal state, and allowing manipulations to its state to be made through a well-defined method or property setter.
- hiding the internal state of the object protects its integrity by preventing users from setting the internal data of the component into an invalid or inconsistent state.
- in C#, typical keywords, such as `public`, `private` and `protected` offers a programmer a degree of control over what to hide in a class or interface implementation
- for application components, encapsulation can be achieved by exposing well-defined interfaces for collaborators to use rather than allowing their state to be modified directly.
- read more [here](https://en.wikipedia.org/wiki/Encapsulation_(computer_programming))

#### **Dependency inversion**
- the direction of dependency within and application should be in the direction of the abstraction, not in the implementation details. 
- high-level modules should not import anything from low-level modules. both should depend of abstractions such as interfaces
- abstractions should not depend on details. details/concrete implementations should depend on abstractions
- generally, when designing the interaction between a high-level and low-level module, they should not interact with each other directly, but rather through a layer of abstraction.
- this is not the case in traditional layers of dependency where one class references another directly when interacting thus creating a direct dependency graph and more complex run times.
- dependency inversion allows apps to be more loosely coupled, making them more testable, modular and maintainable
- read more [here](https://en.wikipedia.org/wiki/Dependency_inversion_principle)



#### **Explicity depencies**

#### **Simple responsibility**

#### **Don't repeat yourself (DRY)**

#### **Persistence ignorance**

#### **Bounded contexts**



</br></br>

## Building MVC Web Apps
### .NET Core Pipeline
- .Net Core Pipeline: specifies how an application should respond to requests received.
- Requests received from the browser goes through the pipeline, which is made of different middlewares.
- MVC is a type of middleware itself and can be added to an application by adding the `AddControllersWithViews()` method to the application builder services.
- This adds a number of MVC focused services to the app builder.
- Other middleware include Authentication, Authorization, Routing and Static Files
- The order in which middleware is added to the application pipeline is extremely important as it  dictates how the request will be passed.
- For example, authentication middleware should always come before authorization middleware

### MVC Archictecture
- **Model**: Represents the shape of the data, represented as classes. Corresponds to all the data related logic used in the application.
- **View**: Represents the user interface.
- **Controller**: Handles user requests and interfaces between the model and the view. Process all business logic.
![](imgs/MVC.png)


### Models and Databases
#### Entity Framework
- asp.net uses [entity framework](https://learn.microsoft.com/en-us/ef/) to build database models in applications
- supports LINQ queries, change tracking, updates and schema migrations.

#### Data Annotations
- in asp.net, data annotations(attributes) are used to add context to the kind of fields defined in a model class
- The code snippet below shows a model class with the data annotations `Key` and `Required` added to give context to the 
Id and Name fields
	```C#
	 public class Category
	 {
        [Key]
        // Key data annotation(attribute) sets the Id column as the primary key and identity column
        public int Id { get; set; }
        [Required]
		// sets Name as a required field 
        public int Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        // sets DateTime.Now as default value
	  }
	```

#### DbContext and Unit of Work
- a [dbcontext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/) instance creates a session with a database to track changes, query and save instance of database entities.
- lifetime of a `DbContext` begins when the instance is created and ends when the instance is disposed
- a	`DbContext` instance is designed to be used for a [single unit of work](https://www.martinfowler.com/eaaCatalog/unitOfWork.html)
- > A Unit of Work keeps track of everything you do during a business transaction that can affect the database. When you're done, it figures out everything that needs to be done to alter the database as a result of your work 
- A unit of work using EF Core includes
	- creation of a `DbContext` instance
	- tracking of entity instances by the context *(need to read more on this)*
	- making of changes to tracked entities as needed to implement business rule
	- calling of `SaveChanges` or `SaveChangesAsync` to detect changes made and writing them to the database
	- disposing of instance
- The idea behind using dbcontext to track changes to entities is to avoid writing directly to the database anytime changes are made
- In many web apps, each http request represents a single unit of work, hence makes sense to tie the dbcontext lifetime to that of the request
- In Asp.net, `ApplicationDbContext`, which is a subclass of `DbContext` is created for each request and passed to the controller to 
perform a unit of work before being disposed when the request ends.

- In the code snippet below, the `ApplicationDbContext` class inherits from the base `DbContext` class
Within the constructor of the same name, the options defined in the custom class are passed down to the base `dbcontext` class.
- To create an interface of the database entity `Category` a `DbSet` of the entity named `Categories` is created with getters and setters.
- The `Categories` dbset can now be called in controllers with the application dbcontext to interact with the database.
```c#
	public class ApplicationDbContext : DbContext
	
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
		// DbContextOptions<ApplicationDbContext> is named options and passes down the options defined in the
		// ApplicationDbcontext to the base DbContext class
		{

		}
		public DbSet<Category> Categories { get; set; }
		// this creates a Category table with the name Categories based on the
		// model definition in the Category class

	}

```

#### Migrations
- Migrations help track code changes to data models and implement them to the database
- Process to making migrations
	- in the NuGet package manager console, install the `Microsoft.EntityFrameworkCore.Tools` package to enable migration commands
	- you can then run `add-migration <migration name>` in the package manager console
	- this creates a migration folder with a migration class file that contains `Up` and `Down` methods for upgrading and downgrading 
	database based on changes to the model class
	- use the update-database command to push the migration to the database
	- entity framework core uses the logic in the migration class to generate and insert sql queries into the database to make the necessary
	database changes

#### Connecting to Databases: Process Overview
- add a database connection string to the `ConnectionStrings` block of the `appsettings.json` file.
	database connection string should contain name of server, database and a trusted_connection boolean
- congifure the application dbcontext to open a session to the database, track changes and make modifications to the database.
	this is done by creating an `ApplicationDbContext` class which subclasses `DbContext`  with various options.
	the application db context class will hold the names of all the database entities defined using the `DbSet` class
- add the created application context to the application services using `builder.Services.AddDbContext()` method in the Program.cs file.
	pass the database management system being used as an option to the `AddDbContext()` method along with the connection string created in 
	the `appsettings.json` file.
- make migrations to the database to create the entities defined in the application


### Controllers and Views
#### Controllers
Controllers handle the application logic and interfaces between the database and the view(user interface).
In an MVC architecture, separate controller classes are usually created to handle related business logic units.
When a Controller class is created, it inherits from the base `Mvc.Controller` class which makes it easy to write controller
logic without writing boilerplate code.

- To interact with the database within a controller, we create a reference to the [application dbcontext](#DbContext-and-Unit-of-Work) in the controller class.
- A [unit of work](#DbContext-and-Unit-of-Work) within a controller are represented by "action" methods.
- Action methods perform a unit of work and then can return a result such as a view, partial view, redirect, json, file, etc.
- Action methods can use various inbuilt interfaces such as  `IActionResult`, `ViewResult`, etc. to return various "results" after an action has been completed.
- `IActionResult` is a generic type that implements all other return types and is appropriate when different return types are possible in one action
- but a more specific class like `ViewResult` can be used when a view is specifically being returned 

	```c#
	public class GenericController : Controller
    {
		// using the generic IActionResult
		public IActionResult Index()
		{
			return View()
		}

		//using ViewResult
		public ViewResult Index()
		{
			return View()
		}
	}
	```

#### Views
- razor pages
- layouts and partials
- default bootstrap

##### Tag Helpers
- enables server-side code to participate in creating and rendering HTML elements in [Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0) files.
- pretty much like django template tags

#### Connecting Controllers with Views
- To automatically associate a controller action with a view(html page) create the view page with the name of the 
associated action within a subfolder with the controller name in the view folder.
For example, an action with the name `Index` under the `Category` controller will expect its view file to be found at
`/Views/Category/Index.cshtml` unless otherwise stated
- To allow a user to navigate to a view page, add a link to the view page from another page, by specifying the name of the controller
and action associated with the view within the `<a>` tag.

	```cshtml
	<li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Category" asp-action="Index">Category</a>
     </li>
	```
- In the code snippet below the tag helpers `asp-controller` and `asp-action` are used to indicate the controller and action that 
should be contacted once user clicks on the link named 'Category'


#### CRUD Operations in Views and Controllers: Process Overview
##### Read
- Reading data from a database and rendering it in a view requires using the application dbcontext and controller action to 
interface between the view and the database.
- In the controller class, access the dbset defined in the application dbcontext to get access to 
the database entity objects. 
- Typical read operations include:
	- getting a list of items from a database entity
	- searching for a specific item within a database
- In the code snippet below, within the `Index()` action method, we have defined an `IEnumberable` object `objCategoryList`.
This object contains an enumerable list of all items in the category model. 
- This is read from the `Categories` dbset in the application dbcontext as `_db.Categories`.
- The result of the data read (`objCategoryList`) is then passed to the and returned with the `View()` function. 
	
	```c#
	public class CategoryController : Controller
		{
			private readonly ApplicationDbContext _db;

			public CategoryController(ApplicationDbContext db)
			{
				_db = db;
			}

			public IActionResult Index()
			{
				IEnumerable<Category> objCategoryList = _db.Categories;
				return View(objCategoryList);
			}
		}
	```


- At the top of the html page, using the `@model` directive with the model name allows the view to accept the model object that the 
controller passed to the view from the `Index` action method.
```cshtml
// in .cshtml file
@model IEnumerable<Category>
```
The code snippet below in the view page tells the view to expect an IEnumerable Category model object to be passed to it


##### Create
When writing to a database in a create action, we still acess the database entity from the application db context
just as we'd do in the read action.
However, the major change comes from how we interact with the database. In a create action, we append a new
row to the database


### Other Features
#### Routes
-  Routes in MVC follows this pattern: `https://localhost:5555(domain)/category(controller)/index(action)/3(id)`
- the id part is optional but controller and action are not. In case those are not provided in a url pattern, the default controllers and actions defined in the program will be used.

#### TempData
- `TempData` in .net core allows the storing of data that persists for only one request.
- Makes it appropriate to store data such as alerts and messages

## Conventions
- When naming partial a partial view, it's best practice to start it's name with an underscore. e.g. `_Layout.cshtml`
- Use PascalCase when when naming a class, record, or struct.
- When naming an interface, use PascalCase in addition to prefixing the name with an 'I'
- When naming public members of types, such as fields, properties, events, methods, 
and local functions, use pascal casing
- Use camel casing ("camelCasing") when naming private or internal fields, and prefix them with _.
- When working with static fields that are private or internal, use the s_ prefix and for thread static use t_.
- When writing method parameters, use camel casing.
- [more conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## Resources
### Video Tutorials
- [Learn ASP.NET Core MVC(.NET6) - Freecodecamp on Youtube](https://www.youtube.com/watch?v=hZ1DASYd9rk)

### Microsoft Learn Documentations
- [entity framework documentation hub](https://learn.microsoft.com/en-us/ef/)
- [dbcontext lifetime, configuration and initialization](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
- [razor syntax reference for asp.net core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0)
- [C# coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [Architecting modern web applications with asp.net core and azure (ebook)](https://dotnet.microsoft.com/en-us/download/e-book/aspnet/pdf)

### External Blogs and Sites
- [a quick introduction to dependency injection ... (freecodecamp)](https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/)
- [loose coupling (wikipedia)](https://en.wikipedia.org/wiki/Loose_coupling)


### Stackoverflow answers
- [fix to "certificate not trusted ... " when connecting to sqlserver database](https://stackoverflow.com/questions/17615260/the-certificate-chain-was-issued-by-an-authority-that-is-not-trusted-when-conn)
- [what's the common practice of gitignore for asp.net core project](https://stackoverflow.com/questions/39289765/whats-the-common-practice-of-gitignore-for-aspnet-core-project)