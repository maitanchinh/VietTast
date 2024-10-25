"use client";
import React, {useEffect, useState} from "react";
import ProductCard from "./ProductCard";
import styles from "../styles/Body.module.css";
import { getProductsByFilter, getCategories } from "../services/productService";
import { Product, ProductFilter } from "../models/Product";
import { Category } from "../models/Category";
import { Filter } from "../models/Filter";

const Body: React.FC = () => {
    const [data, setData] = useState<{ products: Product[]; categories: Category[] }>({ products: [], categories: [] }); // Khởi tạo dữ liệu là một object rỗng, chứa 2 mảng products và categories
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);
    const [productFilter, setProductFilter] = useState<ProductFilter | null>(null); // Khởi tạo state category với giá trị ban đầu là rỗng

    useEffect(() => {
      const getData = async () => {
        try {
          const filter : Filter<ProductFilter> = {criteria: productFilter ?? undefined} // Khởi tạo bộ lọc sản phẩm
            const categoriesResult = await getCategories(); // Gọi hàm lấy dữ liệu danh mục
            const productsResult = await getProductsByFilter(filter); // Gọi hàm lấy dữ liệu sản phẩm với bộ lọc danh mục
          setData({ products: productsResult.result, categories: categoriesResult.result }); // Cập nhật dữ liệu vào state
        } catch (err) {
          setError(err as Error); // Xử lý lỗi
        } finally {
          setLoading(false); // Đánh dấu quá trình tải hoàn tất
        }
      };
      console.log(productFilter);
      getData();
    }, [productFilter]); // Mảng rỗng để chỉ chạy một lần khi component mount

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error.message}</p>;
    if (!data) return <p>No data</p>;
  return (
    <div className="body">
      <section className={styles.productsSection}>
        <h1>Sản phẩm</h1>
        <p>Một vài chủng loại được yêu thích</p>
        <div className={styles.productFilters}>
          <span className={styles.active} onClick={() => setProductFilter(null)}>Tất cả</span>
          {data.categories.map((category) => (
            <span key={category.categoryId} onClick={() => setProductFilter({ categoryId: category.categoryId })}>{category.name}</span>
          ))}
        </div>
        <div className={styles.productsGrid}>
            {data.products.map((product) => (
                <ProductCard key={product.productId} {...product} />
            ))}
          
        </div>
      </section>
    </div>
  );
};

export default Body;
