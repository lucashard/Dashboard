using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class PendingNOAController : BaseController
    {
        //
        // GET: /PendingNOA/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtainListResult()
        {
            var table = ReadExcel(10, 12, 5, 9,6);
            var listName = (from co in table.AsEnumerable()
                select co.Field<string>("colunmn1")).ToList();
            var totalClient = (from co in table.AsEnumerable()
                            select co.Field<string>("colunmn3")).ToList();
            var implemented = (from co in table.AsEnumerable()
                               select co.Field<string>("colunmn5")).ToList();
            var result = new { data = listName, data2 = totalClient.Select(int.Parse).ToList(), data3 = implemented.Select(int.Parse).ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtainListResultGoogle()
        {
            //var table = ReadExcel(10, 12, 5, 9, 6);
            var table = DataTable(10, 12, 5, 9, ReadExcelGoogle("LEAN IMPLEMENTATION!J5:M8"));
            var listName = (from co in table.AsEnumerable()
                            select co.Field<string>("colunmn1")).ToList();
            var totalClient = (from co in table.AsEnumerable()
                               select co.Field<string>("colunmn3")).ToList();
            var implemented = (from co in table.AsEnumerable()
                               select co.Field<string>("colunmn5")).ToList();
            var result = new { data = listName, data2 = totalClient.Select(int.Parse).ToList(), data3 = implemented.Select(int.Parse).ToList() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

      
    }
}