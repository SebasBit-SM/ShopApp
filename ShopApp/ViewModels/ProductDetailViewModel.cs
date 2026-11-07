
using ShopApp.DataAcces;

namespace ShopApp.ViewModels;

public class ProductDetailViewModel : BindingUtilObjects
{
    private string _nombre = string.Empty;
    private string _descripcion = string.Empty;
    private decimal _precio;
    private string _imagen = string.Empty;

    public string Nombre
    {
        get { return _nombre; }
        set
        {
            if (_nombre != value)
            {
                _nombre = value;
                RaisePropertyChanged();
            }
        }
    }

    public string Descripcion
    {
        get { return _descripcion; }
        set
        {
            if (_descripcion != value)
            {
                _descripcion = value;
                RaisePropertyChanged();
            }
        }
    }

    public decimal Precio
    {
        get { return _precio; }
        set
        {
            if (_precio != value)
            {
                _precio = value;
                RaisePropertyChanged();
            }
        }
    }

    public string Imagen
    {
        get { return _imagen; }
        set
        {
            if (_imagen != value)
            {
                _imagen = value;
                RaisePropertyChanged();
            }
        }
    }

    public void CargarProducto(int id)
    {
        using var database = new ShopDbContext();

        var producto = database.Products
            .FirstOrDefault(x => x.Id == id);

        if (producto != null)
        {
            Nombre = producto.Nombre;
            Descripcion = producto.Descripcion;
            Precio = producto.Precio;
            Imagen = producto.Imagen;
        }
        else
        {
            Nombre = "Producto no encontrado";
            Descripcion = string.Empty;
            Precio = 0;
            Imagen = string.Empty;
        }
    }
}
