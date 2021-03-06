﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AM.BLL.Common.Core;
using AM.DAL.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace AM.DAL.Core
{
    public class AMDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AMDBContext()
        {
        }
        public AMDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AMDBContext(DbContextOptions<AMDBContext> options)
            : base(options)
        {
        }

        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Profession> Profession { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<ProfessionalProfile> ProfessionalProfile { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configurations = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var connectionString = configurations.GetConnectionString("DBConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public object Query<T>(string v)
        {
            throw new NotImplementedException();
        }
    }
}
