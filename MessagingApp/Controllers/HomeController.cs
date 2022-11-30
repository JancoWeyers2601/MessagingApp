using MessagingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;


namespace MessagingApp.Controllers
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
            MessagingAppContext db = new MessagingAppContext();
            List<MessageDetails> list = new List<MessageDetails>();

            
            int ConvID = 0;
            var username = "";

            //GET Conversation ID
            try
            {
                Int32.TryParse(HttpContext.Request.Cookies["ConversationID"], out ConvID);
            }
            catch (Exception e)
            {

                throw;
            }

            //GET Username


            //Populate view
            var allmessages = db.TblMessages.Where(x => x.FkTblConversation == ConvID);

            if (allmessages.Any())
            {
                foreach (var message in allmessages)
                {
                    list.Add(new MessageDetails()
                    {
                        TblMessageBody = message.TblMessageBody,
                        TblMessageTime = (DateTime)message.tbl_Message_Time,
                        TblMessageUser = message.FkTblUser
                    });
                }
            }

            ViewBag.Messages = allmessages;

            return View();

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

        [HttpPost]
        public IActionResult SendMessage(MessageModel message)
        {
            MessagingAppContext db = new MessagingAppContext();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int ConvID = 0;

            try
            {
                Int32.TryParse(HttpContext.Request.Cookies["ConversationID"], out ConvID);
            }
            catch (Exception e)
            {

                throw;
            }

            message.FkTblUser = userId;
            message.FkTblConversation = ConvID;
            message.tbl_Message_Time = DateTime.Now;

            db.TblMessages.Add(message);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ViewMessages()
        {
            MessagingAppContext db = new MessagingAppContext();

            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var x = db.TblMessages.Where(x => x.FkTblUser == userId).Select(x => x.FkTblUser);
            //db.TblMessages.Where(x => x.FkTblUser == userId).Select(x=>x.FkTblUser) ;

            return null;
        }

    }
}