using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using My_firstapi.api.Data;
using My_firstapi.api.DTO_s;
using My_firstapi.api.Entities;
using My_firstapi.api.Mapping;
using Microsoft.EntityFrameworkCore ;
using System ;
namespace My_firstapi.api.EndPoints;

public static class GameEndPoint
{
    
const string GetGameEndPointName = "GetGame" ;

private static readonly List<GameSummaryRecord> games = [
    new (
        1,
        "Horizons" ,
        "Adventure" ,
        49.99M , 
        new DateOnly(2023 , 12 , 13)),
    new (
        2 , 
        "Fc 24" ,
        "Football" ,
        19.99M ,
        new DateOnly(2024 , 2 , 1)
    ),
    new (
        3 , 
        "Need For Speed" ,
        "Racing" ,
        59.99M ,
        new DateOnly(2020 , 5 , 23)
    ),
] ;

public static RouteGroupBuilder MapGamesEndPoint(this WebApplication app)
{
    //this is defining a group Builder , where all routes have the "games" prefix
    var group = app.MapGroup("games")
                .WithParameterValidation() ;

    //Get / games : this command gets the games from the server . 
    group.MapGet("/" , () => games);

    //Get with a specific id 
    group.MapGet("/{id}" , (int id , GamestoreContext dbContext) =>
     {
       Game? game = dbContext.Games
                   .Include(g => g.Genre)
                   .FirstOrDefault(g => g.Id == id);

       return game is null ? Results.NotFound() : Results.Ok(game.ToGameRecordDetails());
     })
    .WithName(GetGameEndPointName);

    // i'm just defining the create endpoint , POST /games
    group.MapPost("/" , (CreateGameDTO_s newgame , GamestoreContext dbContext ) => 
    {
        Game game = newgame.ToEntity();

        dbContext.Games.Add(game) ;
        dbContext.SaveChanges() ;
        
        return Results.CreatedAtRoute(
            GetGameEndPointName ,
            new { id = game.Id} ,
            game.ToGameSummaryRecordDto());
    }); 

    // defining the post endpoint POST /games
    group.MapPut("/{id}" , (int id , UpdateGameDTO_s updatedgame) => 
    {
        var index = games.FindIndex(game => game.Id == id);

        // essentially if i sent a put request for a resource and it is not found , it throws a 404 not found
        if(index == -1)
          {
             return Results.NotFound();
          }

         games[index] = new GameSummaryRecord(
         id ,
         updatedgame.Name ,
         updatedgame.Genre ,
         updatedgame.Price ,
         updatedgame.Releasedate 
        );
        return Results.NoContent();
    });

    //defining the delete endpoint DELETE/games
    group.MapDelete("/{id}" , (int id) =>
    {
       games.RemoveAll(game => game.Id == id);

       return Results.NoContent();
    });
       return group ;
  }
}
