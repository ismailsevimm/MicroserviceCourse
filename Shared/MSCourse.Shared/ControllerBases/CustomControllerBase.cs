using Microsoft.AspNetCore.Mvc;
using MSCourse.Shared.Dtos;

namespace MSCourse.Shared.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
