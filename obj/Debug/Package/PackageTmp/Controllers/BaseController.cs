using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace DashBoard.Controllers
{
    public class BaseController : Controller
    {
        private static readonly string[] Scopes = {SheetsService.Scope.SpreadsheetsReadonly};
        private static readonly string ApplicationName = "Google Sheets API .NET Quickstart";

        public DataTable ReadExcel(int startcolumn, int endcolumn, int startrow, int endrow, int sheet)
        {
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            var table = new DataTable();
            int column = 0, pivote = startcolumn;
            while (pivote <= endcolumn)
            {
                column = column + 1;
                table.Columns.Add("colunmn" + column, typeof (string));
                pivote = pivote + 1;
                column = column + 1;
            }

            var rCnt = 0;
            var cCnt = 0;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(@"/ExcelFolder/PERFORMANCE DASHBOARD Rev3.xlsx"), 0, true,
                5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Worksheet) xlWorkBook.Worksheets.get_Item(sheet);

            range = xlWorkSheet.UsedRange;

            for (rCnt = startrow; rCnt <= endrow; rCnt++)
            {
                column = -1;
                var _ravi = table.NewRow();
                for (cCnt = startcolumn; cCnt <= endcolumn; cCnt++)
                {
                    column = column + 1;
                    var str = (range.Cells[rCnt, cCnt] as Range).Text;
                    _ravi[column] = str;
                }
                table.Rows.Add(_ravi);
            }

            xlWorkBook.Close(0);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            return table;
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public IList<IList<object>> ReadExcelGoogle(string sheet)
        {
            UserCredential credential;

            var ruta = AppDomain.CurrentDomain.BaseDirectory;
            using (var stream =
                new FileStream(ruta + @"\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
            #if DEBUG
                credPath = Path.Combine(credPath, @".credentials/sheets.googleapis.com-dotnet-quickstart.json");
            #else
                credPath = Path.Combine(ruta, @".credentials/sheets.googleapis.com-dotnet-quickstart.json");
            #endif

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

           
            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            
            //var spreadsheetId = "1D18vZrS9EnVgVVN99cKHfUJjSxheFsnY-n0oV65iGaI";
           // var spreadsheetId = "1Fq7xF2au6WWHsvBE3lbt1tZlyBOsG1BUxMjlZ7_5kUQ";
            var spreadsheetId = "1LuwdEiY4fPC4UFMUC0lY2A_DZUsfH1wQLb_Wdh4tFmY";
            var range = sheet; 
            var request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            
            return values;
        }

        public DataTable DataTable(int startcolumn, int endcolumn, int startrow, int endrow, IList<IList<object>> lista)
        {
            var table = new DataTable();
            int column = 0, pivote = startcolumn;
            while (pivote <= endcolumn)
            {
                column = column + 1;
                table.Columns.Add("colunmn" + column, typeof(string));
                pivote = pivote + 1;
                column = column + 1;
            }

            var rCnt = 0; var cCnt = 0;
            int indice = 0;
            for (rCnt = startrow; rCnt < endrow; rCnt++)
            {
                column = -1;
                var _ravi = table.NewRow();
               
                int key = 0;
                for (cCnt = startcolumn; cCnt <= endcolumn; cCnt++)
                {
                    column = column + 1;
                    var str = lista[indice][key];
                    _ravi[column] = str;
                    key += 1;
                }
                table.Rows.Add(_ravi);
                indice += 1;
            }

            return table;
        }
    }
}