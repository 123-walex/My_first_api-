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
    group.MapGet("/" , (GamestoreContext dbContext) => 
          dbContext.Games
          .Include(game => game.Genre)
          .Select(game => game.ToGameSummaryRecordDto())
          .AsNoTracking());

    //Get with a specific id 
    group.MapGet("/{id}" , (int id , GamestoreContext dbContext) =>
     {
       Game? game = dbContext.Games
                   .Include(g => g.Genre)
                   .FirstOrDefault(g => g.Id == id);

       return game is null ? Results.NotFound() : Results.Ok(game.ToGameRecordDetails());
     })
    .WithName(GetGameEndPointName);

    // i'm just defining the post endpoint , POST /games
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

    // defining the put endpoint PUT /games || IT UPDATES A DTO 
    group.MapPut("/{id}" , (int id , UpdateGameDTO_s updatedgame , GamestoreContext dbContext) => 
    {
        var existinggame = dbContext.Games.Find(id);

        // essentially if i sent a put request for a resource and it is not found , it throws a 404 not found
        if(existinggame is null)
          {
             return Results.NotFound();
          }
           
        dbContext.Entry(existinggame)
                   .CurrentValues
                   .SetValues(updatedgame.ToEntity(id)) ;
        
        dbContext.SaveChanges() ;  // this line is very necessary when you're don dealing with the database

        return Results.NoContent();
    });

    //defining the delete endpoint DELETE/games ,  GamestoreContext dbContext is the dependency injection 
    group.MapDelete("/{id}" , (int id , GamestoreContext dbContext ) =>
    {
       dbContext.Games
                 .Where(game => game.Id == id)
                 .ExecuteDelete();              // apparently i added something called batch delete here

       return Results.NoContent();
    });
       return group ;
  }
}
