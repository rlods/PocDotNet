using com.mojang.escape.entities;
using com.mojang.escape.level;
using com.mojang.escape.level.block;
using com.mojang.escape.menu;
using System;

namespace com.mojang.escape
{
    public class Game
    {
	    public int time;
	    public Level level;
	    public Player player;
	    public int pauseTime;
	    public Menu menu;

	    public Game() {
		    setMenu(new TitleMenu());
	    }

	    public void newGame() {
		    Level.clear();
		    level = Level.LoadLevel(this, LevelEnum.Start);
            
		    player = new Player();
		    player.level = level;
		    level.player = player;
		    player.x = level.xSpawn;
		    player.z = level.ySpawn;
		    level.addEntity(player);
		    player.rot = Math.PI + 0.4;
	    }

	    public void switchLevel(LevelEnum levelEnum, int id) {
		    pauseTime = 30;
		    level.removeEntityImmediately(player);
            level = Level.LoadLevel(this, levelEnum);
		    level.findSpawn(id);
		    player.x = level.xSpawn;
		    player.z = level.ySpawn;
		    ((LadderBlock) level.getBlock(level.xSpawn, level.ySpawn)).wait = true;
		    player.x += Math.Sin(player.rot) * 0.2;
		    player.z += Math.Cos(player.rot) * 0.2;
		    level.addEntity(player);
	    }

	    public void tick(bool[] keys) {
		    if (pauseTime > 0) {
			    pauseTime--;
			    return;
		    }

		    time++;

		    bool strafe = keys[(int)System.Windows.Forms.Keys.ShiftKey];

            bool lk = keys[(int)System.Windows.Forms.Keys.Left] || keys[(int)System.Windows.Forms.Keys.NumPad4];
            bool rk = keys[(int)System.Windows.Forms.Keys.Right] || keys[(int)System.Windows.Forms.Keys.NumPad6];

            bool up = keys[(int)System.Windows.Forms.Keys.W] || keys[(int)System.Windows.Forms.Keys.Up] || keys[(int)System.Windows.Forms.Keys.NumPad8];
            bool down = keys[(int)System.Windows.Forms.Keys.S] || keys[(int)System.Windows.Forms.Keys.Down] || keys[(int)System.Windows.Forms.Keys.NumPad2];
            bool left = keys[(int)System.Windows.Forms.Keys.A] || (strafe && lk);
            bool right = keys[(int)System.Windows.Forms.Keys.D] || (strafe && rk);

            bool turnLeft = keys[(int)System.Windows.Forms.Keys.Q] || (!strafe && lk);
            bool turnRight = keys[(int)System.Windows.Forms.Keys.E] || (!strafe && rk);

            bool use = keys[(int)System.Windows.Forms.Keys.Space];

		    for (int i = 0; i < 8; i++) {
                if (keys[(int)System.Windows.Forms.Keys.F1 + i])
                {
                    keys[(int)System.Windows.Forms.Keys.F1 + i] = false;
				    player.selectedSlot = i;
				    player.itemUseTime = 0;
			    }
		    }

            if (keys[(int)System.Windows.Forms.Keys.Escape])
            {
                keys[(int)System.Windows.Forms.Keys.Escape] = false;
			    if (menu == null) {
				    setMenu(new PauseMenu());
			    }
		    }

		    if (use) {
                keys[(int)System.Windows.Forms.Keys.Space] = false;
		    }

		    if (menu != null) {
                keys[(int)System.Windows.Forms.Keys.W] = keys[(int)System.Windows.Forms.Keys.Up] = keys[(int)System.Windows.Forms.Keys.NumPad8] = false;
                keys[(int)System.Windows.Forms.Keys.S] = keys[(int)System.Windows.Forms.Keys.Down] = keys[(int)System.Windows.Forms.Keys.NumPad2] = false;
                keys[(int)System.Windows.Forms.Keys.A] = false;
                keys[(int)System.Windows.Forms.Keys.D] = false;

			    menu.tick(this, up, down, left, right, use);
		    } else {
			    player.tick(up, down, left, right, turnLeft, turnRight);
			    if (use) {
				    player.activate();
			    }

			    level.tick();
		    }
	    }

	    public void getLoot(Item item) {
		    player.addLoot(item);
	    }

	    public void win(Player player) {
		    setMenu(new WinMenu(player));
	    }

	    public void setMenu(Menu menu) {
		    this.menu = menu;
	    }

	    public void lose(Player player) {
		    setMenu(new LoseMenu(player));
	    }
    }
}
