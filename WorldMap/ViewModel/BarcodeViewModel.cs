
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorldMap.Model;
using WorldMap.Service;

namespace WorldMap.ViewModel
{
    public partial class BarcodeViewModel : ObservableObject
    {
        private readonly IBarcodeService _barcodeService;

        [ObservableProperty]
        private BarcodeData currentScannedItem;

        [ObservableProperty]
        private string scanStatus;

        [ObservableProperty]
        private bool isScanned;

        [ObservableProperty]
        private bool isScanning;

        [ObservableProperty]
        private string scanButtonText = "Start Scanning";

        public BarcodeViewModel(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        [RelayCommand]
        private void ToggledScanning() {
            IsScanned = !IsScanned;
            ScanButtonText = IsScanning ? "Stop Scanning" : "Start Scanning";
            ScanStatus = IsScanning ? "Scanning..." : "Ready to scan";
        }


        [RelayCommand]
        private async Task HandleScannedBarcode(string barcode)
        {
            try
            {
                var product = await _barcodeService.GetProductByBarcodeAsync(barcode);
                if (product != null)
                {
                    CurrentScannedItem = product;
                    ScanStatus = $"Found: {product.Product.Name}";
                }
                else
                {
                    CurrentScannedItem = null;
                    ScanStatus = $"Product not found for barcode: {barcode}";
                }
            }
            catch (Exception ex)
            {
                ScanStatus = $"Error: {ex.Message}";
            }
        

    }

    }
}
