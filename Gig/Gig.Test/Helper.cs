namespace Gig.Test
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using Gig.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Http;
    using Gig.Persistence;

    /// <summary>
    /// Helper class to create mocking context
    /// </summary>
    public static class MockingHelper
    {
        private static Mock<UserManager<ApplicationUser>> _applicationUserManager;
        private static Mock<ControllerContext> _controllerContext;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static string userId = "39e59a67-d311-4715-845b-7e60702ec3af";
        private static string username = "majid";
        private static ApplicationUser user = new ApplicationUser { Id = userId, UserName = username };

        public static UserManager<ApplicationUser> ApplicationManager
        {
            get
            {
                if (_applicationUserManager == null)
                {
                    InitMockingAuthenticationLayer();
                }
                return _applicationUserManager.Object;
            }
        }

        public static IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new Mock<IUnitOfWork>();
                }

                return _unitOfWork.Object;
            }
        }

        private static void InitMockingAuthenticationLayer()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            _applicationUserManager = new Mock<UserManager<ApplicationUser>>(userStore.Object);
            //m_applicationUserManager.Setup(u => u.FindByIdAsync(user.Id)).Returns(Task.FromResult(user));
            //m_applicationUserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
        }

        public static ClaimsPrincipal GetClaims()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier,userId),
                 new Claim(ClaimTypes.Email,username),
                 new Claim(ClaimTypes.Name,username),
            }));

            return user;
        }
    }
}