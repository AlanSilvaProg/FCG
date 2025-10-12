using System.ComponentModel.DataAnnotations.Schema;

namespace StudyCaseWebApi.Services.Entities;

[Table("user_games")]
public class UserGames
{
    [ForeignKey("users")]
    public Guid user_id { get; set; }

    public Guid[] game_ids { get; set; }

    public Users? users { get; set; }

    public UserGames() { }

    public UserGames(Guid user_id, Guid[] game_ids, Users? users = null)
    {
        this.user_id = user_id;
        this.game_ids = game_ids;
        this.users = users;
    }
}