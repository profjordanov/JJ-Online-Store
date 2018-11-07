# JJ Online Store


## Features:
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

### User Interface and User Experience (UI & UX)
- [x] Using custom web design 
- [x] Followed the best practices for high-quality HTML and CSS: good formatting, good code structure, consistent naming, semantic HTML, correct usage of classes, etc.
- [x] Responsive design 
- [x] Works correctly in the latest HTML5-compatible browsers: Chrome, Firefox, Edge, Opera, Safari 

### Test Suite
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly
- [x] Arrange Act Assert Pattern
