namespace com.mojang.escape.entities
{
    public class Item
    {
	    public readonly static Item none = new Item(-1, 0xFFC363, "", "");
	    public readonly static Item powerGlove = new Item(0, 0xFFC363, "Power Glove", 		"Smaaaash!!");
	    public readonly static Item pistol = new Item(1, 0xEAEAEA, "Pistol",          		"Pew, pew, pew!"); 
	    public readonly static Item flippers = new Item(2, 0x7CBBFF, "Flippers", 			"Splish splash!"); 
	    public readonly static Item cutters = new Item(3, 0xCCCCCC, "Cutters", 			"Snip, snip!");
	    public readonly static Item skates = new Item(4, 0xAE70FF, "Skates", 				"Sharp!");
	    public readonly static Item key = new Item(5, 0xFF4040, "Key", 					"How did you get this?");
        public readonly static Item potion = new Item(6, 0x4AFF47, "Potion", "Healthy!"); 
    	
	    public readonly int icon;
	    public readonly int color;
	    public readonly string name;
	    public readonly string description;
    	
	    private Item(int icon, int color, string name, string description) {
		    this.icon = icon;
		    this.color = color;
		    this.name = name;
		    this.description = description;
	    }
    }
}
