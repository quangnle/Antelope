using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Antelope.Data.Repositories;
using Antelope.Data.Models;

namespace Antelope.Web.Hubs
{
    public class MyHub : Hub
    {
        //public void Send(string name, string message)
        //{
        //    // Call the addNewMessageToPage method to update clients.
        //    Clients.All.addMessage(name, message);
        //}
        public void Send(string accountNumber)
        {
            int count = new AccountRepository(new MainModel()).GetMatchedAcc(true, false, true).Count;
            Clients.All.addMessage(count);
        }
    }
}