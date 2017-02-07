using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace DashBoard.Models.Forecast
{
    public class ForecastModel
    {
        public decimal Forecast { get; set; }
        public decimal TotalClient { get; set; }
        public decimal Client { get; set; }
        public decimal TotalForecast { get; set; }
    }

    public class ListResultForecast
    {
        public string Columna1 { get; set; }
        public string Columna2 { get; set; }
    }
}