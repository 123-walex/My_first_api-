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

public static RouteGroupBuilder MapGamesEndPoint(this WebApplication app)
{
    //this is defining a group Builder , where all routes have the "games" prefix
    var group = app.MapGroup("games")
                .WithParameterValidation() ;

    //Get / games : this command gets the games from the server . 
    group.MapGet("/" , async (GamestoreContext dbContext) => 
           await dbContext.Games
          .Include(game => game.Genre)
          .Select(game => game.ToGameSummaryRecordDto())
          .AsNoTracking()
          .ToListAsync());    // async and await are from asynchronous programming 

        //Get with a specific id 
    group.MapGet("/{id}", async (int id, GamestoreContext dbContext) =>
         {
             Game? game = await dbContext.Games
                     .Include(g => g.Genre)
                     .FirstOrDefaultAsync(g => g.Id == id);

             return game is null ? Results.NotFound() : Results.Ok(game.ToGameRecordDetails());
         })
        .WithName(GetGameEndPointName);

    // i'm just defining the post endpoint , POST /games
    group.MapPost("/" , async (CreateGameDTO_s newgame , GamestoreContext dbContext ) => 
    {
        Game game = newgame.ToEntity();

        dbContext.Games.Add(game) ;
        await dbContext.SaveChangesAsync() ;
        
        return Results.CreatedAtRoute(
            GetGameEndPointName ,
            new { id = game.Id} ,
            game.ToGameSummaryRecordDto());
    }); 

    // defining the put endpoint PUT /games || IT UPDATES A DTO 
    group.MapPut("/{id}" , async (int id , UpdateGameDTO_s updatedgame , GamestoreContext dbContext) => 
    {
        var existinggame = await dbContext.Games.FindAsync(id);

        // essentially if i sent a put request for a resource and it is not found , it throws a 404 not found
        if(existinggame is null)
          {
             return Results.NotFound();
          }
           
        dbContext.Entry(existinggame)
                   .CurrentValues
                   .SetValues(updatedgame.ToEntity(id)) ;
        
        await dbContext.SaveChangesAsync() ;  // this line is very necessary when you're done dealing with the database

        return Results.NoContent();
    });

    //defining the delete endpoint DELETE/games ,  GamestoreContext dbContext is the dependency injection 
    group.MapDelete("/{id}" , async (int id , GamestoreContext dbContext ) =>
    {
        await dbContext.Games
                 .Where(game => game.Id == id)
                 .ExecuteDeleteAsync();              // apparently i added something called batch delete here

       return Results.NoContent();
    });
       return group ;
  }
}
