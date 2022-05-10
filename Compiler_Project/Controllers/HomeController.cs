using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using CompilerProject.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        [ActionName("Index")]
        public IActionResult EditorPost(string input_code)
        {
            ViewBag.code = input_code;

            if (input_code != null)
            {
                LanguageScanner scanner = new LanguageScanner(input_code);
                ArrayList tokensWithLineNumberToView = new ArrayList();
                for (int i = 0; i < scanner.tokensWithLineNumber.Count; i++)
                {
                    TokenWithLineNumber tokenWithLineNumber = (TokenWithLineNumber) scanner.tokensWithLineNumber[i];
                    int lineNumber = tokenWithLineNumber.lineNumber;
                    string tokenName = tokenWithLineNumber.token.tokenName;
                    string tokenValue = tokenWithLineNumber.token.tokenValue;
                    tokensWithLineNumberToView.Add("Line : " + lineNumber + "\tToken Text: " + tokenName + "\tToken Type: " + tokenValue);
                }
                tokensWithLineNumberToView.Add("Total NO of errors: " + scanner.total_number_of_errors);

                ViewBag.vb = tokensWithLineNumberToView;

            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


    }
}
