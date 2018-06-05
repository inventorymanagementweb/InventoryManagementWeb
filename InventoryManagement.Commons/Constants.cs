using System.Configuration;

namespace InventoryManagement.Commons
{
    public class Constants
    {
        public const string EntityFrameworkConnectionString = "InventoryManagement";

        public static readonly int PasswordMinLenght = int.Parse(ConfigurationManager.AppSettings["PasswordMinLenght"]);
        public static readonly double AccountLockoutTimespanMinutes = int.Parse(ConfigurationManager.AppSettings["AccountLockoutTimespanMinutes"]);
        public static readonly int MaxFailedAcessAttemptsBeforeLockout = int.Parse(ConfigurationManager.AppSettings["MaxFailedAcessAttemptsBeforeLockout"]);
        public const string DataProtectorTokenProviderPurposes = "InventoryManagement Identity";

        public static readonly string ConfigurationEmailServiceEmailAddress = ConfigurationManager.AppSettings["ConfigurationEmailServiceEmailAddress"];
        public static readonly string ConfigurationEmailServiceLicenseCode = ConfigurationManager.AppSettings["ConfigurationEmailServiceLicenseCode"];
        public static readonly string ConfigurationEmailServiceDisplayName = ConfigurationManager.AppSettings["ConfigurationEmailServiceDisplayName"];
        public static readonly string ConfigurationEmailServiceSmtpServer = ConfigurationManager.AppSettings["ConfigurationEmailServiceSmtpServer"];
        public static readonly string ConfigurationEmailServiceEmailAccount = ConfigurationManager.AppSettings["ConfigurationEmailServiceEmailAccount"];
        public static readonly string ConfigurationEmailServiceEmailPassword = ConfigurationManager.AppSettings["ConfigurationEmailServiceEmailPassword"];
        public static string EmailContentType = "text/html";

        public static readonly double TokenLifespanMinutes = int.Parse(ConfigurationManager.AppSettings["TokenLifespanMinutess"]);
        public static readonly string CookieLoginPath = ConfigurationManager.AppSettings["CookieLoginPath"];
        public static readonly string TokenLoginPath = ConfigurationManager.AppSettings["TokenLoginPath"];
        public const string ConfigurationAudienceId = "414e1927a3884f68abc79f7283837fd1041292";
        public const string ConfigurationAudienceSecret = "qMCdFDQuF23RV1Y-1Gq9L3cF3VmuFwVbam4fMTdAfpo281293";
        public const string AllowedOrigin = "*";
        public const string AccessControlAllowOrgin = "Access-Control-Allow-Origin";
        public const string ErrInvalidGrant = "invalid_grant";
        public const string AuthenticationType = "JWT";

        public const string InvalidLogin = "Invalid login attempt.";
        public const string UsernamePassIncorrect = "User name or password is incorrect.";
        public const string UsernamePassIncorrectErrorKey = "User name or password is incorrect.";
        public const string EmailNotConfirm = "Your email have not yet confirmed. Please confirm your email or click Confirm Email to resend new confirm email.";
        public const string EmailNotConfirmErrorKey = "Email not confirm";

        public const string EmailNotExistErrorKey = "Email not exist";
        public const string EmailNotExist = "Your email have not yet registered.";

        public const string NotificationSubjectSuccess = "Success";
        public const string NotificationSubjectError = "Error";
        public const string NotificationSubjectAccessDenied = "Access denied";

        public const string NotificationNavigateButtonTitleHome = "Go to Home page";
        public const string NotificationNavigateButtonLinkHome = "../Home/Index";
        public const string NotificationNavigateButtonTitleLogin = "Go to Login page";
        public const string NotificationNavigateButtonLinkLogin = "../Account/Login";
        public const string NotificationNavigateButtonTitleForgotPassword = "Forgot password";
        public const string NotificationNavigateButtonLinkForgotPassword = "../Account/ForgotPassword";
        public const string NotificationNavigateButtonTitleResendConfirmEmail = "Resend confirm email";
        public const string NotificationNavigateButtonLinkResendConfirmEmail = "../Account/ResendConfirmEmail";

        public const string NotificationMessageAlreadyLogin = "Sorry, you are already login. So you can not login or register. Please logoff to continute.";
        public const string NotificationMessageConfirmEmailSuccess = "Congratulation, your password will be reset after you confirm by email. Please check your email to go to next step.";
        public const string NotificationMessageConfirmEmailFailTokenInvalid = "Sorry, your token is invalid. Please click Resend confirm email to get new token.";
        public const string NotificationMessageForgotPasswordSuccess = "Congratulation, your password will be reset after you confirm by email. Please check your email to go to next step.";
        public const string NotificationMessageResetPasswordFailUserIdInvalid = "Sorry, your userid is invalid. Please click Forgot Password to get new confirm email.";
        public const string NotificationMessageResetPasswordSuccess = "Congratulation, your new password will be send to your email. Please check your email to get new password.";
        public const string NotificationMessageResetPasswordFailTokenInvalid = "Sorry, your token is invalid. Please click Resend confirm email to get new token.";
        public const string NotificationMessageChangePasswordSuccess = "Congratulation, your password was changed.";

        public const string RegisterAccountErrorAddNewEmployee = "Can not register your account because can not add new EmployeeData.";
    }
}
