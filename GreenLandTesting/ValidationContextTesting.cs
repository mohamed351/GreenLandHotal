using GreenLandTesting.Moqs;
using NUnit.Framework;
using ValidationInputs;

namespace Tests
{
    [TestFixture]
    public class ValidationContextTesting
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validator_WhenUserDoesNotFillAll_false()
        {
          var result =  WindowsFormValidationContext.Validated<EmployeesMoq>(new EmployeesMoq()
            {
                Name = "Mohamed",
                Salary = null
            });


            Assert.False(result.Item1);
        }
        [Test]
        public void Validator_WhenUserFillAll_true()
        {
            var result = WindowsFormValidationContext.Validated<EmployeesMoq>(new EmployeesMoq()
            {
                Name = "Mohamed",
                Salary = 1000
            });


            Assert.True(result.Item1);
        }
        [Test]
        public void Validator_WhenUserFillAll_null()
        {
            var result = WindowsFormValidationContext.Validated<EmployeesMoq>(new EmployeesMoq()
            {
                Name = "Mohamed",
                Salary = 1000
            });


            Assert.Null(result.Item2);
        }

        [Test]
        public void Validator_WhenUserDoesnNotFillAll_NotNull()
        {
            var result = WindowsFormValidationContext.Validated<EmployeesMoq>(new EmployeesMoq()
            {
                Name = "Mohamed",
                Salary = null
            });


            Assert.IsNotNull(result.Item2);
        }


        [Test]
        public void Validator_WhenUserDoesnNotFillAll_ListIsGreaterThanZero()
        {
            var result = WindowsFormValidationContext.Validated<EmployeesMoq>(new EmployeesMoq()
            {
                Name = "Mohamed",
                Salary = null
            });


            Assert.Greater(result.Item2.Count, 0);
        }


    }
}