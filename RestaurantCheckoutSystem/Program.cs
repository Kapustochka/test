using RestaurantOrderApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/calculateTotal", (Order order ) =>
{
        bool isBeforeDiscountTime = false;
        // Calculate the cost of each item
        decimal startersCost = order.Starters.Count * 4.00m;
        decimal mainsCost = order.Mains.Count * 7.00m;
        decimal drinksCost = order.Drinks.Count * 2.50m;

        // Apply discount on drinks if ordered before 19:00
        if (isBeforeDiscountTime && DateTime.Now.TimeOfDay < TimeSpan.Parse("19:00"))
        {
            drinksCost *= 0.7m; // Apply 30% discount
        }

        // Calculate the total cost of the order
        decimal totalCost = startersCost + mainsCost + drinksCost;

        // Apply 10% service charge on food
        totalCost *= 1.10m;

        return totalCost;
    
})
.WithName("CalculateTotal")
.WithOpenApi();

app.Run();
