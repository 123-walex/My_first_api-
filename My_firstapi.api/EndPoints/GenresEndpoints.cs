using System;
using Microsoft.EntityFrameworkCore;
using My_firstapi.api.Data;
using My_firstapi.api.Mapping;

namespace My_firstapi.api.EndPoints;

public static class GenresEndpoints
{
   public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
     {
        var group = app.MapGroup("genres");
        
        // turns out after using the routegroupbuilder you now use group instead of app 
        group.MapGet("/" , async (GamestoreContext dbContext) => 
            await dbContext.Genres
                      .Select(genre => genre.ToDTO())
                      .AsNoTracking()
                      .ToListAsync()
         );      
        return group ;  
     }
}
