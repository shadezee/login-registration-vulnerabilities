using log_reg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace log_reg.Controllers
{
    public class ImgUploadController : Controller
    {
        private readonly UsersContext _context;

        public ImgUploadController(UsersContext context)
        {
            _context = context;

        }

        public IActionResult Index(IFormFile user_upload)
        {
            Console.WriteLine("start actions");
            var user = _context.UsersObjects.FirstOrDefault(u => u.Id == Convert.ToInt16(HttpContext.Session.GetString("UserId")));

            var viewModel = new UsersViewModel { };
            if (user_upload != null)
            {
                Console.WriteLine("Got File");
                var file_type = Path.GetExtension(Path.GetFileName(user_upload.FileName));
                var file_name = String.Concat(Convert.ToString(HttpContext.Session.GetString("UserId")), file_type);
                Console.WriteLine("Got FileName");

                var path = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_pics")).Root + $@"\{file_name}";

                using (FileStream fs = System.IO.File.Create(path))
                {
                    Console.WriteLine("Got UPLOAD");
                    user_upload.CopyTo(fs);
                    fs.Flush();
                }

                if (user.HasProfileImage == 0)
                {
                    user.HasProfileImage = 1;
                    _context.SaveChanges();
                }
                viewModel = new UsersViewModel
                {
                    Id = Convert.ToInt16(HttpContext.Session.GetString("UserId")),
                    HasProfileImage = user.HasProfileImage,
                    Username = Convert.ToString(HttpContext.Session.GetString("Username")
                    )
                };

                return View("~/Views/Home/DisplayUser.cshtml", viewModel);
            }
            viewModel = new UsersViewModel
            {
                Id = Convert.ToInt16(HttpContext.Session.GetString("UserId")),
                HasProfileImage = user.HasProfileImage,
                Username = Convert.ToString(HttpContext.Session.GetString("Username"))
            };

            TempData["UploadError"] = "Please select a file to upload.";
            return View("~/Views/Home/DisplayUser.cshtml", viewModel);
        }
    }
}
