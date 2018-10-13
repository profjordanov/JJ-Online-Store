 using System;

namespace JjOnlineStore.Common.Enumeration
{
    [Flags]
    public enum TransportationType
    {
        FreeDelivery    = 1,
        FastDelivery    = 2,
        ExpressDelivery = 4
    }
}