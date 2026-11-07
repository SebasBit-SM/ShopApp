using ShopApp.DataAcces;
using System.Linq;

namespace ShopApp.ViewModels;

public class ClientsDetailViewModel : BindingUtilObjects
{
    private string _nombreCliente = string.Empty;

    public string NombreCliente
    {
        get { return _nombreCliente; }
        set
        {
            if (_nombreCliente != value)
            {
                _nombreCliente = value;
                RaisePropertyChanged();
            }
        }
    }

    private string _direccionCliente = string.Empty;

    public string DireccionCliente
    {
        get { return _direccionCliente; }
        set
        {
            if (_direccionCliente != value)
            {
                _direccionCliente = value;
                RaisePropertyChanged();
            }
        }
    }

    public void CargarCliente(int id)
    {
        using var database = new ShopDbContext();

        var cliente = database.Clients
            .FirstOrDefault(x => x.Id == id);

        if (cliente != null)
        {
            NombreCliente = cliente.Nombre;
            DireccionCliente = cliente.Direccion;
        }
        else
        {
            NombreCliente = "Cliente no encontrado";
            DireccionCliente = string.Empty;
        }
    }
}
