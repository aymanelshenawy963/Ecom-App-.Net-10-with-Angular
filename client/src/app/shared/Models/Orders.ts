export interface ICreateOrder {
  deliveyMethodId: number
  basketId: string
  shippingAddress: IShippingAddress
}

export interface IShippingAddress {
  firstName: string
  lastName: string
  street: string
  city: string
  state: string
  zipCode: string
}

export interface IOrder {
  id: number
  buyerEmail: string
  subtotal: number
  total: number
  orderDate: string
  status: string
  deliveryMethod: string
  shippingAddress: IShippingAddress
  orderItems: IOrderItem[]
}

export interface IOrderItem {
  productItemId: number
  mainImage: string
  productName: string
  price: number
  quantity: number
}
