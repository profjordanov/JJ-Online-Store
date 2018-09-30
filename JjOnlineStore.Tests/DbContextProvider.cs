using Microsoft.EntityFrameworkCore;
using JjOnlineStore.Data.EF;

namespace JjOnlineStore.Tests
{
    public static class DbContextProvider
    {
        public static JjOnlineStoreDbContext GetInMemoryDbContext()
            => new JjOnlineStoreDbContext(new DbContextOptionsBuilder<JjOnlineStoreDbContext>()
                .UseInMemoryDatabase("Tests").Options);
    }
}