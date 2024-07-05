using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEntityAPI;
using System.Net.Http;

namespace MVCEntityAPI.Controllers
{
    public class MVCController : Controller
    {
        private Employee1Entities db = new Employee1Entities();

        // GET: MVC
        public ActionResult Index()
        {
            //return View(db.WebApiTabs.ToList());
            IEnumerable<WebApiTab> employees = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                var responseTask = client.GetAsync("getwebapitabs");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<WebApiTab>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<WebApiTab>();
                }



            }
            return View(employees);
        }

        // GET: MVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            WebApiTab employee = null;
            using(var client =new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                var responseTask = client.GetAsync($"getwebapideatiltab/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<WebApiTab>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new WebApiTab();
                }
            }
            return View(employee);

        }


        //    WebApiTab webApiTab = db.WebApiTabs.Find(id);
        //    if (webApiTab == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(webApiTab);
        //}

        // GET: MVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,mark")] WebApiTab webApiTab)
        {
            if (ModelState.IsValid)
            {
                using(var client=new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                    var postTask = client.PostAsJsonAsync<WebApiTab>("postwebapitab", webApiTab);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(webApiTab);




                //db.WebApiTabs.Add(webApiTab);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(webApiTab);
        }

        // GET: MVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            WebApiTab employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                var responseTask = client.GetAsync($"getwebapideatiltab/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<WebApiTab>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new WebApiTab();
                }
            }
            return View(employee);
        }

        // POST: MVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,mark")] WebApiTab webApiTab)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                    var postTask = client.PutAsJsonAsync<WebApiTab>($"putwebapitab/ {webApiTab.Id}", webApiTab);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
                return View(webApiTab);
            }

        // GET: MVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            WebApiTab employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                var responseTask = client.GetAsync($"getwebapideatiltab/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<WebApiTab>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new WebApiTab();
                }
            }
            return View(employee);
        }

            // POST: MVC/Delete/5
            [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59135/api/API/");
                var postTask = client.DeleteAsync($"deletewebapitab/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                //WebApiTab webApiTab = db.WebApiTabs.Find(id);
                //db.WebApiTabs.Remove(webApiTab);
                //db.SaveChanges();
                return RedirectToAction("Delete");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
