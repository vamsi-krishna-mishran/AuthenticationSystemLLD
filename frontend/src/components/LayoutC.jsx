import React, { useEffect, useState } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import BlogList from './BlogList.jsx'
import Profile from './Profile.jsx'
import RatedBlogs from './RatedBlogs.jsx'
import './Layout.css'
import Logout from './Logout.jsx';
import { useNavigate } from 'react-router-dom';
import BaseURI from '../backendConfig.js'
import { message } from 'antd'

import
{
    DesktopOutlined,
    FileOutlined,
    PieChartOutlined,
    ProfileOutlined,
    LogoutOutlined,
    StarOutlined
} from '@ant-design/icons';
import { UploadOutlined } from '@ant-design/icons';


import { Breadcrumb, Layout, Menu, theme } from 'antd';
import UploadBlog from './UploadBlog.jsx';
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
    getItem('Profile', '2', <ProfileOutlined />),
    getItem('Blogs', '1', <FileOutlined />),

    getItem('Rated Blogs', '3', <StarOutlined />),
    getItem('Upload Blog', '4', <UploadOutlined />),

    getItem('Logout', '5', <LogoutOutlined />)


];




const LayoutC = () =>
{
    const [component, setComponent] = useState(<BlogList />)
    const [admin, SetAdmin] = useState(true);

    const navigate = useNavigate()
    // const navigate = useNavigate();
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();
    function getCookie(name)
    {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return true;
        return false;
    }
    const handleClick = async (event) =>
    {
        console.log(event);
        console.log(event.key);
        const key = event.key;
        if (key == "2")
        {
            setComponent(<Profile />);
        }
        else if (key == "1")
        {
            setComponent(<BlogList />);
        }
        else if (key == "3")
        {
            setComponent(<RatedBlogs />);
        }
        else if (key == "5")
        {
            // setComponent(<Logout/>)
            try
            {
                let call = await fetch(BaseURI + "api/User/logout", { "credentials": "include", });
                if (!call.ok)
                {
                    throw new Error(await call.text());
                }
                message.success("Logged out successfully.")
                navigate("/login")
            }
            catch (err)
            {
                message.error(err.message)
            }
        }
        else if (key == "4")
        {
            setComponent(<UploadBlog />)
        }
        // navigate("/blogs")
    }

    useEffect(() =>
    {


        if (!getCookie("token"))
        {
            navigate("/login")
        }
    }, [])
    return (


        <div className='layout-wrapper'>
            <Layout
                style={{
                    minHeight: '95vh',
                }}
            >
                <Sider className="sider" style={{ backgroundColor: "transparent" }} collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                    <div className="demo-logo-vertical" />
                    <Menu style={{ color: "red" }} onClick={event => { handleClick(event) }} defaultSelectedKeys={['1']} mode="inline" items={items} />
                </Sider>
                <Layout>

                    <Content
                        style={{
                            margin: '0 16px',
                        }}
                    >

                        {getCookie("token") && component}


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