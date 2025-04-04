namespace My_firstapi.api.DTO_s;

public record class GameRecordDetails
  (
    int Id , 
    string Name , 
    int GenreId ,
    decimal Price ,
    DateOnly Releasedate );
