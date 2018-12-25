using System;
using JjOnlineStore.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace JjOnlineStore.Tests
{
    public static class DbContextProvider
    {
        public static JjOnlineStoreDbContext GetInMemoryDbContext()
            => new JjOnlineStoreDbContext(new DbContextOptionsBuilder<JjOnlineStoreDbContext>()
                .UseInMemoryDatabase($"Tests-{Guid.NewGuid().ToString()}").Options);
    }
}