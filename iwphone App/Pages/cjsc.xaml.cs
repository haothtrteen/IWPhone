using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;


namespace iwphone.Pages
{
    /// <summary>
    /// cjsc.xaml 的交互逻辑
    /// </summary>
    public partial class cjsc : Window
    {
        public cjsc()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            //factoryfile1.Click += factoryfile1_Click;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(10),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

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

        //string message = "hello";
        //notifier.ShowInformation(message);
        //notifier.ShowSuccess(message);
        //notifier.ShowWarning(message);
        //notifier.ShowError(message);

        public int id { get; set; }
        public string name { get; set; }
        public string uid { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string download { get; set; }
        public string developer { get; set; }

        public string factoryfile1developers { get; set; }
        public string factoryfile2developers { get; set; }
        public string factoryfile3developers { get; set; }
        public string factoryfile4developers { get; set; }
        public string factoryfile5developers { get; set; }
        public string factoryfile6developers { get; set; }

        public string factoryfile1download { get; set; }
        public string factoryfile2download { get; set; }
        public string factoryfile3download { get; set; }
        public string factoryfile4download { get; set; }
        public string factoryfile5download { get; set; }
        public string factoryfile6download { get; set; }



        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            notifier.ShowInformation("正在访问云端github模块仓库，若网络不好可能无法正常访问");


            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://raw.githubusercontent.com/haothtrteen/IWPhone/main/IwphoneModules/Modules_1.json");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    //var data = "[\r\n    {\r\n        \"id\": 1,\r\n        \"name\": \"IWPhone息屏深度doze.iwp\",\r\n        \"uid\": \"000001\",\r\n        \"description\": \"IWPhone息屏深度dozm，官方demo\",\r\n        \"price\": \"3kb\",\r\n        \"download\": \"https://raw.githubusercontent.com/haothtrteen/IWPhone/main/IwphoneModules/src/IWPhone%E6%81%AF%E5%B1%8F%E6%B7%B1%E5%BA%A6doze.iwp\",\r\n        \"developer\": \"iwphone\"\r\n    },\r\n    {\r\n        \"id\": 2,\r\n        \"name\": \"模块2名称\",\r\n        \"uid\": \"模块2唯一标识\",\r\n        \"description\": \"模块2描述信息\",\r\n        \"price\": \"模块2价格\",\r\n        \"download\": \"http\",\r\n         \"developer\": \"iwphone\"\r\n    },\r\n    {\r\n        \"id\": 3,\r\n        \"name\": \"模块3名称\",\r\n        \"uid\": \"模块3唯一标识\",\r\n        \"description\": \"模块3描述信息\",\r\n        \"price\": \"模块3价格\",\r\n        \"download\": \"http\",\r\n         \"developer\": \"iwphone\"\r\n    },\r\n    {\r\n        \"id\": 4,\r\n        \"name\": \"模块3名称\",\r\n        \"uid\": \"模块3唯一标识\",\r\n        \"description\": \"模块3描述信息\",\r\n        \"price\": \"模块3价格\",\r\n        \"download\": \"http\",\r\n         \"developer\": \"iwphone\"\r\n    },\r\n    {\r\n        \"id\": 5,\r\n        \"name\": \"模块3名称\",\r\n        \"uid\": \"模块3唯一标识\",\r\n        \"description\": \"模块3描述信息\",\r\n        \"price\": \"模块3价格\",\r\n        \"download\": \"http\",\r\n         \"developer\": \"iwphone\"\r\n    },\r\n    {\r\n        \"id\": 6,\r\n        \"name\": \"模块3名称\",\r\n        \"uid\": \"模块3唯一标识\",\r\n        \"description\": \"模块3描述信息\",\r\n        \"price\": \"模块3价格\",\r\n        \"download\": \"http\"\r\n    }\r\n]";

                    //factoryfile1name = data;
                    //MessageBox.Show(data);


                    List<cjsc> plugins = JsonConvert.DeserializeObject<List<cjsc>>(data);


                    foreach (var plugin in plugins)
                    {
                        int id = plugin.id;
                        string name = plugin.name;
                        string uid = plugin.uid;
                        string description = plugin.description;
                        string size = plugin.price;
                        string download = plugin.download;
                        string developer = plugin.developer;

                        switch (plugin.id)
                        {
                            case 1:
                                factoryfile1uid.Text = uid;
                                factoryfile1name.Text = name;
                                factoryfile1item.Text = description;
                                factoryfile1size.Text = size;
                                factoryfile1developers = developer;
                                factoryfile1download = download;
                                //MessageBox.Show(factoryfile1download);
                                break;
                            case 2:
                                factoryfile2uid.Text = uid;
                                factoryfile2name.Text = name;
                                factoryfile2item.Text = description;
                                factoryfile2size.Text = size;
                                factoryfile2developers = developer;
                                factoryfile2download = download;
                                break;
                            case 3:
                                factoryfile3uid.Text = uid;
                                factoryfile3name.Text = name;
                                factoryfile3item.Text = description;
                                factoryfile3size.Text = size;
                                factoryfile3developers = developer;
                                factoryfile3download = download;
                                break;
                            case 4:
                                factoryfile4uid.Text = uid;
                                factoryfile4name.Text = name;
                                factoryfile4item.Text = description;
                                factoryfile4size.Text = size;
                                factoryfile4developers = developer;
                                factoryfile4download = download;
                                break;
                            case 5:
                                factoryfile5uid.Text = uid;
                                factoryfile5name.Text = name;
                                factoryfile5item.Text = description;
                                factoryfile5size.Text = size;
                                factoryfile5developers = developer;
                                factoryfile5download = download;
                                break;
                            case 6:
                                factoryfile6uid.Text = uid;
                                factoryfile6name.Text = name;
                                factoryfile6item.Text = description;
                                factoryfile6size.Text = size;
                                factoryfile6developers = developer;
                                factoryfile6download = download;
                                break;
                            default:
                                break;
                        }

                        //factoryfile1.Text = plugin.name;
                        //    Console.WriteLine($"ID: {plugin.id}, Name: {plugin.name}, Description: {plugin.description}, Price: {plugin.price}");
                        //MessageBox.Show($"ID: {plugin.id}, Name: {plugin.name}, Description: {plugin.description}, Price: {plugin.price}");
                    }

                }
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                //MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
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

        public async Task DownloadFile(string url, string savePath, string filename, string key)
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
            //MessageBox.Show("开发者正在快马加鞭制作功能。。。！");
            string uid = factoryfile1uid.Text;
            string name = factoryfile1name.Text;
            string description = factoryfile1item.Text;
            string price = factoryfile1size.Text;
            string developer = factoryfile1developers;
            string downloadmod = factoryfile1download;

            //MessageBox.Show(factoryfile1download);

            cjxz cjxz = new cjxz(id, name, uid, description, price, developer, downloadmod);
            cjxz.Show();

          
       

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
