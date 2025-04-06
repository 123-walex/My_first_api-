using System;
using My_firstapi.api.DTO_s;
using My_firstapi.api.Entities;

namespace My_firstapi.api.Mapping;

public static class GenreMapping
{
   public static GenreDto ToDTO( this Genre genre )
    {
        return new GenreDto(genre.Id , genre.Name);
    }
}
