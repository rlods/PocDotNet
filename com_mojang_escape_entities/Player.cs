using com.mojang.escape;
using com.mojang.escape.level.block;
using System;
using System.Collections.Generic;

namespace com.mojang.escape.entities
{
    public class Player : Entity
    {
	    public double bob, bobPhase, turnBob;
	    public int selectedSlot = 0;
	    public int itemUseTime;
	    public double y, ya;
	    public int hurtTime = 0;
	    public int health = 20;
	    public int keys = 0;
	    public int loot = 0;
	    public bool dead = false;
	    private int deadTime = 0;
	    public int ammo = 0;
	    public int potions = 0;
	    private Block lastBlock;

	    public Item[] items = new Item[8];

	    public Player() {
		    r = 0.3;
		    for (int i = 0; i < items.Length; i++) {
			    items[i] = Item.none;
		    }
	    }

	    bool sliding = false;
	    public int time;

	    public void tick(bool up, bool down, bool left, bool right, bool turnLeft, bool turnRight) {
		    if (dead) {
			    up = down = left = right = turnLeft = turnRight = false;
			    deadTime++;
			    if (deadTime > 60 * 2) {
				    level.lose();
			    }
		    } else {
			    time++;

		    }
		    if (itemUseTime > 0) itemUseTime--;
		    if (hurtTime > 0) hurtTime--;

		    Block onBlock = level.getBlock((int) (x + 0.5), (int) (z + 0.5));

		    double fh = onBlock.getFloorHeight(this);
		    if (onBlock is WaterBlock && !(lastBlock is WaterBlock))
            {
			    Sound.splash.play();
		    }

		    lastBlock = onBlock;

		    if (dead) fh = -0.6;
		    if (fh > y) {
			    y += (fh - y) * 0.2;
			    ya = 0;
		    } else {
			    ya -= 0.01;
			    y += ya;
			    if (y < fh) {
				    y = fh;
				    ya = 0;
			    }
		    }

		    double rotSpeed = 0.05;
		    double walkSpeed = 0.03 * onBlock.getWalkSpeed(this);

		    if (turnLeft) rota += rotSpeed;
		    if (turnRight) rota -= rotSpeed;

		    double xm = 0;
		    double zm = 0;
		    if (up) zm--;
		    if (down) zm++;
		    if (left) xm--;
		    if (right) xm++;
		    double dd = xm * xm + zm * zm;
		    if (dd > 0) dd = Math.Sqrt(dd);
		    else dd = 1;
		    xm /= dd;
		    zm /= dd;

		    bob *= 0.6;
		    turnBob *= 0.8;
		    turnBob += rota;
		    bob += Math.Sqrt(xm * xm + zm * zm);
		    bobPhase += Math.Sqrt(xm * xm + zm * zm) * onBlock.getWalkSpeed(this);
		    bool wasSliding = sliding;
		    sliding = false;

		    if (onBlock is IceBlock && getSelectedItem() != Item.skates) {
			    if (xa * xa > za * za) {
				    sliding = true;
				    za = 0;
				    if (xa > 0) xa = 0.08;
				    else xa = -0.08;
				    z += (((int) (z + 0.5)) - z) * 0.2;
			    } else if (xa * xa < za * za) {
				    sliding = true;
				    xa = 0;
				    if (za > 0) za = 0.08;
				    else za = -0.08;
				    x += (((int) (x + 0.5)) - x) * 0.2;
			    } else {
				    xa -= (xm * Math.Cos(rot) + zm * Math.Sin(rot)) * 0.1;
				    za -= (zm * Math.Cos(rot) - xm * Math.Sin(rot)) * 0.1;
			    }

			    if (!wasSliding && sliding) {
				    Sound.slide.play();
			    }
		    } else {
			    xa -= (xm * Math.Cos(rot) + zm * Math.Sin(rot)) * walkSpeed;
			    za -= (zm * Math.Cos(rot) - xm * Math.Sin(rot)) * walkSpeed;
		    }

		    move();

		    double friction = onBlock.getFriction(this);
		    xa *= friction;
		    za *= friction;
		    rot += rota;
		    rota *= 0.4;
	    }

	    public void activate() {
		    if (dead) return;
		    if (itemUseTime > 0) return;
		    Item item = items[selectedSlot];
		    if (item == Item.pistol) {
			    if (ammo > 0) {
				    Sound.shoot.play();
				    itemUseTime = 10;
				    level.addEntity(new Bullet(this, x, z, rot, 1, 0, 0xffffff));
				    ammo--;
			    }
			    return;
		    }
		    if (item == Item.potion) {
			    if (potions > 0 && health < 20) {
				    Sound.potion.play();
				    itemUseTime = 20;
				    health += 5 + random.Next(6);
				    if (health > 20) health = 20;
				    potions--;
			    }
			    return;
		    }
		    if (item == Item.key) itemUseTime = 10;
		    if (item == Item.powerGlove) itemUseTime = 10;
		    if (item == Item.cutters) itemUseTime = 10;

		    double xa = (2 * Math.Sin(rot));
		    double za = (2 * Math.Cos(rot));

		    int rr = 3;
		    int xc = (int) (x + 0.5);
		    int zc = (int) (z + 0.5);
            List<Entity> possibleHits = new List<Entity>();
		    for (int zi = zc - rr; zi <= zc + rr; zi++) {
			    for (int xi = xc - rr; xi <= xc + rr; xi++) {
				    List<Entity> es = level.getBlock(xi, zi).entities;
                    for (int i = 0; i < es.Count; i++)
                    {
					    Entity e = es[i];
					    if (e == this) continue;
					    possibleHits.Add(e);
				    }
			    }
		    }

		    int divs = 100;
		    for (int i = 0; i < divs; i++) {
			    double xx = x + xa * i / divs;
			    double zz = z + za * i / divs;
			    for (int j = 0; j < possibleHits.Count; j++) {
				    Entity e = possibleHits[j];
				    if (e.contains(xx, zz)) {
					    if (e.use(this, items[selectedSlot])) {
						    return;
					    }
				    }

			    }
			    int xt = (int) (xx + 0.5);
			    int zt = (int) (zz + 0.5);
			    if (xt != (int) (x + 0.5) || zt != (int) (z + 0.5)) {
				    Block block = level.getBlock(xt, zt);
				    if (block.use(level, items[selectedSlot])) {
					    return;
				    }
				    if (block.blocks(this)) return;
			    }
		    }
	    }

	    public override bool blocks(Entity entity, double x2, double z2, double r2) {
		    // if (entity instanceof Bullet && ((B))) return false;
		    return base.blocks(entity, x2, z2, r2);
	    }

	    public Item getSelectedItem() {
		    return items[selectedSlot];
	    }

	    public void addLoot(Item item) {
		    if (item == Item.pistol) ammo += 20;
		    if (item == Item.potion) potions += 1;
            for (int i = 0; i < items.Length; i++)
            {
			    if (items[i] == item) {
				    if (level!=null) level.showLootScreen(item);
				    return;
			    }
		    }

		    for (int i = 0; i < items.Length; i++) {
			    if (items[i] == Item.none) {
				    items[i] = item;
				    selectedSlot = i;
				    itemUseTime = 0;
				    if (level!=null) level.showLootScreen(item);
				    return;
			    }
		    }
	    }

	    public void hurt(Entity enemy, int dmg) {
		    if (hurtTime > 0 || dead) return;

		    hurtTime = 40;
		    health -= dmg;

		    if (health <= 0) {
			    health = 0;
			    Sound.death.play();
			    dead = true;
		    }

		    Sound.hurt.play();

		    double xd = enemy.x - x;
		    double zd = enemy.z - z;
		    double dd = Math.Sqrt(xd * xd + zd * zd);
		    xa -= xd / dd * 0.1;
		    za -= zd / dd * 0.1;
		    rota += (random.NextDouble() - 0.5) * 0.2;
	    }

	    protected override void collide(Entity entity)
        {
            Bullet bullet = entity as Bullet;
            if (null != bullet)
            {
                if (bullet.owner.GetType() == this.GetType())
                {
				    return;
			    }
			    if (hurtTime > 0) return;
			    entity.remove();
			    hurt(entity, 1);
		    }
	    }

	    public void win() {
		    level.win();
	    }
    }
}