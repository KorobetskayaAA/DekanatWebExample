﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DekanatWebExample.Data;
using DekanatWebExample.Models;

namespace DekanatWebExample.Controllers
{
    public class StudentsController : Controller
    {
        private DekanatContext db = new DekanatContext();

        // GET: Students
        public ActionResult Index(int? groupID, string searchString, string sortOrder)
        {
            var groups = from g in db.Groups
                         select g;

            if (groupID == null)
            {
                groupID = groups.First().ID;
            }

            ViewBag.Groups = new SelectList(groups, "ID", "Name", groupID);

            var students = from s in db.Students
                           select s;

            students = students.Where(s => s.GroupId == groupID);


            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) ||
                                               s.LastName.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Name";
            }
            var sortOrderParts = sortOrder.Split('_');
            switch (sortOrderParts[0])
            {
                case "Studbilet":
                    if (sortOrderParts.Length > 1 && sortOrderParts[1] == "desc")
                    {
                        students = students.OrderByDescending(s => s.Studbilet);
                        ViewBag.StudbiletSortParam = "↓";
                    }
                    else
                    {
                        students = students.OrderBy(s => s.Studbilet);
                        ViewBag.StudbiletSortParam = "↑";
                    }
                    break;
                case "BirthDate":
                    if (sortOrderParts.Length > 1 && sortOrderParts[1] == "desc")
                    {
                        students = students.OrderByDescending(s => s.BirthDate);
                        ViewBag.BirthDateSortParam = "↓";
                    }
                    else
                    {
                        students = students.OrderBy(s => s.BirthDate);
                        ViewBag.BirthDateSortParam = "↑";
                    }
                    break;
                // TODO: sort by kurs, ed.program, form, number. IComparable is useless
                case "Group":
                    if (sortOrderParts.Length > 1 && sortOrderParts[1] == "desc")
                    {
                        students = students.OrderByDescending(s => s.GroupId);
                        ViewBag.GroupSortParam = "↓";
                    }
                    else
                    {
                        students = students.OrderBy(s => s.GroupId);
                        ViewBag.GroupSortParam = "↑";
                    }
                    break;
                default: 
                    if (sortOrderParts.Length > 1 && sortOrderParts[1] == "desc")
                    {
                        students = students.OrderByDescending(s => s.LastName + s.FirstName);
                        ViewBag.NameSortParam = "↓";
                    }
                    else
                    {
                        students = students.OrderBy(s => s.LastName + s.FirstName);
                        ViewBag.NameSortParam = "↑";
                    }
                    break;
            }

            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Studbilet,FirstName,LastName,BirthDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,BirthDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
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
