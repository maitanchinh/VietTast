import React from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer';
import Body from '../components/Body';
const HomePage: React.FC = () => {
    return (
        <div>
            <Header />
            <main>
                <Body />
            </main>
            <Footer />
        </div>
    );
};

export default HomePage;