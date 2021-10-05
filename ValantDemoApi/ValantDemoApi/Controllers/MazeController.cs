using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MazeController : ControllerBase
    {
      private readonly ILogger<MazeController> _logger;
      private readonly IMazeStore _mazeStore;

      public MazeController(ILogger<MazeController> logger, IMazeStore mazeStore)
      {
          _logger = logger;
          _mazeStore = mazeStore;
      }

      [HttpGet]
      [ActionName("get")]
      public IEnumerable<string> GetNextAvailableMoves()
      {
        return new List<string> {"Up", "Down", "Left", "Right"};
      }

      [HttpGet]
      [ActionName("getmazenames")]
      public IEnumerable<string> GetMazeNames()
      {
        return _mazeStore.GetMazeNames();
    }

    [HttpGet("{mazeName}")]
    [ActionName("getmazetemplate")]
    public string[][] GetMazeTemplate(string mazeName)
    {
      return _mazeStore.GetMazeTemplate(mazeName);
    }

      [HttpPost]
      [ActionName("upload")]
      public async Task<bool> Upload()
      {
        var files = Request.Form.Files;
        foreach (var file in files)
        {
          try
          {
            if (file.Length > 0)
            {
              using (var ms = new MemoryStream())
              {
                file.CopyTo(ms);
                ms.Position = 0;
                StreamReader reader = new StreamReader(ms);
                string text = await reader.ReadLineAsync();
                int lineCount = 0;
                int characters = text.Length;
                List<string> lines = new List<string>();
                while (text != null && text.Length > 0)
                {
                  lines.Add(text);
                  text = await reader.ReadLineAsync();
                  lineCount++;
                }
                var arr = new string[lineCount][];
                for(int i = 0; i < lines.Count; i++)
                {
                  var strArr = lines[i].ToCharArray();
                  string[] innerArr = new string[strArr.Length];
                  for(int j = 0; j < strArr.Length; j++)
                  {
                    innerArr[j] = strArr[j].ToString();
                  }
                  arr[i] = innerArr;
                }
                
                _mazeStore.AddMazeTemplate(file.FileName, arr);
              }
            }
          }
          catch (Exception ex)
          {
            //TODO: Handle exception
          }
        }
        return true;
      }

  }
}
