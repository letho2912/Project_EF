using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_EF.Data;
using Project_EF.ViewModel;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Project_EF.Models;

namespace Project_EF.ViewComponents
{
    [ViewComponent(Name="Menu")]
    public class MenuViewComponents:ViewComponent
    {
        private readonly Connect _db;

        public MenuViewComponents(Connect db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.displayname = HttpContext.Session.GetString("displayname");
            ViewBag.userId = HttpContext.Session.GetString("userId");
           // HttpContext.Session.SetString("displayname", us.fullname);
            //HttpContext.Session.SetString("userId", us.Id.ToString());
            var c = _db.ParentCate.OrderBy(p => p.Id).Include(p => p.Category);
            return View("Menu",c);
        }
    }
}
