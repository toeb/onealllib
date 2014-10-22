using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAllLib.Sample
{
  class Program
  {
    static void Main(string[] args)
    {
      var res = Login.AuthenticateApplication("thetoeb");
      res.Wait();
    }
  }
}
