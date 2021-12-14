namespace WishingWell.Api.Application
{
    /// <summary>
    /// The Application Settings class.
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the Application Name.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the Authorization Authority.
        /// </summary>
        public string AuthorizationAuthority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether authentication is enabled.
        /// VERY DANGEROUS, TO ONLY BE USED FOR C/I PURPOSES ONLY.
        /// DISABLES AUTHENTICATION.
        /// </summary>
        public bool DONOTUSE_ONLY_FOR_CI_DISABLE_AUTHENTICATION { get; set; }
    }
}