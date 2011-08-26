using com.mojang.escape.gui;

namespace com.mojang.escape
{
    public class Art
    {
	    public static Bitmap floors = LoadBitmap(global::com.mojang.escape.Properties.Resources.floors);
	    public static Bitmap font = LoadBitmap(global::com.mojang.escape.Properties.Resources.font);
	    public static Bitmap items = LoadBitmap(global::com.mojang.escape.Properties.Resources.items);
	    public static Bitmap logo = LoadBitmap(global::com.mojang.escape.Properties.Resources.logo);
	    public static Bitmap panel = LoadBitmap(global::com.mojang.escape.Properties.Resources.gamepanel);
	    public static Bitmap sky = LoadBitmap(global::com.mojang.escape.Properties.Resources.sky);
	    public static Bitmap sprites = LoadBitmap(global::com.mojang.escape.Properties.Resources.sprites);
	    public static Bitmap walls = LoadBitmap(global::com.mojang.escape.Properties.Resources.walls);

	    public static Bitmap LoadBitmap(System.Drawing.Bitmap bmp) {
		    int w = bmp.Width;
		    int h = bmp.Height;
		    Bitmap result = new Bitmap(w, h);
		    for (int y = 0; y < h; ++y)
            {
			    for (int x = 0; x < w; ++x)
                {
                    System.Drawing.Color pixel = bmp.GetPixel(x, y);
                    int argb = pixel.ToArgb();
                    int col = (argb & 0xf) >> 2;
                    if (pixel.A == 255 && pixel.R == 255 && pixel.G == 0 && pixel.B == 255) col = -1;
                    result.pixels[x + y * w] = col;
                }
		    }
		    return result;
	    }

	    public static int GetColor(int c) {
		    int r = (c >> 16) & 0xff;
		    int g = (c >> 8) & 0xff;
		    int b = (c) & 0xff;
		    r = r * 0x55 / 0xff;
		    g = g * 0x55 / 0xff;
		    b = b * 0x55 / 0xff;
		    return r << 16 | g << 8 | b;
	    }
    }
}
