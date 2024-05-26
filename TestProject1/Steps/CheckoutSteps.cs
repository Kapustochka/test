using System.Text;
using System.Text.Json;
using NUnit.Framework;
using RestaurantCheckout.Models;
using TechTalk.SpecFlow;

namespace TestProject1.Steps;

[Binding]
public class CheckoutSteps
{
    private static List<Order> _currentOrders = new();
    private static readonly HttpClient HttpClient = new();


    [BeforeFeature]
    public static void BeforeFeature()
    {
        // Set the base address of the API
        HttpClient.BaseAddress = new Uri("http://localhost:5240/api/checkout/");
    }

    [AfterFeature]
    public static async Task AfterFeature()
    {
        _currentOrders = new List<Order>();
        await HttpClient.DeleteAsync("clearOrders");
    }

    [Given(@"a table of people")]
    public void GivenATableOfPeople()
    {
        //save step for clearer understanding of the gherkin scenario
    }

    [When(@"table orders (.*) starters, (.*) mains, and (.*) drinks at (.*) o'clock")]
    public async Task WhenTableOrdersStartersMainsAndDrinksAtOclock(int starters, int mains, int drinks,
        string currentTime)
    {
        var currentOrder = new Order
        {
            Starters = starters,
            Mains = mains,
            Drinks = drinks,
            OrderTime = TimeSpan.Parse(currentTime)
        };
        _currentOrders.Add(currentOrder);
        // Serialize the Order object to JSON
        var requestBodyJson = JsonSerializer.Serialize(currentOrder);
        // Make a POST request to the API endpoint with the JSON request body
        var response = await HttpClient.PostAsync("addOrder",
            new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }

    [When(@"table cancels (.*) starters, (.*) mains, and (.*) drinks at (.*) o'clock")]
    public async Task WhenTableCancelsStartersMainsAndDrinksAtOclock(int starters, int mains, int drinks,
        string currentTime)
    {
        await WhenTableOrdersStartersMainsAndDrinksAtOclock(-starters, -mains, -drinks, currentTime);
    }


    [Then(@"the endpoint should return a bill with the correct total amount")]
    public async Task ThenTheEndpointShouldReturnABillWithTheCorrectTotalAmountIncludingAServiceChargeOnFood()
    {
        var response = await HttpClient.GetAsync("finalBill");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, double>>(responseString);
        var total = _currentOrders.Sum(order => CalculateTotal(order));
        Assert.That(jsonResponse!["total"], Is.EqualTo(total));
    }

    private static decimal CalculateTotal(Order order)
    {
        const double starterCost = 4.00;
        const double mainCost = 7.00;
        const double drinkCost = 2.50;
        const double serviceChargeRate = 0.10;
        const double drinkDiscountRate = 0.30;
        var discountEndTime = new TimeSpan(19, 0, 0);

        var totalStarters = order.Starters * starterCost;
        var totalMains = order.Mains * mainCost;
        var totalDrinks = order.Drinks * drinkCost;

        // Apply discount to drinks if ordered before 19:00
        if (order.OrderTime < discountEndTime) totalDrinks -= totalDrinks * drinkDiscountRate;

        // Calculate service charge on food (starters + mains)
        var totalFood = totalStarters + totalMains;
        var serviceCharge = totalFood * serviceChargeRate;

        // Calculate final total
        var finalTotal = totalStarters + totalMains + totalDrinks + serviceCharge;
        return (decimal)finalTotal;
    }
}