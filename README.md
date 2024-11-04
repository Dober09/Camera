# .NET MAUI Camera Barcode Scanner

A barcode scanner implementation using Camera.MAUI for cross-platform mobile applications. This project demonstrates how to implement camera functionality and barcode scanning in a .NET MAUI application.

## Features

- 📸 Real-time camera preview
- 🔍 Barcode scanning (EAN-13, UPC-A, CODE-128)
- 🔄 Camera switching support
- 📱 Cross-platform compatibility (iOS and Android)
- ⚡ Real-time barcode detection
- 🎯 Auto-focus support
- 🔐 Permission handling

## Prerequisites

- .NET MAUI SDK
- Visual Studio 2022 or Visual Studio Code
- Android SDK (for Android deployment)

## Installation

1. Install the Camera.MAUI NuGet package:

```xml
<PackageReference Include="Camera.MAUI" Version="1.5.1" />
```

2. Configure your MauiProgram.cs:

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseMauiCameraView();
    
    return builder.Build();
}
```

## Platform Configuration

### Android

Add the following permissions to your Android Manifest (Platforms/Android/AndroidManifest.xml):

```xml
<uses-permission android:name="android.permission.CAMERA" />
```

### iOS

Add the following keys to your Info.plist (Platforms/iOS/Info.plist):

```xml
<key>NSCameraUsageDescription</key>
<string>This app needs access to camera to scan barcodes.</string>
```

## Usage

### 1. Add the XAML Layout

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="YourNamespace.Views.LandingPage"
             xmlns:cv="clr-namespace:Camera.MAUI;assembly=Camera.MAUI">
    <Grid RowDefinitions="*,Auto">
        <!-- Camera View -->
        <cv:CameraView Grid.Row="0"
                       x:Name="cameraView"
                       VerticalOptions="Fill"
                       HorizontalOptions="Fill"
                       CamerasLoaded="CameraView_CamerasLoaded"
                       BarcodeDetected="CameraView_BarcodeDetected"/>
        
        <!-- Barcode Result Display -->
        <Label Grid.Row="0"
               x:Name="barcodeResult"
               TextColor="White"
               BackgroundColor="#80000000"
               Padding="10"
               Margin="20"
               VerticalOptions="Start"
               HorizontalOptions="Fill"/>
        
        <!-- Controls -->
        <Grid Grid.Row="1">
            <HorizontalStackLayout HorizontalOptions="Center" Spacing="20">
                <Button Text="Start Scanning"
                        x:Name="toogleScannningButton"
                        Clicked="OnToggleScanningClicked"/>
                <ImageButton Source="switchcamera.png"
                            WidthRequest="50"
                            HeightRequest="50"
                            BackgroundColor="Transparent"
                            Clicked="OnSwitchCameraClicked"/>
            </HorizontalStackLayout>
        </Grid>
    </Grid>
</ContentPage>
```

### 2. Implement the Code-Behind

```csharp
public partial class LandingPage : ContentPage
{
    private bool isCameraInitialized = false;
    private bool hasPermission = false;
    private bool _isScanning = false;

    public LandingPage()
    {
        InitializeComponent();
    }

    private async void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (!hasPermission)
        {
            await CheckAndRequestPermission();
        }

        if (hasPermission && !isCameraInitialized)
        {
            await InitializeCamera();
        }
    }

    // ... Additional implementation code ...
}
```

## Key Components

### 1. Permission Handling

```csharp
private async Task CheckAndRequestPermission()
{
    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
    if (status != PermissionStatus.Granted)
    {
        status = await Permissions.RequestAsync<Permissions.Camera>();
    }
    hasPermission = status == PermissionStatus.Granted;
}
```

### 2. Barcode Scanner Configuration

```csharp
private void SetupBarcodeScanning()
{
    cameraView.BarCodeOptions = new()
    {
        AutoRotate = true,
        PossibleFormats = new List<BarcodeFormat>
        {
            BarcodeFormat.EAN_13,
            BarcodeFormat.UPC_A,
            BarcodeFormat.CODE_128
        },
        TryHarder = true
    };
}
```

## Troubleshooting

### Common Issues

1. **Camera Not Showing**
   - Verify platform permissions
   - Check camera initialization
   - Ensure device has camera access

2. **Barcode Not Detecting**
   - Check supported formats
   - Verify scanning is enabled
   - Ensure adequate lighting

3. **Permission Denied**
   - Verify manifest entries
   - Check runtime permissions
   - Review platform settings

## Best Practices

1. **Resource Management**
   ```csharp
   protected override void OnDisappearing()
   {
       base.OnDisappearing();
       MainThread.BeginInvokeOnMainThread(async () =>
       {
           await cameraView?.StopCameraAsync();
       });
   }
   ```

2. **Error Handling**
   ```csharp
   try
   {
       await cameraView.StartCameraAsync();
   }
   catch (Exception ex)
   {
       await DisplayAlert("Error", 
           $"Camera initialization error: {ex.Message}", "OK");
   }
   ```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- [Camera.MAUI](https://github.com/hjam40/Camera.MAUI) - Camera implementation for .NET MAUI
- [.NET MAUI](https://github.com/dotnet/maui) - Cross-platform framework

## Support

For support and questions, please create an issue in the repository.

---

Made with ❤️ using .NET MAUI and Camera.MAUI
