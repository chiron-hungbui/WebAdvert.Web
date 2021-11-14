using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<bool> ConfirmAsync(ConfirmAdvertRequest model);
        Task<AdvertResponse> CreateAsync(CreateAdvertModel model);
        Task<List<Advertisement>> GetAllAsync();
        Task<Advertisement> GetAsync(string advertId);
    }
}