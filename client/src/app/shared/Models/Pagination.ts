import { IProduct } from "./Product"

export interface IPagniation {
  pageNumber: number
  pageSize: number
  totalCount: number
  data: IProduct[]
}

