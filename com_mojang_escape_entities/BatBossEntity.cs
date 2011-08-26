using com.mojang.escape;

namespace com.mojang.escape.entities
{
    public class BatBossEntity : EnemyEntity
    {
	    public BatBossEntity(int x, int z)
        :
            base(x, z, 4 * 8, Art.GetColor(0xffff00))
        {
		    this.x = x;
		    this.z = z;
		    health = 5;
		    r = 0.3;

		    flying = true;
	    }

        protected override void die()
        {
		    Sound.bosskill.play();
		    level.addEntity(new KeyEntity(x, z));
	    }

        public override void tick()
        {
		    base.tick();
		    if (random.Next(20) == 0) {
			    double xx = x + (random.NextDouble() - 0.5) * 2;
			    double zz = z + (random.NextDouble() - 0.5) * 2;
			    BatEntity batEntity = new BatEntity(xx, zz);
			    batEntity.level = level;

			    batEntity.x = -999;
			    batEntity.z = -999;
    			
			    if (batEntity.isFree(xx, zz)) {
				    level.addEntity(batEntity);
			    }
		    }
	    }
    }
}
