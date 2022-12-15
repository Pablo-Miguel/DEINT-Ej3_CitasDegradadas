using System.Runtime.Intrinsics.Arm;

namespace DEINT_Ej3_CitasDegradadas;

public partial class MainPage : ContentPage
{
	Random r;
    List<String> citas;

	public MainPage()
	{
		InitializeComponent();
		r = new Random();
        citas = new List<string>();
    }

    private void generarCita() {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0),
            GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = rdmColor(), Offset = 0.1f },
                    new GradientStop { Color = rdmColor(), Offset = 1.0f }
                }
        };

        contentPage.Background = brush;

        LoadMauiAsset();
    }

	protected override async void OnAppearing() { 
		
		base.OnAppearing();
		
		generarCita();

    }

    async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("Citas.txt");
        using var reader = new StreamReader(stream);

        var contents = reader.ReadToEnd();

        citas = contents.Split("\n").ToList<String>();

        lblCita.Text = citas[r.Next(citas.Count)];

    }

    private Color rdmColor() {
        return Color.Parse($"#{r.Next(256).ToString("X2")}{r.Next(256).ToString("X2")}{r.Next(256).ToString("X2")}");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        generarCita();
    }
}

