using System.Text.Json;
using WorldMap.Model;

namespace WorldMap.Service
{
    public interface IBarcodeService
    {
        Task<List<BarcodeData>> LoadBarcodeDataAsync();
        Task<BarcodeData> GetProductByBarcodeAsync(string barcode);
    }
    public class BarcodeService : IBarcodeService
    {

        private List<BarcodeData> _barcodeData;
        private readonly string _JsonFileName = "data.json";

        public BarcodeService()
        {
            _barcodeData = null;
        }

        public async Task<List<BarcodeData>> LoadBarcodeDataAsync()
        {
            try
            {
                if (_barcodeData == null)
                {
                    return _barcodeData;
                }

                //open the data.json file from the package
                using var stream = await FileSystem.OpenAppPackageFileAsync($"Data/{_JsonFileName}");

                //Read the json content
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                _barcodeData = JsonSerializer.Deserialize<List<BarcodeData>>(jsonContent, options);

                return _barcodeData;

            } catch (Exception ex)
            {
                throw new Exception($"Error loading barcode data : {ex.Message}");
            }
        }

        public async Task<BarcodeData> GetProductByBarcodeAsync(string barcode)
        {
            var data = await LoadBarcodeDataAsync();
            return data.FirstOrDefault(item => item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));

        }
    }
}
