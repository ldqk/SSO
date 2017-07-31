namespace SSO.Core
{
    public class LoginResultModel
    {
        /// <summary>
        /// 成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserModel UserInfo { get; set; }
    }

    public class UserModel
    {
        public int UserId { get; set; }
    }
}