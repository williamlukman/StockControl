using System;
using System.Collections.Generic;

class Contact 
{
	public int Id {get; set;}
	public string  Name{get; set;}
	public string Description{get; set;} 
	
	public bool IsDeleted{get; set;}
	
    public bool DeletedAt { get; set; }  // seharusnya DateTime 
	
	public DateTime CreatedAt{get; set;}
	public DateTime ModifiedAt{get; set;}

    public HashSet<string> Errors { get; set; }
}