using AngularProject.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AngularProject.Controllers
{
    public class DeptItems : Controller
    {
        private MyDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostEnvironment;
        public DeptItems(MyDBContext _db, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostEnvironment)
        {
            db = _db;
            _HostEnvironment = HostEnvironment;
        }


        [HttpPost]
        public async Task<IActionResult> Post(IFormFile files)
        {
            string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            filename = this.EnsureCorrectFilename(filename);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await files.CopyToAsync(output);
            return Ok();
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(_HostEnvironment.WebRootPath, "uploads", filename);
        }

        [HttpPost]
        public string AddDeptItemsVm([FromBody] DeptItemsVm md)
        {
            RemoveDeptItemsVm(md.dept2.deptid);
            dept2 m = new dept2() { deptid = md.dept2.deptid, deptname = md.dept2.deptname, location = md.dept2.location };
            db.dept2.Add(m);
            db.SaveChanges();
            foreach (var c in md.items2)
            {
                items2 d = new items2()
                {
                    itemcode = c.itemcode,
                    itemname = c.itemname,
                    deptid = c.deptid,
                    cost = c.cost,
                    rate = c.rate,
                    date = DateTime.Parse(c.date.ToShortDateString()),
                    picture = c.picture
                };
                db.items2.Add(d);
            }
            db.SaveChanges();
            return "1";
        }
        [HttpPost]
        public string RemoveDeptItemsVm(string id)
        {
            List<items2> st5 = db.items2.Where(xx => xx.deptid == id).ToList();
            db.items2.RemoveRange(st5);
            db.SaveChanges();
            dept2 st6 = db.dept2.Find(id);
            if (st6 != null)
            {
                db.dept2.Remove(st6);
            }
            db.SaveChanges();

            return "1";
        }

        public JsonResult GetAllDepts()
        {
            var a = (from d in db.dept2 select new { d.deptid, d.deptname, d.location });
            return Json(a);
        }

        public JsonResult GetDept(string id)
        {
            var a = (from d in db.dept2 where d.deptid == id select new { d.deptid, d.deptname, d.location });
            return Json(a);
        }
        public JsonResult GetItems(string id)
        {
            var a = (from d in db.items2 where d.deptid == id select new { d.itemcode, d.itemname, d.cost, d.rate, d.date, d.picture });
            return Json(a);
        }
        public JsonResult GetAllItems()
        {
            var a = (from d in db.items2 select new { d.itemcode, d.itemname, d.cost, d.rate, d.date, d.picture, d.deptid });
            return Json(a);
        }
        public ActionResult ShowMe()
        {
            IEnumerable<dept2> s = db.dept2.ToList();
            return View(s);
        }

        public ActionResult ShowItems(string sid = "0")
        {
            List<items2> s = db.items2.Where(xx => xx.deptid == sid).ToList();
            return View(s);
        }

        public ActionResult Create2(string sid = "0")
        {
            ViewBag.sid = sid;
            return View();
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    var files = Request.Form.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        IFormFile file = files[i];
                        string fname;

                        fname = file.FileName;

                        // Get the complete folder path and store the file inside it.  
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        string webRootPath = _HostEnvironment.WebRootPath;
                        string fname1 = "";
                        fname1 = Path.Combine(webRootPath, "Uploads/" + fname);
                        file.CopyTo(new FileStream(fname1, FileMode.Create));
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public string GenerateCodeDP()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.dept2 select det.deptid.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "DP-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "AC-0001";
            }
            return a1;
        }
        public string GenerateCodeItems()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.items2 select det.itemcode.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "IT-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "IT-0001";
            }
            return a1;
        }

    }
}
