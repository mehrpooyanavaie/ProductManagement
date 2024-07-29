using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Infrastructure.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId= "05446344-f9cc-4566-bd2c-36791b4e28ed",
                    RoleId= "cb275765-1cac-4652-a03f-f8871dd575d1"
                },
                  new IdentityUserRole<string>
                  {
                      UserId = "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                      RoleId = "9845f909-799c-45fd-9158-58c1336ffddc"
                  }
                );
        }
    }
}