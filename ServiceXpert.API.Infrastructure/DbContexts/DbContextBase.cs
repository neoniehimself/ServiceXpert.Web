using ServiceXpert.API.Domain.Shared.Enums.Database;

namespace ServiceXpert.API.Infrastructure.DbContexts
{
    internal abstract class DbContextBase
    {
        protected string ToVarcharColumn(int length) => $"{nameof(DatabaseDataType.VARCHAR)}({length})";
    }
}
