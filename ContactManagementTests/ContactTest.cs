using ContactManagementWebApp.Models.Contact;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ContactManagementTests
{
    public class ContactTest
    {

        [Fact]
        public void Ensure_Name_Required()
        {
            var contact = new ContactEntity
            {
                Name = "",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456789"
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("The Name field is required"));
        }

        [Theory]
        [InlineData("test1234")]
        [InlineData("Delmer")]
        public void Ensure_Name_AtLeast6(string name)
        {
            var contact = new ContactEntity
            {
                Name = name,
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456789"
            };

            Assert.DoesNotContain(ValidateModel(contact), v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Name should be a string of any size greater or equal to 6"));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("1234")]
        [InlineData("adm")]
        public void Ensure_Name_ThrowsIfLessThan6(string name)
        {
            var contact = new ContactEntity
            {
                Name = name,
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456789"
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("Name should be a string of any size greater or equal to 6"));
        }

        [Fact]
        public void Ensure_EmailAddress_Required()
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "",
                Contact = "123456789"
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("EmailAddress") && v.ErrorMessage.Contains("The EmailAddress field is required"));
        }

        [Theory]
        [InlineData("test@test.com")]
        [InlineData("test@test.com.br")]
        public void Ensure_EmailAddress_IsValid(string emailAddress)
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = emailAddress,
                Contact = "123456789"
            };

            Assert.DoesNotContain(ValidateModel(contact), v => v.MemberNames.Contains("EmailAddress") && v.ErrorMessage.Contains("Email should be a valid email"));
        }

        [Theory]
        [InlineData("@test.com")]
        [InlineData("test@")]
        public void Ensure_EmailAddress_ThrowsIfInvalid(string emailAddress)
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = emailAddress,
                Contact = "123456789"
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("EmailAddress") && v.ErrorMessage.Contains("Email should be a valid email"));
        }

        [Fact]
        public void Ensure_Contact_Required()
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = ""
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("Contact") && v.ErrorMessage.Contains("The Contact field is required"));
        }

        [Fact]
        public void Ensure_Contact_Has9Number()
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456789"
            };

            Assert.DoesNotContain(ValidateModel(contact), v => v.MemberNames.Contains("Contact") && v.ErrorMessage.Contains("Contact should be 9 digits"));
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("12345678910")]
        public void Ensure_Contact_ThrowIfDiffThan9Number(string contactNumber)
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = contactNumber
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("Contact") && v.ErrorMessage.Contains("Contact should be 9 digits"));
        }

        [Fact]
        public void Ensure_Contact_IsOnlyNumber()
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456789"
            };

            Assert.DoesNotContain(ValidateModel(contact), v => v.MemberNames.Contains("Contact") && v.ErrorMessage.Contains("Contact should be 9 digits"));
        }

        [Fact]
        public void Ensure_Contact_ThrowsIfNotNumber()
        {
            var contact = new ContactEntity
            {
                Name = "test123",
                EmailAddress = "alfasoft@alfasoft.com",
                Contact = "123456abc"
            };

            Assert.Contains(ValidateModel(contact), v => v.MemberNames.Contains("Contact") && v.ErrorMessage.Contains("Contact should be 9 digits"));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}