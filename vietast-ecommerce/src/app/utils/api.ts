import axios from 'axios';
import { Response } from '../models/Response';
import { Filter } from '../models/Filter';

const API_URL = process.env.NEXT_PUBLIC_API_URL; // URL của service

export const fetchData = async <T>(endpoint : string): Promise<Response<T>> => {
    try {
        const response = await axios.get(`${API_URL}/api/${endpoint}`); // Gửi request lấy dữ liệu từ service
        const jsonData: Response<T> = await response.data; // Chuyển dữ liệu JSON thành dữ liệu kiểu Response
        return jsonData; // Trả về dữ liệu JSON
    } catch (error) {
        throw error;
    }
};

export const fetchDataWithFilter = async <T, U>(endpoint: string, filter: Filter<T>): Promise<Response<U>> => {
    try {
        const response = await axios.post(`${API_URL}/api/${endpoint}`, filter);
        const jsonData: Response<U> = await response.data;
        return jsonData;
    } catch (error) {
        throw error;
    }
};
