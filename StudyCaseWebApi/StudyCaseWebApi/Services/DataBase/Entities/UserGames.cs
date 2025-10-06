using System.ComponentModel.DataAnnotations.Schema;

namespace StudyCaseWebApi.Services.Entities;

[Table("user_games")]
public record UserGames(Guid user_id, Guid[] game_ids);