using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using adodotnet;
namespace Leave_Tracking_System.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtMail, string txtPswd)
        {
            string connection = database.connectionstring;
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = sqlCon;

                    sqlcmd.CommandText = "select * from EmpDets where EmpEmailID='" + txtMail + "' and EmplID='" + txtPswd + "'";
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader sdr = sqlcmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Session["empId"] = sdr[0];
                        Session["empName"] = sdr[1];
                        
                        //string a = sdr[0].ToString();
                        //string name = sdr[1].ToString();
                        //Session["userName"] = name;

                        sdr.Close();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Leave");
        }
        public ActionResult Leave()
        {
            //int id=Session["empId"],string str=Session["empName"]
            ViewData["employeeID"] = Session["empID"];
            ViewData["employeeName"] = Session["empName"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(string txtEmpId, string txtEmpName, string txtStrDt, string txtEndDt, int items)
        {
            var id = txtEmpId;
            var name = txtEmpName;
            string[] stdt = txtStrDt.Split('-');
            var start = stdt[2];
            string[] eddt = txtEndDt.Split('-');
            var end = eddt[2];
            var days = Convert.ToInt32(end) - Convert.ToInt32(start);
            int num = items;

            string connection = database.connectionstring;
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = sqlCon;

                    sqlcmd.CommandText = "insert into LeaveDets values('"+ id + "','"+txtStrDt+"','"+txtEndDt+"','"+days+"',1,'PAVAN KUMAR')";
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader sdr = sqlcmd.ExecuteReader();
                    sdr.Close();
                    }
                
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Manager");
        }
        public ActionResult Manager()
        {
            return View();
        }
        
    }
}
