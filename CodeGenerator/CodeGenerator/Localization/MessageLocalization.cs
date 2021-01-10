using CodeGenerator.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Localization
{
    public class MessageLocalization : LocalizationManager
    {
        public MessageLocalization() : base(Startup.MessageConfiguration.Language)
        {
        }
    }
}
