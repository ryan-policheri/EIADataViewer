using System.Threading.Tasks;
using EIA.Domain.Model;
using EIA.Services.Clients;
using PoliCommon.MVVM;

namespace EIADataViewer.ViewModel
{
    public class FoobarViewModel : ViewModelBase
    {
        private EiaClient _client;

        public FoobarViewModel(EiaClient client)
        {
            _client = client;
        }

        private string _something;
        public string Something
        {
            get { return _something; }
            set
            {
                _something = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            Category category = await _client.GetCategoryByIdAsync("371");
            await _client.GetSeriesByIdAsync("ELEC.GEN.WND-IA-1.M");
            Something = "Something ELSE";
        }
    }
}
