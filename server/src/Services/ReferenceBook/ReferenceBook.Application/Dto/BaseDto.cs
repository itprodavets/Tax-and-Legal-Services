namespace ReferenceBook.Application.Dto
{
    public class BaseDto<TPrimary>
        where TPrimary : notnull
    {
        public TPrimary Id { get; private set; } = default!;
    }
}