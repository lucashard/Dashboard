using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DashBoard.Models.Forecast;

namespace DashBoard.Controllers
{
    public class ForecastController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtainListResult()
        {
            var table = ReadExcel(7, 8, 52, 53,3);
            var resultquery = (from co in table.AsEnumerable()
                         select new ListResultForecast() { Columna1 = co.Field<string>("colunmn1"), Columna2 = co.Field<string>("colunmn3") }).ToList();
            var model=new ForecastModel();
            BindModel(model, resultquery);
            var result = new { data = model};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtainListResultGoogle()
        {
            //var table = DataTable(7, 8, 52, 54, ReadExcelGoogle("Up-to-date Forecast!G52:H53"));
            var table = DataTable(7, 8, 52, 54, ReadExcelGoogle("Up-to-date Forecast!H3:I4"));
            var resultquery = (from co in table.AsEnumerable()
                               select new ListResultForecast() { Columna1 = co.Field<string>("colunmn1"), Columna2 = co.Field<string>("colunmn3") }).ToList();
            var model = new ForecastModel();
            BindModel(model, resultquery);
            var result = new { data = model };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void BindModel(ForecastModel model, List<ListResultForecast> resultquery)
        {
            model.TotalForecast = Convert.ToDecimal(resultquery[0].Columna1.Replace(",","."));
            model.Forecast = Convert.ToDecimal(resultquery[0].Columna2.Replace(",", "."));
            model.TotalClient = Convert.ToDecimal(resultquery[1].Columna1);
            model.Client = Convert.ToDecimal(resultquery[1].Columna2);
        }
    }
}
