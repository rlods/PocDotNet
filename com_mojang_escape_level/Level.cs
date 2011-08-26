using com.mojang.escape;
using com.mojang.escape.entities;
using com.mojang.escape.level.block;
using com.mojang.escape.menu;
using System;
using System.Collections.Generic;

namespace com.mojang.escape.level
{
    public enum LevelEnum
    {
        Crypt,
        Dungeon,
        Ice,
        Overworld,
        Start,
        Temple
    }

    public abstract class Level
    {
	    public Block[] blocks;
	    public int width, height;
	    private Block solidWall = new SolidBlock();
        public string name;
	    public int xSpawn;
	    public int ySpawn;

	    protected int wallCol = 0xB3CEE2;
	    protected int floorCol = 0x9CA09B;
	    protected int ceilCol = 0x9CA09B;

	    protected int wallTex = 0;
	    protected int floorTex = 0;
	    protected int ceilTex = 0;

        public List<Entity> entities = new List<Entity>();
	    protected Game game;

	    public Player player;

	    public virtual void init(Game game, int w, int h, int[] pixels) {
		    this.game = game;

		    player = game.player;

		    solidWall.col = Art.GetColor(wallCol);
		    solidWall.tex = Art.GetColor(wallTex);
		    this.width = w;
		    this.height = h;
		    blocks = new Block[width * height];

		    for (int y = 0; y < h; y++) {
			    for (int x = 0; x < w; x++) {
				    int col = pixels[x + y * w] & 0xffffff;
				    int id = 255 - ((pixels[x + y * w] >> 24) & 0xff);

				    Block block = getBlock(x, y, col);
				    block.id = id;

				    if (block.tex == -1) block.tex = wallTex;
				    if (block.floorTex == -1) block.floorTex = floorTex;
				    if (block.ceilTex == -1) block.ceilTex = ceilTex;
				    if (block.col == -1) block.col = Art.GetColor(wallCol);
				    if (block.floorCol == -1) block.floorCol = Art.GetColor(floorCol);
				    if (block.ceilCol == -1) block.ceilCol = Art.GetColor(ceilCol);

				    blocks[x + y * w] = block;
				    block.level = this;
				    block.x = x;
				    block.y = y;
			    }
		    }

		    for (int y = 0; y < h; y++) {
			    for (int x = 0; x < w; x++) {
				    int col = pixels[x + y * w] & 0xffffff;
				    decorateBlock(x, y, blocks[x + y * w], col);
			    }
		    }
	    }

	    public void addEntity(Entity e) {
		    entities.Add(e);
		    e.level = this;
		    e.updatePos();
	    }

	    public void removeEntityImmediately(Player player) {
		    entities.Remove(player);
		    getBlock(player.xTileO, player.zTileO).removeEntity(player);
	    }

	    protected virtual void decorateBlock(int x, int y, Block block, int col) {
		    block.decorate(this, x, y);
		    if (col == 0xFFFF00) {
			    xSpawn = x;
			    ySpawn = y;
		    }
		    if (col == 0xAA5500) addEntity(new BoulderEntity(x, y));
		    if (col == 0xff0000) addEntity(new BatEntity(x, y));
		    if (col == 0xff0001) addEntity(new BatBossEntity(x, y));
		    if (col == 0xff0002) addEntity(new OgreEntity(x, y));
		    if (col == 0xff0003) addEntity(new BossOgre(x, y));
		    if (col == 0xff0004) addEntity(new EyeEntity(x, y));
		    if (col == 0xff0005) addEntity(new EyeBossEntity(x, y));
		    if (col == 0xff0006) addEntity(new GhostEntity(x, y));
		    if (col == 0xff0007) addEntity(new GhostBossEntity(x, y));
		    if (col == 0x1A2108 || col == 0xff0007) {
			    block.floorTex = 7;
			    block.ceilTex = 7;
		    }

		    if (col == 0xC6C6C6) block.col = Art.GetColor(0xa0a0a0);
		    if (col == 0xC6C697) block.col = Art.GetColor(0xa0a0a0);
		    if (col == 0x653A00) {
			    block.floorCol = Art.GetColor(0xB56600);
			    block.floorTex = 3 * 8 + 1;
		    }

		    if (col == 0x93FF9B) {
			    block.col = Art.GetColor(0x2AAF33);
			    block.tex = 8;
		    }
	    }

	    protected virtual Block getBlock(int x, int y, int col) {
		    if (col == 0x93FF9B) return new SolidBlock();
		    if (col == 0x009300) return new PitBlock();
		    if (col == 0xFFFFFF) return new SolidBlock();
		    if (col == 0x00FFFF) return new VanishBlock();
		    if (col == 0xFFFF64) return new ChestBlock();
		    if (col == 0x0000FF) return new WaterBlock();
		    if (col == 0xFF3A02) return new TorchBlock();
		    if (col == 0x4C4C4C) return new BarsBlock();
		    if (col == 0xFF66FF) return new LadderBlock(false);
		    if (col == 0x9E009E) return new LadderBlock(true);
		    if (col == 0xC1C14D) return new LootBlock();
		    if (col == 0xC6C6C6) return new DoorBlock();
		    if (col == 0x00FFA7) return new SwitchBlock();
		    if (col == 0x009380) return new PressurePlateBlock();
		    if (col == 0xff0005) return new IceBlock();
		    if (col == 0x3F3F60) return new IceBlock();
		    if (col == 0xC6C697) return new LockedDoorBlock();
		    if (col == 0xFFBA02) return new AltarBlock();
		    if (col == 0x749327) return new SpiritWallBlock();
		    if (col == 0x1A2108) return new Block();
		    if (col == 0x00C2A7) return new FinalUnlockBlock();
		    if (col == 0x000056) return new WinBlock();

		    return new Block();
	    }

	    public Block getBlock(int x, int y) {
		    if (x < 0 || y < 0 || x >= width || y >= height) {
			    return solidWall;
		    }
		    return blocks[x + y * width];
	    }

        private static Dictionary<LevelEnum, Level> loaded = new Dictionary<LevelEnum, Level>();

	    public static void clear() {
		    loaded.Clear();
	    }

	    public static Level LoadLevel(Game game, LevelEnum levelEnum)
        {
            Level level;
            if (!loaded.TryGetValue(levelEnum, out level))
            {
                System.Drawing.Bitmap levelMap = null;
                Type levelType = null;
                switch (levelEnum)
                {
                    case LevelEnum.Crypt:
                        levelMap = global::com.mojang.escape.Properties.Resources.crypt;
                        levelType = typeof(CryptLevel);
                        break;
                    case LevelEnum.Dungeon:
                        levelMap = global::com.mojang.escape.Properties.Resources.dungeon;
                        levelType = typeof(DungeonLevel);
                        break;
                    case LevelEnum.Ice:
                        levelMap = global::com.mojang.escape.Properties.Resources.ice;
                        levelType = typeof(IceLevel);
                        break;
                    case LevelEnum.Overworld:
                        levelMap = global::com.mojang.escape.Properties.Resources.overworld;
                        levelType = typeof(OverworldLevel);
                        break;
                    case LevelEnum.Start:
                        levelMap = global::com.mojang.escape.Properties.Resources.start;
                        levelType = typeof(StartLevel);
                        break;
                    case LevelEnum.Temple:
                        levelMap = global::com.mojang.escape.Properties.Resources.temple;
                        levelType = typeof(TempleLevel);
                        break;
                }

                int w = levelMap.Width;
                int h = levelMap.Height;
                int[] pixels = new int[w * h];
                for (int y = 0; y < h; ++y)
                {
                    for (int x = 0; x < w; ++x)
                    {
                        pixels[x + y * w] = levelMap.GetPixel(x, y).ToArgb();
                    }
                }
                level = levelType.GetConstructor(new Type[] { }).Invoke(new object[] { }) as Level;
                level.init(game, w, h, pixels);
                loaded.Add(levelEnum, level);
            }
		    return level;
	    }

	    public bool containsBlockingEntity(double x0, double y0, double x1, double y1) {
		    int xc = (int)(Math.Floor((x1 + x0) / 2));
		    int zc = (int)(Math.Floor((y1 + y0) / 2));
		    int rr = 2;
		    for (int z = zc - rr; z <= zc + rr; z++) {
			    for (int x = xc - rr; x <= xc + rr; x++) {
				    List<Entity> es = getBlock(x, z).entities;
				    for (int i = 0; i < es.Count; i++) {
					    Entity e = es[i];
					    if (e.isInside(x0, y0, x1, y1)) return true;
				    }
			    }
		    }
		    return false;
	    }

	    public bool containsBlockingNonFlyingEntity(double x0, double y0, double x1, double y1) {
		    int xc = (int)(Math.Floor((x1 + x0) / 2));
		    int zc = (int)(Math.Floor((y1 + y0) / 2));
		    int rr = 2;
		    for (int z = zc - rr; z <= zc + rr; z++) {
			    for (int x = xc - rr; x <= xc + rr; x++) {
				    List<Entity> es = getBlock(x, z).entities;
				    for (int i = 0; i < es.Count; i++) {
					    Entity e = es[i];
					    if (!e.flying && e.isInside(x0, y0, x1, y1)) return true;
				    }
			    }
		    }
		    return false;
	    }

	    public void tick() {
		    for (int i = 0; i < entities.Count; i++) {
			    Entity e = entities[i];
			    e.tick();
			    e.updatePos();
			    if (e.isRemoved()) {
				    entities.RemoveAt(i);
                    i--;
			    }
		    }

		    for (int y = 0; y < height; y++) {
			    for (int x = 0; x < width; x++) {
				    blocks[x + y * width].tick();
			    }
		    }
	    }

	    public virtual void trigger(int id, bool pressed) {
		    for (int y = 0; y < height; y++) {
			    for (int x = 0; x < width; x++) {
				    Block b = blocks[x + y * width];
				    if (b.id == id) {
					    b.trigger(pressed);
				    }
			    }
		    }
	    }

	    public virtual void switchLevel(int id) {
	    }

	    public void findSpawn(int id) {
		    for (int y = 0; y < height; y++) {
			    for (int x = 0; x < width; x++) {
				    Block b = blocks[x + y * width];
				    if (b.id == id && b is LadderBlock) {
					    xSpawn = x;
					    ySpawn = y;
				    }
			    }
		    }
	    }

	    public virtual void getLoot(int id) {
		    if (id == 20) game.getLoot(Item.pistol);
		    if (id == 21) game.getLoot(Item.potion);
	    }

	    public void win() {
		    game.win(player);
	    }

	    public void lose() {
		    game.lose(player);
	    }

	    public void showLootScreen(Item item) {
		    game.setMenu(new GotLootMenu(item));
	    }
    }
}
