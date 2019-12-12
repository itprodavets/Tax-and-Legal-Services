using System.Diagnostics.CodeAnalysis;
using Common.Domain;
using ReferenceBook.Domain.Enums;

namespace ReferenceBook.Domain.AggregatesModel.LanguageAggregate
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
	public class Language : Entity<short>
	{
		private Language() { }
		public Language(string name, string nativeName, LanguageCode code)
		{
			Name = name;
			NativeName = nativeName;
			Code = code;
		}
		
		public string Name { get; private set; }
		public string NativeName { get; private set; }
		public LanguageCode Code { get; private set; }
	}
}