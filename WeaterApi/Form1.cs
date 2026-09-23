using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace WeaterApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "63d395642a0dc28464452ad6a80d784c";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getweather();

        }
        void getweather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", tbCity.Text, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                picIcon.ImageLocation = "https://openweathermap.org/img/w/" + info.weather[0].icon + ".png";
                lblCondition.Text = info.weather[0].main;
                lblDetails.Text = info.weather[0].description;
                lblSunset.Text = convertDateTime(info.sys.sunset).ToShortTimeString();
                lblSunrise.Text = convertDateTime(info.sys.sunrise).ToShortTimeString();

                lblWindSpeed.Text = (info.wind.speed*3.6).ToString() + " Km/h";
                lblPressure.Text = info.main.pressure.ToString() + " hPa";





                /*lblTemp.Text = info.main.temp.ToString() + " °C";
                lblHumidity.Text = info.main.humidity.ToString() + " %";
                lblWind.Text = info.wind.speed.ToString() + " m/s";
                lblDescription.Text = info.weather[0].description;*/

            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            dtDateTime = dtDateTime.AddSeconds(sec).ToLocalTime();
            return dtDateTime;
        }
    }
}

