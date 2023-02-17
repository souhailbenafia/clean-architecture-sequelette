using Application.Persistence;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using persistence.Configurations.Entities;
using persistence.Configurations.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string> ,UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IUnitOfWork
    {

        public const string Schema = "dbo";

        /// <summary>
        /// Initializes a new instance of the <see cref="Context" /> class.
        /// </summary>
        /// <param name="options">Creation options. Useful when using InMemory driver for testing.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
         
        }

        #region DbSet 


        
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Car> voitures { get; set; }

        public DbSet<Offre> offers { get; set; }

        public DbSet<Suggestion> suggestions { get; set; }

        public DbSet<Rent> Rents { get; set; }

        

        #endregion

        #region Congiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Schema
            builder.HasDefaultSchema(Schema);

            base.OnModelCreating(builder);

            // Configuration

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new OffreConfiguration());
            builder.ApplyConfiguration(new SuggestionConfiguration());
            builder.ApplyConfiguration(new RentConfiguration());

            // Identity
            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<Role>()
                .ToTable("Roles");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<UserRole>()
                .ToTable("UserRoles");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens");
        }

#endregion

        #region IUnitOfWork

        /// <inheritdoc />
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            // Dispatch domain events
            //await _mediator.DispatchEvents(this);
            // Commit changes
            return await base.SaveChangesAsync(cancellationToken);
        }
        public Task Save()
        {
            throw new NotImplementedException();
        }
        public IUserRepository userRepository => throw new NotImplementedException();

        public ICarRpository carRpository => throw new NotImplementedException();

        public IOffreRepository offreRepository => throw new NotImplementedException();

        public ISuggestionRepository SuggestionRepository => throw new NotImplementedException();

        public IRentRepository rentRepository => throw new NotImplementedException();

        public IRefreshTokenRepository refreshTokenRepositoryrefreshTokenRepository => throw new NotImplementedException();

        #endregion
    }
}
