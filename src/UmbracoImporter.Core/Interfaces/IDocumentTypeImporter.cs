﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Interfaces
{
	public interface IDocumentTypeImporter
	{
		DocumentTypes Import(DocumentTypes documentTypes);
	}
}
