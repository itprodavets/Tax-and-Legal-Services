namespace ReferenceBook.Application.Dto.Currencies
{
    public class CurrencyDto : BaseDto<short>
    {
        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;
    }
}