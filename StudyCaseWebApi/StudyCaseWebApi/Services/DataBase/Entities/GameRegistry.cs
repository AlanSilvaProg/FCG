using System.ComponentModel.DataAnnotations.Schema;

namespace StudyCaseWebApi.Services.Entities;

[Table("game_registry")]
public record GameRegistry(Guid id, string name, decimal base_price, bool active, DateTime? registry_date);