# auth_net
Implementation of Authentication and JWT based authorization using .NET core and mongoDB

## Why a new project
Available implementations that I could find were outdated and there was no clear documentation on using the most recent MongoDB C# driver for ASP .NET core 2.0. If you've landed here looking for an implementation that works I hope this helps you.

### Pre-requisites
- Install MongoDB
- Install .NET core 2.0 SDK
- This project was developed using VS 2017

### Notable Differences
- The [Documentation](https://docs.mongodb.com/getting-started/csharp/update/) incorrectly defines usage for  `Collection.Update` method. It expects a LINQ functor but the docs say that a Mongo Filter is needed.

```C#
var result = _users.UpdateOne<User>(u => u.UserName == userName, update);
```

- Code samples in [Shawn Wildermuth's blog](https://wildermuth.com/2017/08/19/Two-AuthorizationSchemes-in-ASP-NET-Core-2) inject `IConfigurationRoot` into controllers to access properties. The appropriate way suggested by [Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration?tabs=basicconfiguration) is using `IOptions` classes that allow for strongly typed options objects on top of all available configuration in `appsettings.json`.

```C#
//Startup.cs -> ConfigureServices
services.Configure<TokenOptions>(Configuration.GetSection("Tokens"));

//LoginController
private TokenOptions _tokenOptions;

public LoginController(IOptions<TokenOptions> tokenOptionsAccessor)
{
	_tokenOptions = tokenOptionsAccessor.Value;
}

```
- .NET core 2.0 has [deprecated](https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x) `app.Use<AuthenticationType>` and authentication needs to be added as a service that is called only once in the configured pipeline.

```C#
public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddAuthentication(options =>
	{
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options => 
	{
		options.TokenValidationParameters = tokenValidationParameters;
	});
	...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	...
	app.UseAuthentication();
	...
}
```


### More Research
[Mongoose](http://mongoosejs.com/), an object modeling library for MongoDB in NodeJS exposes a very useful feature `PreSaveHooks`. This allows for controlling fields like passwords and how they are persisted in the DB. The [C# Driver for MongoDB](https://docs.mongodb.com/getting-started/csharp/) does not have any similar features and it would be interesting to investigate if somethign like this can be introduced here.

### References
- [MongoDB C# driver docs](https://docs.mongodb.com/getting-started/csharp/)
- [Dot Net Curry](http://www.dotnetcurry.com/aspnet-mvc/1267/using-mongodb-nosql-database-with-aspnet-webapi-core)
- [Shawn Wildermuth's blog on authentication](https://wildermuth.com/2017/08/19/Two-AuthorizationSchemes-in-ASP-NET-Core-2)
- [Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)
- [Henrik F Neilsen's blog on Using MongoDB with WebAPI - Microsoft Blogs](https://blogs.msdn.microsoft.com/henrikn/2012/02/18/using-mongodb-with-asp-net-web-api/)
- [JWT Validation and Authorization in ASP.NET Core (.NET core 1.x)](https://blogs.msdn.microsoft.com/webdev/2017/04/06/jwt-validation-and-authorization-in-asp-net-core/)
