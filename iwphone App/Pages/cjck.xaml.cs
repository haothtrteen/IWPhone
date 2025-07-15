using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
using System.Xml.Linq;
//using Windows.UI.Xaml.Controls;

namespace iwphone.Pages
{
    /// <summary>
    /// cjck.xaml 的交互逻辑
    /// </summary>
    public partial class cjck : Page
    {


        public cjck()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            //factoryfile1.Click += factoryfile1_Click;
        }
        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Button_Click();

        //}

        //    public static async Task Main()
        //{
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.GetAsync("https://api.example.com/data");
        //    var content = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(content);
        //    MessageBox.Show(content);
        //}
        public int id { get; set; }
        public string name { get; set; }
        public string uid { get; set; }
        public string description { get; set; }
        public string price { get; set; }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("http://125.64.3.239:9966/module.php");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    //factoryfile1name = data;
                    //MessageBox.Show(data);


                    List<cjck> plugins = JsonConvert.DeserializeObject<List<cjck>>(data);


                    foreach (var plugin in plugins)
                    {
                        int id = plugin.id;
                        string name = plugin.name;
                        string uid = plugin.uid;
                        string description = plugin.description;
                        string size = plugin.price;

                        switch (plugin.id)
                        {
                            case 1:
                                factoryfile1uid.Text = uid;
                                factoryfile1name.Text = name;
                                factoryfile1item.Text = description;
                                factoryfile1size.Text = size;
                                break;
                            case 2:
                                factoryfile2uid.Text = uid;
                                factoryfile2name.Text = name;
                                factoryfile2item.Text = description;
                                factoryfile2size.Text = size;
                                break;
                            case 3:
                                factoryfile3uid.Text = uid;
                                factoryfile3name.Text = name;
                                factoryfile3item.Text = description;
                                factoryfile3size.Text = size;
                                break;
                            case 4:
                                factoryfile4uid.Text = uid;
                                factoryfile4name.Text = name;
                                factoryfile4item.Text = description;
                                factoryfile4size.Text = size;
                                break;
                            case 5:
                                factoryfile5uid.Text = uid;
                                factoryfile5name.Text = name;
                                factoryfile5item.Text = description;
                                factoryfile5size.Text = size;
                                break;
                            case 6:
                                factoryfile6uid.Text = uid;
                                factoryfile6name.Text = name;
                                factoryfile6item.Text = description;
                                factoryfile6size.Text = size;
                                break;
                            default:
                                break;
                        }

                        //factoryfile1.Text = plugin.name;
                        //    Console.WriteLine($"ID: {plugin.id}, Name: {plugin.name}, Description: {plugin.description}, Price: {plugin.price}");
                        //    MessageBox.Show($"ID: {plugin.id}, Name: {plugin.name}, Description: {plugin.description}, Price: {plugin.price}");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string filespatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string folderPath = filespatch + "\\Modules"; // 设置你想要创建的文件路

            // 判断文件夹是否存在
            if (!Directory.Exists(folderPath))
            {
                // 如果文件夹不存在，就创建它
                Directory.CreateDirectory(folderPath);

            }



        }

        public async Task DownloadFile(string url, string savePath,string filename,string key)
        {
            using (HttpClient client = new HttpClient())
            {
                                    // 创建POST数据
                    var content = new FormUrlEncodedContent(new[]
                    {
                            new KeyValuePair<string, string>("filename", filename),
                            new KeyValuePair<string, string>("key", key),
                        });

                using (var response = await client.PostAsync(url, content))
                {

                    using (var contents = response.Content)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var stream = await contents.ReadAsStreamAsync())
                            {
                                using (var fileStream = new FileStream(savePath, FileMode.Create))
                                {
                                    await stream.CopyToAsync(fileStream);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to download file: {response.StatusCode}");
                        }
                    }
                }
            }
        }

        private void factoryfiles1(object sender, RoutedEventArgs e)
        {
            //int id = plugin.id;
            MessageBox.Show("开发者正在快马加鞭制作功能。。。！");
            MessageBox.Show(name);

        }


        private async void factoryfiles2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1");
            string filespatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //FileDownloader downloader = new FileDownloader();
            await DownloadFile("http://125.64.3.239:9966/module/download.php", filespatch + "\\Modules\\module.zip", "nb", "000001");

            //string url = "https://fhprotect.com/thirteen/iwphone/000002/module.zip";
            //string filePath = "Modules/file.zip";
            //string filename = "000002";
            //string key = "your_key";

            ////string filespatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            ////string filePaths = filespatch + "\\Modules\\tmp.ext"; // 设置你想要创建的文件路
            ////// 判断文件是否存在
            ////if (!File.Exists(filePaths))
            ////{
            ////    // 如果文件不存在，就创建它
            ////    File.Create(filePaths);
            ////}

            //File.Create(filePath).Close();

            //using (var client = new HttpClient())
            //{
            //    // 创建POST数据
            //    var content = new FormUrlEncodedContent(new[]
            //    {
            //        new KeyValuePair<string, string>("filename", filename),
            //        new KeyValuePair<string, string>("key", key),
            //    });

            //    // 发送POST请求
            //    var response = client.PostAsync(url, content).Result;

            //    // 检查响应状态
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("请求失败，状态码: " + response.StatusCode);
            //        return;
            //    }

            //    // 下载文件
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        response.Content.CopyToAsync(fileStream).Wait();
            //    }

            //    Console.WriteLine("文件下载成功: " + filePath);
            //}

        }

        private void factoryfiles3(object sender, RoutedEventArgs e)
        {

        }

        private void factoryfiles4(object sender, RoutedEventArgs e)
        {

        }

        private void factoryfiles5(object sender, RoutedEventArgs e)
        {

        }

        private void factoryfiles6(object sender, RoutedEventArgs e)
        {

        }

      
    }


}

