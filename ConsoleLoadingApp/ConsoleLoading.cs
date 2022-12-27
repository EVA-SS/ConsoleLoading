namespace ConsoleApp1
{
    public class ConsoleLoading : IDisposable
    {
        public string Txt { get; set; }
        public string OkTxt { get; set; }
        public int Interval = 100;
        ConsoleColor old;
        public ConsoleLoading(string txt, string oktxt)
        {
            old = Console.ForegroundColor;
            Txt = txt;
            OkTxt = oktxt;
        }

        public void Begin()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + Txt);
        }

        public void Start()
        {
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            Begin();
            var top = Console.CursorTop - 1;
            Task.Run(() =>
            {
                while (true)
                {
                    if (token == null || token.Token.IsCancellationRequested) return;
                    else
                    {
                        Console.SetCursorPosition(0, top);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(Loading);
                        Thread.Sleep(Interval);
                    }
                }
            }).ContinueWith((action =>
            {
                Console.SetCursorPosition(0, top);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(Clear("⠿  " + OkTxt));
                Console.CursorVisible = true;
                Console.ForegroundColor = old;
                Event.Set();
            }));
        }

        int temp = -1;
        public char Loading
        {
            get
            {
                temp++;
                switch (temp)
                {
                    case 0:
                        return '⠋';
                    case 1:
                        return '⠛';
                    case 2:
                        return '⠙';
                    case 3:
                        return '⠹';
                    case 4:
                        return '⠸';
                    case 5:
                        return '⠼';
                    case 6:
                        return '⠴';
                    case 7:
                        return '⠶';
                    case 8:
                        return '⠦';
                    case 9:
                        return '⠧';
                    case 10:
                        return '⠇';
                    case 11:
                        temp = -1;
                        return '⠏';
                }
                return ' ';
            }
        }

        public string Clear(string txt)
        {
            var len = Console.WindowWidth - txt.Length;
            var txts = new List<char>(len);
            for (int i = 0; i < len; i++)
            {
                txts.Add(' ');
            }
            return txt + string.Join("", txts);
        }

        #region 释放

        CancellationTokenSource token = new CancellationTokenSource();
        ManualResetEvent Event = new ManualResetEvent(false);

        public void Dispose()
        {
            if (token != null)
            {
                token.Cancel();
                Event.WaitOne();
                token.Dispose();
                token = null;
            }
        }

        #endregion
    }
}
