namespace ReferenceBook.Application.Dto.Languages
{
    public class LanguageDto : BaseDto<short>
    {
        public string Name { get; private set; } = string.Empty;
        public string NativeName { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
    }
}