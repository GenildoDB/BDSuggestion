using BDSuggestion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace BDSuggestion
{
    public class MainEntitie : DbContext
    {
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Sugestoes> Sugestoes { get; set; }

        private readonly string _databasePath;
        public MainEntitie()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "dbsugest.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite($"Filename={_databasePath}");
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }
    }
}
