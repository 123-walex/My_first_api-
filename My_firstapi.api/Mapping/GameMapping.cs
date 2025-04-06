using System; 
// here th mapping is between entities and dtos 
using My_firstapi.api.DTO_s;
using My_firstapi.api.Entities;

namespace My_firstapi.api.Mapping;

public static class GameMapping
{
   public static Game ToEntity(this CreateGameDTO_s game)
   {
        return new Game()
        {
           Name = game.Name,
           GenreId = game.GenreId ,
           Price = game.Price ,
           Releasedate = game.Releasedate 
        };
   }

   public static Game ToEntity(this UpdateGameDTO_s game , int id)
   {
        return new Game()
        {
           Id = id,
           Name = game.Name,
           GenreId = game.GenreId ,
           Price = game.Price ,
           Releasedate = game.Releasedate 
        };
   }

   public static GameSummaryRecord ToGameSummaryRecordDto(this Game game)
   {
       return new 
        (
           game.Id ,
           game.Name ,
           game.Genre?.Name ?? "Unknown" ,
           game.Price ,
           game.Releasedate 
        );
   }
   
   public static GameRecordDetails ToGameRecordDetails(this Game game)
   {
       return new 
        (
           game.Id ,
           game.Name ,
           game.GenreId,
           game.Price ,
           game.Releasedate 
        );
   }
}
