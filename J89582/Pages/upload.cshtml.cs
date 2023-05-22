using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace J89582.Pages
{
    public class uploadModel : PageModel
    {
        private readonly ILogger<uploadModel> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public uploadModel(ILogger<uploadModel> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                return;
            }

            _logger.LogInformation($"Uplaoding {UploadedFile.FileName}.");
            var id = Request.Query["id"];
            string extension = Path.GetExtension(UploadedFile.FileName);
            string newFileName = id + extension;
            string targetFilename = $"{_environment.ContentRootPath}/wwwroot/img/{newFileName}";

            using (var stream = new FileStream(targetFilename, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(stream);
            }
        }
    }
}
