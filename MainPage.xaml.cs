using Locaserv.Bdv.App.Models;
using Refit;
using System.Text.Json;

namespace Locaserv.Bdv.App;

public partial class MainPage : ContentPage
{
    private int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var a = await GetTets();

        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = JsonSerializer.Serialize(a.deu_bom);

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private static async Task<Test> GetTets()
    {
        var testApi = RestService.For<ITest>("https://locaserv-bdv-api.herokuapp.com/");

        return await testApi.GetTest();
    }
}