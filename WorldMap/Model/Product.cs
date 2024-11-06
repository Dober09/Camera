
using System.Text.Json.Serialization;


namespace WorldMap.Model
{
    public class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("weight")]
        public string Weight { get; set; }
        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }
    }

    public class BarcodeData
    {
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }
        [JsonPropertyName("format")]
        public string Format { get; set; }
        [JsonPropertyName("product")]
        public Product Product { get; set; }
    }

}
