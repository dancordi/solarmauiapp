using SolarMauiApp.Services;

namespace SolarMauiApp
{
    public partial class MainPage : ContentPage
    {
        private ISolarAzureFunctionsService _solarAzureFunctionsService { get; set; }

        public MainPage(ISolarAzureFunctionsService solarAzureFunctionsService)
        {
            InitializeComponent();

            _solarAzureFunctionsService = solarAzureFunctionsService;
            this.VersionLb.Text = System.Reflection.Assembly.GetExecutingAssembly()!.GetName()!.Version!.ToString();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            double totalPac = 0;
            this.ActivityIndicator.IsRunning = true;
            var pacInverter1 = await _solarAzureFunctionsService.GetLastPacItem(1);
            if (pacInverter1 != null)
            {
                this.Inverter1PacLb.Text = $"Inverter EST {pacInverter1.Pac} Watt";
                this.Inverter1LastUpdateLb.Text = $"Ultimo aggiornamento {pacInverter1.TimeStamp.ToLocalTime()}";
                totalPac += pacInverter1.Pac;
            }
            var pacInverter2 = await _solarAzureFunctionsService.GetLastPacItem(2);
            if (pacInverter2 != null)
            {
                this.Inverter2PacLb.Text = $"Inverter SUD {pacInverter2.Pac} Watt";
                this.Inverter2LastUpdateLb.Text = $"Ultimo aggiornamento {pacInverter2.TimeStamp.ToLocalTime()}";
                totalPac += pacInverter2.Pac;
            }
            this.InvertersPacLb.Text = $"Totale {totalPac} Watt";
            this.ActivityIndicator.IsRunning = false;
        }
    }
}