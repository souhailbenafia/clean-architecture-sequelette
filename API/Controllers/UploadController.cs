using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {


        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("uploadFiles"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles()
        {
            try
            {
                // Allowed file types
                var allowedFileTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                // Maximum file size (in bytes)
                var maxFileSize = 10485760; // 10 MB
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest("One or more files are empty or corrupt.");
                }
                List<string> response = new List<string>();
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtension = Path.GetExtension(fileName);
                    if (!allowedFileTypes.Contains(fileExtension))
                    {
                        return BadRequest("Invalid file type. Allowed file types are: " + string.Join(", ", allowedFileTypes));
                    }
                    if (file.Length > maxFileSize)
                    {
                        return BadRequest("File size exceeds the maximum allowed size of " + maxFileSize + " bytes.");
                    }
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);


                    using (var stream = System.IO.File.Create(fullPath))
                    {
                       await  file.CopyToAsync(stream);
                      
                    }
                    response.Add(dbPath);
                }
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
