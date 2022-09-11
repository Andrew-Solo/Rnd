using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rnd.Api.Data.Entities;

[Index(nameof(Login), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User : IEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(32)]
    public string Login { get; set; } = null!;

    [MaxLength(320)]
    public string Email { get; set; } = null!;

    [MaxLength(256)]
    public string PasswordHash { get; set; } = null!;

    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}