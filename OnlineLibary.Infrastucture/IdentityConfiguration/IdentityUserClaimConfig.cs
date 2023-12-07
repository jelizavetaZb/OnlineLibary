﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityUserClaimConfig<T> : IEntityTypeConfiguration<T> where T : IdentityUserClaim<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) => builder
            .ToPluralEntityTypeName();
    }
}
