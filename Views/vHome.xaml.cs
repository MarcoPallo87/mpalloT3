namespace mpalloT3.Views;

public partial class vHome : ContentPage
{
    public vHome()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        pckTipoId.SelectedIndex = -1;
        txtIdentificacion.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtCorreo.Text = string.Empty;
        txtSalario.Text = string.Empty;
        dtpFecha.Date = DateTime.Today;
    }

    private async void OnGuardarClicked(object? sender, EventArgs e)
    {
        // 1. Validacion de campo vacio
        if (pckTipoId.SelectedItem == null ||
            pckTipoId.SelectedItem.ToString() == "Seleccione un Tipo" ||
            string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
            string.IsNullOrWhiteSpace(txtNombres.Text) ||
            string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtCorreo.Text) ||
            string.IsNullOrWhiteSpace(txtSalario.Text))
        {
            await DisplayAlertAsync("Error", "Todos los campos son obligatorios y debe seleccionar un Tipo de ID.", "OK");
            return;
        }

        string tipoId = pckTipoId.SelectedItem.ToString()!;
        string identificacion = txtIdentificacion.Text;

        // 2. Validacion segun tipo de documento
        if (tipoId == "Cédula" && (identificacion.Length != 10 || !identificacion.All(char.IsDigit)))
        {
            await DisplayAlertAsync("Error", "La Cédula debe tener exactamente 10 números.", "OK");
            return;
        }

        if (tipoId == "RUC" && (identificacion.Length != 13 || !identificacion.All(char.IsDigit)))
        {
            await DisplayAlertAsync("Error", "El RUC debe tener exactamente 13 números.", "OK");
            return;
        }

        // 3. Validacion de correo
        if (!txtCorreo.Text.Contains("@") || !txtCorreo.Text.Contains("."))
        {
            await DisplayAlertAsync("Error", "Ingrese un correo electrónico válido.", "OK");
            return;
        }

        // 4. Validacion de salario
        if (!double.TryParse(txtSalario.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double salario))
        {
            await DisplayAlertAsync("Error", "El salario debe ser un valor numérico.", "OK");
            return;
        }

        // 5. Direccionar vista Contactos
        var paginaContactos = new vContactos
        {
            TipoIdentificacion = tipoId,
            Identificacion = identificacion,
            Nombres = txtNombres.Text,
            Apellidos = txtApellidos.Text,
            Fecha = ((DateTime)dtpFecha.Date!).ToString("dd/MM/yyyy"),
            Correo = txtCorreo.Text,
            Salario = salario
        };

        await Navigation.PushAsync(paginaContactos);
    }
}