using StudyCaseWebApi.Services;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Extensions;

public static class GameRegistryExtension
{
    public static async Task SaveOnDatabase(this GameRegistry gameRegistry, IDataBaseService dataBaseService)
    {
        dataBaseService.GameRegistry().Add(gameRegistry);
        await dataBaseService.SaveChangesAsync();
    } 
}