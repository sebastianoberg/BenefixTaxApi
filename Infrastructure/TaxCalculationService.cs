using System;
using System.IO;
using System.Text.RegularExpressions;
using BenefitTaxApi.Models;

namespace BenefitTaxApi.Infrastructure
{
    public static class TaxCalculationService
    {
        public static IncomePair GetIncomeInterval(int income)
        {
            var filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory); ;
            var combined = Path.Combine(filePath, "Resources/IncomePairs.csv");

            using (StreamReader reader = new StreamReader(combined))
            {
                string line;
                int top;
                int bottom; 

                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                while ((line = reader.ReadLine()) != null)
                {
                    string[] X = CSVParser.Split(line);
                    string[] Y = X[0].Split("-");

                    bottom = Convert.ToInt32(Y[0]); 
                    top = Convert.ToInt32(Y[1]);

                    if (income >= bottom && income <= top)
                    {
                        return new IncomePair(top, bottom);
                    }
                }
                return null;
            }
        }
    }
}
