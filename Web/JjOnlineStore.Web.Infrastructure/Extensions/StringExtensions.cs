using System.Collections.Generic;
using JjOnlineStore.Common.ViewModels;

namespace JjOnlineStore.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static Error ToError(this IEnumerable<string> errors) =>
            new Error(errors);

        public static Error ToError(this string error) =>
            new Error(error);
    }
}