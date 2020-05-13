using AdminPanel.Models;
using System;

namespace AdminPanel.Common
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Info,
            EditInfo,
            EditDesign,
            CinemasManagement,
            CinemaCreate,
            HallsManagement,
            HallCreate,
            FilmsManagement,
            FilmCreate,
            FilmSessionsManagement,
            FilmSessionCreate,
            About,
            Register,
            SuperAdmin,
            Role,
            UserLogs
        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null, string name = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Info:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Info",
                        IconClassName = "fa fa-link",
                        URLPath = "/Company",
                        LinkCounter = counter,
                    };
                case Module.EditInfo:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Info editing",
                        IconClassName = "fa fa-phone",
                        URLPath = "/Company/Edit",
                        LinkCounter = counter,
                    };
                case Module.EditDesign:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Design editing",
                        IconClassName = "fa fa-phone",
                        URLPath = "/Company/Design",
                        LinkCounter = counter,
                    };
                case Module.CinemasManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Manage",
                        IconClassName = "fa fa-exclamation-triangle",
                        URLPath = "/Cinema/All",
                        LinkCounter = counter,
                    };
                case Module.CinemaCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Create",
                        IconClassName = "fa fa-exclamation-triangle",
                        URLPath = "/Cinema/Create",
                        LinkCounter = counter,
                    };
                case Module.HallsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Manage",
                        IconClassName = "fa fa-exclamation-triangle",
                        URLPath = "/Hall/All",
                        LinkCounter = counter,
                    };
                case Module.HallCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Create",
                        IconClassName = "fa fa-exclamation-triangle",
                        URLPath = "/Hall/Create",
                        LinkCounter = counter,
                    };
                case Module.FilmsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Manage",
                        IconClassName = "fa fa-sign-in",
                        URLPath = "/Film/All",
                        LinkCounter = counter,
                    };
                case Module.FilmCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Create",
                        IconClassName = "fa fa-sign-in",
                        URLPath = "/Film/Create",
                        LinkCounter = counter,
                    };
                case Module.FilmSessionsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Manage",
                        IconClassName = "fa fa-sign-in",
                        URLPath = "/FilmSession/All",
                        LinkCounter = counter,
                    };
                case Module.FilmSessionCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Create",
                        IconClassName = "fa fa-sign-in",
                        URLPath = "/FilmSession/Create",
                        LinkCounter = counter,
                    };
                case Module.Register:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Register",
                        IconClassName = "fa fa-user-plus",
                        URLPath = "/Account/Register",
                        LinkCounter = counter,
                    };
                case Module.About:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "About",
                        IconClassName = "fa fa-users",
                        URLPath = "/Home/About",
                        LinkCounter = counter,
                    };
                case Module.SuperAdmin:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User",
                        IconClassName = "fa fa-link",
                        URLPath = "/SuperAdmin",
                        LinkCounter = counter,
                    };
                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Role",
                        IconClassName = "fa fa-link",
                        URLPath = "/Role",
                        LinkCounter = counter,
                    };
                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "UserLogs",
                        IconClassName = "fa fa-link",
                        URLPath = "/UserLogs",
                        LinkCounter = counter,
                    };

                default:
                    break;
            }

            return null;
        }
    }
}
