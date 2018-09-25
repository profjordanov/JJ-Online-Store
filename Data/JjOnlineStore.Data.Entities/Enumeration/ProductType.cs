using System;

namespace JjOnlineStore.Data.Entities.Enumeration
{
    [Flags]
    public enum ProductType
    {
        Ordinary = 1,
        New      = 2,
        OnSale   = 4
    }
}