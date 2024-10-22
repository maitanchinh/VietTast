import React, { useState } from 'react';
import {
    ProductOutlined,
} from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme } from 'antd';

const { Header, Content, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
    label: React.ReactNode,
    key: React.Key,
    icon?: React.ReactNode,
    children?: MenuItem[],
): MenuItem {
    return {
        key,
        icon,
        children,
        label,
    } as MenuItem;
}

const items: MenuItem[] = [
    getItem('Products', 'products', <ProductOutlined />, [
        getItem('All products', 'allproduct'),
        getItem('SKU', 'sku'),
        getItem('Categories', 'categories')
    ]),
];

const App: React.FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const [selectedParentItem, setSelectedParentItem] = useState(''); // Lưu mục cha
    const [selectedChildItem, setSelectedChildItem] = useState('');
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const handleMenuClick = (e: any) => {
        const keyPath = e.keyPath; // keyPath chứa cả key của mục cha và con (nếu có)
        if (keyPath.length === 2) {
            // Nếu có 2 mục trong keyPath thì tức là đang chọn mục con
            setSelectedParentItem(keyPath[1]); // keyPath[1] là key của mục cha
            setSelectedChildItem(keyPath[0]);  // keyPath[0] là key của mục con
        } else {
            // Nếu chỉ có 1 mục thì tức là chọn mục cha
            setSelectedParentItem(keyPath[0]);
            setSelectedChildItem('');
        }
    };

    return (
        <Layout style={{ minHeight: '100vh', width: '100vw' }}>
            <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <div className="demo-logo-vertical" />
                <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline" items={items} onClick={handleMenuClick} />
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer }} />
                <Content style={{ margin: '0 16px'}}>
                    <Breadcrumb style={{ margin: '16px 0' }}>
                        {selectedParentItem && <Breadcrumb.Item>{selectedParentItem}</Breadcrumb.Item>}
                        {selectedChildItem && <Breadcrumb.Item>{selectedChildItem}</Breadcrumb.Item>}
                    </Breadcrumb>
                    <div
                        style={{
                            padding: 24,
                            minHeight: 360,
                            background: colorBgContainer,
                            borderRadius: borderRadiusLG,
                        }}
                    >
                        Bill is a cat.
                    </div>
                </Content>
            </Layout>
        </Layout>
    );
};

export default App;