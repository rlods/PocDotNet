using com.mojang.escape;

namespace com.mojang.escape.entities
{
    public class BatEntity : EnemyEntity
    {
	    public BatEntity(double x, double z)
        :
        base(x, z, 4 * 8, Art.GetColor(0x82666E))
        {
		    this.x = x;
		    this.z = z;
		    health = 2;
		    r = 0.3;

		    flying = true;
	    }
    }
}
