using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        /// <summary>
        /// IFormFile: The COntract That Maps the HttpPost request
        /// COntaing the File  
        /// </summary>
        /// <param name="file">The 'file' reptresent the Content-NAme in Http Request</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    // Set the Folder Path where the file is written
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

                    // Read the FIle Name with File TYpe and its Extension
                    var fileFullName = ContentDispositionHeaderValue.Parse(
                        file.ContentDisposition).FileName.Trim('"');

                   
                    //Set the FIleUpload FIle Path
                    var fulPath = Path.Combine(folder, fileFullName);
                    // Upload the File
                    using (FileStream fs  = new FileStream(fulPath, FileMode.CreateNew))
                    {
                        await file.CopyToAsync(fs);
                    }
                    return Ok("File uploaded Successfully");
                }
                else
                {
                    return BadRequest($"File is Empty");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Occured while Processing Request {ex.Message}");
               
            }
        }
    }
}
