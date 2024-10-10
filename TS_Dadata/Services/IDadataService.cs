using TS_Dadata.Models;
using System.Threading.Tasks;
namespace TS_Dadata.Services
{
    public interface IDadataService
    {
        Task<AddressResponse> CleanAddressAsync(string address);
    }
}
