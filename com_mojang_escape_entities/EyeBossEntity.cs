using com.mojang.escape;

namespace com.mojang.escape.entities
{
    public class EyeBossEntity : EnemyEntity
    {
	    public EyeBossEntity(double x, double z)
            :
            base(x, z, 4 * 8 + 4, Art.GetColor(0xffff00))
        {
		    this.x = x;
		    this.z = z;
		    health = 10;
		    r = 0.3;
		    runSpeed = 4;
		    spinSpeed *= 1.5;

		    flying = true;
	    }

        protected override void die()
        {
		    Sound.bosskill.play();
		    level.addEntity(new KeyEntity(x, z));
	    }
    }
}
