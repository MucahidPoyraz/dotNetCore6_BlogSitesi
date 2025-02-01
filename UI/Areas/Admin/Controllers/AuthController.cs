using AutoMapper;
using DTOs.AppUser;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserLoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddDto dto)
        {
            if (ModelState.IsValid)
            {
                // DTO'dan AppUser nesnesine dönüştürme
                AppUser appUser = _mapper.Map<AppUser>(dto);

                // Yeni kullanıcı oluşturma
                var result = await _userManager.CreateAsync(appUser, dto.Password);

                if (result.Succeeded)
                {
                    // Kullanıcıya rol atama (opsiyonel)
                    await _userManager.AddToRoleAsync(appUser, "User");

                    // Başarılı kayıt sonrası yönlendirme
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Hataları ModelState'e ekleme
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // Model geçersizse veya hata varsa formu tekrar göster
            return View(dto);
        }

        [HttpGet]
        public IActionResult CikisYap()
        {
            return View();
        }
    }
}
