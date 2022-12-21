import { Product } from "../Product";

export class CustomerBasketRequest {
    userId: string
    product: Product

    constructor(userId:string, product:Product){
        this.userId=userId;
        this.product=product;
    }
}