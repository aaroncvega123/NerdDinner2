using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdDinner2.Controllers;

namespace NerdDinner2.Tests.Controllers
{
    [TestClass]
    public class DinnerControllerTest
    {
        [TestMethod]
        public void DetailsAction_Should_Return_View_For_ExistingDinner()
        {

            // Arrange
            var controller = new DinnersController();

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Expected View");
        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusDinner()
        {

            // Arrange
            var controller = new DinnersController();

            // Act
            var result = controller.Details(999) as ViewResult;

            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}
