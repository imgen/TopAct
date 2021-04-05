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

        public static async Task<IActionResult>
            ResultWithNotFoundHandlingAsync<TResult>(
            Func<Task<TResult>> action)
        {
            try
            {
                var result = await action();
                return new JsonResult(result);
            }
            catch (DataNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
