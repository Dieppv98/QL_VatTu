using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;

namespace DKAC.IRepository
{
    public interface IUserRepository
    {
        int Add(User user);
        int Delete(int id);
        User GetById(int? id);
        int Update(User user);
        UserRequestModel GetByUser(string KeySearch, int page, int pageSize);
        User GetByUserName(string UserName, int? id);
        int EditInfo(User user);
        int ChangePassword(int id, string PassWord);
    }
}
