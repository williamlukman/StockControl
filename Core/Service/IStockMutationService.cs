interface IStockMutationService
{
	public bool CreatePurchaseOrderStockMutation( IPurchaseOrderRepository po, IItemService itemService, Item item )
	{
		
		
		/*
		Bikin stock mutasi.. kalo semua sukses, update item
		*/
		
		int currentPendingReceival = item.PendingReceival; 
		item.PendingReceival += pod.Quantity 
		itemRepository.UpdateObject( item ) ; 
		
	}
	
}