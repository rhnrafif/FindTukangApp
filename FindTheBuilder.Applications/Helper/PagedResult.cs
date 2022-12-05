using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Applications.Helper
{
	public class PagedResult<T>
	{
		public PagedResult(IEnumerable<T> data, long total)
		{
			Data = data;
			Total = total;
		}

		public PagedResult()
		{

		}

		public IEnumerable<T> Data { get; set; }
		public long Total { get; set; }
	}
}
