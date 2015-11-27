using Antelope.Notifier.Exceptions;
using Antelope.Notifier.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Antelope.Notifier.Notifiers
{
    public class SignalRNotifier: INotifier
    {
        HubConnection _connection;

        public static SignalRNotifier CreateNotifier()
        {
            var notifier = new SignalRNotifier();

            return notifier;
        }

        public string Name()
        {
            return "SignalR Notifier";
        }

        public async void Notify(BaseSubcriber subcriber, BaseNotifierData data)
        {
            var signalrSubcriber = subcriber as SignalRSubcriber;
            var signalrData = data as MessageNotifierData;

            if (signalrSubcriber == null || signalrData == null)
                throw new AntelopeInvalidParameter();

            _connection = new HubConnection(signalrSubcriber.Url);
            _connection.Closed += OnConnectionClosed;
            IHubProxy proxy = _connection.CreateHubProxy("MyHub");
            proxy.On<string, string>("AddMessage", (name, message) =>
                {

                });

            try
            {
                await _connection.Start();
            }
            catch (HttpRequestException)
            {
                
                throw;
            }

            await proxy.Invoke("Send", signalrData.Content);           
        }

        private void OnConnectionClosed()
        {

        }
    }
}
