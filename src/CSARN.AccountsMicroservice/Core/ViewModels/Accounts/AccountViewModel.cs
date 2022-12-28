namespace Core.ViewModels.Accounts
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public Guid PassportId { get; set; }
        public IList<string> Roles { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
