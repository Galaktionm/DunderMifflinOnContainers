import { Product } from "../Product";

export class CustomerBasket {
    userId: string
    products: Product[]

    constructor(userId:string, products:Product[]){
        this.userId=userId;
        this.products=products;
    }
}