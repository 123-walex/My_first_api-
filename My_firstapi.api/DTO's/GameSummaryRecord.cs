namespace My_firstapi.api.DTO_s;

public record class GameSummaryRecord
  (
    int Id , 
    string Name , 
    string Genre ,
    decimal Price ,
    DateOnly Releasedate );
