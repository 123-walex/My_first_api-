using System;
using Microsoft.EntityFrameworkCore;

namespace My_firstapi.api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
      {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GamestoreContext>();
        dbContext.Database.Migrate();
      }
}
