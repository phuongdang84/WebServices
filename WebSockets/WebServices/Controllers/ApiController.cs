using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; 
using WebService;

namespace WebServices.Controllers
{
    public class ApiController : Controller
    {
        private readonly TemperatureSocketManager _socketManager;

        private ApiController(TemperatureSocketManager socketManager)
        {
            _socketManager = socketManager ;
        }

        public async Task Report(double liquidTemp)
        {
            var reading = new
            {
                Date = DateTime.Now,
                LiquidTemp = liquidTemp
            };

            await _socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(reading));
        }

        public async Task Generate()
        {
            var rnd = new Random();

            for (var i = 0; i < 100; i++)
            {
                await Report(rnd.Next(23, 35));
                await Task.Delay(5000);
            }
        }
    }
}