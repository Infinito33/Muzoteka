using Muzoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muzoteka
{
    public class UserManager
    {
        public void AddUserAccount(uzytkownik user)
        {
            using (muzotekaEntities db = new muzotekaEntities())
            {
                user.uprawnienia = "USER";
                db.uzytkownik.Add(user);
                db.SaveChanges();
            }
        }

        public bool IsLoginNameExist(string loginName)
        {
            using (muzotekaEntities db = new muzotekaEntities())
            {
                return db.uzytkownik.Where(o => o.login.Equals(loginName)).Any();
            }
        }

        public string GetUserPassword(string loginName)
        {
            using (muzotekaEntities db = new muzotekaEntities())
            {
                var user = db.uzytkownik.Where(o => o.login.ToLower().Equals(loginName));
                if (user.Any())
                    return user.FirstOrDefault().password;
                else
                    return string.Empty;
            }
        }

        public bool IsUserInRole(string loginName, string roleName)
        {
            using (muzotekaEntities db = new muzotekaEntities())
            {
                var userList = db.uzytkownik.Where(o => o.login.ToLower().Equals(loginName));
                uzytkownik user;
                if(userList.Any())
                {
                    user = userList.First();
                    if (user != null)
                    {
                        if (user.uprawnienia == roleName)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}