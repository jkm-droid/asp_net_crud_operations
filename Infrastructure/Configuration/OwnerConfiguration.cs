using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {

        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(
                new Owner
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "John Doe",
                    Email = "johndoe@info.co",
                    Address = "354 St.Avenue",
                    Country = "kenya"
                }
                );
        }
    }
}
