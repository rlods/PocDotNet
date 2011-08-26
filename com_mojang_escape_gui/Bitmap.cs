using com.mojang.escape;

namespace com.mojang.escape.gui
{
    public class Bitmap
    {
	    public readonly int width;
	    public readonly int height;
        public readonly int[] pixels;
	    private const string chars = "" + //
			    "ABCDEFGHIJKLMNOPQRSTUVWXYZ.,!?\"'/\\<>()[]{}" + //
			    "abcdefghijklmnopqrstuvwxyz_               " + //
			    "0123456789+-=*:;����                      " + //
			    "";

	    public Bitmap(int width, int height) {
		    this.width = width;
		    this.height = height;
		    pixels = new int[width * height];
	    }

	    public void draw(Bitmap bitmap, int xOffs, int yOffs) {
		    for (int y = 0; y < bitmap.height; y++) {
			    int yPix = y + yOffs;
			    if (yPix < 0 || yPix >= height) continue;

			    for (int x = 0; x < bitmap.width; x++) {
				    int xPix = x + xOffs;
				    if (xPix < 0 || xPix >= width) continue;

				    pixels[xPix + yPix * width] = bitmap.pixels[x + y * bitmap.width];
			    }
		    }
	    }

	    public void flipDraw(Bitmap bitmap, int xOffs, int yOffs) {
		    for (int y = 0; y < bitmap.height; y++) {
			    int yPix = y + yOffs;
			    if (yPix < 0 || yPix >= height) continue;

			    for (int x = 0; x < bitmap.width; x++) {
				    int xPix = xOffs + bitmap.width - x - 1;
				    if (xPix < 0 || xPix >= width) continue;

				    pixels[xPix + yPix * width] = bitmap.pixels[x + y * bitmap.width];
			    }
		    }
	    }

	    public void draw(Bitmap bitmap, int xOffs, int yOffs, int xo, int yo, int w, int h, int col) {
		    for (int y = 0; y < h; y++) {
			    int yPix = y + yOffs;
			    if (yPix < 0 || yPix >= height) continue;

			    for (int x = 0; x < w; x++) {
				    int xPix = x + xOffs;
				    if (xPix < 0 || xPix >= width) continue;

				    int src = bitmap.pixels[(x + xo) + (y + yo) * bitmap.width];
				    if (src >= 0) {
					    pixels[xPix + yPix * width] = src * col;
				    }
			    }
		    }
	    }

	    public void scaleDraw(Bitmap bitmap, int scale, int xOffs, int yOffs, int xo, int yo, int w, int h, int col) {
		    for (int y = 0; y < h * scale; y++) {
			    int yPix = y + yOffs;
			    if (yPix < 0 || yPix >= height) continue;

			    for (int x = 0; x < w * scale; x++) {
				    int xPix = x + xOffs;
				    if (xPix < 0 || xPix >= width) continue;

				    int src = bitmap.pixels[(x / scale + xo) + (y / scale + yo) * bitmap.width];
				    if (src >= 0) {
					    pixels[xPix + yPix * width] = src * col;
				    }
			    }
		    }
	    }

	    public void draw(string str, int x, int y, int col) {
		    for (int i = 0; i < str.Length; i++) {
                int ch = chars.IndexOf(str[i]);
			    if (ch < 0) continue;

			    int xx = ch % 42;
			    int yy = ch / 42;
			    draw(Art.font, x + i * 6, y, xx * 6, yy * 8, 5, 8, col);
		    }
	    }
    	
	    public void fill(int x0, int y0, int x1, int y1, int color) {
		    for (int y = y0; y < y1; y++) {
			    for (int x = x0; x < x1; x++) {
				    pixels[x + y * width] = color;
			    }
		    }
	    }    	
    }
}
