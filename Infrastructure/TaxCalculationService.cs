using System;
using BenefitTaxApi.Models;

namespace BenefitTaxApi.Infrastructure
{
    public static class TaxCalculationService
    {
        public static IncomePair CalculateAmount(int income)
        {
            // TODO: implement support for incomes below 20 000
            var incomeSpan = new IncomePair();
                    
            var incomeFrom = RoundOffIncomeFrom(income);
            var incomeTo = RoundOffIncomeTo(income);

            incomeSpan.IncomeBottom = incomeFrom;
            incomeSpan.IncomeTop = incomeTo;

            return incomeSpan;
        }

        private static int RoundOffIncomeTo(int income)
        {
            var evenOrOdd = GetEvenOrOddDigit(income, 3);
            var thousands = GetThousands(income);
            var hundreds = GetHundreds(income);

            switch (evenOrOdd % 2)
            {
                case 0: Console.WriteLine(evenOrOdd + " is even number");
                    var evenRounding = ((int)Math.Ceiling(hundreds / 100.0)) * 100 + 100;
                    return Convert.ToInt32(string.Format("{0}{1}", thousands, evenRounding));
 
                case 1: Console.WriteLine(evenOrOdd + " is odd number");
                    var oddRounding = ((int)Math.Ceiling(hundreds / 100.0)) * 100;
                    return Convert.ToInt32(string.Format("{0}{1}", thousands, oddRounding));

                default:
                    return 0;
            }
        }

        private static int RoundOffIncomeFrom(int income)
        {
            var evenOrOdd = GetEvenOrOddDigit(income, 3);
            var thousands = GetThousands(income);
            var hundreds = GetHundreds(income);

            switch (evenOrOdd % 2)
            {
                case 0: Console.WriteLine(evenOrOdd + " is even number");
                    var evenRounding = ((int)Math.Floor(hundreds / 100.0)) * 100 + 1;
                    return Convert.ToInt32(string.Format("{0}{1}", thousands, evenRounding));
 
                case 1: Console.WriteLine(evenOrOdd + " is odd number");
                    var oddRounding = ((int)Math.Floor(hundreds / 100.0)) * 100 - 100 + 1;
                    return Convert.ToInt32(string.Format("{0}{1}", thousands, oddRounding));
 

                default:
                    return 0;
            }
        }

        private static int GetThousands(int income)
        {
            var thousands = income.ToString().Substring(0,2);
            return int.Parse(thousands);
        }

        private static int GetHundreds(int income)
        {
            var digit = Convert.ToString(income);
            var hundreds = income.ToString().Substring(digit.Length - 2);

            return int.Parse(hundreds);
        }

        private static int GetEvenOrOddDigit(int number, int digit)
	    {
		    for (var i = 0; i < digit - 1; i++)
			    number /= 10;
                
		    return number % 10;
	    }
    }
}
