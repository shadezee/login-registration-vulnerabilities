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

        public bool ValidatePngContentType(IFormFile user_upload)
        {
            bool bool_content_type;
            string content_type = user_upload.ContentType;
            if (content_type == "image/png")
            {
                bool_content_type = true;
            }
            else
            {
                bool_content_type = false;
            }
            return bool_content_type;
        }

        public bool ValidatePngSignature(IFormFile user_upload)
        {
            byte[] pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            byte[] fileSignature = new byte[pngSignature.Length];
            using (var stream = user_upload.OpenReadStream())
            {
                stream.Read(fileSignature, 0, pngSignature.Length);
            }

            bool isPng = fileSignature.SequenceEqual(pngSignature);
            return isPng;
        }

        public IActionResult Index(IFormFile user_upload)
        {
            Console.WriteLine("start actions");
            var user = _context.UsersObjects.FirstOrDefault(u => u.Id == Convert.ToInt16(HttpContext.Session.GetString("UserId")));

            var viewModel = new UsersViewModel { };
            if (user_upload != null)
            {
                bool isPngSignature = ValidatePngSignature(user_upload);
                bool isPngContent = ValidatePngContentType(user_upload);

                if (isPngContent && isPngSignature)
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
            }
            viewModel = new UsersViewModel
            {
                Id = Convert.ToInt16(HttpContext.Session.GetString("UserId")),
                HasProfileImage = user.HasProfileImage,
                Username = Convert.ToString(HttpContext.Session.GetString("Username"))
            };

            TempData["UploadError"] = "Please select a valid file to upload.";
            return View("~/Views/Home/DisplayUser.cshtml", viewModel);
        }
    }
}
