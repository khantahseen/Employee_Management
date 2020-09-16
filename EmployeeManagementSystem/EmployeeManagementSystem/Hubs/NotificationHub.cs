﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Hubs
{
    public class NotificationHub:Hub
    {
        /*public async Task SendAddEmployeeMessage(string name, string surname)
        {
            var groupName = "Employee";
            await Clients.Group(groupName).SendAsync("AddEmployeeMessage", name, surname);
        }*/

        public async Task SendAddDepartmentMessage(string name)
        {
            var groups = "HR";
            await Clients.Group(groups).SendAsync("AddDepartmentMessage", name);
        }

        public async Task SendEditProfileMessage(string name)
        {
            var group1 = "Admin";
            var group2 = "HR";
            await Clients.Groups(group1, group2).SendAsync("EditProfileMessage", name);
        }
        public override async Task OnConnectedAsync()
        {
            if (this.Context.User.IsInRole("Admin"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Admin");
            }
            else if (this.Context.User.IsInRole("HR"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "HR");
            }
            else if (this.Context.User.IsInRole("Employee"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Employee");
            }
            await base.OnConnectedAsync();
        }
    }
}
