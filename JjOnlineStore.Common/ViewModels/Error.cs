using System;

namespace JjOnlineStore.Common.ViewModels
{
    public struct Error
    {
        public Error(string message)
        {
            this.Message = message;
            this.Date = DateTime.Now;
        }

        public string Message { get; }

        public DateTime Date { get; }
    }
}