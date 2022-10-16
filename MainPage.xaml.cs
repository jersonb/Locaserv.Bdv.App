using Locaserv.Bdv.App.Models;
using Refit;
using System.Text.Json;

namespace Locaserv.Bdv.App;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void GetComunication(object sender, EventArgs e)
    {
        Loading.IsRunning = true;
        var testApi = RestService.For<ITest>("https://locaserv-bdv-api.herokuapp.com/");

        var result = await testApi.GetTest();

        ApiComnunicationLbl.Text = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        Loading.IsRunning = false;

        SemanticScreenReader.Announce(ApiComnunicationLbl.Text);
    }

    private async void GetLocation(object sender, EventArgs e)
    {
        Loading.IsRunning = true;

        var permissionStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (permissionStatus != PermissionStatus.Granted)
        {
            var message = "Sua localização atual será usada apenas durante o uso do app.\n" +
                          "Utilizaremos sua Localização facilitar seu uso.";

            await DisplayAlert("Permissão de Geolocalização", message, "Ok");
        }

        permissionStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (permissionStatus == PermissionStatus.Granted)
        {
            var geolocation = await Geolocation.GetLocationAsync();

            var latitude = geolocation.Latitude;
            var longitude = geolocation.Longitude;

            var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);

            var placemark = placemarks.FirstOrDefault();

            var geocodeAddress =
                $"Rua/Logradouro:   {placemark.Thoroughfare}\n" +
                $"Número(1):        {placemark.SubThoroughfare}\n" +
                $"Número(2):        {placemark.FeatureName}\n" +
                $"Bairro:           {placemark.SubLocality}\n" +
                $"Cidade:           {placemark.SubAdminArea}\n" +
                $"Estado:           {placemark.AdminArea}\n" +
                $"Sigla país:       {placemark.CountryCode}\n" +
                $"Nome País:        {placemark.CountryName}\n" +
                $"CEP:              {placemark.PostalCode}\n";
            GeoLocationLbl.Text = geocodeAddress;
        }
        else
        {
            GeoLocationLbl.Text = "Permissão de geolocalização Negada";
            await DisplayAlert("Permissão de Geolocalização", "Permissão de uso da localização atual negada. \n Permita o uso da Geolocalização para o funcionamento adequado do app", "Ok");
        }

        Loading.IsRunning = false;
        SemanticScreenReader.Announce(GeoLocationLbl.Text);
    }
}