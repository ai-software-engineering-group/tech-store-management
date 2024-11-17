using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BLL
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();
        public UserBLL() { }
        public List<User> GetUser()
        {
            try
            {
                return userDAL.LoadUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách khách hàng: " + ex.Message);
                return null;
            }
        }
    }
}
