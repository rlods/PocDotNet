using com.mojang.escape.entities;
using com.mojang.escape.gui;
using com.mojang.escape.level;
using System;
using System.Collections.Generic;

namespace com.mojang.escape.level.block
{
    public class Block
    {
        protected static readonly Random random = new Random();

        public bool blocksMotion = false;
        public bool solidRender = false;

        public string[] messages;

        public static Block solidWall = new SolidBlock();

        public List<Sprite> sprites = new List<Sprite>();
        public List<Entity> entities = new List<Entity>();

        public int tex = -1;
        public int col = -1;

        public int floorCol = -1;
        public int ceilCol = -1;

        public int floorTex = -1;
        public int ceilTex = -1;

        public Level level;
        public int x, y;

        public int id;

        public void addSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public virtual bool use(Level level, Item item)
        {
            return false;
        }

        public virtual void tick()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                Sprite sprite = sprites[i];
                sprite.tick();
                if (sprite.removed)
                {
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void removeEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public virtual void addEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public virtual bool blocks(Entity entity)
        {
            return blocksMotion;
        }

        public virtual void decorate(Level level, int x, int y)
        {
        }

        public virtual double getFloorHeight(Entity e)
        {
            return 0;
        }

        public virtual double getWalkSpeed(Player player)
        {
            return 1;
        }

        public virtual double getFriction(Player player)
        {
            return 0.6;
        }

        public virtual void trigger(bool pressed)
        {
        }
    }
}
