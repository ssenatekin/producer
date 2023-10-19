using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OgrenciSistem.Models;
using OgrenciSistem.RabbitMQ;

namespace OgrenciSistem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRabbitMQProducer _rabbitMQ;
    public HomeController(ILogger<HomeController> logger, IRabbitMQProducer rabbitMQ)
    {
        _logger = logger;
        _rabbitMQ = rabbitMQ;
    }

    public IActionResult Index()
    {
        var ogrenci=new Ogrenci{Id=1,FirstName="Sena",LastName="Tekin"};
        _rabbitMQ.SendMessage<Ogrenci>(ogrenci);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
