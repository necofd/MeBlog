﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeBlog.Models;

namespace MeBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.Blog.Take(4).OrderByDescending(x => x.CreateTime).ToList();//display 4 blogs (able to be modified)
        foreach (var blog in list){
            blog.Author = _context.Author.Find(blog.AuthorId);//id si verilen yazari blog'un yazarina ekliyor
        }
        return View(list);
    }

    public IActionResult Post(int Id)
    {
        var blog = _context.Blog.Find(Id);
        blog.Author = _context.Author.Find(blog.AuthorId); //yazar atama. null reference hatasi giderilmesi
        blog.ImagePath = "/img/"+blog.ImagePath;
        return View(blog);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
