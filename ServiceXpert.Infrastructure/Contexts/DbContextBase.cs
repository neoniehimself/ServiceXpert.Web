using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.Contexts
{
    internal abstract class DbContextBase
    {
        protected static string ToVarcharColumn(int length) => $"{nameof(DatabaseDataType.VARCHAR)}({length})";
    }
}
