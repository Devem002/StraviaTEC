using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace StraviaSqlApi.Utilities;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter(ConverterMappingHints mappingHints = null)
        : base(
            v => new DateTime(v.Year, v.Month, v.Day),
            v => new DateOnly(v.Year, v.Month, v.Day),
            mappingHints)
    {
    }
}