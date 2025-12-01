using System;
using System.Security.Permissions;



namespace HotelManagementSystem.Api.Services
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredPermissionAttribute : Attribute
    {
        public string PermissionKey { get; }

        public RequiredPermissionAttribute(string permissionKey)
        {
            PermissionKey = permissionKey;
        }
    }
}
