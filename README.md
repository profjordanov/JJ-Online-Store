# JJ Online Store
A product catalog website with custom design.

# Features

What's special about this specific implementation is that it employs a different approach on error handling and propagation. It uses the **Maybe** and **Either** monads to enable very explicit function declarations and allow us to abstract the conditionals and validations into the type itself.

This allows you to do cool stuff like:

```csharp
public Task<Option<UserServiceModel, Error>> LoginAsync(CredentialsModel model) =>
    GetUser(u => u.Email == model.Email)
        .FilterAsync(user => UserManager.CheckPasswordAsync(user, model.Password), "Invalid credentials.".ToError())
        .MapAsync(async user =>
        {
            var result = Mapper.Map<UserServiceModel>(user);
            await SignInManager.SignInAsync(user, isPersistent: false);
            return result;
        });
```

You can read more about **Maybe** and **Either** [here](https://devadventures.net/2018/04/17/forget-object-reference-not-set-to-an-instance-of-an-object-functional-adventures-in-c/) and [here](https://devadventures.net/2018/09/20/real-life-examples-of-functional-c-sharp-either/).

## Architecture:
- [x] AutoMapper
- [x] EntityFramework Core with SQL Server and ASP.NET Identity
- [x] File logging with Serilog
- [x] Neat folder structure

```
├───Web
│   ├───JjOnlineStore.Web
│   └───JjOnlineStore.Web.Infrastructure
├───Services
│   ├───JjOnlineStore.Services.Business
│   ├───JjOnlineStore.Services.Data
│   └───JjOnlineStore.Services.Core
├───Data
│   ├───JjOnlineStore.Data.EF
│   ├───JjOnlineStore.Data.Entities
│   └───JjOnlineStore.Data
├───JjOnlineStore.Common
├───JjOnlineStore.Extensions
└───Tests
    └───JjOnlineStore.Tests
```
- [x] Thin Controllers

```csharp
/// POST: /Account/Login
/// <summary>
/// Login.
/// </summary>
[HttpPost]
public async Task<IActionResult> Login(CredentialsModel model)
    => (await _usersService.LoginAsync(model))
        .Match(RedirectToLocal, ErrorLogin);
```

- [x] Database Transactions
- [x] Database Triggers
- [x] Using Azure Websites and Azure Blob Storage
- [x] Integrated Azure Enterprise Productivity Chatbot
- [x] Integrated Azure SendGrid Email Service

![alt text](https://raw.githubusercontent.com/profjordanov/JJ-Online-Store/master/Resources/azure-poratal.PNG)

### User Interface and User Experience (UI & UX)
- [x] Mobile-First Responsive Design
- [x] Using custom web design 
- [x] Followed the best practices for high-quality HTML and CSS: good formatting, good code structure, consistent naming, semantic HTML, correct usage of classes, etc.
- [x] Works correctly in the latest HTML5-compatible browsers: Chrome, Firefox, Edge, Opera, Safari 
- [x] Preloader

### Test Suite
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly
- [x] Arrange Act Assert Pattern

 
## Achievements

- Excellent grade in C# MVC Frameworks - ASP.NET Core course with trainer : Nikolai Kostov.

![alt text](https://raw.githubusercontent.com/profjordanov/JJ-Online-Store/master/Resources/Softuni_Certificate.jpeg)
