# auth_net
Toy app for implementing authentication and JWT based authorization using .NET core and mongoDB

## Why a new project
Just because I could ;). Also, available implementations that I could find were outdated and there was no clear guidance on usng the most recent MongoDB C# driver for ASP .NET core 2.0. If you've landed here looking for an implementation that works maybe this will help you.

### Additional info
The [Documentation](http://www.dotnetcurry.com/aspnet-mvc/1267/using-mongodb-nosql-database-with-aspnet-webapi-core) incorrectly defines usage for  `Collection.Update` method. It expects a LINQ functor but the docs say that a Mongo Filter is needed.

```C#
var result = _users.UpdateOne<User>(u => u.UserName == userName, update);
```

### References
- [MongoDB C# driver docs](https://docs.mongodb.com/getting-started/csharp/update/)
- [Dot Net Curry](http://www.dotnetcurry.com/aspnet-mvc/1267/using-mongodb-nosql-database-with-aspnet-webapi-core)

