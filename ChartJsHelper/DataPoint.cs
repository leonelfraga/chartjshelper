using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartJsHelper
{
	/// <summary>
	/// Represents a single data point
	/// </summary>
	public class DataPoint
	{
		public string Label { get; set; }
		public double ValueX { get; set; }
		public double ValueY { get; set; }
	}
}
