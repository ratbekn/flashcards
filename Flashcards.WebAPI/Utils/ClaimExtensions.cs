using System;
using System.Linq;
using System.Security.Claims;

namespace Flashcards.WebAPI.Utils
{
    public static class ClaimExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal) =>
            TryGetUserId(principal) ?? throw new ClaimException(ClaimTypes.NameIdentifier);

        public static Guid? TryGetUserId(this ClaimsPrincipal principal) =>
            TryGetGuid(principal, ClaimTypes.NameIdentifier);

        private static Guid? TryGetGuid(ClaimsPrincipal principal, string type) =>
            Guid.TryParse(TryGet(principal, type), out var guid) ? (Guid?) guid : null;

        private static string TryGet(ClaimsPrincipal principal, string type) =>
            principal.Claims.FirstOrDefault(x => x.Type == type)?.Value;
    }

    public class ClaimException : Exception
    {
        public ClaimException(string type)
            : base($"Claim {type} not found or has invalid value format")
        {
        }
    }
}