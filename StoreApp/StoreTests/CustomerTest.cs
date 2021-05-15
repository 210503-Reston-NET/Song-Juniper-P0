using System;
using Xunit;
using StoreModels;

namespace StoreTests
{
    public class CustomerTest
    {
        [Theory]
        [InlineData("Auryn")]
        [InlineData("abCed.Jr")]
        [InlineData("Shin Jong Ou")]
        [InlineData("Something-hyphen")]
        public void NameShouldSetValidData(string input)
        {
            Customer test = new Customer();

            test.Name = input;

            Assert.Equal(input, test.Name);
        }

        [Theory]
        [InlineData("328")]
        [InlineData("")]
        [InlineData("@#!!!")]
        public void NameShouldNotSetInvalidData(string input)
        {
            Customer test = new Customer();

            Assert.Throws<Exception>(() => test.Name = input);
        }
    }
}
