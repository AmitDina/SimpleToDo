using Microsoft.Owin.Hosting;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleToDo.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:51573/"))
            {
                Console.WriteLine("Simple Todo Server is running. localhost:51573}");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}