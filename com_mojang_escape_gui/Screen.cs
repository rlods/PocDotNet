using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.level.block;
using System;

namespace com.mojang.escape.gui
{
    public class Screen : Bitmap
    {
        private const int PANEL_HEIGHT = 29;

	    private Bitmap testBitmap;
	    private Bitmap3D viewport;

	    public Screen(int width, int height)
            :
            base(width, height)
        {

		    viewport = new Bitmap3D(width, height - PANEL_HEIGHT);

		    Random random = new Random();
		    testBitmap = new Bitmap(64, 64);
		    for (int i = 0; i < 64 * 64; i++) {
			    testBitmap.pixels[i] = random.Next() * (random.Next(5) / 4);
		    }
	    }

	    public void render(Game game) {
		    if (game.level == null) {
			    fill(0, 0, width, height, 0);
		    }
            else
            {
			    bool itemUsed = game.player.itemUseTime > 0;
			    Item item = game.player.items[game.player.selectedSlot];

			    if (game.pauseTime > 0) {
				    fill(0, 0, width, height, 0);
				    string[] messages = { "Entering " + game.level.name, };
                    for (int y = 0; y < messages.Length; y++)
                    {
                        draw(messages[y], (width - messages[y].Length * 6) / 2, (viewport.height - messages.Length * 8) / 2 + y * 8 + 1, 0x111111);
                        draw(messages[y], (width - messages[y].Length * 6) / 2, (viewport.height - messages.Length * 8) / 2 + y * 8, 0x555544);
				    }
			    } else {
				    viewport.render(game);
				    viewport.postProcess(game.level);

				    Block block = game.level.getBlock((int) (game.player.x + 0.5), (int) (game.player.z + 0.5));
				    if (block.messages != null) {
                        for (int y = 0; y < block.messages.Length; y++)
                        {
						    viewport.draw(block.messages[y], (width - block.messages[y].Length * 6) / 2, (viewport.height - block.messages.Length * 8) / 2 + y * 8 + 1, 0x111111);
                            viewport.draw(block.messages[y], (width - block.messages[y].Length * 6) / 2, (viewport.height - block.messages.Length * 8) / 2 + y * 8, 0x555544);
					    }
				    }

				    draw(viewport, 0, 0);
				    int xx = (int) (game.player.turnBob * 32);
				    int yy = (int) (Math.Sin(game.player.bobPhase * 0.4) * 1 * game.player.bob + game.player.bob * 2);

				    if (itemUsed) xx = yy = 0;
				    xx += width / 2;
				    yy += height - PANEL_HEIGHT - 15 * 3;
				    if (item != Item.none) {
					    scaleDraw(Art.items, 3, xx, yy, 16 * item.icon + 1, 16 + 1 + (itemUsed ? 16 : 0), 15, 15, Art.GetColor(item.color));
				    }

				    if (game.player.hurtTime > 0 || game.player.dead) {
					    double offs = 1.5 - game.player.hurtTime / 30.0;
					    Random random = new Random(111);
					    if (game.player.dead) offs = 0.5;
					    for (int i = 0; i < pixels.Length; i++) {
						    double xp = ((i % width) - viewport.width / 2.0) / width * 2;
						    double yp = ((i / width) - viewport.height / 2.0) / viewport.height * 2;

						    if (random.NextDouble() + offs < Math.Sqrt(xp * xp + yp * yp)) pixels[i] = (random.Next(5) / 4) * 0x550000;
					    }
				    }
			    }

			    draw(Art.panel, 0, height - PANEL_HEIGHT, 0, 0, width, PANEL_HEIGHT, Art.GetColor(0x707070));

			    draw("å", 3, height - 26 + 0, 0x00ffff);
			    draw("" + game.player.keys + "/4", 10, height - 26 + 0, 0xffffff);
			    draw("Ä", 3, height - 26 + 8, 0xffff00);
			    draw("" + game.player.loot, 10, height - 26 + 8, 0xffffff);
			    draw("Å", 3, height - 26 + 16, 0xff0000);
			    draw("" + game.player.health, 10, height - 26 + 16, 0xffffff);

			    for (int i = 0; i < 8; i++) {
				    Item slotItem = game.player.items[i];
				    if (slotItem != Item.none) {
					    draw(Art.items, 30 + i * 16, height - PANEL_HEIGHT + 2, slotItem.icon * 16, 0, 16, 16, Art.GetColor(slotItem.color));
					    if (slotItem == Item.pistol) {
						    string str = "" + game.player.ammo;
						    draw(str, 30 + i * 16 + 17 - str.Length * 6, height - PANEL_HEIGHT + 1 + 10, 0x555555);
					    }
					    if (slotItem == Item.potion) {
						    string str = "" + game.player.potions;
						    draw(str, 30 + i * 16 + 17 - str.Length * 6, height - PANEL_HEIGHT + 1 + 10, 0x555555);
					    }
				    }
			    }

			    draw(Art.items, 30 + game.player.selectedSlot * 16, height - PANEL_HEIGHT + 2, 0, 48, 17, 17, Art.GetColor(0xffffff));

			    draw(item.name, 26 + (8 * 16 - item.name.Length * 4) / 2, height - 9, 0xffffff);
		    }

		    if (game.menu != null) {
			    for (int i = 0; i < pixels.Length; i++) {
				    pixels[i] = (pixels[i] & 0xfcfcfc) >> 2;
			    }			
			    game.menu.render(this);
		    }
	    }
    }
}
