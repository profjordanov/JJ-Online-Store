using System;

namespace JjOnlineStore.Data.Entities.Base
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}