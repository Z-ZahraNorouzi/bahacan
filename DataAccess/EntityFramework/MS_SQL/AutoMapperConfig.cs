using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessModel;
using DataAccess.EntityFramework.Model;
using DataTransferModel.Security;

namespace DataAccess.EntityFramework.MS_SQL
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region Security
            CreateMap<ActionBusinessModel, Model.Action>();
            CreateMap<Model.Action, ActionBusinessModel>();

            CreateMap<LoginHistoryBusinessModel, LoginHistory>();
            CreateMap<LoginHistory, LoginHistoryBusinessModel>();

            CreateMap<LoginStatusBusinessModel, LoginStatus>();
            CreateMap<LoginStatus, LoginStatusBusinessModel>();

            CreateMap<RoleBusinessModel, Role>();
            CreateMap<Role, RoleBusinessModel>();

            CreateMap<RolePermissionBusinessModel, RolePermission>();
            CreateMap<RolePermission, RolePermissionBusinessModel>();

            CreateMap<ServiceActionBusinessModel, ServiceAction>();
            CreateMap<ServiceAction, ServiceActionBusinessModel>();

            CreateMap<ServiceBusinessModel, Service>();
            CreateMap<Service, ServiceBusinessModel>();

            CreateMap<ServiceItemBusinessModel, ServiceItem>();
            CreateMap<ServiceItem, ServiceItemBusinessModel>();

            CreateMap<TenantBusinessModel, Tenant>();
            CreateMap<Tenant, TenantBusinessModel>();

            CreateMap<UserInfoBusinessModel, UserInfo>();
            CreateMap<UserInfo, UserInfoBusinessModel>();

            CreateMap<UserInRoleBusinessModel, UserInRole>();
            CreateMap<UserInRole, UserInRoleBusinessModel>();

            CreateMap<UserPermissionBusinessModel, UserPermission>();
            CreateMap<UserPermission, UserPermissionBusinessModel>();

            CreateMap<UserServiceItemBusinessModel, UserServiceItem>();
            CreateMap<UserServiceItem, UserServiceItemBusinessModel>();

            CreateMap<UserTenantBusinessModel, UserTenant>();
            CreateMap<UserTenant, UserTenantBusinessModel>();


            CreateMap<ActionDataTransferModel, Model.Action>();
            CreateMap<Model.Action, ActionDataTransferModel>();

            CreateMap<LoginHistoryDataTransferModel, LoginHistory>();
            CreateMap<LoginHistory, LoginHistoryDataTransferModel>();

            CreateMap<LoginStatusDataTransferModel, LoginStatus>();
            CreateMap<LoginStatus, LoginStatusDataTransferModel>();

            CreateMap<RoleDataTransferModel, Role>();
            CreateMap<Role, RoleDataTransferModel>();

            CreateMap<RolePermissionDataTransferModel, RolePermission>();
            CreateMap<RolePermission, RolePermissionDataTransferModel>();

            CreateMap<ServiceActionDataTransferModel, ServiceAction>();
            CreateMap<ServiceAction, ServiceActionDataTransferModel>();

            CreateMap<ServiceDataTransferModel, Service>();
            CreateMap<Service, ServiceDataTransferModel>();

            CreateMap<ServiceItemDataTransferModel, ServiceItem>();
            CreateMap<ServiceItem, ServiceItemDataTransferModel>();

            CreateMap<TenantDataTransferModel, Tenant>();
            CreateMap<Tenant, TenantDataTransferModel>();

            CreateMap<UserInfoDataTransferModel, UserInfo>();
            CreateMap<UserInfo, UserInfoDataTransferModel>();

            CreateMap<UserInRoleDataTransferModel, UserInRole>();
            CreateMap<UserInRole, UserInRoleDataTransferModel>();

            CreateMap<UserPermissionDataTransferModel, UserPermission>();
            CreateMap<UserPermission, UserPermissionDataTransferModel>();

            CreateMap<UserServiceItemDataTransferModel, UserServiceItem>();
            CreateMap<UserServiceItem, UserServiceItemDataTransferModel>();

            CreateMap<UserTenantDataTransferModel, UserTenant>();
            CreateMap<UserTenant, UserTenantDataTransferModel>();
            #endregion

        }

    }
}
