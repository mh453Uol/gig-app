using Gig.Models;
using Gig.Persistence;
using Gig.WebApiControllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace Gig.Test
{
    [TestClass]
    public class UnitTest1
    {
        private GigsController controller;
        private Mock<UserManager<ApplicationUser>> userManager;
        public UnitTest1()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1"),
                 new Claim(ClaimTypes.Email,"HUSSAIN"),
                 new Claim(ClaimTypes.Name,"MAJID"),
                 new Claim(ClaimTypes.Anonymous, "example claim value")
            }));

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            userManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var mockUow = new Mock<IUnitOfWork>();

            controller = new GigsController(mockUow.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = MockingHelper.GetClaims() }
            };


            //var mockHttpContext = new Mock<HttpContext>();

            //var identity = new GenericIdentity("user1@domain.com");

            //identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "username"));
            //identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            //var principal = new GenericPrincipal(identity, new string[] { "users" });

            //mockHttpContext.SetupGet(x => x.User).Returns(principal);

            //var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            //userManager = new Mock<UserManager<ApplicationUser>>(
            //    userStoreMock.Object, null, null, null, null, null, null, null, null);

            //var mockUow = new Mock<IUnitOfWork>();

            //controller = new GigsController(mockUow.Object, userManager.Object)
            //{
            //    ControllerContext = new ControllerContext
            //    {
            //        HttpContext = mockHttpContext.Object
            //    }
            //};
        }

        [TestMethod]
        public void TestMethod1()
        {
            controller = new GigsController(MockingHelper.UnitOfWork, MockingHelper.ApplicationManager);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = MockingHelper.GetClaims() }
            };

            var u = controller._userManager.GetUserId(controller.User);
        }
    }
}
