namespace DKAC.Common
{
    public class Encryption
    {
        public static string EncryptPassword(string password)
        {
            // hash password
            return SecurityUtility.EncryptBase64(SecurityUtility.EncryptMd5(password), CommonConstants.KeyPassWord);
        }
    }
}