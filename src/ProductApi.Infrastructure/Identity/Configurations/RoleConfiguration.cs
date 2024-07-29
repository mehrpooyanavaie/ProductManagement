using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Infrastructure.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id= "9845f909-799c-45fd-9158-58c1336ffddc",
                    Name= "karbar",
                    NormalizedName="KARBAR"
                },
                new IdentityRole
                {
                    Id = "cb275765-1cac-4652-a03f-f8871dd575d1",
                    Name = "administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
                );
        }
    }
}
