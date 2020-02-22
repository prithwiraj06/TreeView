import { Domain } from "./domain.model";

export interface Product {
  productId: number;
  productName: string;
  domains: Domain[];
}
