using Annium.Testing;

namespace TaxLegal.Cbc.Report.UnitTests
{
    public class SampleTest
    {
        [Fact]
        public void True_IsTrue()
        {
            // arrange
            var value = true;

            // assert
            value.IsTrue();
        }
    }
}