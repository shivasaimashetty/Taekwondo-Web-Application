using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taekwondo.Models;
using System.Data.SqlClient;

namespace Taekwondo.Controllers
{
    public class AccountController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-86M640S9;database = Taekwondo;integrated security = SSPI;";
        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tbl_login where UserName = '" + acc.Name + "' and Password = '" + acc.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return RedirectToAction("Index","Home");

            }
            else
            {
                con.Close();
                return View("Error");
            }
        }
    }
}