using com.mojang.escape;

namespace com.mojang.escape.entities
{
    public class EyeEntity : EnemyEntity
    {
	    public EyeEntity(double x, double z)
            :
            base(x, z, 4 * 8+4, Art.GetColor(0x84ECFF))
        {
		    this.x = x;
		    this.z = z;
		    health = 4;
		    r = 0.3;
		    runSpeed = 2;
		    spinSpeed *= 1.5;
    		
		    flying = true;
	    }
    }
}
