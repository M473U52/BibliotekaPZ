using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteka.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BibUser class
public class BibUser : IdentityUser
{
    public string name { get; set; }
    public string surname { get; set; }
    public DateTime? birthDate { get; set; }

    [AllowNull]
    public byte[]? profilePicData { get; set; }
}

