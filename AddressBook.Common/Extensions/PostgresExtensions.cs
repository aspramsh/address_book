using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;

namespace AddressBook.Common.Extensions
{
    public static class PostgresExtensions
    {
        public static void PostgresModelCreating(this ModelBuilder builder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // modify column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName(), null))));
                }

                // modify table name
                entity.SetTableName(mapper.TranslateMemberName(entity.GetTableName()));

                // modify keys names
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(mapper.TranslateMemberName(key.GetName()));
                }

                // modify foreign keys names
                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(mapper.TranslateMemberName(key.GetConstraintName()));
                }

                // modify indexes names
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(mapper.TranslateMemberName(index.GetDatabaseName()));
                }

                // move asp_net tables into schema 'identity'
                if (entity.GetTableName().StartsWith("asp_net_"))
                {
                    entity.SetTableName(entity.GetTableName().Replace("asp_net_", string.Empty));
                    entity.SetSchema("identity");
                }
            }
        }
    }
}
