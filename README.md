# BenefixTaxApi
Api to calculate benefit tax for company owned cars in Sweden. 



There are three GET endpoints:

**1. ../calculateBenefitTaxNetCost**
### Example BenefitTaxRequest:
```
{    
    "Municipality": "Kungsbacka",
    "Income": 38000,
    "BenefitTax": 5317,
    "IncomeYear": 2020,
    "ChurchMember": false,
    "Congregation": ""
}
```
**2. ../municipalities**
### Example MunicipalitiesRequest:
```
{
    "incomeYear": 2020
}
```
**3. ../congregations**
### Example CongregationsRequest:
```
{    
    {
    "IncomeYear": 2020,
    "Municipality": "Kungsbacka"
    }
}
```
