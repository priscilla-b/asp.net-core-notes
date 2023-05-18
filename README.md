# ASP.Net Core Notes

## General
- .Net Core was initially released in June 2016 as an improved framework on the previous .NET MVC framework (2009) that preceded the Webforms framework(2002).
- Pros:
	- fast and open source compared to previous frameworks
	- cross platform and no longer dependent on windows
	- built in dependency injection. [what is that ??](https://www.freecodecamp.org/news/a-quick-intro-to-dependency-injection-what-it-is-and-when-to-use-it-7578c84fa88f/)
	- new versions easy update and are easy to keep up with
	- cloud friendly (was written with cloud architecture in mind) and compatible with all cloud platforms
	- improved performace compared to previous frameworks


## ASP.Net Core MVC Web App Notes
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

### MVC Web App Features
#### Routes
-  Routes in MVC follows this pattern: `https://localhost:5555(domain)/category(controller)/index(action)/3(id)`
- the id part is optional but controller and action are not. In case those are not provided in a url pattern, the default controllers and actions defined in the program will be used.

#### Tag Helpers
- enables server-side code to participate in creating and rendering HTML elements in [Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0) files.
- pretty much like django template tags