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
        public ActionResult Editor()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Editor")]
        public ActionResult EditorPost(string input_code, string btn, string fileContent, string filename,string removeFile)
        {
            if (removeFile == "Remove File") {
                fileContent = null;
                Session["fileContent"] = null;
                filename = null;
            }
            string input = "";

            if (input_code != "")
            {
                input = input_code;
            }

            if (fileContent != null && fileContent != "")
            {
                Session["fileContent"] = fileContent;
                input = (string)Session["fileContent"];
            }

            ViewBag.code = input_code;
            ViewBag.filename = filename;


            Session["CurrentCode"] = input;
            string currentCode = (string)(Session["CurrentCode"]);
            string pastCode = (string)(Session["PastCode"]);
            if (!currentCode.Equals(pastCode))
            {
                Session["isScanned"] = false;
            }
            switch (btn)
            {
                case "scan":
                    if (input != "")
                    {
                        LanguageScanner scanner = new LanguageScanner(input);
                        ArrayList tokensWithLineNumberToView = new ArrayList();
                        for (int i = 0; i < scanner.tokensWithLineNumber.Count; i++)
                        {
                            TokenWithLineNumber tokenWithLineNumber = (TokenWithLineNumber)scanner.tokensWithLineNumber[i];
                            int lineNumber = tokenWithLineNumber.lineNumber;
                            string tokenName = tokenWithLineNumber.token.tokenName;
                            string tokenValue = tokenWithLineNumber.token.tokenValue;
                            tokensWithLineNumberToView.Add("Line : " + lineNumber + "\t\tToken Text: " + tokenName + "\t\t\t\tToken Type: " + tokenValue);
                        }
                        tokensWithLineNumberToView.Add("Total NO of errors: " + scanner.total_number_of_errors);

                        ViewBag.vb = tokensWithLineNumberToView;
                        Session["isScanned"] = true;
                        Session["PastCode"] = input;

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