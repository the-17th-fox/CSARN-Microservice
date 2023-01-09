namespace Core.ViewModels.Accounts
{
    public class TokensViewModel : TokensRefreshingViewModel
    {
        public DateTime RefreshTokenExpiresAt { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
    }
}
