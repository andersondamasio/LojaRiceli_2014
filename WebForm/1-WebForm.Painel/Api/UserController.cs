using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Loja
{
    public class UserController  : ApiController
    {

        public string GetAllUsers()
        {

            return "GetAllUsers";
        }

        public string GetUserById(int id)
        {
            return "GetUserById";
        }


        public string AddUser(string user)
        {
            return "AddUser";
        }

        public void DeleteUser(int id)
        {
            
        }
    }
}