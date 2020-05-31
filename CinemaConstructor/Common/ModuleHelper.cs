using System;
using CinemaConstructor.Models;

namespace CinemaConstructor.Common
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
            TicketControl,
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
                        Name = name ?? "Info",
                        IconClassName = "fa fa-id-card",
                        URLPath = "/Company",
                        LinkCounter = counter,
                    };
                case Module.EditInfo:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Info editing",
                        IconClassName = "fa fa-id-card",
                        URLPath = "/Company/Edit",
                        LinkCounter = counter,
                    };
                case Module.EditDesign:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Design editing",
                        IconClassName = "fa fa-id-card",
                        URLPath = "/Company/Design",
                        LinkCounter = counter,
                    };
                case Module.CinemasManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Manage",
                        IconClassName = "fa fa-building",
                        URLPath = "/Cinema/All",
                        LinkCounter = counter,
                    };
                case Module.CinemaCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Create",
                        IconClassName = "fa fa-building",
                        URLPath = "/Cinema/Create",
                        LinkCounter = counter,
                    };
                case Module.HallsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Manage",
                        IconClassName = "fa fa-braille",
                        URLPath = "/Hall/All",
                        LinkCounter = counter,
                    };
                case Module.HallCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Create",
                        IconClassName = "fa fa-braille",
                        URLPath = "/Hall/Create",
                        LinkCounter = counter,
                    };
                case Module.FilmsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Manage",
                        IconClassName = "fa fa-film",
                        URLPath = "/Film/All",
                        LinkCounter = counter,
                    };
                case Module.FilmCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Create",
                        IconClassName = "fa fa-film",
                        URLPath = "/Film/Create",
                        LinkCounter = counter,
                    };
                case Module.FilmSessionsManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Manage",
                        IconClassName = "fa fa-calendar",
                        URLPath = "/FilmSession/All",
                        LinkCounter = counter,
                    };
                case Module.FilmSessionCreate:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Create",
                        IconClassName = "fa fa-calendar",
                        URLPath = "/FilmSession/Create",
                        LinkCounter = counter,
                    };
                case Module.Register:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Register",
                        IconClassName = "fa fa-users",
                        URLPath = "/Account/Register",
                        LinkCounter = counter,
                    };
                case Module.SuperAdmin:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Users",
                        IconClassName = "fa fa-users",
                        URLPath = "/SuperAdmin",
                        LinkCounter = counter,
                    };
                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Role",
                        IconClassName = "fa fa-users",
                        URLPath = "/Role",
                        LinkCounter = counter,
                    };
                case Module.TicketControl:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "Ticket control",
                        IconClassName = "fa fa-check-square",
                        URLPath = "/TicketControl",
                        LinkCounter = counter,
                    };
                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = name ?? "UserLogs",
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
