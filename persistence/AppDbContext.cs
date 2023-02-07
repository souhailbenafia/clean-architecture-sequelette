using Application.Persistence;
using Domain.Entities.identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        private readonly IMediator _mediator;

       


        /// <summary>
        /// Initializes a new instance of the <see cref="Context" /> class.
        /// </summary>
        /// <param name="options">Creation options. Useful when using InMemory driver for testing.</param>
        /// <param name="mediator"><see cref="IMediator"/> instance.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        #region DbSet 



        #endregion

        #region Congiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Schema
            builder.HasDefaultSchema(Schema);

            base.OnModelCreating(builder);

            // Configuration
          //  builder.ApplyConfiguration(new CertificationConfiguration());
           

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

        #endregion
    }
}
