using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace TwojaStaraXD
{
    public class Config : IConfig
    {
        [Description("Włączone czy ni twoja stara to kopara rapapara rapapara")]
        public bool IsEnabled { get; set; }
        [Description("zmienna jakiegoś zjeba bo auu?")]
        public string TwojaStara { get; set; } = "XD";
    }
}
