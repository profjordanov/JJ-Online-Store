using System;

namespace JjOnlineStore.Data.Entities.Base
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}