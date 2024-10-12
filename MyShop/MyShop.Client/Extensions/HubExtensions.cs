// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.SignalR.Client;
// using BlazorHero.CleanArchitecture2.Shared.Constants.Application;
//
// namespace BlazorHero.CleanArchitecture2.Client.Extensions
// {
//     public static class HubExtensions
//     {
//         public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager)
//         {
//             if (hubConnection == null)
//             {
//                 hubConnection = new HubConnectionBuilder()
//                     .WithUrl(navigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl))
//                     .Build();
//             }
//
//             return hubConnection;
//         }
//     }
// }