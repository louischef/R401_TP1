using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        [TestMethod()]
        public void GetAllTest()
        {

        }

        [TestMethod()]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod()]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(4);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult"); //Test de l'erreur
            Assert.IsNull(result.Value, "Value est pas null"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void GetAll_ReturnsRightsItems()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            List<Devise> devises = new List<Devise>();
            //Act
            var result = controller.GetAll();
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));
            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un IEnumerable");
            CollectionAssert.AreEqual(devises, result.ToList());
        }

        [TestMethod()]
        public void Post_ValidObjectPassed_ReturnsObject()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.Post(new Devise(4, "Rouble", 500));
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult");
            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created);
            Assert.AreEqual(routeResult.Value, new Devise(4, "Rouble", 500));
        }

        /*[TestMethod()]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.Post(new Devise(4, null, 500));
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult");
            Assert.AreEqual(routeResult.StatusCode, StatusCodes.Status201Created);
            Assert.AreEqual(routeResult.Value, new Devise(4, null, 500));
        }*/

        [TestMethod()]
        public void Put_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.Put(3, new Devise(3, "Rouble", 500));
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void Put_InvalidUpdate_ReturnsNotFound()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.Put(4, new Devise(4, "Rouble", 500));
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void Put_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.Put(2, new Devise(3, "Rouble", 500));
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Delete_Ok_ReturnsRightItem()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.DeleteById(1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsNull(result.Result, "Erreur est pas null");
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise");
        }

        [TestMethod()]
        public void Delete_NotOk_ReturnsNotFound()
        {
            //Arrange
            DevisesController controller = new DevisesController();
            //Act
            var result = controller.DeleteById(7);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
            Assert.IsNull(result.Value, "Value est pas null");
        }
    }
}