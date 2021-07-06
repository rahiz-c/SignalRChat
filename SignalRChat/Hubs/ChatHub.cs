using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        int count = 0;
        public async Task SendMessage(string message)
        {
            UserViewModel user = new UserViewModel();
            string callerId = Context.ConnectionId;


            user.Message = message;
            count += 1;
            user.Id = count;

            await Clients.AllExcept(callerId).SendAsync("ReceiveMessage", user);

        }

        List<string> Participants = new List<string>();
        public async Task AddParticipant(string userName)
        {
            Participant participant = new Participant();

            Participants.Add(userName);

            participant.Id = Participants.Count;
            participant.Name = userName;

            await Clients.All.SendAsync("Participant", participant);
        }
    }
}
