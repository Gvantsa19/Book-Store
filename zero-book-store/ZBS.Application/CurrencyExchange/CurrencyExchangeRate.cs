using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZBS.Application.CurrencyExchange
{
    public class CurrencyExchangeRate
    {
        private IConfiguration _configuration;

        public CurrencyExchangeRate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<double> ExchangeRate(string fromIdent)
        {
            fromIdent = fromIdent.ToUpper();

            string url = _configuration["NBGLink:URL"];
            using var client = new HttpClient();


            var xml = "";
            try
            {
                xml = await client.GetStringAsync(url);
            }
            catch
            {
                throw new Exception("bla");
            }

            xml = xml.Replace("<![CDATA[<table border=\"0\">", "").Replace("</table>]]>", "");
            xml = Regex.Replace(xml, @"<img.*?>", "");


            var doc = XDocument.Parse(xml);


            var Quantity = (from tr in doc.Descendants("description").ToArray()[1].Descendants("tr")
                            let TDs = tr.Descendants("td").ToArray()
                            where TDs.First().Value == fromIdent
                            select Regex.Match(TDs[1].Value, @"\d+").Value).FirstOrDefault();

            var Price = (from tr in doc.Descendants("description").ToArray()[1].Descendants("tr")
                         let TDs = tr.Descendants("td").ToArray()
                         where TDs.First().Value == fromIdent
                         select TDs[2].Value).FirstOrDefault();

            int QuantityInt;
            int.TryParse(Quantity, out QuantityInt);

            Price = Price.Replace(".", ",");

            double PriceDouble;
            double.TryParse(Price, out PriceDouble);

            return PriceDouble / QuantityInt;
        }
    }
}
