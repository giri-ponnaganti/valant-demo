using System;
using System.Collections.Generic;

public interface IMazeStore
{
  public void AddMazeTemplate(string templateName, string[][] maze);
  public List<string> GetMazeNames();
  string[][] GetMazeTemplate(string mazeName);
}
