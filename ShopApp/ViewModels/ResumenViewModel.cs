
using ShopApp.DataAcces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShopApp.ViewModels;

public partial class ResumenViewModel : ViewModelGlobal
{
    [ObservableProperty]
    private int visitas;

    [ObservableProperty]
    private decimal total;

    [ObservableProperty]
    private int clients;

    [ObservableProperty]
    private int totalProducts;

    public ResumenViewModel(ShopOutDbContext shopOutDbContext)
    {
        var compras = shopOutDbContext.Compras
            .AsNoTracking()
            .ToList();

        // Total de registros de compras
        Visitas = compras.Count;

        // Ingresos totales
        Total = compras.Sum(
            c => c.Cantidad * c.Precio);

        // Clientes diferentes que realizaron compras
        Clients = compras
            .Select(c => c.ClienteId)
            .Distinct()
            .Count();

        // Cantidad total de unidades vendidas
        TotalProducts = compras.Sum(
            c => c.Cantidad);
    }
}
