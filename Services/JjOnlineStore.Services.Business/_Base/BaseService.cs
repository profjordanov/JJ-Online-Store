using System;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using Optional;
using JjOnlineStore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JjOnlineStore.Services.Business._Base
{
    public abstract class BaseService
    {
        protected BaseService(JjOnlineStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected JjOnlineStoreDbContext DbContext { get; }

        protected virtual Task<Option<ApplicationUser, Error>> GetUserByIdOrError(string userId) =>
            GetUserById(userId.SomeNotNull())
                .WithException($"No user with an id of {userId} has been found.".ToError());

        protected virtual Task<Option<ApplicationUser, Error>> GetUserByNameOrError(string username) =>
            GetUserByName(username.SomeNotNull())
                .WithException($"No user '{username}' has been found.".ToError());

        protected virtual Task<Option<ApplicationUser>> GetUserByName(Option<string> username) =>
            username.FlatMapAsync(name =>
                GetUser(u => u.UserName == name));

        protected virtual Task<Option<ApplicationUser>> GetUserById(Option<string> userId) =>
            userId.FlatMapAsync(id =>
                GetUser(u => u.Id == id));

        protected virtual async Task<Option<ApplicationUser>> GetUser(Func<ApplicationUser, bool> predicate) =>
            (await DbContext
                .Users
                .FirstOrDefaultAsync(u => predicate(u))).SomeNotNull();
    }
}