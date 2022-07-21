using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSCourse.Services.PhotoStock.Dtos;
using MSCourse.Shared.ControllerBases;
using MSCourse.Shared.Dtos;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MSCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null && file.Length < 1)
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("File is empty", 400));

            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", file.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            PhotoDto photo = new() { Url = "Photos/" + file.FileName };

            return CreateActionResultInstance(Response<PhotoDto>.Success(photo, 200));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo is not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
