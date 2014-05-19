using System;
using System.Collections.Generic;

class Item 
{
	public int Id { get; set; }
	public string Sku { get; set; }
	public string Name {get; set;} 
	public string Description {get; set;}
	
	public bool IsDeleted {get; set;}
	public bool DeletedAt {get; set;} 
	
	public DateTime CreatedAt {get; set;}
	public DateTime ModifiedAt {get; set;}

    public virtual ICollection<StockMutation> StockMutations { get; set; }
    public HashSet<string> Errors { get; set; }
}