using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Identity.Infastructure;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using InventoryManagement.Services.Interface.Utility;
using Constants = InventoryManagement.Commons.Constants;

namespace InventoryManagement.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IPasswordGeneratorService _passwordGeneratorService;
        public SignInManagerCookie SignInManager => HttpContext.GetOwinContext().Get<SignInManagerCookie>();
        public UserManagerCookie UserManager => HttpContext.GetOwinContext().GetUserManager<UserManagerCookie>();

        public AccountController(IPasswordGeneratorService passwordGeneratorService)
        {
            _passwordGeneratorService = passwordGeneratorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return AuthenticationManager.User.Identity.IsAuthenticated ? (ActionResult)RedirectToAction("AlreadyLogIn", "Account") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LogIn model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindByNameAsync(model.Username);
                    if (await UserManager.IsEmailConfirmedAsync(user.Id))
                        return string.IsNullOrEmpty(returnUrl)
                            ? RedirectToAction("Index", "Home")
                            : RedirectToLocal(returnUrl);
                    ModelState.AddModelError(Constants.EmailNotConfirmErrorKey, Constants.EmailNotConfirm);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return View(model);
                case SignInStatus.Failure:
                    ModelState.AddModelError(Constants.UsernamePassIncorrectErrorKey, Constants.UsernamePassIncorrect);
                    return View(model);
                case SignInStatus.LockedOut:
                    break;
                case SignInStatus.RequiresVerification:
                    break;
            }
            ModelState.AddModelError(string.Empty, Constants.InvalidLogin);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AlreadyLogIn()
        {
            var notificationSuccess = new UserNotification
            {
                Subject = Constants.NotificationSubjectAccessDenied,
                Message = Constants.NotificationMessageAlreadyLogin,
                NavigateButtonTitle = Constants.NotificationNavigateButtonTitleHome,
                NavigateButtonLink = Constants.NotificationNavigateButtonLinkHome
            };
            return RedirectToAction("Notification", "Account", notificationSuccess);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResendConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResendConfirmEmail(ResendConfirmEmail model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(Constants.EmailNotExistErrorKey, Constants.EmailNotExist);
                    return View(model);
                }

                await SendConfirmEmail(user);
                return RedirectToAction("LogIn", "Account");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(Guid userId, string code)
        {
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                var notificationSuccess = new UserNotification
                {
                    Subject = Constants.NotificationSubjectSuccess,
                    Message = Constants.NotificationMessageConfirmEmailSuccess,
                    NavigateButtonTitle = Constants.NotificationNavigateButtonTitleLogin,
                    NavigateButtonLink = Constants.NotificationNavigateButtonLinkLogin
                };
                return RedirectToAction("Notification", "Account", notificationSuccess);
            }

            var notificationError = new UserNotification
            {
                Subject = Constants.NotificationSubjectError,
                Message = Constants.NotificationMessageConfirmEmailFailTokenInvalid,
                NavigateButtonTitle = Constants.NotificationNavigateButtonTitleResendConfirmEmail,
                NavigateButtonLink = Constants.NotificationNavigateButtonLinkResendConfirmEmail
            };
            return RedirectToAction("Notification", "Account", notificationError);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(Constants.EmailNotExistErrorKey, Constants.EmailNotExist);
                    return View(model);
                }

                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ModelState.AddModelError(Constants.EmailNotConfirmErrorKey, Constants.EmailNotConfirm);
                    return View(model);
                }

                var confirmationCode = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var route = new ConfirmEmail
                {
                    UserId = user.Id,
                    Code = confirmationCode
                };

                var callbackUrl = Url.Action("ResetPassword", "Account", route, Request.Url?.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                var notification = new UserNotification
                {
                    Subject = Constants.NotificationSubjectSuccess,
                    Message = Constants.NotificationMessageForgotPasswordSuccess,
                    NavigateButtonTitle = Constants.NotificationNavigateButtonTitleLogin,
                    NavigateButtonLink = Constants.NotificationNavigateButtonLinkLogin
                };

                return RedirectToAction("Notification", "Account", notification);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(Guid userId, string code)
        {
            var findUserResult = await UserManager.FindByIdAsync(userId);

            if (findUserResult == null)
            {
                var notificationErrorUserId = new UserNotification
                {
                    Subject = Constants.NotificationSubjectError,
                    Message = Constants.NotificationMessageResetPasswordFailUserIdInvalid,
                    NavigateButtonTitle = Constants.NotificationNavigateButtonTitleForgotPassword,
                    NavigateButtonLink = Constants.NotificationNavigateButtonLinkForgotPassword
                };
                return RedirectToAction("Notification", "Account", notificationErrorUserId);
            }

            var newPassword = _passwordGeneratorService.Random();

            var resetResult = await UserManager.ResetPasswordAsync(userId, code, newPassword);

            if (resetResult.Succeeded)
            {
                await UserManager.SendEmailAsync(userId, "New password", $"Your new password is {newPassword}");

                var notificationSuccess = new UserNotification
                {
                    Subject = Constants.NotificationSubjectSuccess,
                    Message = Constants.NotificationMessageResetPasswordSuccess,
                    NavigateButtonTitle = Constants.NotificationNavigateButtonTitleLogin,
                    NavigateButtonLink = Constants.NotificationNavigateButtonLinkLogin
                };
                return RedirectToAction("Notification", "Account", notificationSuccess);
            }

            var notificationErrorToken = new UserNotification
            {
                Subject = Constants.NotificationSubjectError,
                Message = Constants.NotificationMessageResetPasswordFailTokenInvalid,
                NavigateButtonTitle = Constants.NotificationNavigateButtonTitleResendConfirmEmail,
                NavigateButtonLink = Constants.NotificationNavigateButtonLinkResendConfirmEmail
            };
            return RedirectToAction("Notification", "Account", notificationErrorToken);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(AuthenticationManager.User.Identity.GetUserId());
                var changePasswordResult = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }

                    return View(model);
                }

                var notification = new UserNotification
                {
                    Subject = Constants.NotificationSubjectSuccess,
                    Message = Constants.NotificationMessageChangePasswordSuccess,
                    NavigateButtonTitle = Constants.NotificationNavigateButtonTitleHome,
                    NavigateButtonLink = Constants.NotificationNavigateButtonLinkHome
                };

                return RedirectToAction("Notification", "Account", notification);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Notification(UserNotification model)
        {
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task SendConfirmEmail(User user)
        {
            var confirmationCode = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var route = new ConfirmEmail
            {
                UserId = user.Id,
                Code = confirmationCode
            };
            var callbackUrl = Url.Action("ConfirmEmail", "Account", route, Request.Url?.Scheme);
            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        }
    }
}