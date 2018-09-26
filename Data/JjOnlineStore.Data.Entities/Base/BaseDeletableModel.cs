using System;
using System.ComponentModel.DataAnnotations;

using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Data.Entities.Base
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}