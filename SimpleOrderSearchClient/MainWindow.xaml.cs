using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleOrderSearchClient.SimpleOrderSearch;
using System.Web;
using System.Web.Script.Serialization;

namespace SimpleOrderSearchClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<OrderInfo> _orderInfos = new List<OrderInfo>();
        private int _indexer = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string errorMsg = ValidateInput();

            if (string.IsNullOrEmpty(errorMsg))
            {
                _indexer = 0;
                OrderInfo orderQuery = new OrderInfo();
                DateTime selectedDate = DateTime.Parse(this.DatePicker.Text).Date;
                orderQuery.CompletionDte = new DateTime(
                    year: selectedDate.Year,
                    month: selectedDate.Month,
                    day: selectedDate.Day,
                    hour: int.Parse(this.Hours.Text),
                    minute: int.Parse(this.Minutes.Text),
                    second: int.Parse(this.Seconds.Text)).ToString("yyyy-MM-ddTHH:mm:ss");

                if (string.IsNullOrWhiteSpace(this.OrderNumber.Text))
                {
                    orderQuery.MSA = int.Parse(this.MSA.Text);
                    orderQuery.Status = int.Parse(this.Status.Text);
                }
                else
                {
                    orderQuery.OrderID = int.Parse(this.OrderNumber.Text);
                }

                Service1Client service1 = new Service1Client();
                OrderInfoResponse orderInfoResponse = service1.GetOrderInfos(orderQuery);

                _orderInfos = new List<OrderInfo>(orderInfoResponse.OrderInfos);

                if (_orderInfos.Count > 0)
                {
                    this.OutputArea.Text = (new JavaScriptSerializer().Serialize(_orderInfos[_indexer])).ToString();
                } 
                else
                {
                    this.OutputArea.Text = string.Empty;
                }

                var json = new JavaScriptSerializer().Serialize(orderInfoResponse.OrderInfos);
                this.ConsoleOutput.Text = json.ToString();
            }
            else
            {
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            if (_orderInfos.Count > 0)
            {
                if (_indexer > 0)
                {
                    _indexer--;
                }

                this.OutputArea.Text = (new JavaScriptSerializer().Serialize(_orderInfos[_indexer])).ToString();
            }
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            if (_orderInfos.Count > 0)
            {
                if (_indexer < this._orderInfos.Count - 1)
                {
                    _indexer++;
                }

                this.OutputArea.Text = (new JavaScriptSerializer().Serialize(_orderInfos[_indexer])).ToString();
            }
        }

        private string ValidateInput()
        {
            string errorMessage = string.Empty;
            DateTime dateCheck = DateTime.MinValue;
            int numCheck = 0;

            if (!DateTime.TryParse(this.DatePicker.Text, out dateCheck))
            {
                errorMessage += "Invalid Date.\n";
            }

            if (!int.TryParse(this.Hours.Text, out numCheck) ||
                this.Hours.Text.Length > 2 ||
                numCheck > 23 ||
                numCheck < 0)
            {
                errorMessage += "Invalid Hours.\n";
            }

            if (!int.TryParse(this.Minutes.Text, out numCheck) ||
                this.Minutes.Text.Length > 2 ||
                numCheck > 59 ||
                numCheck < 0)
            {
                errorMessage += "Invalid Minutes.\n";
            }

            if (!int.TryParse(this.Seconds.Text, out numCheck) ||
                this.Seconds.Text.Length > 2 ||
                numCheck > 59 ||
                numCheck < 0)
            {
                errorMessage += "Invalid Seconds.\n";
            }

            if (!string.IsNullOrWhiteSpace(this.OrderNumber.Text))
            {
                if (!int.TryParse(this.OrderNumber.Text, out numCheck))
                {
                    errorMessage += "Order Number must be numeric.\n";
                }
            }
            else
            {
                if (!int.TryParse(this.MSA.Text, out numCheck) || !int.TryParse(this.Status.Text, out numCheck))
                {
                    errorMessage += "MSA and Status must be numeric.\n";
                }
            }
            return errorMessage;
        }
    }
}
