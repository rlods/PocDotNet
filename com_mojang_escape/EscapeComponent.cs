using com.mojang.escape.gui;

namespace com.mojang.escape
{
    public class EscapeComponent : System.Windows.Forms.Panel
    {
        #region Attributes
	    private const long serialVersionUID = 1L;
	    private const int WIDTH = 160;
	    private const int HEIGHT = 120;
	    private const int SCALE = 3;

	    private bool[] m_keys;
	    private Game game;
	    private Screen screen;
	    private System.Drawing.Bitmap img;
        private System.Windows.Forms.Timer m_timer = new System.Windows.Forms.Timer();
        int m_frames = 0;
        int m_tickCount = 0;
        #endregion

        #region Constructor
	    public EscapeComponent()
        {
            System.Drawing.Size size = new System.Drawing.Size(WIDTH * SCALE, HEIGHT * SCALE);
            this.DoubleBuffered = true;
            this.Size = size;
            this.MaximumSize = size;
            this.MinimumSize = size;

		    this.game = new Game();
		    this.screen = new Screen(WIDTH, HEIGHT);

            this.img = new System.Drawing.Bitmap(WIDTH, HEIGHT);

            this.m_timer.Interval = 1000 / 60;
            this.m_timer.Tick += new System.EventHandler(timer_Tick);
	    }
        #endregion

        public void Start(bool[] keys)
        {
            this.m_keys = keys;
            this.m_timer.Start();
        }

        public void Stop()
        {
            this.m_timer.Stop();
        }

        #region Control Events
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            // base.OnPaint(e);
            screen.render(game);

            for (int y = 0; y < HEIGHT; ++y)
            {
                for (int x = 0; x < WIDTH; ++x)
                {
                    System.Drawing.Color pixel = System.Drawing.Color.FromArgb(screen.pixels[x + y * WIDTH]);
                    this.img.SetPixel(x, y, System.Drawing.Color.FromArgb(255, pixel)); // TODO : check
                }
            }
            e.Graphics.DrawImage(this.img, 0, 0, WIDTH * SCALE, HEIGHT * SCALE);
            this.m_frames++;
        }
        #endregion
        
        #region Timer
        void timer_Tick(object sender, System.EventArgs e)
        {
            this.game.tick(this.m_keys);
            this.m_tickCount++;
            if (this.m_tickCount % 60 == 0)
            {
                System.Console.WriteLine("{0} fps", this.m_frames);
                this.m_frames = 0;
            }
            this.Invalidate();
        }
        #endregion
    }
}
