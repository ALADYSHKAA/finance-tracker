using Application._Common.Interfaces.Infrastructure.Services;
using Domain.Domains.Users.Enums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class SimpleUserIdService : IUserIdService
    {
        private readonly IHttpContextAccessor _accessor;

        public SimpleUserIdService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public long? GetUserId()
        {
            if (_accessor.HttpContext is null) return (long) WellKnownEmployeeIds.FinanceTrackerPlatform;
            
            var id = _accessor.HttpContext?.Session?.GetInt32("userId");
            return id ?? (long) WellKnownEmployeeIds.TestSuperAdminEmployee;
        }

        public long GetLoggedUserId()
        {
            var id = _accessor.HttpContext?.Session.GetInt32("userId");
            return id ?? (long) WellKnownEmployeeIds.TestSuperAdminEmployee;
        }

        public string GetSsoUserGuid()
        {
            throw new NotImplementedException();
        }

        public long? GetImposerId()
        {
            if (_accessor.HttpContext != null &&
                _accessor.HttpContext.Request.Cookies.TryGetValue("is.imposter", out var userIsImposter) &&
                !string.IsNullOrWhiteSpace(userIsImposter) &&
                bool.TryParse(userIsImposter, out var imposter) &&
                imposter)
            {
                if (_accessor.HttpContext.Request.Cookies.TryGetValue("imposter.id", out var userImposterId) &&
                    !string.IsNullOrWhiteSpace(userImposterId))
                {
                    try
                    {
                        return Convert.ToInt64(userImposterId);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }

            return null;
        }

        public string GetSsoUserEmail()
        {
            return "testSuperAdmin@financeTracker.ru";
        }
    }
}