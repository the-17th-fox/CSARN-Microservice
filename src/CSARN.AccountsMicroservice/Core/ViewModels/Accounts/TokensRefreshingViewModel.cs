namespace Core.ViewModels.Accounts
{
    /// <summary>
    /// TODO: Add validation attributes
    /// </summary>
    public class TokensRefreshingViewModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid RefreshToken { get; set; } = Guid.Empty;
    }
}
