using Microsoft.AspNetCore.Mvc;
using RandomUsers.Web.Data;
using RandomUsers.Web.Models;
using RandomUsers.Web.Services;
using System.Diagnostics;

namespace RandomUsers.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRandomUserApiService _randomUserApiService;
        private readonly UserRepository _userRepository;

        public HomeController(IRandomUserApiService randomUserApiService, UserRepository userRepository)
        {
            _randomUserApiService = randomUserApiService;
            _userRepository = userRepository;  
        }

        public async Task<IActionResult> Index()
        {
            List<UserModel> users = new List<UserModel>();

            //consome dados da API Random User Generator e insere no banco de dados
            users = await _randomUserApiService.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name.First} {user.Name.Last}");
                Console.WriteLine($"Gender: {user.Gender}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Cell: {user.Cell}");
                Console.WriteLine($"UUID: {user.Login.Uuid}");
                Console.WriteLine();

                await _userRepository.InsertUserAsync( user );  
            }

            //recupera usuários do banco de dados
            var usersFromDB = await _userRepository.GetUsersAsync();

            return View(usersFromDB);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
