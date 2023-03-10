using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Helper
{
	public class FileHelper
	{
		public static string GetUniqueFileName(string fileName)
		{
			fileName = Path.GetFileName(fileName);
			return string.Concat(Path.GetFileNameWithoutExtension(fileName),
				"-",
				Guid.NewGuid().ToString().AsSpan(0, 4),
				Path.GetExtension(fileName));
		}
	}
}
