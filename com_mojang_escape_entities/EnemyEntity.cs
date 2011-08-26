using com.mojang.escape;
using com.mojang.escape.gui;
using System;

namespace com.mojang.escape.entities
{
    public class EnemyEntity : Entity
    {
	    protected Sprite sprite;
	    protected new double rot;
        protected new double rota;
	    protected int defaultTex;
	    protected int defaultColor;
	    protected int hurtTime = 0;
	    protected int animTime = 0;
	    protected int health = 3;
	    protected double spinSpeed = 0.1;
	    protected double runSpeed = 1;

	    public EnemyEntity(double x, double z, int defaultTex, int defaultColor) {
		    this.x = x;
		    this.z = z;
		    this.defaultColor = defaultColor;
		    this.defaultTex = defaultTex;
            this.sprite = new Sprite(0, 0, 0, 4 * 8, defaultColor);
            this.sprites.Add(this.sprite);
            this.r = 0.3;
	    }

        private static bool haveNextNextGaussian = false;
        private static double nextNextGaussian = 0.0;
        private static double NextGaussian()
        {
            // Implementation of Java Random.nextGaussian()
            double res = 0.0;
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                res = nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                do
                {
                    v1 = 2 * random.NextDouble() - 1;   // between -1.0 and 1.0
                    v2 = 2 * random.NextDouble() - 1;   // between -1.0 and 1.0
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1 || s == 0);
                double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                res = v1 * multiplier;
            }
            return res;
        }

	    public override void tick() {
		    if (hurtTime > 0) {
			    hurtTime--;
			    if (hurtTime == 0) {
				    sprite.col = defaultColor;
			    }
		    }
		    animTime++;
		    sprite.tex = defaultTex + animTime / 10 % 2;
		    move();
		    if (xa == 0 || za == 0) {
			    rota += (NextGaussian() * random.NextDouble()) * 0.3;
		    }

            rota += (NextGaussian() * random.NextDouble()) * spinSpeed;
		    rot += rota;
		    rota *= 0.8;
		    xa *= 0.8;
		    za *= 0.8;
		    xa += Math.Sin(rot) * 0.004 * runSpeed;
		    za += Math.Cos(rot) * 0.004 * runSpeed;
	    }

	    public override bool use(Entity source, Item item) {
		    if (hurtTime > 0) return false;
		    if (item != Item.powerGlove) return false;

		    hurt(Math.Sin(source.rot), Math.Cos(source.rot));

		    return true;
	    }

	    protected virtual void hurt(double xd, double zd) {
		    sprite.col = Art.GetColor(0xff0000);
		    hurtTime = 15;

		    double dd = Math.Sqrt(xd * xd + zd * zd);
		    xa += xd / dd * 0.2;
		    za += zd / dd * 0.2;
		    Sound.hurt2.play();
		    health--;
		    if (health <= 0) {
			    int xt = (int) (x + 0.5);
			    int zt = (int) (z + 0.5);
			    level.getBlock(xt, zt).addSprite(new PoofSprite(x - xt, 0, z - zt));
			    die();
			    remove();
			    Sound.kill.play();
		    }
	    }

	    protected virtual void die() {

	    }

	    protected override void collide(Entity entity)
        {
            Player player = entity as Player;
            if (null != player)
            {
                player.hurt(this, 1);
            }
            else
            {
                Bullet bullet = entity as Bullet;
		        if (null != bullet) {
                    if (bullet.owner.GetType() == this.GetType())
                    {
				        return;
			        }
			        if (hurtTime > 0) return;
			        entity.remove();
			        hurt(entity.xa, entity.za);
		        }
            }
	    }
    }
}
