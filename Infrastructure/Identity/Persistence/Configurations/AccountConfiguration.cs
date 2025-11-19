using IGCSELearningHub.Domain.Identity.Entities;
using IGCSELearningHub.Domain.Identity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IGCSELearningHub.Infrastructure.Identity.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            var comparer = new ValueComparer<HashedPassword?>(
                (left, right) =>
                    (left == null && right == null) ||
                    (left != null && right != null && left.Value == right.Value),
                value => value == null ? 0 : value.Value.GetHashCode(StringComparison.Ordinal),
                value => value == null ? null : HashedPassword.From(value.Value));

            builder.Property(a => a.Password)
                .HasConversion(
                    password => password == null ? null : password.Value,
                    value => string.IsNullOrWhiteSpace(value) ? null : HashedPassword.From(value))
                .Metadata.SetValueComparer(comparer);
        }
    }
}
