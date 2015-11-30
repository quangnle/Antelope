using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Antelope.Notifier.Reports;
using Microsoft.AspNet.SignalR.Client;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public abstract class SignalRNotifier: INotifier
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static IFormatter _formatter = new WebFormatter();

        HubConnection _connection;
        IHubProxy _proxy;

        public async void Notify(ISubcriber subcriber, BaseNotifierData data)
        {
            var signalrSubcriber = subcriber as SignalRSubcriber;
            var signalrData = data as MessageNotifierData;

            if (signalrSubcriber == null || signalrData == null)
                throw new AntelopeInvalidParameter();

            try
            {
                _connection = new HubConnection(signalrSubcriber.Url);

                _proxy = _connection.CreateHubProxy(HubName());

                _connection.Closed += OnConnectionClosed;

                await _connection.Start();
            }
            catch (HttpRequestException)
            {
                _logger.Info("Can't connect to {0}", signalrSubcriber.Url);
            }

            await _proxy.Invoke(HubMethod(), Formatter.Format(signalrData.Content));           
        }

        private void OnConnectionClosed()
        {

        }

        public abstract string HubName();
        public abstract string HubMethod();

        public string Name
        {
            get { return "SignalR Notifier"; }
        }


        public IFormatter Formatter
        {
            get { return _formatter; }
        }
    }
}
