using ServiceXpert.Api.Domain.Shared.Enums;

namespace ServiceXpert.Api.Infrastructure.DbContexts
{
    internal abstract class DbContextBase
    {
        protected string ToVarcharColumn(int length) => $"{nameof(DatabaseDataType.VARCHAR)}({length})";
    }
}
