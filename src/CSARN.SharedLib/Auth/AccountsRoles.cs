using Microsoft.AspNetCore.Identity;

namespace SharedLib.Auth
{
    public class AccountsRoles
    {
        public const string Citizen = "Citizen";
        public const string Administrator = "Administrator";

        public const string MES = "MinOfEmergencySituations";
        public const string MIA = "MinOfInternalAffairs";
        public const string MH = "MinOfHealth";
        public const string MD = "MinOfDefence";
        public const string ME = "MinOfEducation";

        public static List<IdentityRole<Guid>> Roles { get; } = new()
        {
            new IdentityRole<Guid>(Citizen) { Id = Guid.NewGuid(), NormalizedName = Citizen.Normalize() },
            new IdentityRole<Guid>(Administrator) { Id = Guid.NewGuid(), NormalizedName = Administrator.Normalize() },
            new IdentityRole<Guid>(MES) { Id = Guid.NewGuid(), NormalizedName = MES.Normalize() },
            new IdentityRole<Guid>(MIA) { Id = Guid.NewGuid(), NormalizedName = MIA.Normalize() },
            new IdentityRole<Guid>(MH) { Id = Guid.NewGuid(), NormalizedName = MH.Normalize() },
        };
    }
}
