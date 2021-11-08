using System;
using Microsoft.EntityFrameworkCore;

namespace FinalDesarrollo.DbModels
{
    public class ApplicationDbContextOptions : IApplicationDbContextOptions
    {
        public readonly DbContextOptions<ctrlalumnosContext> Options;

        public ApplicationDbContextOptions(DbContextOptions<ctrlalumnosContext> options)
        {
            Options = options;
        }
    }
}
