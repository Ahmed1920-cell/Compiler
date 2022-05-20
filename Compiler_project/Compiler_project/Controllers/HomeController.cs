using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compiler_project.Models;
using System.Collections;

namespace Compiler_project.Controllers
{
    public class HomeController : Controller
    {
        private Boolean isScanned = true;
        public ActionResult Editor()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Editor")]
        public ActionResult EditorPost(string input_code, string btn)
        {
            ViewBag.code = input_code;
            Session["CurrentCode"] = input_code;
            string currentCode = (string)(Session["CurrentCode"]);
            string pastCode = (string)(Session["PastCode"]);
            if (!currentCode.Equals(pastCode)) {
                Session["isScanned"] = false;
            }
            switch (btn)
            {
                case "scan":
                    if (input_code != null)
                    {
                        LanguageScanner scanner = new LanguageScanner(input_code);
                        ArrayList tokensWithLineNumberToView = new ArrayList();
                        for (int i = 0; i < scanner.tokensWithLineNumber.Count; i++)
                        {
                            TokenWithLineNumber tokenWithLineNumber = (TokenWithLineNumber)scanner.tokensWithLineNumber[i];
                            int lineNumber = tokenWithLineNumber.lineNumber;
                            string tokenName = tokenWithLineNumber.token.tokenName;
                            string tokenValue = tokenWithLineNumber.token.tokenValue;
                            tokensWithLineNumberToView.Add("Line : " + lineNumber + "\tToken Text: " + tokenName + "\tToken Type: " + tokenValue);
                        }
                        tokensWithLineNumberToView.Add("Total NO of errors: " + scanner.total_number_of_errors);

                        ViewBag.vb = tokensWithLineNumberToView;
                        isScanned = true;
                        Session["isScanned"] = true;
                        Session["PastCode"] = input_code;

                    }
                    return View();
                case "parse":
                    ArrayList parseToView = new ArrayList();

                    if ((Boolean)Session["isScanned"] == true)
                    {
                        parseToView.Add("Parse Working");
                    }
                    else
                    {
                        parseToView.Add("Scanner Should Working first before the Parser");
                    }
                    ViewBag.vb = parseToView;

                    return View();
                case "comment":
                    return View();
                case "uncomment":
                    return View();



            }

            return View();
        }


        
    }
}