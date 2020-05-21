using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models.SuperAdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaConstructor.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class SuperAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly CompanyMemberRepository _companyMemberRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        private readonly ApplicationUser _testUser = new ApplicationUser
        {
            UserName = "TestTestForPassword",
            Email = "testForPassword@test.test"
        };

        public SuperAdminController(UserManager<ApplicationUser> userMgr, IUserValidator<ApplicationUser> userValid,
            IPasswordValidator<ApplicationUser> passValid, IPasswordHasher<ApplicationUser> passHasher,
            CompanyMemberRepository companyMemberRepository, CompanyRepository companyRepository,
            UserSessionRepository userSessionRepository)
        {
            _userManager = userMgr;
            _userValidator = userValid;
            _passwordValidator = passValid;
            _passwordHasher = passHasher;
            _companyMemberRepository = companyMemberRepository;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        // GET: /<controller>/
        public async Task<ViewResult> Index(CancellationToken token)
        {
            var company = await GetCompany(token);
            var allUsers = await _userManager.Users.ToArrayAsync(token);
            var companyMembers = await _companyMemberRepository.FindByCompanyIdAsync(company.Id, token);

            var users = companyMembers.Select(companyMember => allUsers.Single(p => p.Id == companyMember.UserId)).ToList();

            return View(users);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVm createVm, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = createVm.Email,
                    Email = createVm.Email,
                    //extended properties
                    FirstName = createVm.FirstName,
                    LastName = createVm.LastName,
                    AvatarURL = "/images/user.png",
                    DateRegistered = DateTime.UtcNow.ToString(),
                    Position = "",
                    NickName = "",
                };

                var result = await _userManager.CreateAsync(user, createVm.Password);

                if (result.Succeeded)
                {
                    var companyMember = new CompanyMember
                    {
                        Company = await GetCompany(token),
                        UserId = user.Id,
                        Role = MemberRole.Administrator
                    };

                    await _companyMemberRepository.AddAsync(companyMember, CancellationToken.None);

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(createVm);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View("Index", _userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // the names of its parameters must be the same as the property of the User class if we use asp-for in the view
        // otherwise form values won't be passed properly
        public async Task<IActionResult> Edit(string id, string userName, string email)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                // Validate UserName and Email 
                user.UserName = userName; // UserName won't be changed in the database until UpdateAsync is executed successfully
                user.Email = email;
                var validUseResult = await _userValidator.ValidateAsync(_userManager, user);
                if (!validUseResult.Succeeded)
                {
                    AddErrors(validUseResult);
                }

                // Update user info
                if (validUseResult.Succeeded)
                {
                    // UpdateAsync validates user info such as UserName and Email except password since it's been hashed 
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            ;

            return View(user);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, string password)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                // Validate password
                // Step 1: using built in validations
                var passwordResult = await _userManager.CreateAsync(_testUser, password);
                if (passwordResult.Succeeded)
                {
                    await _userManager.DeleteAsync(_testUser);
                }
                else
                {
                    AddErrors(passwordResult);
                }
                /* Step 2: Because of DI, IPasswordValidator<User> is injected into the custom password validator. 
                   So the built in password validation stop working here */
                var validPasswordResult = await _passwordValidator.ValidateAsync(_userManager, user, password);
                if (validPasswordResult.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                }
                else
                {
                    AddErrors(validPasswordResult);
                }

                // Update user info
                if (passwordResult.Succeeded && validPasswordResult.Succeeded)
                {
                    // UpdateAsync validates user info such as UserName and Email except password since it's been hashed 
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(user);
        }

        private async Task<Company> GetCompany(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }
    }
}
