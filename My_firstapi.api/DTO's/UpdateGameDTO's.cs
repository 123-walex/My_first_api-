using System.ComponentModel.DataAnnotations;

namespace My_firstapi.api.DTO_s;

public record class UpdateGameDTO_s
(
    [Required][StringLength(100)]string Name , //i used data annotations such as required
    int GenreId ,
    [Range(1 , 100)]decimal Price ,
    DateOnly Releasedate
);
