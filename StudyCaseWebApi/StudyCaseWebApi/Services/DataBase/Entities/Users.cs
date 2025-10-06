using System.ComponentModel.DataAnnotations.Schema;

namespace StudyCaseWebApi.Services.Entities;

[Table("users")]
public record Users(Guid id, string username, string email, string password, bool is_admin);