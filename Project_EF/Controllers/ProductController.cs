using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Controllers
{
    public class ProductController : Controller
    {
        private readonly Connect _db;
        public Product Product { get; set; }
        public ProductController(Connect db)
        {
            _db = db;
        }

        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }


        public IActionResult ListProduct(string id)
        {
            IEnumerable<Product> b = _db.Product.Where(b => b.ParentCateId == id);
            return View(b);
        }
        public IActionResult ListProducts(string id)
        {
            IEnumerable<Product> b = _db.Product.Where(b => b.CategoryId == id);
            return View(b);
        }
        [HttpGet]         
        public IActionResult Details(int id)
        {
            var detail = getDetails(id);
            int idd = id;
            var query = _db.Product
                .FromSqlRaw("select top 4 * from Product where deleted='False' and ParentCateId = (select ParentCateId from Product where Id ={0})", idd)
                .ToList();

            ViewBag.listrelated = query;


            return View("Details", detail);
           

        }

        private Product getDetails(int id)
        {
            var detail = _db.Product.
                Include(s=>s.ParentCate)
                .ThenInclude(m=>m.Category)
                .Where(b => b.Id == id).FirstOrDefault();
            return detail;
        }
        public IActionResult Notices()
        {
            return View();
        }
        /*Thêm sản phẩm vào giỏ hàng*/
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {
            var session = HttpContext.Session;
            string user = session.GetString("userId");
            if (user == null)
            {
                return RedirectToAction("Notices");
            }
            else
            {
                var product = _db.Product
               .Where(p => p.Id == productid)
               .FirstOrDefault();

                var cart = GetCartItems();
                var cartitem = cart.Find(p => p.Product.Id == productid);
                if (cartitem != null)
                {
                    cartitem.quantity++;
                }
                else
                {
                    cart.Add(new CartItem() { quantity = 1, Product = product });
                }

                SaveCartSession(cart);
            }
            return RedirectToAction(nameof(Cart));
        }
        // xóa sản phẩm trong giỏ hàng
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Cập nhật giỏ hàng
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            return Ok();
        }


        //Trang giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            ViewBag.jsoncart = jsoncart;
            return View(GetCartItems());
        }

        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }
    }
}
