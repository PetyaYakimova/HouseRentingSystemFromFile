using HouseRentingSystemFromFile.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemFromFile.Tests.IntegrationsTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController homeController;

        [OneTimeSetUp]
        public void SetUp() => this.homeController = new HomeController(null);

        [Test]
        public void Error_ShouldReturnCorrectView()
        {
            //Arrange
            var statusCode = 500;

            //Act
            var result = this.homeController.Error(statusCode);

            //Assert
            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
