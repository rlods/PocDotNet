namespace com.mojang.escape
{
    public class Sound
    {
	    public static Sound altar = LoadSound(global::com.mojang.escape.Properties.Resources.altar);
	    public static Sound bosskill = LoadSound(global::com.mojang.escape.Properties.Resources.bosskill);
	    public static Sound click1 = LoadSound(global::com.mojang.escape.Properties.Resources.click);
	    public static Sound click2 = LoadSound(global::com.mojang.escape.Properties.Resources.click2);
	    public static Sound hit = LoadSound(global::com.mojang.escape.Properties.Resources.hit);
	    public static Sound hurt = LoadSound(global::com.mojang.escape.Properties.Resources.hurt);
	    public static Sound hurt2 = LoadSound(global::com.mojang.escape.Properties.Resources.hurt2);
	    public static Sound kill = LoadSound(global::com.mojang.escape.Properties.Resources.kill);
	    public static Sound death = LoadSound(global::com.mojang.escape.Properties.Resources.death);
	    public static Sound splash = LoadSound(global::com.mojang.escape.Properties.Resources.splash);
	    public static Sound key = LoadSound(global::com.mojang.escape.Properties.Resources.key);
	    public static Sound pickup = LoadSound(global::com.mojang.escape.Properties.Resources.pickup);
	    public static Sound roll = LoadSound(global::com.mojang.escape.Properties.Resources.roll);
	    public static Sound shoot = LoadSound(global::com.mojang.escape.Properties.Resources.shoot);
	    public static Sound treasure = LoadSound(global::com.mojang.escape.Properties.Resources.treasure);
	    public static Sound crumble = LoadSound(global::com.mojang.escape.Properties.Resources.crumble);
	    public static Sound slide = LoadSound(global::com.mojang.escape.Properties.Resources.slide);
	    public static Sound cut = LoadSound(global::com.mojang.escape.Properties.Resources.cut);
	    public static Sound thud = LoadSound(global::com.mojang.escape.Properties.Resources.thud);
	    public static Sound ladder = LoadSound(global::com.mojang.escape.Properties.Resources.ladder);
	    public static Sound potion = LoadSound(global::com.mojang.escape.Properties.Resources.potion);

	    private static Sound LoadSound(System.IO.UnmanagedMemoryStream memoryStream) {
		    Sound sound = new Sound();
		    sound.player = new System.Media.SoundPlayer() { Stream = memoryStream };
			sound.player.Load();
		    return sound;
	    }

	    private System.Media.SoundPlayer player;

	    public void play() {
            this.player.Play();
	    }
    }
}
