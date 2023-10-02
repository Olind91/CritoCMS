using MailKit;
using Microsoft.AspNetCore.Mvc;
using oskarCMS.Models;
using oskarCMS.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace oskarCMS.Controllers
{
    public class ContactsController : SurfaceController
    {
        public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }


        [HttpPost]
        public IActionResult Index(ContactForm contactForm)
        {
           if(!ModelState.IsValid)
                return CurrentUmbracoPage();

            using var mail = new MailHandler("no-reply-limpan@internet.com", "smtp.support.se", 587, "umraco@test.com", "BytMig123!" );

            //sender
            mail.SendAsync(contactForm.Email, "Task was Successful", "hi, test").ConfigureAwait(false);


            //us
            mail.SendAsync("umraco@test.com", $"{contactForm.Name} sent a request", contactForm.Message).ConfigureAwait(false);

            return LocalRedirect(contactForm.RedirectUrl ?? "/");

            
        }
    }
}
