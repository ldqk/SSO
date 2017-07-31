using System;
using System.Configuration;
using System.Web;
using Masuit.Tools.DateTimeExt;
using Masuit.Tools.Net;
using Masuit.Tools.Security;

namespace SSO.Core
{
    public class AuthernUtil
    {
        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string CreateToken(DateTime timestamp) => timestamp.GetTotalMilliseconds().ToString().MDString2();

        public static UserInfoModel CurrentUser => HttpContext.Current.Session?.GetSession<UserInfoModel>(ConstantHelper.USER_SESSION_KEY);

        public static string GetAutherUrl(string token, DateTime timestamp) => $"{ConfigurationManager.AppSettings["PassportCenterUrl"]}/Home/PassportCenter?token={token}&timestamp={timestamp}";
    }

    /// <summary>
    /// 静态变量帮助类
    /// </summary>
    public class ConstantHelper
    {
        /// <summary>
        /// 用户SessionKey
        /// </summary>
        public const string USER_SESSION_KEY = "UserSessionKey";

        /// <summary>
        /// 用户CookieKey
        /// </summary>
        public const string USER_COOKIE_KEY = "UserCookieKey";

        public const string TOKEN_KEY = "TokenKey";
    }
}
