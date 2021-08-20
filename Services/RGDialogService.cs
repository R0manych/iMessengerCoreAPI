using iMessengerCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMessengerCoreAPI.Services
{
    /// <summary>
    /// Сервис для работы с диалогами пользователей.
    /// </summary>
    public class RGDialogService
    {
        private List<RGDialogsClients> _clients { get; set; }

        public RGDialogService()
        {
            var client = new RGDialogsClients();
            _clients = client.Init();
        }

        /// <summary>
        /// Поиск диалога, в котором участвуют все клиенты из списка.
        /// </summary>
        /// <param name="clientIds">Список клиентов диалога.</param>
        /// <returns>Список диалогов в котором есть все клиенты из списка</returns>
        public IEnumerable<Guid> SearchRGDialogsForClients(IEnumerable<Guid> clientIds)
        {
            var dialogsWithClientsIds = _clients.Select(p => p.IDRGDialog).Distinct();
            var dialogsWithClients = new List<RGDialogsClients>();
            int i = 0;
            while (i < clientIds.Count() && dialogsWithClientsIds.Count() != 0)
            {
                dialogsWithClients = GetAllDialogsForClient(dialogsWithClientsIds, clientIds.ElementAt(i)).ToList();
                dialogsWithClientsIds = dialogsWithClients.Select(p => p.IDRGDialog).Distinct().ToList();
                i++;
            }

            return dialogsWithClientsIds.Any() ? dialogsWithClientsIds : new List<Guid>() { Guid.Empty };
        }

        private IEnumerable<RGDialogsClients> GetAllDialogsForClient(IEnumerable<Guid> dialogIds, Guid clientId) =>
             _clients.Where(p => p.IDClient == clientId && dialogIds.Contains(p.IDRGDialog));
    }
}
