namespace SSO.Core
{
    public class LoginService
    {
        /// <summary>
        /// 根据ticket来解密获取当前用户信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public UserInfoModel GetUserInfo(string ticket)
        {
            //todo 解密ticket 获取用户Id
            return new UserInfoModel { UserId = 123, UserName = "懒得勤快" };
        }
    }
}