export class Product{
    
    id: string;
    name:string;
    price:number;
    manufacturer:string;
    additionalInfo:string;

    constructor(id: string,name:string, price:number, manufacturer:string, additionalInfo:string){
        this.id=id;
        this.name=name;
        this.price=price;
        this.manufacturer=manufacturer;
        this.additionalInfo=additionalInfo;
    }

}