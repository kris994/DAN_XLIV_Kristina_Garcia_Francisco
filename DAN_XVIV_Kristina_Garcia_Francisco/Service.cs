using DAN_XVIV_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_XVIV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        /// <summary>
        /// Saves the login user
        /// </summary>
        public static List<tblUser> LoggedInUser = new List<tblUser>();

        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (OrderDB context = new OrderDB())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
