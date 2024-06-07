import React, { useState } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import BlogList from './BlogList.jsx'
import Profile from './Profile.jsx'
import RatedBlogs from './RatedBlogs.jsx'
import './Layout.css'
import
{
    DesktopOutlined,
    FileOutlined,
    PieChartOutlined,
    TeamOutlined,
    UserOutlined,
} from '@ant-design/icons';

import { Breadcrumb, Layout, Menu, theme } from 'antd';
const { Header, Content, Footer, Sider } = Layout;
function getItem(label, key, icon, children)
{
    return {
        key,
        icon,
        children,
        label,
    };
}
const items = [
    getItem('Profile', '1', <PieChartOutlined />),
    getItem('Blogs', '2', <DesktopOutlined />),

    getItem('Rated Blogs', '3', <FileOutlined />),
    getItem('Logout', '4', <FileOutlined />),

];




const LayoutC = () =>
{
    // const navigate = useNavigate();
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const handleClick = (event) =>
    {
        console.log(event);
        console.log(event.key);
        // navigate("/blogs")
    }
    return (


        <div className='layout-wrapper'>
            <Layout
                style={{
                    minHeight: '95vh',
                }}
            >
                <Sider className="sider" style={{ backgroundColor: "transparent" }} collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                    <div className="demo-logo-vertical" />
                    <Menu onClick={event => { handleClick(event) }} defaultSelectedKeys={['1']} mode="inline" items={items} />
                </Sider>
                <Layout>

                    <Content
                        style={{
                            margin: '0 16px',
                        }}
                    >

                        <Profile />


                        {/* <div
                            style={{
                                padding: 24,
                                minHeight: 360,
                                background: "white",
                                borderRadius: borderRadiusLG,
                            }}
                        >
                            Bill is a cat.
                        </div> */}
                    </Content>
                    <Footer
                        style={{
                            textAlign: 'center',
                        }}
                    >
                        Blogger.com ©{new Date().getFullYear()} Maintained by Vamsi
                    </Footer>
                </Layout>
            </Layout>
        </div>
    );
};
export default LayoutC;