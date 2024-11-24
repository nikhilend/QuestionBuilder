using QuestionBuilder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBuilder.Data
{
    public class UserData
    {
        public static List<User> GetUserData() {

            List<User> users = new List<User>();
            users.Add(new User(){ UserName = "admin", Password = "admin", UserType = "A" });
            users.Add(new User() { UserName = "trainer", Password = "trainer", UserType = "T" });
            users.Add(new User() { UserName = "employee", Password = "employee", UserType = "E" });

            return users;
        }
    }
}
