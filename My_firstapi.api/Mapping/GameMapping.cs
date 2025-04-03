using System;
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

   public static GameRecord ToDto(this Game game)
   {
       return new 
        (
           game.Id ,
           game.Name ,
           game.Genre!.Name ,
           game.Price ,
           game.Releasedate 
        );
   }
}
