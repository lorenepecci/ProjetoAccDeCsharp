﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trybe.Domain.Entidades;

namespace Trybe.Domain.Context
{
    public class RedeSocialContext : DbContext
    {
        public RedeSocialContext(DbContextOptions<RedeSocialContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Post> Post { get; set; }

    }
}