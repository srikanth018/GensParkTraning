export class PlaceOrderModel {
  constructor(
    public customerName: string,
    public customerPhone: string,
    public customerEmail: string,
    public customerAddress: string,
    public totalAmount: number,
    public paymentType: string,
    public userId: number
  ) {}
}

