namespace Api.Models.Configuration
{
    public class Settings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int ExpiryTimeInSeconds { get; set; } = 0;
    }
}
