using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartJsHelper;
using System.Web.UI.HtmlControls;
using System.Text;

namespace WebformsSampleApp
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				StringBuilder iHtml = new StringBuilder()
					.Append(GeneratePieChart())
					.Append(GenerateDoughnutChart())
					.Append(GenerateLineChart())
					.Append(GenerateBarChart());

				DivCharts.InnerHtml = iHtml.ToString();
			}
		}

		#region Sample chart generation
		private string GeneratePieChart()
		{
			//Point Collection
			List<DataPoint> pointCollection = new List<DataPoint>()
			{
				new DataPoint(){ValueX = 10, Label = "Sample 1"},
				new DataPoint(){ValueX = 30, Label = "Sample 2"},
				new DataPoint(){ValueX = 40, Label = "Sample 3"},
				new DataPoint(){ValueX = 40, Label = "Sample 4"},
				new DataPoint(){ValueX = 10, Label = "Sample 5"},
			};

			double sumValues = pointCollection.Sum(p => p.ValueX);

			//Raw HTML is stored in ViewBag for view rendering using @Html.Raw method
			return ChartHelper.Instance.GeneratePieChart("Sample Pie Chart", pointCollection.ToArray(), String.Format("Total: {0}", sumValues));
		}

		private string GenerateDoughnutChart()
		{
			//Point Collection
			List<DataPoint> pointCollection = new List<DataPoint>()
			{
				new DataPoint(){ValueX = 20, Label = "Sample 1"},
				new DataPoint(){ValueX = 30, Label = "Sample 2"},
				new DataPoint(){ValueX = 40, Label = "Sample 3"},
				new DataPoint(){ValueX = 40, Label = "Sample 4"},
				new DataPoint(){ValueX = 50, Label = "Sample 5"},
			};

			double sumValues = pointCollection.Sum(p => p.ValueX);

			//Raw HTML is stored in ViewBag for view rendering using @Html.Raw method
			return ChartHelper.Instance.GenerateDoughnutChart("Sample Doughnut Chart", pointCollection.ToArray(), String.Format("Total: {0}", sumValues));
		}

		private string GenerateLineChart()
		{
			//Serie Collection
			List<DataSerie> series = new List<DataSerie>();

			DataSerie Lula = new DataSerie() { SerieLabel = "Lula" };
			List<DataPoint> LulaVotes = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 30}, //Each position corresponds to respective position in Categories (month in this case) array
				new DataPoint(){ValueY = 35},
				new DataPoint(){ValueY = 32},
				new DataPoint(){ValueY = 28},
				new DataPoint(){ValueY = 25},
				new DataPoint(){ValueY = 30},
				new DataPoint(){ValueY = 40},
				new DataPoint(){ValueY = 35},
				new DataPoint(){ValueY = 30},
				new DataPoint(){ValueY = 28},
				new DataPoint(){ValueY = 27},
				new DataPoint(){ValueY = 26},
			};
			Lula.Points = LulaVotes.ToArray();
			series.Add(Lula);

			DataSerie Bolsonaro = new DataSerie() { SerieLabel = "Bolsie" };
			List<DataPoint> BolsonaroVotes = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 20},
				new DataPoint(){ValueY = 25},
				new DataPoint(){ValueY = 12},
				new DataPoint(){ValueY = 18},
				new DataPoint(){ValueY = 15},
				new DataPoint(){ValueY = 20},
				new DataPoint(){ValueY = 30},
				new DataPoint(){ValueY = 25},
				new DataPoint(){ValueY = 20},
				new DataPoint(){ValueY = 18},
				new DataPoint(){ValueY = 17},
				new DataPoint(){ValueY = 16},
			};
			Bolsonaro.Points = BolsonaroVotes.ToArray();
			series.Add(Bolsonaro);

			//This serie have 10 points (other series have 12 points). Chart generation automatically fill last positions (10 values / 12 categories) with ValueY = 0
			DataSerie Alckmin = new DataSerie() { SerieLabel = "Picolé de Chuchu" };
			List<DataPoint> AlckminVotes = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 0}, //January value (category index 0)
				new DataPoint(){ValueY = 5},
				new DataPoint(){ValueY = 2},
				new DataPoint(){ValueY = 8},
				new DataPoint(){ValueY = 5},
				new DataPoint(){ValueY = 0},
				new DataPoint(){ValueY = 0},
				new DataPoint(){ValueY = 5},
				new DataPoint(){ValueY = 0},
				new DataPoint(){ValueY = 8}, //October value (category index 9)
				//November value = 0 (auto fill)
				//December value = 0 (auto fill)
			};
			Alckmin.Points = AlckminVotes.ToArray();
			series.Add(Alckmin);

			//Categories Collection
			string[] months = { "Jan", "Fev", "Mar", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

			return ChartHelper.Instance.GenerateLineChart("Voting intentions for 2018 Presidential Brazilian Elections",
				"Candidate", "% of votes", series.ToArray(), months, "2500 pepole sample", "votingChartContainer");
		}

		private string GenerateBarChart()
		{
			string[] requestTypes = { "Hardware", "Systems", "Network", "External" };

			List<DataSerie> requests = new List<DataSerie>();

			DataSerie opened = new DataSerie();
			opened.SerieLabel = "Opened";
			List<DataPoint> openedRequests = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 102}, //# of Opened Hardware requests
				new DataPoint(){ValueY = 50}, //# of Opened System requests
				new DataPoint(){ValueY = 425}, //# of Opened Network requests
				new DataPoint(){ValueY = 20} //# of Opened External requests
			};
			opened.Points = openedRequests.ToArray();
			requests.Add(opened);

			DataSerie pending = new DataSerie();
			pending.SerieLabel = "Pending";
			List<DataPoint> pendingRequests = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 50}, //# of Pending Hardware requests
				new DataPoint(){ValueY = 40}, //# of Pending System requests
				new DataPoint(){ValueY = 152}, //# of Pending Network requests
				new DataPoint(){ValueY = 10} //# of Pending External requests
			};
			pending.Points = pendingRequests.ToArray();
			requests.Add(pending);

			DataSerie solved = new DataSerie();
			solved.SerieLabel = "Solved";
			List<DataPoint> solvedRequests = new List<DataPoint>()
			{
				new DataPoint(){ValueY = 200}, //# of Solved Hardware requests
				new DataPoint(){ValueY = 350}, //# of Solved System requests
				new DataPoint(){ValueY = 400}, //# of Solved Network requests
				new DataPoint(){ValueY = 15} //# of Solved External requests
			};
			solved.Points = solvedRequests.ToArray();
			requests.Add(solved);

			return ChartHelper.Instance.GenerateBarChart("Requests x State",
				"Request type", "nº of requests", requests.ToArray(), requestTypes, "", "requestChartContainer");

		}
		#endregion

	}
}