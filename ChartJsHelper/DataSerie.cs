using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartJsHelper
{
	/// <summary>
	/// Represents a data serie
	/// </summary>
	public class DataSerie
	{
		public string SerieLabel { get; set; }
		public DataPoint[] Points { get; set; }
	}
}
