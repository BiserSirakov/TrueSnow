﻿namespace TrueSnow.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TrueSnow.Data.Models;

    public class TrueSnowDbContext : IdentityDbContext<User>
    {
        public TrueSnowDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public static TrueSnowDbContext Create()
        {
            return new TrueSnowDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}