import { Product } from "./Product";

export interface Response<T> {
    isSuccess: boolean;
    message: string;
    result: T[];
}