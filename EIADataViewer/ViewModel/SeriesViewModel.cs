using EIA.Domain.Model;
using EIA.Services.Clients;
using PoliCommon.EventAggregation;
using PoliCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIADataViewer.ViewModel
{
    public class SeriesViewModel : ViewModelBase
    {
        private readonly EiaClient _client;
        private readonly IMessageHub _messageHub;

        public SeriesViewModel(EiaClient client, IMessageHub messageHub)
        {
            _client = client;
            _messageHub = messageHub;
        }

        private string _seriesName;
        public string SeriesName
        {
            get { return _seriesName; }
            private set
            {
                _seriesName = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(string seriesId)
        {
            Series series = await _client.GetSeriesByIdAsync(seriesId);
            SeriesName = series.Name;
        }
    }
}
