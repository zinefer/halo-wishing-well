using System;
using System.Security.Claims;

namespace WishingWell.Api.Extensions
{
    /// <summary>
    /// JWT user information tokens.
    /// </summary>
    public static class JwtTokenExtension
    {
        /// <summary>
        /// returns the jwt tokens user id.
        /// </summary>
        /// <param name="user">the claims principal.</param>
        /// <returns>the value of "sub" (user id) if it exists else -1.</returns>
        public static int Id(this ClaimsPrincipal user)
        {
            string claimValue = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(claimValue))
            {
                return GrabClaim<int>(user, "sub");
            }

            return GrabClaim<int>(user, ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// retrieves the org id of the authorization token.
        /// </summary>
        /// <param name="user">Claims principal user.</param>
        /// <returns>the org id for the user in the jwt token.</returns>
        public static int OrgId(this ClaimsPrincipal user)
        {
            return GrabClaim<int>(user, "org_id");
        }

        /// <summary>
        /// methodologies for grabbing info from relias jwt token.
        /// </summary>
        /// <param name="user">Claims principal user.</param>
        /// <param name="name">name of the claim.</param>
        /// <returns>value of requested claim.</returns>
        private static T GrabClaim<T>(ClaimsPrincipal user, string name)
        {
            T value;

            string claimValue = user.FindFirstValue(name);

            if (string.IsNullOrEmpty(claimValue))
            {
                throw new ArgumentNullException(name, "not found in claims.");
            }
            else
            {
                try
                {
                    value = (T)Convert.ChangeType(claimValue, typeof(T));

                    return value;
                }
                catch
                {
                    throw new InvalidCastException($"Unable to cast claim {name} to type {typeof(T)}");
                }
            }
        }
    }
}