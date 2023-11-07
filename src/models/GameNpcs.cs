using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class GameNpcs
  {
    public object[]? Npcs { get; set; }
  }

  public class GameNpc
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Greeting { get; set; }
    public string? Swear { get; set; }
    public string? Description { get; set; }
    public string? Clue { get; set; }
    public string? Goodbye { get; set; }
    public NpcPathDialogs? Dialogs { get; set; }
  }

  public class NpcPathDialogs
  {
    public NpcDialogs? PathA { get; set; }
    public NpcDialogs? PathB { get; set; }
    public NpcDialogs? PathC { get; set; }
    public NpcDialogs? PathD { get; set; }
  }

  public class NpcDialogs : NpcPathDialogs
  {
    public string? Prompt { get; set; }
    public string? MsgA { get; set; }
    public string? MsgB { get; set; }
    public string? MsgC { get; set; }
    public string? MsgD { get; set; }
  }
}