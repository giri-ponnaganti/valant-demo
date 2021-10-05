using System;
using System.Collections.Generic;

/// <summary>
/// Class to maiantain maze store
/// </summary>
public class MazeStore : IMazeStore
{
  Dictionary<string, string[][]> mazeDict = new Dictionary<string, string[][]>();

  public MazeStore()
  {
    //SOXXXXXXXX
    //OOOXXXXXXX
    //OXOOOXOOOO
    //XXXXOXOXXO
    //OOOOOOOXXO
    //OXXOXXXXXO
    //OOOOXXXXXE
    string[][] sampleMaze = { new []{"S", "O", "X", "X", "X", "X", "X", "X", "X", "X" }
                              , new []{"O", "O", "O", "X", "X", "X", "X", "X", "X", "X" }
                              , new []{"O", "X", "O", "O", "O", "X", "O", "O", "O", "O" }
                              , new []{"X", "X", "X", "X", "O", "X", "O", "X", "X", "O" }
                              , new []{"O", "O", "O", "O", "O", "O", "O", "X", "X", "O" }
                              , new []{"O", "X", "X", "O", "X", "X", "X", "X", "X", "O" }
                              , new []{"O", "O", "O", "O", "X", "X", "X", "X", "X", "E" } };
    mazeDict.Add("SampleMaze", sampleMaze);
  }
  /// <summary>
  /// Add maze template to store
  /// </summary>
  /// <param name="templateName"></param>
  /// <param name="maze"></param>
  public void AddMazeTemplate(string templateName, string[][] maze)
  {
    mazeDict.Add(templateName, maze);
  }

  /// <summary>
  /// Get list of maze template names
  /// </summary>
  /// <returns></returns>
  public List<string> GetMazeNames()
  {
    List<string> mazeNames = new List<string>();
    foreach(string key in mazeDict.Keys)
    {
      mazeNames.Add(key);
    }

    return mazeNames;
  }

  /// <summary>
  /// Get Maze template
  /// </summary>
  /// <param name="mazeName">maze name</param>
  /// <returns></returns>
  public string[][] GetMazeTemplate(string mazeName)
  {
    if (mazeDict.ContainsKey(mazeName))  
      return mazeDict[mazeName];

    return null;
  }
}
