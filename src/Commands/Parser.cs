﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class Parser
  {
    public static Command? GetCommand(string inputLine)
    {
      CommandWords commandWords = new();
      string[] words = inputLine.ToLower().Split();


      if (words.Length == 0)
      {
        return null;
      }

      if (words.Length > 1)
      {
        // If there are two or more words, construct the command as a single string
        string command = string.Join(" ", words, 0, 2);
        if (commandWords.IsValidCommand(command))
        {
          GameConsole.WriteLine(command);
          return new Command(command);
        }
        else
        {
          return null; // Command is invalid
        }
      }

      if (!commandWords.IsValidCommand(words[0]))
      {
        return null;
      }

      return new Command(words[0]);
    }
  }

}
