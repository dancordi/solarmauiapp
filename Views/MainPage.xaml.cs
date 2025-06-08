using SolarMauiApp.Services;
using SolarMauiApp.Views;

namespace SolarMauiApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ISolarAzureFunctionsService _solarAzureFunctionsService;
        private readonly SettingsService _settingsService;
        private bool _hasAppeared = false;


        public MainPage(ISolarAzureFunctionsService solarAzureFunctionsService,
            SettingsService settingsService)
        {
            InitializeComponent();

            _solarAzureFunctionsService = solarAzureFunctionsService;
            _settingsService = settingsService;
            VersionLb.Text = System.Reflection.Assembly.GetExecutingAssembly()!.GetName()!.Version!.ToString();
            
            MyRefreshView.Command = new Command(async () => await RefreshInvertesPacAsync());
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasAppeared)
                return;

            _hasAppeared = true;

            // Slight delay to ensure UI is rendered
            await Task.Delay(100); // Tune this if needed

            await RefreshInvertesPacAsync();
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(_settingsService));
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await RefreshInvertesPacAsync();
        }

        private async Task RefreshInvertesPacAsync()
        {
            double totalPac = 0;
            this.ActivityIndicator.IsRunning = true;

            try
            {
                var pacInverter1 = await _solarAzureFunctionsService.GetLastPacItem(1);
                if (pacInverter1 != null)
                {
                    this.Inverter1PacLb.Text = $"Inverter EST {pacInverter1.Pac} Watt";
                    this.Inverter1LastUpdateLb.Text = $"Ultimo aggiornamento {pacInverter1.TimeStamp.ToLocalTime()}";
                    totalPac += pacInverter1.Pac;
                }
            }
            catch (Exception exception)
            {
                this.Inverter1PacLb.Text = exception.Message;
            }

            try
            {
                var pacInverter2 = await _solarAzureFunctionsService.GetLastPacItem(2);
                if (pacInverter2 != null)
                {
                    this.Inverter2PacLb.Text = $"Inverter SUD {pacInverter2.Pac} Watt";
                    this.Inverter2LastUpdateLb.Text = $"Ultimo aggiornamento {pacInverter2.TimeStamp.ToLocalTime()}";
                    totalPac += pacInverter2.Pac;
                }
            }
            catch (Exception exception)
            {
                this.Inverter2PacLb.Text = exception.Message;
            }
            finally
            {
                MyRefreshView.IsRefreshing = false;
            }

            this.InvertersPacLb.Text = $"Totale {totalPac} Watt";
            this.ActivityIndicator.IsRunning = false;
        }
    }
}