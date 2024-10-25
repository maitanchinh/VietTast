import { Product } from "./Product";

export interface Response {
    isSuccess: boolean;
    message: string;
    result: Product[];
}