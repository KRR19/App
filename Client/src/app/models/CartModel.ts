export interface CartModel {
  userName?: string;
  PrintingEdition?: CartItem[];
}

export interface CartItem {
  printingEditionId?: string;
  printingEditionName?: string;
  printingEditionCount?: number;
  printingEditionPrice?: number;
  printingEditionCurrency?: number;
}
