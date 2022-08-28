using JobWeb2022.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace JobWeb2022.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var job = db.Categories.ToList();
            return View(job);
        }
        public ActionResult Details(int jobId)
        {
            var job = db.Jobs.Find(jobId);
            if(job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];
            var checkUser = db.ApplyForJobs.Where(x => x.JobId == jobId && x.UserId == userId).ToList();
            if (checkUser.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = userId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "شكرا لك  تم  التقدم للوظيفة بنجاح";
            }
            else
            {
                ViewBag.Result = "! نعتذر لك لقد قمت بالتقدم للوظيفة من قبل ";
            }

            return View();
        }
        public ActionResult GetUserById()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(x => x.UserId == userId).ToList();
            return View(jobs);
        }
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }
        [HttpGet]
        public ActionResult EditOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }
        [HttpPost]
        public ActionResult EditOfJob(ApplyForJob applyForJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applyForJob).State = EntityState.Modified;
                applyForJob.ApplyDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(applyForJob);
        }
        [HttpGet]
        public ActionResult GetJobByPublisher()
        {
            // تشر الوظائف المقدم اليها الناشر الحالي والتي قام بنشرها
           
           var userId = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.JobId
                       where job.User.Id == userId
                       select app;

            var groub = from j in Jobs
                        group j by j.job.JobTile
                        into gr
                        select new JobsViewModel
                        {
                            JobTitle = gr.Key,
                            Items = gr
                        };
            return View(groub.ToList());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Delete(ApplyForJob applyForJob)
        {
            var job = db.ApplyForJobs.Find(applyForJob.ApplyForJobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            db.ApplyForJobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Contact(ContactModel model)
        //{
        //    MailMessage msg = new MailMessage();

        //    msg.From = new MailAddress(model.Email);
        //    msg.To.Add("duaa.albsharat1992@gmail.com");
        //    msg.Subject = model.Subject;
        //    msg.Body = model.Message;
        //    msg.Priority = MailPriority.High;

        //    SmtpClient client = new SmtpClient();

        //    client.Credentials = new NetworkCredential("password", "duaa.albsharat1992@gmail.com");
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = true;

        //    client.Send(msg);

        //    return RedirectToAction("Index");
        //}
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(x => x.JobTile.Contains(searchName)
             || x.JobContent.Contains(searchName)
             || x.Category.CategoryDescription.Contains(searchName)
             || x.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}