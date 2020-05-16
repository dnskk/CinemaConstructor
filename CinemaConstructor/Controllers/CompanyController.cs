using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models;
using CinemaConstructor.Models.CompanyViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public CompanyController(UserManager<ApplicationUser> userManager, CompanyRepository companyRepository, UserSessionRepository userSessionRepository)
        {
            _userManager = userManager;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            AddBreadcrumb("Company", "/Company");

            var company = await GetCompany(token);
            var viewModel = new InfoViewModel
            {
                Company = company
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(object model)
        {
            AddPageAlerts(PageAlertType.Info, "you may view the summary <a href='#'>here</a>");
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(CancellationToken token)
        {
            AddBreadcrumb("Company", "/Company");
            AddBreadcrumb("Info", "/Company/Edit");

            var company = await GetCompany(token);
            var viewModel = new EditViewModel
            {
                Phone = company.Phone,
                Email = company.Email,
                InstagramLink = company.InstagramLink,
                FacebookLink = company.FacebookLink
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Company", "/Company");
            AddBreadcrumb("Info", "/Company/Edit");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var company = await GetCompany(token);
                company.Phone = model.Phone;
                company.Email = model.Email;
                company.InstagramLink = model.InstagramLink;
                company.FacebookLink = model.FacebookLink;
                
                await _companyRepository.UpdateAsync(company, token);

                return RedirectToAction(nameof(Index), "Company");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Design(CancellationToken token)
        {
            AddBreadcrumb("Company", "/Company");
            AddBreadcrumb("Design", "/Company/Design");

            var company = await GetCompany(token);
            var viewModel = new DesignViewModel()
            {
                AccentColorFirst = company.AccentColorFirst,
                AccentColorSecond = company.AccentColorSecond
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Design(DesignViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Company", "/Company");
            AddBreadcrumb("Design", "/Company/Design");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var company = await GetCompany(token);
                company.AccentColorFirst = model.AccentColorFirst;
                company.AccentColorSecond = model.AccentColorSecond;

                await _companyRepository.UpdateAsync(company, token);
                return RedirectToAction(nameof(Index), "Company");
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task<Company> GetCompany(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }

        #region Get data method.

        /// <summary>
        /// GET: /Home/GetData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetData()
        {
            try
            {
                // Initialization.
                string search = Request.Form["search[value]"][0];
                string draw = Request.Form["draw"][0];
                string order = Request.Form["order[0][column]"][0];
                string orderDir = Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(Request.Form["length"][0]);

                // Loading.
                List<SalesOrderDetail> data = this.LoadData();

                // Total record count.
                int totalRecords = data.Count;

                // Verification.
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.sr.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.ordertracknumber.ToLower().Contains(search.ToLower()) ||
                                           p.quantity.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.productname.ToLower().Contains(search.ToLower()) ||
                                           p.specialoffer.ToLower().Contains(search.ToLower()) ||
                                           p.unitprice.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.unitpricediscount.ToString().ToLower().Contains(search.ToLower())).ToList();
                }

                // Sorting.
                data = this.SortByColumnWithOrder(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                var result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data });
                return result;
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
                return null;
            }
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<SalesOrderDetail> LoadData()
        {
            // Initialization.
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();

            try
            {
                // Initialization.
                string line = string.Empty;
                //string srcFilePath = "content/files/SalesOrderDetail.txt";
                //var rootPath = Path.GetDirectoryName(AppContext.BaseDirectory);
                //var fullPath = Path.Combine(rootPath, srcFilePath);
                //string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(@"wwwroot\files\SalesOrderDetail.txt", FileMode.Open, FileAccess.Read));

                // Read file.
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.
                    SalesOrderDetail infoObj = new SalesOrderDetail();
                    string[] info = line.Split(',');

                    // Setting.
                    infoObj.sr = Convert.ToInt32(info[0].ToString());
                    infoObj.ordertracknumber = info[1].ToString();
                    infoObj.quantity = Convert.ToInt32(info[2].ToString());
                    infoObj.productname = info[3].ToString();
                    infoObj.specialoffer = info[4].ToString();
                    infoObj.unitprice = Convert.ToDouble(info[5].ToString());
                    infoObj.unitpricediscount = Convert.ToDouble(info[6].ToString());

                    // Adding.
                    lst.Add(infoObj);
                }

                // Closing.
                sr.Dispose();
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #region Sort by column with order method

        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        private List<SalesOrderDetail> SortByColumnWithOrder(string order, string orderDir, List<SalesOrderDetail> data)
        {
            // Initialization.
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.sr).ToList()
                                                                                                 : data.OrderBy(p => p.sr).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ordertracknumber).ToList()
                                                                                                 : data.OrderBy(p => p.ordertracknumber).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.quantity).ToList()
                                                                                                 : data.OrderBy(p => p.quantity).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.productname).ToList()
                                                                                                 : data.OrderBy(p => p.productname).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.specialoffer).ToList()
                                                                                                   : data.OrderBy(p => p.specialoffer).ToList();
                        break;

                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.unitprice).ToList()
                                                                                                 : data.OrderBy(p => p.unitprice).ToList();
                        break;

                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.unitpricediscount).ToList()
                                                                                                 : data.OrderBy(p => p.unitpricediscount).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.sr).ToList()
                                                                                                 : data.OrderBy(p => p.sr).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}
