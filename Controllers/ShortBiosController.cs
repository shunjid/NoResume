using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NoResume.Models;
using RestSharp;

namespace NoResume.Controllers
{
    public class ShortBiosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShortBiosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        private string _getCurrentlyLoggedInUser()
        {
            return _userManager.GetUserId(HttpContext.User);
        }

        public JsonResult createOTP(string phonenumber)
        {
            
            var authKey = "ExPbbOlgmWgOPY3z4fOUWybe8mM8";
            var url = "https://apigw.grameenphone.com:9001/payments/v2/customers/"+ phonenumber +"/pushotp";

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "161");
            request.AddHeader("Host", "apigw.grameenphone.com:9001");
            request.AddHeader("Postman-Token", "b3b5fd9e-f0b0-4213-8dd7-e54c8c36f17a,4b591436-bac7-4297-8c8e-f5f1a7818bb3");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            request.AddHeader("Authorization", "Bearer "+authKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Encoding", "application/gzip");
            request.AddParameter("undefined", "{\r\n \"sourceId\":\"AGWWolfP\",\r\n \"idType\":\"MSISDN\",\r\n \"amount\":\"1\",\r\n \"priceCode\":\"PPU00021805630191022841\",\r\n \"serviceId\":\"PPU00021805630\",\r\n \"description\":\"Any\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var jsonAfterOtp = JObject.Parse(response.Content);
            var transactionId = jsonAfterOtp["data"]["otpTrasactionId"].ToString();            
            
            var tranlog = new TransactionLog
            {
                DevId = _getCurrentlyLoggedInUser(),
                PhoneNumber = phonenumber,
                OtpTimeStamp = "date",
                OtpTransactionId = transactionId
            };
            _context.Add(tranlog);
            _context.SaveChanges();
            
            return Json(jsonAfterOtp);
        }


        public JsonResult chargeOTP(string tpin)
        {
            var authKey = "ExPbbOlgmWgOPY3z4fOUWybe8mM8";
            var tranlog = _context.TransactionLogs.Last(t => t.DevId == _getCurrentlyLoggedInUser());

            var client = new RestClient("https://apigw.grameenphone.com:9001/payments/v2/customers/"+ tranlog.PhoneNumber +"/chargeotp");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "210");
            request.AddHeader("Host", "apigw.grameenphone.com:9001");
            request.AddHeader("Postman-Token", "17bbb67a-1e01-40dc-b716-faa5b494b12d,326f3dcb-2643-4206-b7b1-dff033446b5d");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.19.0");
            request.AddHeader("Authorization", "Bearer "+ authKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Encoding", "application/gzip");
            request.AddParameter("undefined", "{\r\n \"sourceId\": \"AGWWolfP\",\r\n \"idType\": \"MSISDN\",\r\n \"serviceId\": \"PPU00021805630\",\r\n \"transactionPin\": \""+ tpin +"\",\r\n \"otpTransactionId\": \""+  tranlog.OtpTransactionId  +"\",\r\n \"category\": \"web\",\r\n \"description\": \"Any\"\r\n}\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var jsonAfterOtp = JObject.Parse(response.Content);
            var transactionId = jsonAfterOtp["data"]["transactionId"].ToString();  
            var severreferencecode = jsonAfterOtp["data"]["serverReferenceCode"].ToString();
            var amountpaid = jsonAfterOtp["data"]["amount"].ToString();  
            var timestamp = jsonAfterOtp["accessInfo"]["timestamp"].ToString();  

            var addNewSubscription = new Subscription{
                DevId = _getCurrentlyLoggedInUser(),
                ServeReferenceCode = severreferencecode,
                AmountPaid = float.Parse(amountpaid),
                TimeStamp = timestamp,
                TransactionId = transactionId
            };

            _context.Add(addNewSubscription);
            _context.SaveChanges();


            return Json("Success");
        }
        
        
        
        
        
        // GET: ShortBios/Edit/5
        [Authorize]
        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id !=_getCurrentlyLoggedInUser())
            {
                // Route user to authorized page if request id is invalid
                id = _getCurrentlyLoggedInUser();
            }
            
            var shortBio = await _context.ShortBios.FindAsync(id);
            //var subscription = await _context.Subscriptions.FindAsync(id);

            
            TextInfo caseTitle = new CultureInfo("en-US",false).TextInfo;
            ViewBag.loggedInUserName = caseTitle.ToTitleCase(_userManager.GetUserName(HttpContext.User));
            ViewBag.loggedInUserId = _userManager.GetUserId(HttpContext.User);
            // if (subscription != null)
            // {
            //     ViewBag.subscriptionStatus = "Yes";
            // }
            // else
            // {
            //     ViewBag.subscriptionStatus = "No";
            // }
            //ViewBag.subscriptionStatus = "No";

            try{

                var subscription = await _context.Subscriptions.FirstAsync(t => t.DevId == id);
                ViewBag.subscriptionStatus = "Yes";

            }catch(Exception e){

                ViewBag.subscriptionStatus = "No";
            }
            
            if (shortBio == null)
            {
                return NotFound();
            }

            return View(shortBio);
        }

        // POST: ShortBios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(string id, [Bind("DeveloperId,DeveloperName,DeveloperPhoneNumber,ShortDescription,CurrentCity,IsAvailableForJob")] ShortBio shortBio)
        {
            if (id != shortBio.DeveloperId)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shortBio);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortBioExists(shortBio.DeveloperId))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(shortBio);
            }
            return null;
        }
        
        private bool ShortBioExists(string id)
        {
            return _context.ShortBios.Any(e => e.DeveloperId == id);
        }
    }
}
