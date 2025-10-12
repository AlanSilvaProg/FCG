using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyCaseWebApi.Services.Entities;

[Table("users")]
public class Users
{
    [Required] public Guid id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public bool is_admin { get; set; }
    public UserGames? userGames { get; set; }

    public Users(){}
    
    public Users(Guid id, string username = "", string email = "", string password = "", bool is_admin = false, UserGames? userGames = null)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
        this.is_admin = is_admin;
        this.userGames = userGames;
    }
}