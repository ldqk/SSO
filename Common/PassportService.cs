using System;

namespace SSO.Core
{
    public class PassportService
    {
        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public bool AuthernVertify(string token, DateTime timestamp) => AuthernUtil.CreateToken(timestamp) == token;

        public LoginResultModel Login(string name, string password)
        {
            LoginResultModel result = new LoginResultModel { IsSuccess = false };
            if (name == "admin" && password == "123")
            {
                result.IsSuccess = true;
                result.UserInfo = new UserModel { UserId = 123 };
            }
            return result;
        }

        public string CreateTicket(string userinfo)
        {
            //加密userinfo
            return "123";
        }

        public string GetReturnUrl(string userinfo, string token, string returnUrl) => $"{returnUrl}{(returnUrl.Contains("?") ? "&" : "?")}ticket={CreateTicket(userinfo)}&token={token}";
    }
}