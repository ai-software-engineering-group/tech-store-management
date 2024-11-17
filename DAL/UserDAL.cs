using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using DTO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DAL
{
    public class UserDAL
    {
        scriptDataContext db = new scriptDataContext();
        public UserDAL() { }
        public List<User> LoadUser()
        {
            try
            {
                if (db == null)
                {
                    throw new InvalidOperationException("Chưa có dữ liệu");
                }

                var userList = db.Users.ToList();
                return userList ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new List<User>();
            }
        }
    }
}
