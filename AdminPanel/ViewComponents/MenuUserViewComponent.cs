﻿using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminPanel.ViewComponents
{
    public class MenuUserViewComponent : ViewComponent
    {

        public MenuUserViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
