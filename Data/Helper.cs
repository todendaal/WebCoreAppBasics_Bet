using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebCoreAppBasics;


namespace WebCoreAppBasics.Data
{


    public class Helper
    {
        public IConfiguration Configuration { get; }
        private readonly IConfiguration _config;
        public string CnnVal(string name)
        {
            //return Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            //return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //return Configuration.GetConnectionString("DefaultConnection");
            return "Server=LAPTOP-I8IPM6D7;Database=MVCBasics;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}
