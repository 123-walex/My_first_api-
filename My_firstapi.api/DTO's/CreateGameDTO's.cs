using System.ComponentModel.DataAnnotations;
using My_firstapi.api.Entities;

namespace My_firstapi.api.DTO_s;

public record class CreateGameDTO_s
(   
    //the id was not specified beacuse we don't need that info from the user , the api provides us with that info
    [Required][StringLength(100)]string Name , //i used annotations such as required
    int GenreId ,
    [Range(1 , 100)]decimal Price ,
    DateOnly Releasedate
);
