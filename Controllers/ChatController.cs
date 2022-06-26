using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using webapi.Model;
using System.Data.SqlClient;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly string Connstr = "Data Source=daniell.database.windows.net;Initial Catalog=daniel;Persist Security Info=True;User ID=daniel;Password=5627Abcd;";
    
    [HttpGet]
    [Route("get")]
    public string a()
    {
        return "This is HttpGet Method";
    }

    [HttpPost]
    [Route("post")]
    public object Post(Practice request)
    {
        return request;
    }

    [HttpGet]
    [Route("list")]
    public Dictionary<string, object> List()
    {
        SqlConnection sql = new SqlConnection(Connstr);
        SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Test];");
        command.Connection = sql;
        sql.Open();
        SqlDataReader reader = command.ExecuteReader();
        List<Test> response = new List<Test>();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                response.Add(new Test()
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    name = reader.GetString(reader.GetOrdinal("name"))
                });
            }
        }
        sql.Close();
        Dictionary<string, object> obj = new Dictionary<string, object>();
        obj.Add("list", response);
        return obj;
    }
    
    [HttpPost]
    [Route("New")]
    public Dictionary<string, string> New(Test request)
    {
        var obj = new Dictionary<string, string>();
        var sql = new SqlConnection(Connstr);
        string sqlStr = $"INSERT INTO [dbo].[Test] VALUES (N'{request.name}');";
        SqlCommand command = new SqlCommand(sqlStr);
        command.Connection = sql;
        sql.Open();
        try
        {
            command.ExecuteReader();
            obj.Add("result", "success");
            obj.Add("message", "success");
            return obj;
        }
        catch(Exception e)
        {
            obj.Add("result", "fail");
            obj.Add("message", e.Message);
            return obj;
        }
        finally
        {
            sql.Close();
        }
        
        
    }

    [HttpPost]
    [Route("delete")]
    public Dictionary<string, string> Delete(Practice request)
    {
        var obj = new Dictionary<string, string>();
        var sql = new SqlConnection(Connstr);
        string sqlStr = $"DELETE FROM [dbo].[Test] WHERE id = {request.id}";
        SqlCommand command = new SqlCommand(sqlStr);
        command.Connection = sql;
        sql.Open();
        try
        {
            command.ExecuteReader();
            obj.Add("result", "success");
            obj.Add("message", "success");
            return obj;
        }
        catch(Exception e)
        {
            obj.Add("result", "fail");
            obj.Add("message", e.Message);
            return obj;
        }
        finally
        {
            sql.Close();
        }
    }

    [HttpPost]
    [Route("modify")]
    public Dictionary<string, string> Modify(Test request)
    {
        var obj = new Dictionary<string, string>();
        var sql = new SqlConnection(Connstr);
        string sqlStr = $"UPDATE [dbo].[Test] SET name=N'{request.name}' WHERE id = ${request.id}";
        SqlCommand command = new SqlCommand(sqlStr);
        command.Connection = sql;
        sql.Open();
        try
        {
            command.ExecuteReader();
            obj.Add("result", "success");
            obj.Add("message", "success");
            return obj;
        }
        catch(Exception e)
        {
            obj.Add("result", "fail");
            obj.Add("message", e.Message);
            return obj;
        }
        finally
        {
            sql.Close();
        }
    }
}