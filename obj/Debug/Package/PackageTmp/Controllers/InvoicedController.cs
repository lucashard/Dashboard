using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class InvoicedController : BaseController
    {
        //
        // GET: /Invoiced/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObtainListResult()
        {
            var listaDouble = new List<double>();
            var listaNoa = new List<double>();
            var listaFollowUp = new List<int>();
            string total=string.Empty;
            try
            {
                //var lista = ReadExcelGoogle("Invoice & NOA Follow up!AG4:AG15");
                var lista = ReadExcelGoogle("Invoice & NOA Follow up!I3:I14");
#if DEBUG
                foreach (var list in lista.Select(elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1))))
                {
                    listaDouble.AddRange(list.Select(Convert.ToDouble));
                }
#else
                foreach (
                    var list in
                        lista.Select(
                            elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1).Replace(".", ","))))
                {
                    listaDouble.Add(Convert.ToDouble(list[0].Replace(",", "."), new NumberFormatInfo{ NumberDecimalSeparator = "."}));
                }
#endif
              
                //var table = ReadExcel(33, 33, 4, 15, 4);

                //foreach (DataRow row in table.Rows)
                //{
                //    foreach (string item in row.ItemArray)
                //    {
                //        if (!string.IsNullOrEmpty(item))
                //        {
                //            lista.Add(Convert.ToDecimal(item.Substring(1).Replace(".", ",")));
                //        }
                //    }
                //}
                var totalresult = ReadExcelGoogle("Invoice & NOA Follow up!I15");
                foreach (string elem in totalresult[0])
                {
                    total = elem.Substring(0);
                }
                
                var listaInvoNoa = ReadExcelGoogle("Invoice & NOA Follow up!H3:H14");
                #if DEBUG
                    listaNoa.AddRange(listaInvoNoa.Select(elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1))).Select(list => Convert.ToDouble(list[0].Replace(",", "."))));
                #else
                listaNoa.AddRange(listaInvoNoa.Select(elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1))).Select(list => Convert.ToDouble(list[0])));
                #endif

                    //var table = DataTable(7, 7, 3, 14, ReadExcelGoogle("Contract Renewals!G3:G14"));
                    var table = ReadExcelGoogle("Invoice & NOA Follow up!G3:G14").ToList();
                    
                    foreach (var elm in table)
                    {
                        foreach (var elemento in elm)
                        {
                            if (!elemento.ToString().Equals(" "))
                            {
                                listaFollowUp.Add(Convert.ToInt32(elemento));
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(new {listadouble=listaDouble,listanoa=listaNoa, listafollowup=listaFollowUp,total=total}, JsonRequestBehavior.AllowGet);
        }
    }
}