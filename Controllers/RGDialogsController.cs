using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMessengerCoreAPI.Models;
using iMessengerCoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iMessengerCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RGDialogsController : ControllerBase
    {
        private RGDialogService _dialogService { get; set; }

        public RGDialogsController()
        {
            _dialogService = new RGDialogService();
        }

        /// <summary>
        ///  Поиск диалога, в котором участвуют все клиенты из списка.
        /// </summary>
        /// <param name="clientIds">Cписок идентфикаторов клиентов для которых необходимо найти диалог</param>
        /// <returns>Список диалогов в котором есть все указанные клиенты.</returns>
        [HttpPost]
        public IEnumerable<Guid> GetRGDialogs ([FromBody]IEnumerable<Guid> clientIds)
        {
            return _dialogService.SearchRGDialogsForClients(clientIds);                        
        }          
    }
}