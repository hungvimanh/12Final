using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwelveFinal.Controller.form;
using TwelveFinal.Controller.majors;
using TwelveFinal.Repositories;

namespace TwelveFinal.Common
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {
        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private IUOW UOW;
        private ICurrentContext CurrentContext;
        // CurrentContext để lưu thông tin trạng thái của 1 phiên đăng nhập
        // thông tin về permission bảo gồm 2 phần: 1 phần trong jwt, 1 phần do hệ thống kiểm tra và lấy ra
        public PermissionHandler(ICurrentContext CurrentContext, IUOW UOW)
        {
            this.CurrentContext = CurrentContext;
            this.UOW = UOW;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var types = context.User.Claims.Select(c => c.Type).ToList();
            // kiểm tra có thông tin user trong jwt ko
            // ClaimTypes.NameIdentifier == unique_name
            // c.Type là key của đoạn json
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                context.Fail();
                return;
            }
            Guid UserId = Guid.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value, out Guid u) ? u : Guid.Empty;
            bool IsAdmin = bool.TryParse(context.User.FindFirst(c => c.Type == "IsAdmin").Value, out bool b) ? b : false;
            // lấy ra mvcContext, phục vụ cho việc lấy ra url path
            var mvcContext = context.Resource as AuthorizationFilterContext;

            var HttpContext = mvcContext.HttpContext;
            string url = HttpContext.Request.Path.Value;

            CurrentContext.UserId = UserId;
            CurrentContext.IsAdmin = IsAdmin;

            //Kiểm tra phân quyền
            if (!IsAdmin)
            {
                if (!url.StartsWith(FormRoute.Default))
                {
                    context.Fail();
                    return;
                }
            }
            //List<APPSPermission> permissions = await UOW.APPSPermissionRepository.List(new APPSPermissionFilter
            //{
            //    UserId = UserId,// xac dinh xem user truyen vao tuong ung voi tap quyen nao
            //    OperationId = url.ToGuid() // url path la cai nay, neu co path thi tuc la co quyen voi path do
            //});

            context.Succeed(requirement);
        }
    }
}
