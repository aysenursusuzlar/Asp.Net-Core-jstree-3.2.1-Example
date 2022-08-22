using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using treeviewaspnetcore.Models;

namespace treeviewaspnetcore.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //Döngü yaparak ve Ana Düğümleri ekleme
            foreach (State type in _context.State)
            {
                nodes.Add(new TreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Title });
            }

            //Döngü yaparak ve Alt Düğümleri ekleme
            foreach (City subType in _context.City)
            {
                nodes.Add(new TreeViewNode { id = subType.StateId.ToString() + "-" + subType.Id.ToString(), parent = subType.StateId.ToString(), text = subType.Name });
            }

            //JSON dizesine seri hale getirilir.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
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
}