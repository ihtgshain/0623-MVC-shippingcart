using prjLottoApp.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public ActionResult demoForm()
        {
            ViewBag.Ans = "?";
            ViewBag.txtA = "0";
            ViewBag.txtB = "0";
            ViewBag.txtC = "0";

            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                int a = Convert.ToInt32(Request.Form["txtA"]);
                int b = Convert.ToInt32(Request.Form["txtB"]);
                int c = Convert.ToInt32(Request.Form["txtC"]);
                ViewBag.txtA = a.ToString();
                ViewBag.txtB = b.ToString();
                ViewBag.txtC = c.ToString();
                double r1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                double r2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                ViewBag.Ans = $"{Math.Round(r1,0)},{Math.Round(r2, 2)}";
            }
            return View();
        }

        public string testingNumber()
        {
            string str = new CCustomerFactory().queryAll().Count.ToString();
            return str;
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\qnote\clock.jpg");
            Response.End();
            return "";
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "找不到該客戶資料";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();

            string s = "找不到該客戶資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" +
                    reader["fPhone"].ToString() + " / " + reader["fEmail"].ToString();
            con.Close();
            return s;

        }

        public string demoParameter(int? id)
        {

            if (id == 1)
                return "XBox 加入購物車成功";
            else if (id == 2)
                return "PS5 加入購物車成功";
            else if (id == 3)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoRequest()
        {
            string id = Request.QueryString["productId"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }
        [NonAction]
        public string lotto()
        {
            return (new CLottoGen()).getLotto();
        }
        // GET: A
        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                }
                con.Close();
            }
            return View(x);
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "沒有指定PK";
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";

        }
        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId=5,
                fAddress = "Taipei",
                fPhone = "0911223445"
            };
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "Clock Chen",
               // fAddress = "Taipei",
                fEmail = "clock@cdc.gov.tw",
                fPassword = "1922",
              //  fPhone = "0911223445"
            };
            (new CCustomerFactory()).insert(x);
            return "新增資料成功";
        }

        // GET: A
        public ActionResult showById(int? id)
        {
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    ViewBag.KK = x;
                }
                con.Close();
            }
            return View();
        }
    }
}