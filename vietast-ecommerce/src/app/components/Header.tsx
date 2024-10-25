import React from 'react';
import styles from '../styles/Header.module.css';
import Image from 'next/image';

const Header: React.FC = () => {
  return (
    <header className={styles.header}>
      <Image src="/assets/CTN-AVT.png" alt="Cá Tắm Nắng" className={styles.headerLogo} />
      {/* <h1 className={styles.headerTitle}>Cá Tắm Nắng</h1> */}
    </header>
  );
};

export default Header;