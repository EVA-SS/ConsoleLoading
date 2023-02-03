namespace ConsoleLoadingApp
{
    public class ConsoleLoading : IDisposable
    {
        public string Txt { get; set; }

        public int Interval = 100;
        public int ProgWidth = 18;
        ConsoleColor old, back;
        public ConsoleLoading(string txt)
        {
            old = Console.ForegroundColor;
            back = Console.BackgroundColor;
            Txt = txt;
            Start();
        }

        void Begin()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + Txt);
        }

        void Start()
        {
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            Begin();
            top = Console.CursorTop - 1;
            Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) return;
                    else
                    {
                        Console.SetCursorPosition(0, top);
                        if (Value > 0 && MaxValue > 0)
                        {
                            var prog = Value / MaxValue;
                            if (prog > 1) prog = 1;
                            var width = (int)Math.Round(ProgWidth * prog);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            if (width > 0)
                            {
                                Console.Write("[");
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.Write(TxtLen(' ', width));
                                Console.BackgroundColor = back;
                                var _len = ProgWidth - width;
                                if (_len > 0) Console.Write(TxtLen('.', _len));
                                Console.Write("] ");
                            }
                            else
                            {
                                Console.Write("[" + TxtLen('.', ProgWidth) + "] ");
                            }
                            Console.CursorLeft = ProgWidth + 3;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(Math.Round(prog * 100).ToString().PadLeft(2, ' ') + "% ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(Loading);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(" " + (ProgTxt == null ? Txt : ProgTxt));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(Loading);
                            if (ProgTxt != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write(" " + ProgTxt);
                            }
                        }
                        Thread.Sleep(Interval);
                    }
                }
            }).ContinueWith((action =>
            {
                Console.SetCursorPosition(0, top);
                Console.ForegroundColor = endColor;
                var oktxt = "⠿  " + endTxt;
                Console.Write("⠿  ");
                Console.BackgroundColor = endColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(endTxt);
                Console.ForegroundColor = old;
                Console.BackgroundColor = back;
                Console.WriteLine(ClearRight(oktxt.Length));
                Console.CursorVisible = true;
                Event.Set();
            }));
        }

        ManualResetEvent Event = new ManualResetEvent(false);
        int top = 0;
        string? endTxt;
        ConsoleColor endColor = ConsoleColor.DarkGreen;
        public void OK(string txt)
        {
            End(txt, ConsoleColor.DarkGreen);
        }

        public void Fail(string txt)
        {
            End(txt, ConsoleColor.DarkRed);
        }

        public void End(string txt, ConsoleColor color)
        {
            endTxt = txt;
            endColor = color;
            Dispose();
        }

        int temp = -1;
        char Loading
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

        public string? ProgTxt = null;
        public double Value = 0D;
        public double MaxValue = 100D;

        #region 释放

        CancellationTokenSource token = new CancellationTokenSource();

        public void Dispose()
        {
            if (!token.IsCancellationRequested)
            {
                token.Cancel();
                Event.WaitOne();
                token.Dispose();
                Event.Dispose();
            }
        }

        #endregion

        #region API

        public static string TxtLen(char txt, int len)
        {
            var txts = new List<char>(len);
            for (int i = 0; i < len; i++)
            {
                txts.Add(txt);
            }
            return string.Join("", txts);
        }
        public static string Clear(string txt)
        {
            var len = Console.WindowWidth - txt.Length;
            var txts = new List<char>(len);
            for (int i = 0; i < len; i++)
            {
                txts.Add(' ');
            }
            return txt + string.Join("", txts);
        }
        public static string ClearRight(int txt_len)
        {
            var len = Console.WindowWidth - txt_len;
            var txts = new List<char>(len);
            for (int i = 0; i < len; i++)
            {
                txts.Add(' ');
            }
            return string.Join("", txts);
        }
        public static string Clear()
        {
            var len = Console.WindowWidth;
            var txts = new List<char>(len);
            for (int i = 0; i < len; i++)
            {
                txts.Add(' ');
            }
            return string.Join("", txts);
        }

        #endregion
    }
}
