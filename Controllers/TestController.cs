using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("get")]
    public string a()
    {
        return "This is HttpGet Method";
    }

    [HttpPost]
    [Route("post")]
    public object post(Information information)
    {
        string s = Newtonsoft.Json.JsonConvert.SerializeObject(information);
        Console.WriteLine("Data: " + s);
        Information obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Information>(s);
        return obj;
    }
    public class Information
    {
        public int id { get; set; }
    }
}