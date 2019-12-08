using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SortAnArray
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _inputData;
        public string InputData
        {
            get { return _inputData; }
            set
            {
                _inputData = value;
                RaisePropertyChanged("InputData");
            }
        }

        private string _outputData;
        public string OutputData
        {
            get { return _outputData; }
            set
            {
                _outputData = value;
                RaisePropertyChanged("OutputData");
            }
        }

        public RelayCommand SortInputArrayCommand { get; set; }

        public MainWindowViewModel()
        {
            SortInputArrayCommand = new RelayCommand(SortInputData);
        }

        private void SortInputData()
        {
            try
            {
                if (string.IsNullOrEmpty(InputData))
                {
                    OutputData = string.Empty;
                    return;
                }
                if (!InputData.Contains(","))
                {
                    if (double.TryParse(InputData, out double output))
                    {
                        OutputData = output.ToString();
                        return;
                    }
                    OutputData = string.Empty;
                    return;
                }

                var inputData = new List<double>();
                foreach (var input in InputData.Split(',').Where(x => !string.IsNullOrEmpty(x)))
                {
                    if (double.TryParse(input, out double parshedInput))
                    {
                        inputData.Add(parshedInput);
                    }
                }

                var inputArray = inputData.ToArray();

                inputArray.MergeSort();

                OutputData = string.Join(",", inputArray);
            }
            catch(Exception ex)
            {
                // this is wrong
                // we cann't show a dialog from view model
                // as no logging is implemented so I am adding it here
                MessageBox.Show(ex.Message, "Error!");
            }
        }
    }
}
