import axios from 'axios';
import { Response } from '../models/Response';

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

export const fetchDataWithFilter = async <T>(endpoint: string, filter: any): Promise<Response<T>> => {
    try {
        const response = await axios.post(`${API_URL}/api/${endpoint}`, filter);
        const jsonData: Response<T> = await response.data;
        return jsonData;
    } catch (error) {
        throw error;
    }
};
