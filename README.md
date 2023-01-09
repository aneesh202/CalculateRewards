# CalculateRewards

Used .NET Core 6.0

Integrtaed swagger for testing the Web API 

End point Post: https://localhost:7158/api/RewardsApi

Sample request :[
  {
    "customerId": 1,
    "date": "2023-01-09T19:44:29.489Z",
    "amount": 51
  },
 {
    "customerId": 1,
    "date": "2023-02-09T19:44:29.489Z",
    "amount": 333
  }, {
    "customerId": 2,
    "date": "2023-01-09T19:44:29.489Z",
    "amount": 51
  }

]

Response Obj:	

{
  "1": {
    "2023-01": 1,
    "2023-02": 516
  },
  "2": {
    "2023-01": 1
  }
}
