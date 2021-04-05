using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TopAct.Domain.Exceptions;

namespace TopAct.WebApi
{
    public class ControllerUtils
    {
        public static async Task<IActionResult>
            WithNotFoundHandlingAsync(
            Func<Task> action)
        {
            try
            {
                await action();
                return new OkResult();
            }
            catch (DataNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
