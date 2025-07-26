using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly int[] _allowedRoles;
    public RoleAuthorizeAttribute(params int[] allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var roleId = context.HttpContext.Session.GetInt32("idRole");
        if (roleId == null || !_allowedRoles.Contains(roleId.Value))
        {
            // Không đúng quyền, chuyển về trang đăng nhập hoặc báo lỗi
            context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
        }
        base.OnActionExecuting(context);
    }
}
