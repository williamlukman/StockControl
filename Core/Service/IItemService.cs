public interface IItemService
{
	
	public Item CreateObject( Item );
	public Item UpdateObject( Item  );
	
	public Item GetObjectById( itemId  );
	
	// phase altering state.  => returning the object whose value is being altered 
	public Item SoftDeleteObject( Item );  // phase altering is just the same to the 
	public Item ConfirmObject( Item );
	
	
	public bool DeleteObject( Item );
	
	public IList<Item> GetAll(); 
}