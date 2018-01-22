using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoImporter;

namespace ConsoleSandbox
{
	class Program
	{
		static void Main(string[] args)
		{

			YamlParser parser = new YamlParser();
			parser.Load();
		}
	}
}
