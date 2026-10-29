using CRMWebConsole.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.Helper
{
    public static class AuditHelper
    {
        public static T ApplyAuditInfo<T>(T entity) where T : IAuditable
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var userId = HttpContextHelper.LoginUser.Id;

            entity.CreatedBy = userId;
            entity.LastModifiedBy = userId;

            return entity;
        }
    }
}
