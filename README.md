### Assumptions:
#### As SUT I created restaraunt checkout API under respective project
1) Order is for table, no separation by people
2) Order structure looks like this (without specific dish names):
   {
  "starters": 10,
  "mains": 10,
  "drinks": 0,
  "orderTime": "19:20:00"
  }
3) Test scenarios changed according to SUT architecture
#### Flaws of the architecture:
1) Cancelation and changing of order required storing it's ID, modifying it and so on and to simplify SUT architecture it is just creating another order with minus sign for those items that should be cancelled
2) So logic where drinks ordered before and after 7 pm and then cancelled is difficult because we can't know which of the order was cancelled
   
Endpoints for the SUT:

[HttpPost("addOrder")]

[HttpPost("removeOrder")]

[HttpGet("finalBill")]

[HttpGet("currentOrders")] - just to manually convinience of debugging

[HttpDelete("clearOrders")] - was created to be able to clean up netween scenarious

Innsomnia screenshot to visualise
![image](https://github.com/Kapustochka/test/assets/24918452/39f95e71-6f4a-4c8e-a0be-3ba655109f4d)
![image](https://github.com/Kapustochka/test/assets/24918452/5f85fcb0-967b-40d5-97c1-4f8c6a56ed20)
![image](https://github.com/Kapustochka/test/assets/24918452/8e4685ca-22d2-4e59-8b3d-44fcb1a839d5)
![image](https://github.com/Kapustochka/test/assets/24918452/920afc02-2312-4ed6-9618-101a2d542cab)
![image](https://github.com/Kapustochka/test/assets/24918452/68094ce2-efa9-472c-b21d-e25c643dd568)
