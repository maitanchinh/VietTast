import React from "react";
import { Product } from "../models/Product";
import styles from "../styles/ProductCard.module.css";

const ProductCard: React.FC<Product> = ({
  imageUrl,
  name,
}) => {
  return (
    <div className={styles.productCard}>
      <img
        src={imageUrl}
        alt={name}
        className={styles.productImage}
      />
      <div className={styles.overlay}>
        <button className={styles.iconBtn}>⛶</button>
        <button className={styles.iconBtn}>🔗</button>
      </div>
    </div>
  );
};

export default ProductCard;
