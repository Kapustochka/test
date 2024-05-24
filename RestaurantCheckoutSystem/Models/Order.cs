using System.ComponentModel;
using System.Text.Json.Serialization;



namespace RestaurantOrderApi.Models
{
    public class Order
    {
        public List<Starter> Starters { get; set; }

        public List<Main> Mains { get; set; }

        public List<Drink> Drinks { get; set; }


        public override string ToString()
        {
            var startersString = string.Join(", ", Starters.Select(s => s.ToString()));
            var mainsString = string.Join(", ", Mains.Select(m => m.ToString()));
            var drinksString = string.Join(", ", Drinks.Select(d => d.ToString()));

            return $"Starters: {startersString}\nMains: {mainsString}\nDrinks: {drinksString}";
        }
    }

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Starter
{
    [Description("Bruschetta")]
    Bruschetta,
    
    [Description("Tapas")]
    Tapas,
    
    [Description("Gyoza")]
    Gyoza,
    
    [Description("Hummus")]
    Hummus
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Main
{
    [Description("Steak")]
    Steak,
    
    [Description("Tom Yum")]
    TomYum,
    
    [Description("Peking Duck")]
    PekingDuck,
    
    [Description("Lasagna")]
    Lasagna
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Drink
{
    [Description("Lemonade")]
    Lemonade,
    
    [Description("Guinness")]
    Guinness,
    
    [Description("Cappuccino")]
    Cappuccino,
    
    [Description("Milkshake")]
    Milkshake
}


