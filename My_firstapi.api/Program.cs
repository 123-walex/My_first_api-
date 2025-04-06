//this must be kept clean at all costs

using My_firstapi.api.Data;
using My_firstapi.api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore") ;

builder.Services.AddSqlite<GamestoreContext>(connString) ;

var app = builder.Build();

app.MapGamesEndPoint() ;

await app.MigrateDbAsync();

app.Run();