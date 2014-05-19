using System;
using System.Collections.Generic;

class PurchaseOrderDetail
{
	public int Id {get; set;} 
	public int PurchaseOrderId {get; set;} 
	public int ItemId {get; set;}
	public int Quantity {get; set;} 
	
	public bool IsConfirmed {get; set;} 
	public bool IsDeleted {get; set;} 
	
	public bool IsDeleted {get; set;}
    public bool DeletedAt { get; set; }
	
	public DateTime CreatedAt {get; set;}
	public DateTime ModifiedAt {get; set;}

    public HashSet<string> Errors { get; set; }
}