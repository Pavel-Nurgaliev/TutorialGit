using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace AsyncProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string output = string.Empty;
        static readonly Stopwatch Clock = Stopwatch.StartNew();

        void Log(string msg) =>
           output += $"[{Clock.ElapsedMilliseconds,5}ms | thread {Thread.CurrentThread.ManagedThreadId}] {msg}";
        async Task NonBlockingAsync()
        {
            Log("NonBlocking: start");
            await Task.Delay(1000);      // thread is released back during this second
            Log("NonBlocking: done");
        }
        async Task FakeAsync()
        {
            Log("FakeAsync: start");
            Thread.Sleep(5000);          // (or a busy while-loop) — never yields
            Log("FakeAsync: done");
            await Task.CompletedTask;    // only here to satisfy 'async'
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //DownloadHtmlAsync("https://www.microsoft.com/ru-kz");

            //var html = await GetHtmlAsync("https://www.microsoft.com/ru-kz");

            //MessageBox.Show(html.Substring(0, 10));

            Log("=== starting non-blocking work ===");
            Task work = NonBlockingAsync();   // returns almost immediately, at the await
            for (int i = 1; i <= 5; i++)
            {
                Log($"tick {i}");
                await Task.Delay(100);        // heartbeat interleaves with the work
            }
            await work;

            // ---- Case 2: blocking ----
            Log("=== starting blocking work ===");
            Task bad = FakeAsync();           // does NOT return until the 1s sleep finishes
            for (int i = 1; i <= 5; i++)
            {
                Log($"tick {i}");
                await Task.Delay(100);
            }
            await bad;

            MessageBox.Show(output);
        }

        private async Task<string> GetHtmlAsync(string url)
        {
            var webClient = new WebClient();

            return await webClient.DownloadStringTaskAsync(url);
        }

        private string GetHtml(string url)
        {
            var webClient = new WebClient();

            return webClient.DownloadString(url);
        }

        private async Task DownloadHtmlAsync(string link)
        {
            var webClient = new WebClient();

            var html = await webClient.DownloadStringTaskAsync(link);

            using (var sw = new StreamWriter(@"E:\Repository\TutorialGit\CSharp_Basics\08-Advanced_CSharp\AsyncProgramming\result.html"))
            {
                await sw.WriteAsync(html);
            }
        }
        private void DownloadHtml(string link)
        {
            var webClient = new WebClient();

            var html = webClient.DownloadString(link);

            using (var sw = new StreamWriter(@"E:\Repository\TutorialGit\CSharp_Basics\08-Advanced_CSharp\AsyncProgramming\result.html"))
            {
                sw.Write(html);
            }
        }
    }
}