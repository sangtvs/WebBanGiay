using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using testt.Models;

namespace testt.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Login(){
        return View();
    }
    public IActionResult IndexAdmin()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(User model)
    {
        Database database = new Database();
        User users = database.GetUsers(model);
        if (users != null)
        {
            // Đăng nhập thành công
            // Kiểm tra vai trò của người dùng và chuyển hướng đến trang tương ứng
            TempData["UserName"] = model.UserName;
            TempData.Keep("UserName");
            if (users.Role == "admin")
            {
                TempData["Role"] = "admin";
                TempData["ID"] = users.ID.ToString();
                TempData["DaDangNhap"] = true;
                TempData["Name"] = users.Name.ToString();
                TempData["Email"] = users.Email.ToString();
                TempData["Phone"] = users.Phone.ToString();
                TempData["Address"] = users.Address.ToString();
                TempData.Keep("Role");
                TempData.Keep("ID");
                return RedirectToAction("IndexAdmin", "Home"); // Chuyển hướng đến trang admin
            }
            else
            {
                TempData["Role"] = "user";
                TempData["ID"]=users.ID.ToString();
                TempData["DaDangNhap"] = true;
                TempData["Name"] =users.Name.ToString();
                TempData["Email"] = users.Email.ToString();
                TempData["Phone"] = users.Phone.ToString();
                TempData["Address"] = users.Address.ToString();
                TempData.Keep("Role");
                TempData.Keep("ID");
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
            }

        }
        else
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            // Đăng nhập không thành công, hiển thị lại trang đăng nhập với thông báo lỗi
            return View();
        }
    }
    [HttpPost]
    public IActionResult dangky(User model)
    {
        Database database=new Database();
        User existingUser = database.CheckUsers(model);
        if (model.UserName == null || model.Name == null || model.PassWord == null || model.Email == null || model.Phone == null)
        {
            return View();
        }
        else
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Người dùng đã tồn tại.");
            return View();
        }
        else
        
        {
            database.AddUser(model);
            // Đăng ký thành công, chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login");
        }
    }
    [HttpPost]
    public IActionResult thongtin(User model){
        Database database=new Database();
        User existingUser = database.CheckUsers(model);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Người dùng đã tồn tại.");
            return View();
        }
        else
        {
            database.Themkh(model);
            return View();
        }
    }
    [HttpGet]
    public IActionResult suathongtin(int id)
    {
        Database database=new Database();
        User user=database.GetUsers(id);
        return View(user);
    }
    public ActionResult DeleteUser(int id)
    {
        Database database=new Database();
        database.DeleteUser(id);
        return RedirectToAction("thongtin");
    }
    [HttpPost]
    public IActionResult suathongtin(User model) { 
        Database database=new Database();
        database.saveUser(model);
        return RedirectToAction("thongtin","Home");
    }
    [HttpPost]
    public IActionResult ThemGiay(Products model)
    {
        Database database=new Database();
        using (var memoryStream = new MemoryStream())
        {
            model.AnhGiay.CopyTo(memoryStream);
            model.Image = memoryStream.ToArray();
        }
        database.ThemGiay(model);
        return RedirectToAction("IndexAdmin");
    }
    public ActionResult GetImage(int id)
    {
        Database db=new Database();
        byte[] imageData = db.GetAnh(id);

        // Trả về hình ảnh dưới dạng FileResult
        return File(imageData, "image/jpeg");
    }
    public IActionResult Dangky()
    {
        return View();
    }
    public IActionResult Chitiet(int id) {
        Database database=new Database();
        Products model=database.DetailGiay(id);
        return View(model) ;
    }
    public IActionResult DeleteGiay(int id)
    {
        Database database=new Database();
        database.DeleteGiay(id);
        return RedirectToAction("IndexAdmin");
    }
    public IActionResult Thongtin(){
        return View();
    }
    public IActionResult Suathongtin()
    {
        return View();
    }
    public IActionResult Logout()
    {
        TempData.Remove("UserName");
        TempData.Remove("Role");
        return RedirectToAction("Index", "Home");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Cart()
    {
        Database db =new Database();
        int userid = Convert.ToInt32(TempData["ID"]);
        Orders orders = db.GetOders(userid);
        TempData["ID"] = userid;
        return View(orders);
    }
    [HttpPost]
    public IActionResult AddToCart(int ProductId, int quantity,int Size)
    {
        var ok = TempData["Role"];
        if (ok!=null) {
            Database db = new Database();
            int userid = Convert.ToInt32(TempData["ID"]);
            Orders order = db.GetOders(userid);
            if (order.ID == 0)
            {
                db.AddOrders(userid);
                order = db.GetOders(userid);

            }
            
            var giay = db.GetGiay(ProductId);
            OrderDetails orderDetail = new OrderDetails
            {
                OrderId = order.ID,
                ProductID = ProductId,
                Quantity = quantity,
                Price = giay.Price * quantity,
                Size = Size
            };
            db.AddOrderDetails(orderDetail);
            TempData["ID"] = userid;
            return RedirectToAction("sanpham", "Home");
        }
        else
        {
            return RedirectToAction("Login", "Home");
        }
        
    }
    public ActionResult DeleteProduct(int id,int idorder)
    {
        Database database = new Database();
        database.DeleteProduct(id,idorder);
        return RedirectToAction("Cart","Home");
    }
    public ActionResult Xacnhan()
    {
        Database db = new Database();
        int id = Convert.ToInt32(TempData["ID"]);
        User model = db.GetUsers(id);
        TempData["ID"] = id;
        return View(model);
    }
    [HttpPost]
    public IActionResult Confirm(int id)
    {
        Database db = new Database();
        Orders orders = db.GetOders(id);
        db.UpdateOrderStatus(orders.ID);
        List<OrderDetails> orderDetails = db.GetOrderDetail(orders.ID);
        foreach(var item in orderDetails)
        {
            db.ReduceProductQuantity(item.ProductID, item.Quantity);
        }
        return RedirectToAction("Index"); 
    }
    public IActionResult thongtingiay()
    {
        return View();
    }
    [HttpGet]
    public IActionResult suathongtingiay(int id)
    {
        Database database = new Database();
        Products giay = database.GetGiay(id);
        return View(giay);
    }
    public ActionResult xoagiay(int id)
    {
        Database database = new Database();
        database.DeleteGiay(id);
        return RedirectToAction("thongtin");
    }
    [HttpPost]
    public IActionResult suathongtingiay(Products model)
    {
        Database database = new Database();
        database.saveGiay(model);
        return RedirectToAction("thongtingiay", "Home");
    }
    public IActionResult sanpham()
    {
        return View();
    }
}
