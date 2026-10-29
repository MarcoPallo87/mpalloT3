using System.Text;

namespace mpalloT3.Views;

public partial class vContactos : ContentPage
{
    // Recepcion Datos vHome
    public string TipoIdentificacion { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public double Salario { get; set; }

    public vContactos()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // 1. CALCULO Aporte IESS
        double aporteIess = Salario * 0.0945;

        // 2. Llenamos los Labels con los datos recibidos
        lblTipoId.Text = $"Tipo de ID: {TipoIdentificacion}";
        lblIdentificacion.Text = $"Identificación: {Identificacion}";
        lblNombres.Text = $"Nombres: {Nombres}";
        lblApellidos.Text = $"Apellidos: {Apellidos}";
        lblFecha.Text = $"Fecha: {Fecha}";
        lblCorreo.Text = $"Correo: {Correo}";
        lblSalario.Text = $"Salario: ${Salario:F2}";

        // 3. Se muestra el aporte
        lblAporte.Text = $"Aporte al IESS (9.45%): ${aporteIess:F2}";
    }

    private async void OnExportarClicked(object? sender, EventArgs e)
    {
        // Calculamos el IESS para el archivo
        double aporteIess = Salario * 0.0945;

        // 4. Texto para el archivo txt
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("===== DATOS DEL CONTACTO =====");
        sb.AppendLine($"Tipo de ID: {TipoIdentificacion}");
        sb.AppendLine($"Identificación: {Identificacion}");
        sb.AppendLine($"Nombres: {Nombres}");
        sb.AppendLine($"Apellidos: {Apellidos}");
        sb.AppendLine($"Fecha: {Fecha}");
        sb.AppendLine($"Correo: {Correo}");
        sb.AppendLine($"Salario: ${Salario:F2}");
        sb.AppendLine($"Aporte al IESS (9.45%): ${aporteIess:F2}");

        // 5. Guardar el archivo en el almacenamiento de la app
        string nombreArchivo = $"Contacto_{Identificacion}.txt";
        string ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

        File.WriteAllText(ruta, sb.ToString());

        // 6. Mensaje de exito
        await DisplayAlertAsync("Éxito", $"Archivo guardado correctamente en:\n{ruta}", "OK");
    }

    private async void OnNuevoContactoClicked(object? sender, EventArgs e)
    {
        // PopToRootAsync() destruye las páginas que están encima y regresa a la página principal (vHome)
        await Navigation.PopToRootAsync();
    }
}