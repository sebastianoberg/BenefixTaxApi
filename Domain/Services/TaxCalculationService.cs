using System;
using System.IO;
using System.Text.RegularExpressions;
using BenefitTaxApi.Models;

namespace BenefitTaxApi.Domain.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        public IncomePair GetIncomeInterval(int income)
        {
            var filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory); ;
            var incomeBelowLimit = Path.Combine(filePath, "Resources/IncomePairs.csv");

            using (StreamReader reader = new StreamReader(incomeBelowLimit))
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

                // EGB86K
            }
        }
    }
}
