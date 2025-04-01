namespace My_firstapi.api.DTO_s;

public record class GameRecord
  (
    int Id , 
    string Name , 
    string Genre ,
    decimal Price ,
    DateOnly Releasedate );
