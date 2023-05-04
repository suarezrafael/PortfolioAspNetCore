using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PortfolioAspNetCore.Pages
{
    public class ContactModel : PageModel
    {
        public bool hasData = false;
        public string firstName = "";
        public string lastName = "";
        public string message = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            hasData = true;
            firstName = Request.Form["firstName"];
            lastName = Request.Form["lastName"];
            message = Request.Form["message"];
        }
    }
}
