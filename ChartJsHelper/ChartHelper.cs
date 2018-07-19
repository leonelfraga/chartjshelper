using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartJsHelper.Properties;
using System.Threading;

namespace ChartJsHelper
{
	/// <summary>
	/// Class for simple chart generation using ChartJS Library (visit http://www.chartjs.org for ChartJS documentation).
	/// Generates full HTML with container (using Bootstrap panels) + chart for use in dashboards or other locations.
	/// </summary>
	public class ChartHelper
	{
		#region Singleton
		private static ChartHelper _instance;
		public static ChartHelper Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ChartHelper();
				return _instance;
			}
		}
		#endregion

		#region Chart generation methods
		/// <summary>
		/// Generates full HTML / JavaScript code for Pie Chart using ChartJS Library
		/// </summary>
		/// <param name="Title">Chart Title</param>
		/// <param name="DataPoints">Data Points collection</param>
		/// <param name="AdditionalFooterInfo">Additional footer info</param>
		/// <returns></returns>
		public string GeneratePieChart(string Title, DataPoint[] DataPoints, string AdditionalFooterInfo, string ContainerCSSClass = "chartContainer")
		{
			AdditionalFooterInfo = String.IsNullOrEmpty(AdditionalFooterInfo) ? "&nbsp;" : AdditionalFooterInfo;
			string chartId = Guid.NewGuid().ToString(); //generates new ID to chart canvas container for each call to this method

			List<string> values = new List<string>();
			List<string> labels = new List<string>();
			List<string> valuesColors = new List<string>();

			foreach (DataPoint point in DataPoints)
			{

				//Generates a random color for each data point
				int r = new Random().Next(0, 255);
				Thread.Sleep(5); //For generate a new random number.
				int g = new Random().Next(0, 255);
				Thread.Sleep(5);
				int b = new Random().Next(0, 255);


				values.Add(point.ValueX.ToString());
				labels.Add(String.Format("'{0}'", point.Label));
				valuesColors.Add(String.Format("'rgba({0},{1},{2},0.7)'",
					r,
					g,
					b
					));
			}

			//Assigning HTML / JavaScript values to template placeholders
			string strScript = Resources.PieChartScriptTemplate.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@VALUE_POINTS", String.Join(",", values.ToArray()))
				.Replace("@CHART_DATA_COLORS", String.Join(",", valuesColors.ToArray()))
				.Replace("@VALUE_LABELS", String.Join(",", labels.ToArray()));

			string fullChart = Resources.ChartContainer
				.Replace("@CONTAINER_CSS_CLASS", ContainerCSSClass)
				.Replace("@CHART_TITLE", Title)
				.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_JAVASCRIPT", strScript)
				.Replace("@ADDITIONAL_FOOTER_INFO", AdditionalFooterInfo);

			return fullChart;
		}

		/// <summary>
		/// Generates full HTML / JavaScript code for Doughnut Chart using ChartJS Library
		/// </summary>
		/// <param name="Title">Chart Title</param>
		/// <param name="DataPoints">Data Points collection</param>
		/// <param name="AdditionalFooterInfo">Additional footer info</param>
		/// <returns></returns>
		public string GenerateDoughnutChart(string Title, DataPoint[] DataPoints, string AdditionalFooterInfo, string ContainerCSSClass = "chartContainer")
		{
			AdditionalFooterInfo = String.IsNullOrEmpty(AdditionalFooterInfo) ? "&nbsp;" : AdditionalFooterInfo;
			string chartId = Guid.NewGuid().ToString(); //generates new ID to chart canvas container for each call to this method

			List<string> values = new List<string>();
			List<string> labels = new List<string>();
			List<string> valuesColors = new List<string>();

			foreach (DataPoint point in DataPoints)
			{

				//Generates a random color for each data point
				int r = new Random().Next(0, 255);
				Thread.Sleep(5); //For generate a new random number.
				int g = new Random().Next(0, 255);
				Thread.Sleep(5);
				int b = new Random().Next(0, 255);

				values.Add(point.ValueX.ToString());
				labels.Add(String.Format("'{0}'", point.Label));
				valuesColors.Add(String.Format("'rgba({0},{1},{2},0.7)'",
					r,
					g,
					b
					));
			}

			//Assigning HTML / JavaScript strings to template placeholders
			string strScript = Resources.DoughnutChartScriptTemplate.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@VALUE_POINTS", String.Join(",", values.ToArray()))
				.Replace("@CHART_DATA_COLORS", String.Join(",", valuesColors.ToArray()))
				.Replace("@VALUE_LABELS", String.Join(",", labels.ToArray()));

			string fullChart = Resources.ChartContainer
				.Replace("@CONTAINER_CSS_CLASS", ContainerCSSClass)
				.Replace("@CHART_TITLE", Title)
				.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_JAVASCRIPT", strScript)
				.Replace("@ADDITIONAL_FOOTER_INFO", AdditionalFooterInfo);

			return fullChart;
		}

		public string GenerateLineChart(string ChartTitle, string XAxisTitle, string YAxisTitle, DataSerie[] DataSeries, string[] Categories, string AdditionalFooterInfo, string ContainerCSSClass = "chartContainer")
		{
			AdditionalFooterInfo = String.IsNullOrEmpty(AdditionalFooterInfo) ? "&nbsp;" : AdditionalFooterInfo;
			string chartId = Guid.NewGuid().ToString(); //generates new ID to chart canvas container for each call to this method

			/*
			Verifies that each data series has the same number of points and each point in each series corresponds to one category.
			If there are points in the series that do not correspond to a category, zero (0) will be assigned to the ValueY property
			*/
			int numberOfCategories = Categories.Length;
			List<string> DataSetScript = new List<string>();
			List<string> labels = new List<string>();

			foreach(string catg in Categories)
			{
				labels.Add(String.Format("'{0}'", catg));
			}

			foreach (DataSerie serie in DataSeries)
			{
				if(serie.Points.Length < numberOfCategories)
				{
					int diff = numberOfCategories - serie.Points.Length;
					List<DataPoint> tmpPoints = new List<DataPoint>();
					tmpPoints.AddRange(serie.Points);

					for(int i = 0; i < diff;i++)
					{
						tmpPoints.Add(new DataPoint() { ValueY = 0 });
					}
					serie.Points = tmpPoints.ToArray();
				}

				//Generates a random color for each dataset
				int r = new Random().Next(0, 255);
				Thread.Sleep(5); //For generate a new random number.
				int g = new Random().Next(0, 255);
				Thread.Sleep(5);
				int b = new Random().Next(0, 255);

				//Generate DataSet script
				List<string> yPoints = new List<string>();
				foreach(DataPoint point in serie.Points)
				{
					yPoints.Add(point.ValueY.ToString());
				}

				string script = String.Format(@"{{label:'{0}',backgroundColor:{1},borderColor:{1},data:[{2}],fill:false}}",
					serie.SerieLabel,
					String.Format("'rgba({0},{1},{2},0.7)'", r, g, b),
					String.Join(",", yPoints.ToArray()));
				DataSetScript.Add(script);
			}

			//Assigning HTML / JavaScript strings to template placeholders
			string strScript = Resources.LineChartScriptTemplate.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_DATASETS", String.Join(",", DataSetScript.ToArray()))
				.Replace("@VALUE_LABELS", String.Join(",", labels.ToArray()))
				.Replace("@TITLE_X", XAxisTitle)
				.Replace("@TITLE_Y", YAxisTitle);

			string fullChart = Resources.ChartContainer
				.Replace("@CONTAINER_CSS_CLASS", ContainerCSSClass)
				.Replace("@CHART_TITLE", ChartTitle)
				.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_JAVASCRIPT", strScript)
				.Replace("@ADDITIONAL_FOOTER_INFO", AdditionalFooterInfo);

			return fullChart;
		}

		public string GenerateBarChart(string ChartTitle, string XAxisTitle, string YAxisTitle, DataSerie[] DataSeries, string[] Categories, string AdditionalFooterInfo, string ContainerCSSClass = "chartContainer")
		{
			AdditionalFooterInfo = String.IsNullOrEmpty(AdditionalFooterInfo) ? "&nbsp;" : AdditionalFooterInfo;
			string chartId = Guid.NewGuid().ToString(); //generates new ID to chart canvas container for each call to this method

			/*
			Verifies that each data series has the same number of points and each point in each series corresponds to one category.
			If there are points in the series that do not correspond to a category, zero (0) will be assigned to the ValueY property
			*/
			int numberOfCategories = Categories.Length;
			List<string> DataSetScript = new List<string>();
			List<string> labels = new List<string>();

			foreach (string catg in Categories)
			{
				labels.Add(String.Format("'{0}'", catg));
			}

			foreach (DataSerie serie in DataSeries)
			{
				if (serie.Points.Length < numberOfCategories)
				{
					int diff = numberOfCategories - serie.Points.Length;
					List<DataPoint> tmpPoints = new List<DataPoint>();
					tmpPoints.AddRange(serie.Points);

					for (int i = 0; i < diff; i++)
					{
						tmpPoints.Add(new DataPoint() { ValueY = 0 });
					}
					serie.Points = tmpPoints.ToArray();
				}

				//Generates a random color for each dataset
				int r = new Random().Next(0, 255);
				Thread.Sleep(5); //For generate a new random number.
				int g = new Random().Next(0, 255);
				Thread.Sleep(5);
				int b = new Random().Next(0, 255);

				//Generate DataSet script
				List<string> yPoints = new List<string>();
				foreach (DataPoint point in serie.Points)
				{
					yPoints.Add(point.ValueY.ToString());
				}

				string script = String.Format(@"{{label:'{0}',backgroundColor:{1},borderColor:{1},data:[{2}],fill:false}}",
					serie.SerieLabel,
					String.Format("'rgba({0},{1},{2},0.7)'", r, g, b),
					String.Join(",", yPoints.ToArray()));
				DataSetScript.Add(script);
			}

			//Assigning HTML / JavaScript strings to template placeholders
			string strScript = Resources.BarChartScriptTemplate.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_DATASETS", String.Join(",", DataSetScript.ToArray()))
				.Replace("@VALUE_LABELS", String.Join(",", labels.ToArray()))
				.Replace("@TITLE_X", XAxisTitle)
				.Replace("@TITLE_Y", YAxisTitle);

			string fullChart = Resources.ChartContainer
				.Replace("@CONTAINER_CSS_CLASS", ContainerCSSClass)
				.Replace("@CHART_TITLE", ChartTitle)
				.Replace("@CHART_CONTAINER_ID", chartId)
				.Replace("@CHART_JAVASCRIPT", strScript)
				.Replace("@ADDITIONAL_FOOTER_INFO", AdditionalFooterInfo);

			return fullChart;
		}
		#endregion
	}
}
