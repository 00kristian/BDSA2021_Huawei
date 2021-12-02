using Core;
using Microsoft.AspNetCore.Mvc;

namespace Server.Model;

public static class Extensions
{
    public static IActionResult ToActionResult(this Status status) => status switch
    {
        Status.Updated => new NoContentResult(),
        Status.Deleted => new NoContentResult(),
        Status.NotFound => new NotFoundResult(),
        Status.Conflict => new ConflictResult(),
        _ => throw new NotSupportedException($"{status} not supported")
    };
}